namespace Cqs.SampleApp.Core.Cqs
{
    /// <summary>
    /// Passed around to all allow dispatching a query and to be mocked by unit tests
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
    }
}