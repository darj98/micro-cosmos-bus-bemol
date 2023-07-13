using DesafioBemol.Models;
using DesafioBemol.Repositories;
using DesafioBemol.Services;
using Microsoft.AspNetCore.Routing;
using Moq;
using NUnit.Framework;
using System;

namespace DesafioBemol.Tests
{
    [TestFixture]
    public class ObjetoServiceTests
    {
        private ObjetoService _objetoService;
        private Mock<IObjetoRepository> _objetoRepositoryMock;

        [SetUp]
        public void Setup()
        {
            _objetoRepositoryMock = new Mock<IObjetoRepository>();
            _objetoService = new ObjetoService(_objetoRepositoryMock.Object);
        }

        [Test]
        public void ProcessarObjeto_NewObjeto_CallsRepositoryCreate()
        {
            // Arrange
            var objeto = new Objeto
            {
                Id = Guid.NewGuid(),
                Nome = "Objeto de teste"
            };

            _objetoRepositoryMock.Setup(repository => repository.Create(objeto)).Verifiable();

            // Act
            _objetoService.ProcessarObjeto(objeto);

            // Assert
            _objetoRepositoryMock.Verify(repository => repository.Create(objeto), Times.Once);
        }

        [Test]
        public void ProcessarObjeto_NullObjeto_ThrowsArgumentNullException()
        {
            // Arrange
            Objeto objeto = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _objetoService.ProcessarObjeto(objeto));
            _objetoRepositoryMock.Verify(repository => repository.Create(It.IsAny<Objeto>()), Times.Never);
        }

        [Test]
        public void ProcessarObjeto_RepositoryCreateFails_ThrowsException()
        {
            // Arrange
            var objeto = new Objeto
            {
                Id = Guid.NewGuid(),
                Nome = "Objeto de teste"
            };

            _objetoRepositoryMock.Setup(repository => repository.Create(objeto)).Throws<Exception>();

            // Act & Assert
            Assert.Throws<Exception>(() => _objetoService.ProcessarObjeto(objeto));
            _objetoRepositoryMock.Verify(repository => repository.Create(objeto), Times.Once);
        }
    }
}