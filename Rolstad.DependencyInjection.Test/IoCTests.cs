using System.Linq;
using NUnit.Framework;
using Ninject;
using Rhino.Mocks;

namespace Rolstad.DependencyInjection.Test
{
    [TestFixture]
    public class IoCTests
    {
         [Test]
         public void When_setting_the_kernel_then_it_is_set()
         {
             // Arrange
             var kernel = MockRepository.GenerateStub<IKernel>();

             // Act
             IoC.SetKernel(kernel);

             // Assert
             Assert.That(IoC.GetKernel(),Is.SameAs(kernel));
         }

        [Test]
        public void When_IoC_is_configured_then_each_module_is_registered()
        {
            // Arrange
            var kernel = MockRepository.GenerateStub<IKernel>();
            IoC.SetKernel(kernel);

            var registrations = new[]
                                    {
                                        MockRepository.GenerateStub<IContainerRegistration>(),
                                        MockRepository.GenerateStub<IContainerRegistration>()
                                    };

            // Act
            IoC.Configure(registrations);

            // Assert
            registrations.ToList().ForEach(r=>r.AssertWasCalled(a=>a.Register(kernel)));
        }
    }
}