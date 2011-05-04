using System;
using NUnit.Framework;
using Rolstad.System.Extensions;

namespace Rolstad.System.Test.Extensions.Given_a_nullable_value
{
    [TestFixture]
    public class When_obtaining_its_string_representation
    {

        [Test]
        public void And_the_value_is_null_then_null_is_obtained()
        {
            // Arrange
            var nullable = new DateTime?();

            // Act
            var result = nullable.ToString("MMddyyyy");

            // Assert
            Assert.That(result,Is.Null);
        }

        [Test]
        public void And_the_value_is_not_null_then_string_value_is_obtained()
        {
            // Arrange
            var nullable = new DateTime?(new DateTime(2010,4,5));

            // Act
            var result = nullable.ToString("MMddyyyy");

            // Assert
            Assert.That(result, Is.EqualTo("04052010"));
        }

    }
}