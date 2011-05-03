using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Rolstad.System.Extensions;

namespace Rolstad.System.Test.Extensions.Given_an_enumerable_of_string
{
    [TestFixture]
    public class When_segmenting_it_to_4_items_each
    {
        private IEnumerable<char> Original;
        private IEnumerable<char>[] Result;


        [TestFixtureSetUp]
        public void BeforeAll()
        {
            Original = "123456789".ToCharArray();

            Result = Original.Segment(4).ToArray();
        }


        [Test]
        public void Then_the_first_element_is_the_first_four_items()
        {
            // Assert
            Assert.That(Result[0].ToArray(), Is.EquivalentTo("1234".ToCharArray()));
        }

        [Test]
        public void Then_the_second_element_is_the_second_four_items()
        {
            // Assert
            Assert.That(Result[1].ToArray(), Is.EquivalentTo("5678".ToCharArray()));
        }

         [Test]
        public void Then_the_last_element_is_the_remaining_items()
        {
            // Assert
            Assert.That(Result[2].ToArray(), Is.EquivalentTo("9".ToCharArray()));
        }
       
    }
}