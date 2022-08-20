using AutoMapper;
using BlogWebApp.BLL.Models.Entities;
using BlogWebApp.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogWebApp.BLL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostController : Controller
    {

        private readonly IPostRepository _repo;

        public PostController(IPostRepository repo)
        {
            _repo = repo;
        }

        //получить все статьи
        // GET: PostController
        [HttpGet]
        [Route("GetPosts")]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _repo.GetPosts();
            return View(posts);
        }

        //получить все статьи одного пользователя
        // GET: PostController
        [HttpGet]
        [Route("GetPostsByUserId")]
        public async Task<IActionResult> GetPostsByUserId(int id)
        {
            var posts = await _repo.GetPostsByUserId(id);

            return View(posts);
        }

        // GET: PostController/Create
        [HttpPost]
        public async Task<IActionResult> Create(Post newPost)
        {
            await _repo.CreatePost(newPost);
            return View(newPost);
        }


        // Put: PostController/Edit/5
        [HttpPut]
        [Route("Edit")]
        public async Task<IActionResult> Edit(Post newPost)
        {
            await _repo.EditPost(newPost);
            return View(newPost);
        }


        // GET: PostController/Delete/5
        [HttpDelete]
        [Route("Delete/{Id}")]
        public async Task<IActionResult> DeletePost([FromRoute] int id)
        {

            var post = await _repo.GetPostById(id);
            if (post == null) { return RedirectToAction(nameof(Index)); }
            else
            {
                await _repo.DelPost(post);
                return View(post);
            }
        }

    }
}
