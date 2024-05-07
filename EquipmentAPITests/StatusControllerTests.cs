using Moq;
using EquipmentAPI.Controllers;
using EquipmentAPI.DataAccessLayer;
using EquipmentAPI.Models;
using Microsoft.EntityFrameworkCore;
using Moq.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

[TestClass]
public class StatusControllerTests
{
    private Mock<DbSet<Equipment>> _mockSet;
    private List<Equipment> data;
    private Mock<ApplicationDBContext> _mockContext;
    private StatusController _controller;

    [TestInitialize]
    public void TestInitialize()
    {
        data = new List<Equipment>
    {
        new Equipment { Id = 1, /* other properties */ },
        new Equipment { Id = 2, /* other properties */ },
        // Add more equipment objects as needed
    };


        _mockSet = new Mock<DbSet<Equipment>>();

        _mockContext = new Mock<ApplicationDBContext>();
        _mockContext.Setup(x => x.Equipment).ReturnsDbSet(data);
        _mockContext.Setup(x => x.Equipment.FindAsync(1)).ReturnsAsync(data.First());


        _controller = new StatusController(_mockContext.Object);
    }

    [TestMethod]
    public async Task GetEquipment_ReturnsAllEquipment()
    {
        var result = await _controller.GetEquipment();

        Assert.AreEqual(2, result.Value.Count()); // Adjust this based on the number of equipment objects you added in the constructor
    }

    [TestMethod]
    public async Task GetEquipmentById_ReturnsCorrectEquipment()
    {
        var result = await _controller.GetEquipment(1);

        Assert.AreEqual(1, result.Value.Id); // Check that the returned equipment has the correct ID
    }


    [TestMethod]
    public async Task PutEquipment_WithInvalidIdAndEquipment_ReturnsBadRequest()
    {
        // Arrange
        int id = 1;
        var equipment = new Equipment { Id = id + 1 };


        // Act
        var result = await _controller.PutEquipment(id, equipment);

        // Assert
        Assert.IsInstanceOfType(result, typeof(BadRequestResult));
    }

    [TestMethod]
    public async Task PostEquipment_AddsNewEquipment()
    {
        var newEquipment = new Equipment { Id = 3, /* other properties */ };

        var result = await _controller.PostEquipment(newEquipment);

        _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once()); // Verify that SaveChangesAsync was called once

        var createdAtActionResult = result.Result as CreatedAtActionResult;
        Assert.IsNotNull(createdAtActionResult);
        var returnValue = createdAtActionResult.Value as Equipment;
        Assert.AreEqual(3, returnValue.Id); // Check that the returned equipment has the correct ID
    }


    [TestMethod]
    public async Task DeleteEquipment_CallsRemoveAndSaveChanges()
    {
        await _controller.DeleteEquipment(1);
        _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once()); // Verify that SaveChangesAsync was called once
    }
}

