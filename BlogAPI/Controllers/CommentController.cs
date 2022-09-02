using BlogAPI.Contracts.Models.Posts;
using BlogAPI.DATA.Models;
using BlogAPI.DATA.Repositories.Interfaces;
using BlogAPI.Handlers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogAPI.Controllers
{
    [ExceptionHandler]
    [ApiController]
    [Route("[controller]")]
    public class CommentController : Controller
    {
        private readonly ICommentRepository _repo;
        private readonly IPostRepository _repoposts;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<CommentController> _logger;

        public CommentController(ICommentRepository repo, IPostRepository repoposts, UserManager<User> userManager, ILogger<CommentController> logger)
        {
            _repo = repo;
            _repoposts = repoposts;
            _userManager = userManager;
            _logger = logger;
        }



        // GET: CommentController/Create
        [HttpPost]
        [Route("Create/{id}")]
        public async Task<IActionResult> Create([FromForm] ShowPostAndCommentModel newComment, [FromRoute] int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                ClaimsPrincipal currentUser = User;
                var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

                var user = await _userManager.FindByIdAsync(currentUserID);
                if (user != null)
                {
                    var post = await _repoposts.GetPostById(id);
                    if (post != null)
                    {

                        Comment comment = new Comment();
                        comment.commentTexte = newComment.Comment;
                        comment.Post = post;
                        comment.User = user;

                        await _repo.CreateComment(comment);
                        return RedirectToAction("GetPost", "Post", new { id = id });
                    }
                    else return RedirectToAction("Error500", "Error");
                }
                else return RedirectToAction("Error500", "Error");
            }
            else
            {
                return RedirectToAction("Error500", "Error");
            }

        }


        // GET: CommentController/Delete/5
        [HttpPost]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id, int PostId)
        {

            var comment = await _repo.GetCommentById(id);
            if (comment == null) { return RedirectToAction(nameof(Index)); }
            else
            {
                await _repo.DelComment(comment);
                return RedirectToAction("GetPost", "Post", new { id = PostId });
            }
        }
    }
}
