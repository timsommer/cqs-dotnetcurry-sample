using System;
using System.Diagnostics;
using Cqs.SampleApp.Core.Cqs.Data;
using Cqs.SampleApp.Core.DataAccess;
using log4net;

namespace Cqs.SampleApp.Core.Cqs
{
    public abstract class QueryHandler<TParameter, TResult> : IQueryHandler<TParameter, TResult>
        where TResult : IResult, new()
        where TParameter : IQuery, new()
    {
        protected readonly ILog Log;
        protected ApplicationDbContext ApplicationDbContext;

        protected QueryHandler(ApplicationDbContext applicationDbContext)
        {
            ApplicationDbContext = applicationDbContext;
            Log = LogManager.GetLogger(GetType().FullName);
        }

        public TResult Retrieve(TParameter query)
        {
            var _stopWatch = new Stopwatch();
            _stopWatch.Start();

            TResult _queryResult;

            try
            {
                //do authorization and validatiopn

                //handle the query request
                _queryResult = Handle(query);
                
            }
            catch (Exception _exception)
            {
                Log.ErrorFormat("Error in {0} queryHandler. Message: {1} \n Stacktrace: {2}", typeof(TParameter).Name, _exception.Message, _exception.StackTrace);
                //Do more error more logic here
                throw;
            }
            finally
            {
                _stopWatch.Stop();
                Log.DebugFormat("Response for query {0} served (elapsed time: {1} msec)", typeof(TParameter).Name, _stopWatch.ElapsedMilliseconds);
            }


            return _queryResult;
        }

        /// <summary>
        /// The actual Handle method that will be implemented in the sub class
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected abstract TResult Handle(TParameter request);
        
    }
}