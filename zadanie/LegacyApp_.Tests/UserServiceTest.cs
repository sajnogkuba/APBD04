using System;
using JetBrains.Annotations;
using LegacyApp;
using Xunit;

namespace LegacyApp_.Tests;

[TestSubject(typeof(UserService))]
public class UserServiceTest
{

    [Fact]
    public void AddUser_Should_Return_False_When_First_Name_Is_Missing()
    {
        var userService = new UserService();

        var addResult = userService.AddUser("", "Kowalski", "kowalski@wp.pl", DateTime.Parse("1980-12-03"), 1);
        
        Assert.False(addResult);
    }
    
    [Fact]
    public void AddUser_Should_Return_False_When_Email_Is_Incorrect()
    {
        var userService = new UserService();

        var addResult = userService.AddUser("Jan", "Kowalski", "kowalskiwppl", DateTime.Parse("1980-12-03"), 1);
        
        Assert.False(addResult);
    }
    
    [Fact]
    public void AddUser_Should_Throw_ArgumentException_When_User_Doesnt_Exist()
    {
        var userService = new UserService();

        Assert.Throws<ArgumentException>(() =>
        {
            var addResult = userService.AddUser("Jan", "Kowalski", "kowalski@wp.pl", DateTime.Parse("1980-12-03"), 7); 
        });
        
    }
    
    [Fact]
    public void AddUser_Should_Return_False_When_Age_Is_Lower_Than_21()
    {
        var userService = new UserService();

        var addResult = userService.AddUser("Jan", "Kowalski", "kowalski@wp.pl", DateTime.Parse("2010-12-03"), 7); 
        
        Assert.False(addResult);
    }
    
    [Fact]
    public void AddUser_Should_Return_False_When_User_Credit_Limit_Is_Lower_Than_500()
    {
        var userService = new UserService();

        var addResult = userService.AddUser("Filip", "Kowalski", "kowalski@wp.pl", DateTime.Parse("1900-12-03"), 1);
        
        Assert.False(addResult);
    }
    
    [Fact]
    public void AddUser_Should_Return_True_When_Client_Is_Very_Important()
    {
        var userService = new UserService();

        var addResult = userService.AddUser("Filip", "Kowalski", "kowalski@wp.pl", DateTime.Parse("1900-12-03"), 2);
        
        Assert.True(addResult);
    }
    [Fact]
    public void AddUser_Should_Return_True_When_Client_Is_Important()
    {
        var userService = new UserService();

        var addResult = userService.AddUser("Filip", "Kowalski", "kowalski@wp.pl", DateTime.Parse("1900-12-03"), 4);
        
        Assert.True(addResult);
    }
}