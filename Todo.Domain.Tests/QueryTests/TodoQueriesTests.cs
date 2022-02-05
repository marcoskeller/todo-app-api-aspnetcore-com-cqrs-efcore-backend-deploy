using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Todo.Domain.Entities;
using Todo.Domain.Queries;

namespace Todo.Domain.Tests.QueryTests
{
    [TestClass]
    public class TodoQueriesTests
    {

        private List<TodoItem> _items;

        public TodoQueriesTests()
        {
            _items = new List<TodoItem>();
            _items.Add(new TodoItem("Tarefa 1", DateTime.Now, "Marcos"));
            _items.Add(new TodoItem("Tarefa 2", DateTime.Now, "Usuário Beta"));
            _items.Add(new TodoItem("Tarefa 3", DateTime.Now, "Marcos"));
            _items.Add(new TodoItem("Tarefa 4", DateTime.Now, "Usuário Beta"));
            _items.Add(new TodoItem("Tarefa 5", DateTime.Now, "Marcos"));
        }


        [TestMethod]
        public void Retorna_tarefas_usuario_informado()
        {
            #region Arrange
            var result = _items.AsQueryable().Where(TodoQueries.GetAll("Marcos"));
            #endregion

            #region Act
            #endregion

            #region Assert
            Assert.AreEqual(3, result.Count());
            #endregion
        }
    }
}
