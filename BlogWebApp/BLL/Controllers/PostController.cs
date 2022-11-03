// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using BlogAPI.DATA.Models;
using BlogWebApp.BLL.Interfaces.Services;
using BlogWebApp.BLL.Models.ViewModels.PostViews;
using BlogWebApp.Handlers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogWebApp.BLL.Controllers
{
    /// <summary>
    /// The post controller.
    /// </summary>
    [ExceptionHandler]
    [ApiController]
    [Route("[controller]")]
    public class PostController : Controller
    {

        private readonly IPostService _postService;
        private readonly ITagService _tagService;
        private readonly ILogger<PostController> _logger;
        private readonly UserManager<User> _userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="PostController"/> class.
        /// </summary>
        /// <param name="tagService"> tags. </param>
        /// <param name="userManager">The user manager.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="postService"> posts. </param>
        public PostController(IPostService postService, ITagService tagService, UserManager<User> userManager, ILogger<PostController> logger)
        {
            _postService = postService;
            _tagService = tagService;
            _userManager = userManager;
            _logger = logger;
        }

        /// <summary>
        /// получить статью по ID.
        /// </summary>
        /// <param name="id">id статьи.</param>
        /// <returns>An IActionResult.</returns>
        // GET: PostController
        [HttpGet]
        [Route("GetPost/{id}")]
        public async Task<IActionResult> GetPost([FromRoute] int id)
        {
            var post = await _postService.GetPostById(id);
            {
                ShowPostAndCommentViewModel model = new ShowPostAndCommentViewModel { ShowPost = post, PostId=id };
                _logger.LogInformation("Получаем статью с ID: " + id.ToString());

                return View(model);
            }
        }

        /// <summary>
        /// получить все статьи.
        /// </summary>
        /// <returns>An IActionResult.</returns>
        // GET: PostController
        [HttpGet]
        [Route("GetPosts")]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _postService.ListAsync();
            ShowPostsViewModel model = new ShowPostsViewModel
            {
                ShowPosts = posts.ToList(),
            };
            _logger.LogInformation("Показываем все статьи ");
            return View(model);
        }

        /// <summary>
        /// Creates the.
        /// </summary>
        /// <returns>A Task.</returns>
        [HttpGet]
        [Route("Create")]
        public async Task<IActionResult> Create()
        {
            var postTags = await _tagService.ListAsync();
            CreatePostViewModel model = new CreatePostViewModel
            {
                PostTags = postTags.ToList(),
            };
            _logger.LogInformation("открываем страницу создания новой статьи");
            return View(model);
        }

        /// <summary>
        /// Creates the.
        /// </summary>
        /// <param name="newPost">The new post.</param>
        /// <param name="postTags">The post tags.</param>
        /// <returns>A Task.</returns>
        /// GET: PostController/Create
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromForm] CreatePostViewModel newPost, [FromForm] List<Tag> postTags)
        {
            var searchuser = _userManager.Users.FirstOrDefault(u => u.Email == newPost.PostAuthorEmail);
            if (searchuser != null)
            {
                List<Tag> posttags = new List<Tag>();

                foreach (var tag in postTags)
                {
                    Tag newtag = await _tagService.GetTagById(tag.Id);
                    posttags.Add(newtag);
                }

                Post post = new Post
                {
                    PostName = newPost.PostName,
                    PostText = newPost.PostText,
                    User = searchuser,
                    Tags = posttags,
                };

                await _postService.CreatePost(post);
                _logger.LogInformation("новая статья добавлена: " + post.PostName);
            }

            return RedirectToAction("GetPosts");
        }

        /// <summary>
        /// Edits the.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>A Task.</returns>
        [HttpGet]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id)
        {
            var post = await _postService.GetPostById(id);

            // редактировать статью может только автор или администратор
            if (User.Identity?.Name == post.User?.UserName || User.IsInRole("Администратор"))
            {
                var postTags = await _tagService.ListAsync();

                if (post.PostName != null && post.PostText != null)
                {
                    var model = new EditPostViewModel
                    {
                        PostName = post.PostName,
                        PostText = post.PostText,
                        PostTagsCurrent = post.Tags,
                        PostTagsAll = postTags.ToList(),
                        PostId = id,
                    };

                    _logger.LogInformation("Статья отредактирована: " + post.PostName);
                    return View(model);
                }
                else
                {
                    return Redirect("~/Error/Error404");
                }
            }
            else
            {
                return Redirect("~/Error/Error403");
            }
        }

        /// <summary>
        /// Edits the.
        /// </summary>
        /// <param name="newPost">The new post.</param>
        /// <param name="postTags">The post tags.</param>
        /// <param name="id">The id.</param>
        /// <returns>A Task.</returns>
        [HttpPost]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit([FromForm] EditPostViewModel newPost, [FromForm] List<string> postTags, [FromRoute] int id)
        {
            List<Tag> newposttags = new List<Tag>();

            foreach (var tag in postTags)
            {
                Tag newtag = await _tagService.GetTagByName(tag);
                newposttags.Add(newtag);
            }

            var post = await _postService.GetPostById(id);

            post.PostName = newPost.PostName;
            post.PostText = newPost.PostText;
            post.Tags = newposttags;

            await _postService.EditPost(id, post);
            _logger.LogInformation("Статья отредактирована: " + post.PostName);

            return RedirectToAction("GetPosts");
        }

        // GET: PostController/Delete/5
        /// <summary>
        /// Deletes the.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>A Task.</returns>
        [HttpPost]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var post = await _postService.GetPostById(id);

            // удалить статью может только автор или администратор
            if ((User.Identity != null && post.User != null && (User.Identity.Name == post.User.UserName)) || User.IsInRole("Администратор"))
            {
                    await _postService.DeletePost(id);
                    _logger.LogInformation("Статья удалена: " + post.PostName);
                    return RedirectToAction("GetPosts");
            }
            else
            {
                return Redirect("~/Error/Error403");
            }
        }
    }
}
