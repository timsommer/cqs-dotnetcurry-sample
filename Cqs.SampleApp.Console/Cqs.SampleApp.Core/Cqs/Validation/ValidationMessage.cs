using System;

namespace Cqs.SampleApp.Core.Cqs.Validation
{
    [Serializable]
    public class ValidationMessage
    {
        public ValidationMessage(ValidationType validationType, string code, string message)
        {
            ValidationType = validationType;
            Code = code;
            Message = message;
        }

        public ValidationType ValidationType { get; private set; }

        public string Code { get; private set; }

        public string Message { get; private set; }
    }
}