using System;
using Autofac;
using Cqs.SampleApp.Core.Cqs.Data;
using log4net;

namespace Cqs.SampleApp.Core.Cqs
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IComponentContext _Context;

        public QueryDispatcher(IComponentContext context)
        {
            _Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public TResult Dispatch<TParameter, TResult>(TParameter query)
            where TParameter : IQuery
            where TResult : IResult
        {
            var _handler = _Context.Resolve<IQueryHandler<TParameter, TResult>>();
            return _handler.Retrieve(query);
        }
    }
}