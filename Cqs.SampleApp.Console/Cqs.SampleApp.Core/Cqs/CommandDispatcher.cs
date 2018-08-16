using System;
using Autofac;

namespace Cqs.SampleApp.Core.Cqs
{
    /// <inheritdoc />
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IComponentContext _Context;

        public CommandDispatcher(IComponentContext context)
        {
            _Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public TResult Dispatch<TParameter, TResult>(TParameter command) where TParameter : ICommand where TResult : IResult
        {
            var _handler = _Context.Resolve<ICommandHandler<TParameter, TResult>>();
            return _handler.Handle(command);
        }
    }
}