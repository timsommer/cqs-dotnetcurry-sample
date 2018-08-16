using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Cqs.SampleApp.Core.Cqs.Validation
{
    [Serializable]
    public class ValidationException : Exception
    {
        public ValidationException()
        {

        }

        public ValidationException(ValidationBag validationBag)
        {
            ValidationBag = validationBag;
        }

        public ValidationBag ValidationBag { get; set; }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue("ValidationBag", ValidationBag);
        }
    }
}