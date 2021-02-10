using System.Threading.Tasks;

namespace SharpDomain.IoC
{
    public interface ISystemInitializer
    {
        Task InitializeIfNeed();
        Task InitializeForcefully();
    }
}