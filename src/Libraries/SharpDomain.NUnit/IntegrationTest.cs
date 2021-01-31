using System;
using Autofac;
using MediatR;
using NUnit.Framework;
using SharpDomain.IoC;

namespace SharpDomain.NUnit
{
    public abstract class IntegrationTest<TSystemBuilder> where TSystemBuilder : SystemBuilder, new()
    {
        private IContainer? _container;
        protected IContainer Container
        {
            get => _container ?? throw new NullReferenceException();
            private set => _container = value;
        }
        
        private IMediator? _mediator;
        protected IMediator Mediator
        {
            get => _mediator ?? throw new NullReferenceException();
            private set => _mediator = value;
        }
        
        [SetUp]
        public void InitApplication()
        {
            var systemBuilder = new TSystemBuilder();
            
            ConfigureSystem(systemBuilder);
            
            Container = systemBuilder
                .WireUpApplication()
                .Build();
            
            Mediator = Container.Resolve<IMediator>();
        }
        
        [TearDown]
        protected void DisposeApplication()
        {
            Container.Dispose();
        }
        
        protected virtual void ConfigureSystem(TSystemBuilder systemBuilder)
        {
        }
    }
}