using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Todo.Domain.Commands;

namespace Todo.Domain.Tests.CommandTests
{
    [TestClass]
    public class CreateTodoCommandTests
    {
        private readonly CreateTodoCommand _invalidCommand = new CreateTodoCommand("", DateTime.Now, "");

        private readonly CreateTodoCommand _validCommand = new CreateTodoCommand("Titulo da tarefa", DateTime.Now, "Marcos");

        [TestMethod]
        public void Dado_um_comano_invalido()
        {
            #region Arrange

            #endregion

            #region Act
            _invalidCommand.Validate();
            #endregion

            #region Assert
            Assert.AreEqual(_invalidCommand.Valid, false);
            #endregion
        }

        [TestMethod]
        public void Dado_um_comano_valido()
        {
            #region Arrange

            #endregion

            #region Act
            _validCommand.Validate();
            #endregion

            #region Assert
            Assert.AreEqual(_validCommand.Valid, true);
            #endregion
        }
    }
}
