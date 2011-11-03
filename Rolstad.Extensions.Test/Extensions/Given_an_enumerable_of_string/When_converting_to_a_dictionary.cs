using NUnit.Framework;

namespace Rolstad.Extensions.Test.Extensions.Given_an_enumerable_of_string
{
    [TestFixture]
    public class When_converting_to_a_dictionary
    {
         [Test]
         public void Then_the_exception_says_what_is_going_on()
         {
             // Arrange
             var values = new[] {"one", "one"};

             // Act
             var result = values.ToDictionaryExplicit(v => v, v => v);

             // Assert
         }
    }
}