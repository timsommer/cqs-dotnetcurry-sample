using Cqs.SampleApp.Core.Cqs.Data;

namespace Cqs.SampleApp.Core.Cqs
{
    /// <summary>
    /// Base interface for command handlers
    /// </summary>
    /// <typeparam name="TParameter"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public interface ICommandHandler<in TParameter, out TResult> where TParameter : ICommand
        where TResult : IResult
    {
        /// <summary>
        /// Executes a command handler
        /// </summary>
        /// <param name="command">The command to be used</param>
        TResult Handle(TParameter command);
    }
}