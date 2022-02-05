using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Todo.Domain.Entities;

namespace Todo.Domain.Tests.EntityTests
{
    [TestClass]
    public class TodoItemTests
    {
        private readonly TodoItem _validTodo = new TodoItem("Titulo aqui", DateTime.Now, "Marcos");

        [TestMethod]
        public void Dado_um_todo_nao_pode_ser_concluidot()
        {
            #region Arrange
            #endregion

            #region Act
            #endregion

            #region Assert
            Assert.AreEqual(_validTodo.Done, false);
            #endregion

        }
    }
}
