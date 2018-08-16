using System;

namespace Cqs.SampleApp.Core.Cqs
{
    public interface IRequest
    {
        Guid CorrelationId { get; set; }
    }
}