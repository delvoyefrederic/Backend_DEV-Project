using API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.BackendDTO;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationTest
{
    [TestClass]
    public class UnitTest1
    {
        private Task<List<TblFestivalsDTO>> GetFestivals = Task.FromResult(new List<TblFestivalsDTO>());

        [TestInitialize]
        public async Task GetAllFakeGetFestivals()
        {

        }

        [TestMethod]
        public async Task TaskAPICtrlAsync_GET_ReturnsToDoTasks()
        {
            // Arrange (Mock op de interface - anders not supported exception)
            var mockRepo = new Mock<IFestivalRepo>();
            mockRepo.Setup(repo => repo.GetFestivals()).Returns(GetFestivals)
         ;
            API.Controllers.festivalsController controllerAPI = new API.Controllers.festivalsController(mockRepo.Object);
            // Act met await
            var actionResult = controllerAPI.GetFestivals();
            var okResult = actionResult;
            IEnumerable<TblFestivalsDTO> Tasks = okResult.Value as IEnumerable<TblFestivalsDTO>;
            // Assert: Controleer altijd  null + datatypes + statuscode + inhoud
            Assert.IsNotNull(okResult);
            Assert.IsInstanceOfType(okResult.Value, typeof(Task<List<TblFestivalsDTO>>));
            Assert.IsTrue(Tasks.Count() == 5);
        }


    }
}
