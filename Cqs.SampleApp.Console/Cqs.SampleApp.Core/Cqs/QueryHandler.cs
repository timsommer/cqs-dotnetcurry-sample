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

                var _response = _queryResult as Result;
                if (_response == null) return _queryResult;

                //check the validationbag and throw exception if warnings or errors occured
                _response.ValidationBag.ThrowExceptionIfInvalid();

            }
            catch (Exception _exception)
            {
                Log.ErrorFormat("Error in queryHandler. Message: {0} \n Stacktrace: {1}", _exception.Message, _exception.StackTrace);
                //Do more error more logic here
                throw;
            }
            finally
            {
                _stopWatch.Stop();
                Log.DebugFormat("Response served (elapsed time: {0} msec)", _stopWatch.ElapsedMilliseconds);
            }


            return _queryResult;
        }

        protected abstract TResult Handle(TParameter request);
        
        protected TResult CreateTypedResult()
        {
            return (TResult)Activator.CreateInstance(typeof(TResult));
        }
    }
}