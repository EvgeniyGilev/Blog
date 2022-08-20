using BlogWebApp.DAL.Entities;
using BlogWebApp.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogWebApp.BLL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentController : Controller
    {
        private readonly ICommentRepository _repo;

        public CommentController(ICommentRepository repo)
        {
            _repo = repo;
        }

        //получить все комментарии
        // GET: CommentController
        [HttpGet]
        [Route("GetComments")]
        public async Task<IActionResult> GetComments()
        {
            var comments = await _repo.GetComments();
            return View(comments);
        }

        //получить комментарий по id
        // GET: CommentController
        [HttpGet]
        [Route("GetCommentById")]
        public async Task<IActionResult> GetCommentById(int id)
        {
            var comment = await _repo.GetCommentById(id);

            return View(comment);
        }

        // GET: CommentController/Create
        [HttpPost]
        public async Task<IActionResult> Create(CommentEntity newComment)
        {
            await _repo.CreateComment(newComment);
            return View(newComment);
        }


        // GET: CommentController/Edit/5
        [HttpPut]
        [Route("Edit")]
        public async Task<IActionResult> Edit(CommentEntity newComment)
        {
            await _repo.EditComment(newComment);
            return View(newComment);
        }


        // GET: CommentController/Delete/5
        [HttpDelete]
        [Route("Delete/{Id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {

            var comment = await _repo.GetCommentById(id);
            if (comment == null) { return RedirectToAction(nameof(Index)); }
            else
            {
                await _repo.DelComment(comment);
                return View(comment);
            }
        }
    }
}
