using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BooksServices;
using BooksModel;

namespace BooksStore.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BooksController : ControllerBase
    {
        private readonly BookServices _booksServices;

        public BooksController(BookServices booksServices)
        {
            _booksServices = booksServices;
        }

        [HttpGet]
        public ActionResult<List<Book>> getAllBook(){
            return _booksServices.GetAll();
        }

        [HttpGet]
        public Object getBookByPage([FromQuery(Name = "page")] int querryPage,[FromQuery(Name = "category")] string category){
        return _booksServices.pagination(querryPage,category);   
        }

        [HttpGet("{id}", Name = ("GetBook"))]
        [Route("{id}")]
        public ActionResult<Book> getBook(string id){
            var book = _booksServices.Get(id);
            if (book == null)
            {
                return StatusCode(400,"Khong tim thay sach");
            }
            return book;
        }
        [HttpGet]
        public ActionResult<Book> getLatestBooks(){
         var latestBooks = _booksServices.GetLatestBooks();
         try
         {
             if(latestBooks == null){
                 return StatusCode(400,"Khong tim thay nhung quyen sach moi nhat");
             }
             return StatusCode(200,latestBooks);

         }
         catch (System.Exception e)
         {
             return StatusCode(500,e.Message);
         }
        }
        [HttpGet]
        public ActionResult<Book> getHighLightBooks(){
         var highLightBooks = _booksServices.GetHighLightBook();
         try
         {
             if(highLightBooks == null){
                 return StatusCode(400,"Khong tim thay nhung quyen sach noi bat nhat");
             }
             return StatusCode(200,highLightBooks);

         }
         catch (System.Exception e)
         {
             return StatusCode(500,e.Message);
         }
        }
        [HttpPost]
        public ActionResult<Book> addNewBook(Book book)
        {
            _booksServices.Add(book);

            return CreatedAtRoute("GetBook", new { id = book.Id.ToString() }, book);
        }
        [HttpPut("{id}")]
        public IActionResult updateBook(string id, Book bookIn)
        {
            var book = _booksServices.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            _booksServices.Update(id, bookIn);

            return NoContent();
        }
         [HttpDelete("{id}")]
        public IActionResult deleteBook(string id)
        {
            var book = _booksServices.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            _booksServices.Remove(book.Id);

            return NoContent();
        }

    }
}
