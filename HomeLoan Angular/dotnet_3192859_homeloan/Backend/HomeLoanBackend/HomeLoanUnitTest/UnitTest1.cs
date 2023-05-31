using NUnit.Framework;
using Moq;
using DataAccessLayer.Repository.RepositoryInterface;
using System.Threading.Tasks;
using DataAccessLayer.Model;

[TestFixture]
public class MyDatabaseTests
{
    private Mock<IWriteOperation<User>> _mockWriteOperation;

    [SetUp]
    public void Setup()
    {
        // Create a new mock database object before each test
        _mockWriteOperation = new Mock<IWriteOperation<User>>();
    }

    //[Test]
    //public void TestGetUserById()
    //{
    //    // Arrange
    //    var expectedUser = new User { Id = 1, Name = "John" };
    //    mockDatabase.Setup(db => db.GetUserById(1)).Returns(expectedUser);

    //    // Act
    //    var actualUser = mockDatabase.Object.GetUserById(1);

    //    // Assert
    //    Assert.AreEqual(expectedUser, actualUser);
    //}

    [Test]
    public void TestAddUser()
    {
        // Arrange
        var userToAdd = new User { EmailId="uniteTestUser@test.com", Password="Un!t098765", MobileNumber="9876543210", CountryCode="IND", StateCode="DEL", CityCode="NDL" };
        //_mockWriteOperation.Setup(db => db.AddTask(userToAdd)).Returns(true);
        //_mockWriteOperation.Setup(db => db.AddUser(userToAdd)).Returns(2);

        // Act
        var newUserId = _mockWriteOperation.Object.AddTask(userToAdd);
        //var newUserId = mockDatabase.Object.AddUser(userToAdd);

        // Assert
        Assert.AreEqual(true, newUserId);
    }

    //[Test]
    //public void TestUpdateUser()
    //{
    //    // Arrange
    //    var userToUpdate = new User { Id = 1, Name = "John Doe" };
    //    mockDatabase.Setup(db => db.UpdateUser(userToUpdate)).Returns(true);

    //    // Act
    //    var isUpdated = mockDatabase.Object.UpdateUser(userToUpdate);

    //    // Assert
    //    Assert.IsTrue(isUpdated);
    //}
}


