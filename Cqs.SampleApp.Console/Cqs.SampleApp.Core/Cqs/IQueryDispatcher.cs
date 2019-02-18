using System.Threading.Tasks;
using Cqs.SampleApp.Core.Cqs.Data;

namespace Cqs.SampleApp.Core.Cqs
{
    /// <summary>
    /// Dispatches a query and invokes the corresponding handler
    /// </summary>
    public interface IQueryDispatcher
    {
        /// <summary>
        /// Dispatches a query and retrieves a query result
        /// </summary>
        /// <typeparam name="TParameter">Request to execute type</typeparam>
        /// <typeparam name="TResult">Request Result to get back type</typeparam>
        /// <param name="query">Request to execute</param>
        /// <returns>Request Result to get back</returns>
        TResult Dispatch<TParameter, TResult>(TParameter query)
            where TParameter : IQuery
            where TResult : IResult;

        /// <summary>
        /// Dispatches a query and retrieves am async query result
        /// </summary>
        /// <typeparam name="TParameter">Request to execute type</typeparam>
        /// <typeparam name="TResult">Request Result to get back type</typeparam>
        /// <param name="query">Request to execute</param>
        /// <returns>Request Result to get back</returns>
        Task<TResult> DispatchAsync<TParameter, TResult>(TParameter query)
            where TParameter : IQuery
            where TResult : IResult;
    }
}