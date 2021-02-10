using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;

namespace SharpDomain.IoC
{
    public static class InitializeExtension
    {
        public static TBuilder InitializeIfNeed<TBuilder>(this TBuilder systemBuilder) 
            where TBuilder : SystemBuilder
        {
            systemBuilder.OnBuild(context =>
            {
                var initializers = context.Resolve<IEnumerable<ISystemInitializer>>();
                return InitializeIfNeed(initializers);
            });
            
            return systemBuilder;
        }
        
        public static TBuilder InitializeForcefully<TBuilder>(this TBuilder systemBuilder) 
            where TBuilder : SystemBuilder
        {
            systemBuilder.OnBuild(context =>
            {
                var initializers = context.Resolve<IEnumerable<ISystemInitializer>>();
                return InitializeForcefully(initializers);
            });
            
            return systemBuilder;
        }
        
        private static async Task InitializeIfNeed(IEnumerable<ISystemInitializer> initializers)
        {
            foreach (var initializer in initializers)
            {
                await initializer.InitializeIfNeed();
            }
        }
        
        private static async Task InitializeForcefully(IEnumerable<ISystemInitializer> initializers)
        {
            foreach (var initializer in initializers)
            {
                await initializer.InitializeForcefully();
            }
        }
    }
}