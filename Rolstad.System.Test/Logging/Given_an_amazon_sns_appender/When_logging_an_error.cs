using System.Collections.Generic;
using System.Linq;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using log4net.Core;
using log4net.Layout;
using NUnit.Framework;
using Rhino.Mocks;
using Rolstad.System.Logging;

namespace Rolstad.System.Test.Logging.Given_an_amazon_sns_appender
{
    [TestFixture]
    public class When_logging_an_error
    {
        private string LogMessage;
        private List<PublishRequest> SentRequests = new List<PublishRequest>();

        [TestFixtureSetUp]
        public void BeforeAll()
        {
            var notificationService = MockRepository.GenerateStub<AmazonSimpleNotificationService>();
            notificationService
                .Stub(s => s.Publish(Arg<PublishRequest>.Is.Anything))
                .WhenCalled(p => SentRequests.Add((PublishRequest)p.Arguments[0]));

            var appender = new AmazonSimpleNotificationServiceAppender()
            {
                NotificationService = notificationService,
                Layout = new PatternLayout("%message")
            };

            LogMessage = "Testing 123";
            var loggingEventData = new LoggingEventData {ExceptionString = LogMessage};
            var loggingEvent = new LoggingEvent(loggingEventData);

            appender.DoAppend(loggingEvent);
        }


        [Test]
        public void Then_the_message_is_sent_to_the_aws_topic()
        {
            // Assert
            var requestWithMessage = SentRequests.FirstOrDefault(r => r.Message.Contains(LogMessage));
            Assert.That(requestWithMessage,Is.Not.Null);
        }


    }
}