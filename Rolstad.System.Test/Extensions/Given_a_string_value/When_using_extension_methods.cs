using System;
using NUnit.Framework;
using Rolstad.System.Extensions;

namespace Rolstad.System.Test.Extensions.Given_a_string_value
{
    [TestFixture]
    public class When_using_extension_methods
    {
        [Test]
        public void SafeTrim_with_null_then_a_null_is_returned()
        {
            string testString = null;
            Assert.That(testString.SafeTrim(), Is.EqualTo(null));
        }

        [Test]
        public void SafeTrim_with_a_string_with_spaces_then_the_spaces_are_trimmed()
        {
            const string testString = " my test String ";
            Assert.That(testString.SafeTrim(), Is.EqualTo("my test String"));
        }


        [Test]
        public void SafeTrim_with_a_string_with_no_spaces_then_the_same_string_is_returned()
        {
            const string testString = "my test String";
            Assert.That(testString.SafeTrim(), Is.EqualTo("my test String"));
        }


        [Test]
        public void IsEmpty_with_a_null_string_then_the_string_is_empty()
        {
            // Arrange
            string input = null;

            // Act
            var result = input.IsEmpty();

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsEmpty_with_an_empty_string_then_the_string_is_empty()
        {
            // Arrange
            string input = "";

            // Act
            var result = input.IsEmpty();

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsEmpty_with_a_non_empty_string_then_the_string_is_not_empty()
        {
            // Arrange
            string input = "some value";

            // Act
            var result = input.IsEmpty();

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void When_converting_a_null_to_nullable_decimal_then_null()
        {
            // Arrange
            string value = null;

            // Act
            var result = value.To<decimal?>();

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void When_converting_a_1_point22_to_nullable_decimal_then_1point22()
        {
            // Arrange
            var value = "1.22";

            // Act
            var result = value.To<decimal?>();

            // Assert
            Assert.That(result, Is.EqualTo(1.22m));
        }

        [Test]
        public void When_converting_a_1_point22_to_decimal_then_1point22()
        {
            // Arrange
            var value = "1.22";

            // Act
            var result = value.To<decimal>();

            // Assert
            Assert.That(result, Is.EqualTo(1.22m));
        }

        [Test]
        public void  When_converting_a_null_to_decimal_then_0()
        {
            // Arrange
            string value = null;

            // Act
            var result = value.To<decimal>();

            // Assert
            Assert.That(result, Is.EqualTo(0M));
        }

        [Test]
        public void  When_converting_a_null_to_DateTime_then_DateTime_Min()
        {
            // Arrange
            string value = null;

            // Act
            var result = value.To<DateTime>();

            // Assert
            Assert.That(result, Is.EqualTo(DateTime.MinValue));
        }

        [Test]
        public void  When_converting_a_good_value_to_DateTime_then_DateTime_value()
        {
            // Arrange
            var value = "2000.12.31";

            // Act
            var result = value.To<DateTime>();

            // Assert
            Assert.That(result, Is.EqualTo(new DateTime(2000, 12, 31)));
        }

        

        [Test]
        public void When_formatting_a_string_it_returns_the_formatted_value()
        {

            // Act
            var result = "Some value {0} {1}".StringFormat("foo", 1);

            // Assert
            Assert.That(result, Is.EqualTo("Some value foo 1"));
        }
    }
}