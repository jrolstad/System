using System.Collections.Generic;
using System.Linq;
using Ninject;

namespace Rolstad.DependencyInjection
{
    /// <summary>
    /// IoC wrapper class
    /// </summary>
    public static class IoC
    {
        /// <summary>
        /// Ninject kernel to use for wrapping
        /// </summary>
        private static IKernel _kernel;

        /// <summary>
        /// Gets an instance of a given type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Get<T>()
        {
            return _kernel.Get<T>();
        }

        /// <summary>
        /// Configures the IoC container
        /// </summary>
        /// <param name="modules"></param>
        public static void Configure(IEnumerable<IContainerRegistration> modules)
        {
            modules.ToList().ForEach(m=>m.Register(_kernel));
        }

        /// <summary>
        /// Sets the underlying Ninject kernel
        /// </summary>
        /// <param name="kernel"></param>
        public static void SetKernel(IKernel kernel)
        {
            _kernel = kernel;
        }

        /// <summary>
        /// Gets the underlying Ninject kernel
        /// </summary>
        /// <param name="kernel"></param>
        public static IKernel GetKernel()
        {
            return _kernel;
        }
    }
}