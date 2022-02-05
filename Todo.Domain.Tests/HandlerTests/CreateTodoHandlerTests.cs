using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Todo.Domain.Commands;
using Todo.Domain.Handlers;
using Todo.Domain.Tests.Repositories;

namespace Todo.Domain.Tests.HandlerTests
{
    [TestClass]
    public class CreateTodoHandlerTests
    {
        private readonly CreateTodoCommand _invalidCommand = new CreateTodoCommand("", DateTime.Now, "");

        private readonly CreateTodoCommand _validCommand = new CreateTodoCommand("Titulo da tarefa", DateTime.Now, "Marcos");

        private readonly TodoHandler _handler = new TodoHandler(new FakeTodoRepository());

        private GenericCommandResult _result = new GenericCommandResult();


        [TestMethod]
        public void Dado_um_comano_invalido()
        {
            #region Arrange
            var handler = new TodoHandler(new FakeTodoRepository());

            #endregion

            #region Act
            _result = (GenericCommandResult)_handler.Handle(_invalidCommand);
            #endregion

            #region Assert
            Assert.AreEqual(_result.Sucess, false);
            #endregion
        }

        [TestMethod]
        public void Dado_um_comano_valido()
        {
            #region Arrange
            var handler = new TodoHandler(new FakeTodoRepository());
            #endregion

            #region Act
            _result = (GenericCommandResult)_handler.Handle(_validCommand);
            #endregion

            #region Assert
            Assert.AreEqual(_result.Sucess, true);
            #endregion
        }
    }
}
