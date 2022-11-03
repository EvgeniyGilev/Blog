// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using System.Security.Claims;
using BlogAPI.DATA.Models;
using BlogWebApp.BLL.Interfaces.Services;
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
        private readonly ICommentService _commentService;
        private readonly IPostService _postService;
        private readonly UserManager<User> _userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentController"/> class.
        /// </summary>
        /// <param name="commentService">The comment Service.</param>
        /// <param name="postService">The posts.</param>
        /// <param name="userManager">The user manager.</param>
        public CommentController(ICommentService commentService, IPostService postService, UserManager<User> userManager)
        {
            _commentService = commentService;
            _postService = postService;
            _userManager = userManager;
        }

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
            if (User.Identity is { IsAuthenticated: true })
            {
                ClaimsPrincipal currentUser = User;
                var currentUserId = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var user = await _userManager.FindByIdAsync(currentUserId);
                if (user != null)
                {
                    var post = await _postService.GetPostById(id);
                    {
                        Comment comment = new Comment();
                        comment.CommentTexte = newComment.Comment;
                        comment.Post = post;
                        comment.User = user;

                        await _commentService.CreateComment(comment);
                        return RedirectToAction("GetPost", "Post", new {id });
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

        /// <summary>
        /// Deletes the.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="postId">The post id.</param>
        /// <returns>A Task.</returns>
        [HttpPost]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id, int postId)
        {
            var comment = await _commentService.GetCommentById(id);
            await _commentService.DeleteComment(comment);
            return RedirectToAction("GetPost", "Post", new { id = postId });
        }
    }
}
