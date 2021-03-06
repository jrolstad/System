using NUnit.Framework;
using Rhino.Mocks;
using Rolstad.Logging.Performance;
using log4net;

namespace Rolstad.Logging.Test.Logging.Given_a_method_logger
{
    [TestFixture]
    public class When_logging_method_details_at_the_info_level
    {
        private ILog Log;

        [TestFixtureSetUp]
        public void BeforeAll()
        {
            Log = MockRepository.GenerateStub<ILog>();
            Log.Stub(l => l.IsInfoEnabled).Return(true);

            using(new MethodLogger(Log,"Some Method",MethodLoggingLevel.Info))
            {
                var a = 1 + 1;
            }
        }


        [Test]
        public void Then_the_start_message_is_logged()
        {
            // Assert
            Log.AssertWasCalled(l=>l.Info("Some Method started"));
        }


        [Test]
        public void Then_then_the_end_message_is_logged()
        {
            // Assert
            Log.AssertWasCalled(l => l.Info(Arg<string>.Matches(s => s.Contains("Some Method complete"))));

        }
    }
}