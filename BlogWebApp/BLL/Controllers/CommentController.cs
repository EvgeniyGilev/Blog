using System.Security.Claims;
using BlogAPI.DATA.Models;
using BlogAPI.DATA.Repositories.Interfaces;
using BlogWebApp.BLL.Models.ViewModels.PostViews;
using BlogWebApp.Handlers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogWebApp.BLL.Controllers
{
    /// <summary>
    /// The comment controller.
    /// </summary>
    [ExceptionHandler]
    [ApiController]
    [Route("[controller]")]
    public class CommentController : Controller
    {
        private readonly ICommentRepository _repo;
        private readonly IPostRepository _repoposts;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<CommentController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentController"/> class.
        /// </summary>
        /// <param name="repo">The repo.</param>
        /// <param name="repoposts">The repoposts.</param>
        /// <param name="userManager">The user manager.</param>
        /// <param name="logger">The logger.</param>
        public CommentController(ICommentRepository repo, IPostRepository repoposts, UserManager<User> userManager, ILogger<CommentController> logger)
        {
            _repo = repo;
            _repoposts = repoposts;
            _userManager = userManager;
            _logger = logger;
        }

        // GET: CommentController/Create
        /// <summary>
        /// Creates the.
        /// </summary>
        /// <param name="newComment">The new comment.</param>
        /// <param name="id">The id.</param>
        /// <returns>A Task.</returns>
        [HttpPost]
        [Route("Create/{id}")]
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
                    else
                    {
                        return RedirectToAction("Error500", "Error");
                    }
                }
                else
                {
                    return RedirectToAction("Error500", "Error");
                }
            }
            else
            {
                return RedirectToAction("Error500", "Error");
            }
        }

        // GET: CommentController/Delete/5
        /// <summary>
        /// Deletes the.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="PostId">The post id.</param>
        /// <returns>A Task.</returns>
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
