using System.Reflection;
using Autofac;
using AutoMapper;
using AutoMapper.Configuration;

namespace SharpDomain.AutoMapper
{
    public static class AutofacExtensions
    {
        public static ContainerBuilder RegisterAutoMapper(
            this ContainerBuilder containerBuilder, 
            params Assembly[] assemblies)
        {
            return containerBuilder
                .RegisterAutoMapper()
                .RegisterMappers(assemblies);
        }
        
        private static ContainerBuilder RegisterAutoMapper(this ContainerBuilder containerBuilder)
        {
            containerBuilder
                .RegisterType<MapperConfigurationExpression>()
                .AsSelf()
                .SingleInstance();
            
            containerBuilder.Register(c =>
                {
                    var context = c.Resolve<IComponentContext>();
                    var configExpression = context.Resolve<MapperConfigurationExpression>();
                    return new MapperConfiguration(configExpression);
                })
                .AsSelf()
                .SingleInstance();
            
            containerBuilder.Register(c =>
                {
                    var context = c.Resolve<IComponentContext>();
                    var config = context.Resolve<MapperConfiguration>();
                    return config.CreateMapper(context.Resolve);
                })
                .As<IMapper>()
                .InstancePerLifetimeScope();
            
            return containerBuilder;
        }
        
        private static ContainerBuilder RegisterMappers(
            this ContainerBuilder containerBuilder, 
            params Assembly[] assemblies)
        {
            containerBuilder.RegisterBuildCallback(context =>
            {
                var mappings = context.Resolve<MapperConfigurationExpression>();
                mappings.AddMaps(assemblies);
            });
            
            return containerBuilder;
        }
    }
}