using Ninject;

namespace Rolstad.DependencyInjection
{
    /// <summary>
    /// Registers a given set of containers
    /// </summary>
    public interface IContainerRegistration
    {
        /// <summary>
        /// Registers containers with the kernel
        /// </summary>
        /// <param name="kernel"></param>
        void Register(IKernel kernel);
    }
}