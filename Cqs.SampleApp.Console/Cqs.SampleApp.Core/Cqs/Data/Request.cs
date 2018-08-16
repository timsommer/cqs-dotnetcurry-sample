using System;
using System.Security.Principal;

namespace Cqs.SampleApp.Core.Cqs.Data
{
    public abstract class Request : IRequest
    {
        public Guid CorrelationId { get; set; }
    }
}