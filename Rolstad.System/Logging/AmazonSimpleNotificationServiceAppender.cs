using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using log4net.Appender;
using log4net.Core;

namespace Rolstad.System.Logging
{
    /// <summary>
    /// Appender that sends messages to the Amazon Simple Notification Service
    /// </summary>
    public class AmazonSimpleNotificationServiceAppender : AppenderSkeleton
    {
        /// <summary>
        /// SNS Topic to send messages to
        /// </summary>
        public virtual string Topic { get; set; }

        /// <summary>
        /// Amazon Access Key
        /// </summary>
        public virtual string AWSAccessKey { get; set; }

        /// <summary>
        /// Amazon Secret Key
        /// </summary>
        public virtual string AWSSecretKey { get; set; }

        /// <summary>
        /// Subject of the message being sent
        /// </summary>
        public virtual string MessageSubject { get; set; }

        /// <summary>
        /// SNS instanced to use for sending
        /// </summary>
        internal AmazonSimpleNotificationService NotificationService { get; set; }

        /// <summary>
        /// Override of the append method.  This is where the message is sent to the SNS
        /// </summary>
        /// <param name="loggingEvent">Event to be sent</param>
        protected override void Append(LoggingEvent loggingEvent)
        {
            // Get the message
            var logMessage = RenderLoggingEvent(loggingEvent);

            // Get the reference to the Amazon SNS if we don't have one
            if(NotificationService == null)
            {
                NotificationService = new AmazonSimpleNotificationServiceClient(AWSAccessKey, AWSSecretKey);
            }

            // Push out the message
            var publishRequest = new PublishRequest()
                .WithTopicArn(Topic)
                .WithSubject(MessageSubject)
                .WithMessage(logMessage);
            this.NotificationService.Publish(publishRequest);

        }
        
    }
}