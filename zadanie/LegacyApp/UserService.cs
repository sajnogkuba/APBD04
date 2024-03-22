using System;

namespace LegacyApp
{
    public class UserService
    {
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (IsIncorrectUserName(firstName, lastName) || IsIncorrectEmail(email) || CalculateAge(dateOfBirth) < 21)
            {
                return false;
            }

            var clientRepository = new ClientRepository();
            var client = clientRepository.GetById(clientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };

            switch (client.Type)
            {
                case "VeryImportantClient":
                    SetCreditLimitForVeryImportantClient(user);
                    break;
                case "ImportantClient":
                    SetCreditLimitForImportantClient(user);
                    break;
                default:
                    SetCreditLimitForNormalClient(user);
                    break;
                
            }

            if (HasTooLowCreditLimit(user))
            {
                return false;
            }

            UserDataAccess.AddUser(user);
            return true;
        }

        private bool HasTooLowCreditLimit(User user)
        {
            return user.HasCreditLimit && user.CreditLimit < 500;
        }

        private void SetCreditLimitForNormalClient(User user)
        {
            user.HasCreditLimit = true;
            using (var userCreditService = new UserCreditService())
            {
                int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                user.CreditLimit = creditLimit;
            }
        }

        private void SetCreditLimitForImportantClient(User user)
        {
            using var userCreditService = new UserCreditService();
            var creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
            creditLimit *= 2;
            user.CreditLimit = creditLimit;
        }

        private void SetCreditLimitForVeryImportantClient(User user)
        {
            user.HasCreditLimit = false;
        }

        private bool IsIncorrectEmail(string email)
        {
            return !email.Contains("@") || !email.Contains(".");
        }

        private bool IsIncorrectUserName(string firstName, string lastName)
        {
            return string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName);
        }

        private int CalculateAge(DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;
            return age;
        }
    }
}
