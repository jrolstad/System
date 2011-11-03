using NUnit.Framework;
using Rhino.Mocks;
using Rolstad.Data.Providers;
using ScrappyDB;
using ScrappyDB.BaseClasses;
using System.Linq;

namespace Retrospect.Test.Providers
{
    [TestFixture]
    public class SimpleDBProviderTests
    {
        [Test]
        public void When_getting_an_item_then_it_is_retrieved()
        {
            // Arrage
            var Note = FizzWare.NBuilder.Builder<Note>.CreateNew().Build();
            
            var db = MockRepository.GenerateStub<IDb>();
            db.Stub(d => d.Find<Note>(Note.NoteId)).Return(Note);
            var provider = new SimpleDBProvider<Note, string>(db);

            // Act
            var result = provider.Get(Note.NoteId);

            // Assert
            Assert.That(result,Is.SameAs(Note));
        }

        [Test]
        public void When_obtaining_all_items_they_are_obtained()
        {
            // Arrange
            var Notes = FizzWare.NBuilder.Builder<Note>.CreateListOfSize(10).Build();

            var simpleDbNotes = new SdbCollection<Note>();
            Notes.ToList().ForEach(simpleDbNotes.Add);

            var db = MockRepository.GenerateStub<IDb>();
            db.Stub(d => d.Query<Note>()).Return(simpleDbNotes);

            var provider = new SimpleDBProvider<Note, string>(db);

            // Act
            var result = provider.GetAll();

            // Assert
            Assert.That(result,Is.EquivalentTo(Notes));
        }

        [Test]
        public void When_searching_then_all_matching_items_are_obtained()
        {
            // Arrange
            var Notes = FizzWare.NBuilder.Builder<Note>.CreateListOfSize(10).Build();
            Notes[3].NoteId = "some other id";

            var simpleDbNotes = new SdbCollection<Note>();
            Notes.ToList().ForEach(simpleDbNotes.Add);

            var db = MockRepository.GenerateStub<IDb>();
            db.Stub(d => d.Query<Note>()).Return(simpleDbNotes);

            var provider = new SimpleDBProvider<Note, string>(db);

            // Act
            var result = provider.Search((Note) => Note.NoteId == "some other id");

            // Assert
            Assert.That(result.ToArray(),Is.EquivalentTo(new[]{Notes[3]}));
        }

        [Test]
        public void When_saving_an_item_it_is_saved()
        {
            // Arrage
            var Note = FizzWare.NBuilder.Builder<Note>.CreateNew().Build();

            var db = MockRepository.GenerateStub<IDb>();
            var provider = new SimpleDBProvider<Note, string>(db);

            // Act
            provider.Save(Note);

            // Assert
            db.AssertWasCalled(d=>d.SaveChanges(Note));
        }

        [Test]
        public void When_deleting_an_item_it_is_deleted()
        {
            // Arrage
            var Note = FizzWare.NBuilder.Builder<Note>.CreateNew().Build();

            var db = MockRepository.GenerateStub<IDb>();
            var provider = new SimpleDBProvider<Note, string>(db);

            // Act
            provider.Delete(Note.NoteId);

            // Assert
            db.AssertWasCalled(d => d.Delete<Note>(Note.NoteId));
        }

        private class Note
        {
            public string NoteId { get; set; }
        }
    }
}