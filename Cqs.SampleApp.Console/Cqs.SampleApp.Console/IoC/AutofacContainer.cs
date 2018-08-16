using System;
using System.Collections;
using System.Collections.Generic;
using Autofac;
using log4net;

namespace Cqs.SampleApp.Console.IoC
{
    public class AutofacContainer : IAutofacContainer
    {
        private readonly ILog _Log = LogManager.GetLogger(typeof(AutofacContainer).Name);

        private readonly Autofac.IContainer _InnerContainer;

        public AutofacContainer(Action<ContainerBuilder> builderAction)
        {
            _Log.DebugFormat("Contructing autofac container");

            var _builder = new ContainerBuilder();

            builderAction?.Invoke(_builder);

            _InnerContainer = _builder.Build();
        }

        /// <inheritdoc />
        /// <summary>
        /// Return the list of all instances that implements T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<T> ResolveAll<T>()
        {
            _Log.DebugFormat("Resolve all registered instances of type {0}", typeof(T));

            return _InnerContainer.Resolve<IEnumerable<T>>();
        }

        /// <inheritdoc />
        /// <summary>
        /// Return the list of all instances that implements type
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public IEnumerable ResolveAll(Type type)
        {
            _Log.DebugFormat("Resolve all registered instances of type {0}", type);

            var _genericType = typeof(IEnumerable<>).MakeGenericType(type);
            return (IEnumerable)_InnerContainer.Resolve(_genericType);
        }

        /// <inheritdoc />
        /// <summary>
        /// Resolve an instance of type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Resolve<T>()
        {
            _Log.DebugFormat("Resolve instance of type {0}", typeof(T));
            
            return _InnerContainer.Resolve<T>();
        }

        /// <inheritdoc />
        /// <summary>
        /// Resolve an instance of the specified type
        /// Throws an exception when the type is not found
        /// </summary>		
        /// <param name="type">The type to resolve.</param>
        /// <returns>The instance</returns>		
        public object Resolve(Type type)
        {
            _Log.DebugFormat("Resolve instance of type {0}", type);

            return _InnerContainer.Resolve(type);
        }
        
        /// <inheritdoc />
        /// <summary>
        /// Resolve an instance of the specified type
        /// </summary>		
        /// <param name="type">The type to resolve.</param>
        /// <returns>The instance or null when not found</returns>		
        public object TryResolve(Type type)
        {
            _Log.DebugFormat("Try to resolve instance of type {0}", type);

            return _InnerContainer.IsRegistered(type) ? _InnerContainer.Resolve(type) : null;
        }

        /// <inheritdoc />
        /// <summary>
        /// Resolve a named instance of type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public T Resolve<T>(string key)
        {
            _Log.DebugFormat("Resolve instance of type {0} with name {1}", typeof(T), key);
            
            return _InnerContainer.ResolveNamed<T>(key);
        }

        public Autofac.IContainer InnerContainer => _InnerContainer;
    }
}