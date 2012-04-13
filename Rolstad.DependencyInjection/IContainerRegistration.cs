using System;
using Ninject;

namespace Rolstad.DependencyInjection
{
    /// <summary>
    /// Registers a given set of containers
    /// </summary>
    [Obsolete]
    public interface IContainerRegistration
    {
        /// <summary>
        /// Registers containers with the kernel
        /// </summary>
        /// <param name="kernel"></param>
        void Register(IKernel kernel);
    }
}