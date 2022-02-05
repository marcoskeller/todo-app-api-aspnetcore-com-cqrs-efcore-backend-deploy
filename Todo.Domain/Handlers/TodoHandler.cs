using Flunt.Notifications;
using Todo.Domain.Commands;
using Todo.Domain.Commands.Contracts;
using Todo.Domain.Entities;
using Todo.Domain.Handlers.Contracts;
using Todo.Domain.Repositories;

namespace Todo.Domain.Handlers
{
    public class TodoHandler : Notifiable, 
        IHandler<CreateTodoCommand>, 
        IHandler<UpdateTodoCommand>,
        IHandler<MarkTodoAsDoneCommand>,
        IHandler<MarkTodoAsUnDoneCommand>
    {
        private readonly ITodoRepository _repository;

        public TodoHandler(ITodoRepository repository)
        {
            _repository = repository;
        }

        public ICommandResult Handle(CreateTodoCommand command)
        {

            //Criar um Todo (Fail Fast Validation)
            command.Validate();
            
            if(command.Invalid)
            {
                return new GenericCommandResult(false, "Ops, parece que sua tarefa esta errada!", command.Notifications);
            }

            var todo = new TodoItem(command.Title, command.Date, command.User);

            //Salvar um Todo no Banco
            _repository.Create(todo);

            //Retorna o resultado
            return new GenericCommandResult(true, "Tarefa salva!", todo);
        }

        public ICommandResult Handle(UpdateTodoCommand command)
        {
            //Atualiza um Todo (Fail Fast Validation)
            command.Validate();

            if (command.Invalid)
            {
                return new GenericCommandResult(false, "Ops, parece que sua tarefa esta errada!", command.Notifications);
            }

            //Recuperar um Todo do Banco (Método também chamado de Reidratação)
            var todo = _repository.GetById(command.Id, command.User);

            //Altera o Titulo
            todo.UpdateTitle(command.Title);

            //Salvar um Todo no Banco
            _repository.Update(todo);

            //Retorna o resultado
            return new GenericCommandResult(true, "Tarefa salva!", todo);
        }

        public ICommandResult Handle(MarkTodoAsDoneCommand command)
        {
            //Atualiza um Todo (Fail Fast Validation)
            command.Validate();

            if (command.Invalid)
            {
                return new GenericCommandResult(false, "Ops, parece que sua tarefa esta errada!", command.Notifications);
            }

            //Recuperar um TodoItem do Banco (Método também chamado de Reidratação)
            var todo = _repository.GetById(command.Id, command.User);

            //Altera o estado
            todo.MarkAsDone();

            //Salvar um Todo no Banco
            _repository.Update(todo);

            //Retorna o resultado
            return new GenericCommandResult(true, "Tarefa salva!", todo);
        }

        public ICommandResult Handle(MarkTodoAsUnDoneCommand command)
        {
            //Atualiza um Todo (Fail Fast Validation)
            command.Validate();

            if (command.Invalid)
            {
                return new GenericCommandResult(false, "Ops, parece que sua tarefa esta errada!", command.Notifications);
            }

            //Recuperar um TodoItem do Banco (Método também chamado de Reidratação)
            var todo = _repository.GetById(command.Id, command.User);

            //Altera o estado
            todo.MarkAsUnDone();

            //Salvar um Todo no Banco
            _repository.Update(todo);

            //Retorna o resultado
            return new GenericCommandResult(true, "Tarefa salva!", todo);
        }
    }
}
