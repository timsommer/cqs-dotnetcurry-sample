using System;
using Cqs.SampleApp.Core.Cqs.Validation;

namespace Cqs.SampleApp.Core.Cqs.Data
{
    public abstract class Result : IResult
    {
        protected Result()
        {
            ValidationBag = new ValidationBag();
        }

        public ValidationBag ValidationBag { get; set; }
        public Exception Exception { get; set; }
        public bool Success { get; set; }
    }
}