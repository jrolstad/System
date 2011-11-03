using System.Linq;
using FizzWare.NBuilder;
using NUnit.Framework;
using Retrospect.Adapters;

namespace Rolstad.Data.Test.Adapters
{
    [TestFixture]
    public class AutoMapperAdapterTests
    {
        private class Note
        {
            public string NoteId { get; set; }
        }

        private class NoteFrom
        {
            public string NoteId { get; set; }
        }

        [Test]
        public void When_converting_from_one_type_to_another_then_it_is_converted()
        {
            // Arrange
            var notes = Builder<NoteFrom>.CreateListOfSize(3).Build();

            var adapter = new AutoMapperAdapter<NoteFrom, Note>();

            // Act
            var result = adapter.Convert(notes);

            // Assert
            Assert.That(notes.Select(n=>n.NoteId).ToArray(),Is.EquivalentTo(result.Select(r=>r.NoteId).ToArray()));
        }
    }
}