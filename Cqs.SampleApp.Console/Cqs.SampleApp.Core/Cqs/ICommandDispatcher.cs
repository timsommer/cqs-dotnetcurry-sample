using System.Windows.Input;
using Cqs.SampleApp.Core.Cqs.Data;
using ICommand = Cqs.SampleApp.Core.Cqs.Data.ICommand;

namespace Cqs.SampleApp.Core.Cqs
{
    /// <summary>
    /// Passed around to all allow dispatching a command and to be mocked by unit tests
    /// </summary>
    public interface ICommandDispatcher
    {
        /// <summary>
        /// Dispatches a command to its handler
        /// </summary>
        /// <typeparam name="TParameter">Command Type</typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="command">The command to be passed to the handler</param>
        TResult Dispatch<TParameter, TResult>(TParameter command) where TParameter : ICommand where TResult : IResult;
    }
}