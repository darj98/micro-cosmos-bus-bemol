using DesafioBemol.Controllers;
using DesafioBemol.Models;
using DesafioBemol.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Moq;
using NUnit.Framework;
using System;

namespace DesafioBemol.Tests
{
    [TestFixture]
    public class ObjetoControllerTests
    {
        private ObjetoController _objetoController;
        private Mock<IObjetoService> _objetoServiceMock;

        [SetUp]
        public void Setup()
        {
            _objetoServiceMock = new Mock<IObjetoService>();
            _objetoController = new ObjetoController(_objetoServiceMock.Object);
        }

        [Test]
        public void PostObjeto_ValidObjeto_ReturnsOkResult()
        {
            // Arrange
            var objeto = new Objeto
            {
                Id = Guid.NewGuid(),
                Nome = "Objeto de teste"
            };

            _objetoServiceMock.Setup(service => service.ProcessarObjeto(objeto)).Verifiable();

            // Act
            var result = _objetoController.PostObjeto(objeto);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
            _objetoServiceMock.Verify(service => service.ProcessarObjeto(objeto), Times.Once);
        }

        [Test]
        public void PostObjeto_InvalidObjeto_ReturnsBadRequest()
        {
            // Arrange
            var objeto = new Objeto(); // Objeto inválido, sem propriedades preenchidas

            // Act
            var result = _objetoController.PostObjeto(objeto);

            // Assert
            Assert.IsInstanceOf<BadRequestResult>(result);
            _objetoServiceMock.Verify(service => service.ProcessarObjeto(It.IsAny<Objeto>()), Times.Never);
        }

        [Test]
        public void PostObjeto_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var objeto = new Objeto
            {
                Id = Guid.NewGuid(),
                Nome = "Objeto de teste"
            };

            _objetoServiceMock.Setup(service => service.ProcessarObjeto(objeto)).Throws<Exception>();

            // Act
            var result = _objetoController.PostObjeto(objeto);

            // Assert
            Assert.IsInstanceOf<StatusCodeResult>(result);
            var statusCodeResult = result as StatusCodeResult;
            Assert.AreEqual(500, statusCodeResult.StatusCode);
            _objetoServiceMock.Verify(service => service.ProcessarObjeto(objeto), Times.Once);
        }
    }
}