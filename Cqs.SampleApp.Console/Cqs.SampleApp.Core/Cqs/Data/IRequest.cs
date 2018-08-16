using System;

namespace Cqs.SampleApp.Core.Cqs.Data
{
    public interface IRequest
    {
        Guid CorrelationId { get; set; }
    }
}