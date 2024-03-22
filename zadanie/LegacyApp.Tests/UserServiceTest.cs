using System;
using JetBrains.Annotations;
using LegacyApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LegacyApp.Tests;

[TestClass]
[TestSubject(typeof(UserService))]
public class UserServiceTest
{

    [TestMethod]
    public void AddUser_Sould_Return_False_When_FirstName_Is_Missing()
    {
        var userService = new UserService();

        var addResult = userService.AddUser("", "Kowalski", "kowalski@wp.pl", DateTime.Parse("1980-12-12"), 1);
        
        Assert.IsFalse(addResult);
    }
}