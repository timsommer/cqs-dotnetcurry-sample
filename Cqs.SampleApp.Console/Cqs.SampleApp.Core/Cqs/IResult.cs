using System;

namespace Cqs.SampleApp.Core.Cqs
{
    /// <summary>
    /// Marker interface to mark Result
    /// </summary>
    public interface IResult
    {
        Exception Exception { get; set; }
    }
}