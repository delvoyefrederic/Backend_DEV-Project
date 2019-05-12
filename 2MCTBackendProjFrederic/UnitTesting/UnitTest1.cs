using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace UnitTesting
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
        }
        [TestMethod]
        public async Task TaskAPICtrlAsync_GET_ReturnsToDoTasks()
        {
            // Arrange(Mock op de interface - anders not supported exception)
            /*var mockRepo = new Mock<IToDoTaskRepo>();
            mockRepo.Setup(repo => repo.GetAlltoDoTasksAsync()).Returns(toDoTasks);
            PI.Controllers.ToDoTaskController controllerAPI = new API.Controllers.ToDoTaskController(mockRepo.Object, null)*/
        }
    }
}


