using Cqs.SampleApp.Core.Cqs.Data;

namespace Cqs.SampleApp.Core.Cqs
{
    /// <summary>
    /// Base interface for query handlers
    /// </summary>
    /// <typeparam name="TParameter">Request type</typeparam>
    /// <typeparam name="TResult">Request Result type</typeparam>
    public interface IQueryHandler<in TParameter, out TResult> where TResult : IResult where TParameter : IQuery
    {
        /// <summary>
        /// Retrieve a query result from a query
        /// </summary>
        /// <param name="query">Request</param>
        /// <returns>Retrieve Request Result</returns>
        TResult Retrieve(TParameter query);
    }
}