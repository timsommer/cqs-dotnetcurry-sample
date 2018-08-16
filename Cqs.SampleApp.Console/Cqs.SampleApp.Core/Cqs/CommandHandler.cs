using System;
using System.Diagnostics;
using Cqs.SampleApp.Core.Cqs.Data;
using Cqs.SampleApp.Core.Cqs.Validation;
using Cqs.SampleApp.Core.DataAccess;
using log4net;

namespace Cqs.SampleApp.Core.Cqs
{
    public abstract class CommandHandler<TRequest, TResult> : ICommandHandler<TRequest, TResult>
        where TRequest : ICommand
        where TResult : Result, new()
    {
        protected readonly ILog Log;
        protected ApplicationDbContext ApplicationDbContext;
        private readonly ICommandValidator<TRequest> _CommandValidator;

        protected CommandHandler(ApplicationDbContext dbContext, ICommandValidator<TRequest> commandValidator)
        {
            _CommandValidator = commandValidator;
            ApplicationDbContext = dbContext;
            Log = LogManager.GetLogger(GetType().FullName);
        }


        public TResult Handle(TRequest command)
        {
            var _stopWatch = new Stopwatch();
            _stopWatch.Start();
            
            TResult _response = null;

            try
            {
                var _validationbag = new ValidationBag();
                _CommandValidator.Validate(command, _validationbag);
                _validationbag.ThrowExceptionIfInvalid();

                //do authorization

                _response = DoHandle(command, _validationbag);
                _response.ValidationBag = _validationbag;
                _validationbag.ThrowExceptionIfInvalid();
            }
            catch (ValidationException _e)
            {
                Log.WarnFormat("Validation Warning: {0} \n Stacktrace: {1}", _e.Message, _e.StackTrace);
                throw;
            }
            catch (Exception _e)
            {
                Log.ErrorFormat("Error: {0} \n Stacktrace: {1}", _e.Message, _e.StackTrace);
                throw;
            }

            return _response;
        }

        // Protected methods
        protected abstract TResult DoHandle(TRequest request, ValidationBag bag);

        protected TResult CreateTypedResult()
        {
            return (TResult)Activator.CreateInstance(typeof(TResult));
        }
    }
}