using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GitHubCopilotApp_C_React.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private List<BookAttributes> _booksInfo = new List<BookAttributes>();

        //CREATE a book
        [HttpPost]
        public IActionResult Post([FromBody] BookAttributes book)
        {
            book.Id = _booksInfo.Count + 1;
            _booksInfo.Add(book);
            return CreatedAtAction(nameof(Post), new { id = book.Id }, book);

        }

        //GET a book/ READ book information
        [HttpGet("{id}")]
        public IActionResult GetBook(int id)
        {
            var book = _booksInfo.Find(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);

        }
        //GET all books
        [HttpGet]
        public IActionResult GetBooks()
        {
            return Ok(_booksInfo);
        }

        //UPDATE a book
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] BookAttributes book)
        {
            var bookToUpdate = _booksInfo.Find(b => b.Id == id);
            if (bookToUpdate == null)
            {
                return NotFound();
            }
            bookToUpdate.Title = book.Title;
            bookToUpdate.Author = book.Author;
            bookToUpdate.Genre = book.Genre;
            bookToUpdate.Description = book.Description;
            bookToUpdate.Genres = book.Genres;
            bookToUpdate.Image = book.Image;
            bookToUpdate.Link = book.Link;
            bookToUpdate.Publisher = book.Publisher;
            bookToUpdate.PublishedDate = book.PublishedDate;
            bookToUpdate.ISBN = book.ISBN;
            return NoContent();
        }

        //DELETE a book
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var bookToDelete = _booksInfo.Find(b => b.Id == id);
            if (bookToDelete == null)
            {
                return NotFound();
            }
            _booksInfo.Remove(bookToDelete);
            return NoContent();
        }

    }
    [TestClass]
    public class BooksControllerTests
    {
        private readonly List<BookAttributes> _booksInfo = new List<BookAttributes>();
        private readonly BooksController _controller;

        public BooksControllerTests()
        {
            _controller = new BooksController();
        }

        [TestMethod]
        public void Delete_ValidBookId_ReturnsNoContent()
        {
            // Arrange
            var book = new BookAttributes()
            {
                Id = 1,
                Title = "Test Book",
                Author = "Test Author",
                Genre = "Test Genre",
                Description = "Test Description",
                Genres = "Test Genres",
                Image = "Test Image",
                Link = "Test Link",
                Publisher = "Test Publisher",
                PublishedDate = "Test Published Date",
                ISBN = "Test ISBN"
            };
            _booksInfo.Add(book);
            int idToDelete = book.Id;

            // Act
            var result = _controller.Delete(idToDelete);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
            Assert.IsFalse(_booksInfo.Contains(book));
        }

        [TestMethod]
        public void Delete_InvalidBookId_ReturnsNotFound()
        {
            // Arrange
            int idToDelete = 1;

            // Act
            var result = _controller.Delete(idToDelete);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
