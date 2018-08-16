using System;
using System.Diagnostics;
using Cqs.SampleApp.Core.Cqs.Data;
using Cqs.SampleApp.Core.DataAccess;
using log4net;

namespace Cqs.SampleApp.Core.Cqs
{
    public abstract class CommandHandler<TRequest, TResult> : ICommandHandler<TRequest, TResult>
        where TRequest : ICommand
        where TResult : IResult, new()
    {
        protected readonly ILog Log;
        protected ApplicationDbContext ApplicationDbContext;

        protected CommandHandler(ApplicationDbContext context)
        {
            ApplicationDbContext = context;
            Log = LogManager.GetLogger(GetType().FullName);
        }


        public TResult Handle(TRequest command)
        {
            var _stopWatch = new Stopwatch();
            _stopWatch.Start();
            
            TResult _response;

            try
            {
                //do data validation
                //do authorization

                _response = DoHandle(command);
            }
            catch (Exception _e)
            {
                Log.ErrorFormat("Error: {0} \n Stacktrace: {1}", _e.Message, _e.StackTrace);
                throw;
            }

            return _response;
        }

        // Protected methods
        protected abstract TResult DoHandle(TRequest request);

        protected TResult CreateTypedResult()
        {
            return (TResult)Activator.CreateInstance(typeof(TResult));
        }
    }
}