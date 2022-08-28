using BlogWebApp.BLL.Models.Entities;
using BlogWebApp.BLL.Models.ViewModels.PostViews;
using BlogWebApp.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogWebApp.BLL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentController : Controller
    {
        private readonly ICommentRepository _repo;
        private readonly IPostRepository _repoposts;
        private readonly UserManager<User> _userManager;

        public CommentController(ICommentRepository repo, IPostRepository repoposts, UserManager<User> userManager)
        {
            _repo = repo;
            _repoposts = repoposts;
            _userManager = userManager;
        }



        // GET: CommentController/Create
        [HttpPost]
        [Route("Create/{Id}")]
        public async Task<IActionResult> Create([FromForm] ShowPostAndCommentViewModel newComment, [FromRoute] int id)
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
                    return RedirectToAction("InternalError", "Home");
                }
                return RedirectToAction("InternalError", "Home");
            }
            else
            {
                return RedirectToAction("InternalError", "Home");
            }

        }


        // GET: CommentController/Delete/5
        [HttpPost]
        [Route("Delete/{Id}")]
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
