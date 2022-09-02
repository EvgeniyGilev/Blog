using BlogWebApp.BLL.Models.Entities;
using BlogWebApp.BLL.Models.ViewModels.PostViews;
using BlogWebApp.DAL.Repositories.Interfaces;
using BlogWebApp.Handlers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogWebApp.BLL.Controllers
{
    [ExceptionHandler]
    [ApiController]
    [Route("[controller]")]
    public class PostController : Controller
    {

        private readonly IPostRepository _repo;
        private readonly ITagRepository _repotags;
        private readonly ILogger<PostController> _logger;
        private readonly UserManager<User> _userManager;

        public PostController(IPostRepository repo, ITagRepository repotags, UserManager<User> userManager, ILogger<PostController> logger)
        {
            _repo = repo;
            _repotags = repotags;
            _userManager = userManager;
            _logger = logger;
        }

        /// <summary>
        /// получить статью по ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: PostController
        [HttpGet]
        [Route("GetPost/{id}")]
        public async Task<IActionResult> GetPost([FromRoute] int id)
        {
         
            var post = await _repo.GetPostById(id);
            if (post != null)
            {
                
                ShowPostAndCommentViewModel model = new ShowPostAndCommentViewModel { ShowPost = post, PostId=id };
                _logger.LogInformation("Получаем статью с ID: "+ id.ToString());
                

                return View(model);
            }
            _logger.LogInformation("По текущему ID не смогли получить статью" + id.ToString() + "возвращаемся на страницу всех статей.");
            return RedirectToAction("GetPosts");
        }

        /// <summary>
        /// получить все статьи
        /// </summary>
        /// <returns></returns>
        // GET: PostController
        [HttpGet]
        [Route("GetPosts")]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _repo.GetPosts();
            ShowPostsViewModel model = new ShowPostsViewModel
            {
                ShowPosts = posts
            };
            _logger.LogInformation("Показываем все статьи ");
            return View(model);
        }

        /// <summary>
        /// получить все статьи одного пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: PostController
        [HttpGet]
        [Route("GetPostsByUserId")]
        public async Task<IActionResult> GetPostsByUserId(string id)
        {
            var posts = await _repo.GetPostsByUserId(id);
            _logger.LogInformation("Получить все статьи пользователя с id: " + id);
            return View(posts);
        }

        [HttpGet]
        [Route("Create")]
        public async Task<IActionResult> Create()
        {
            CreatePostViewModel model = new CreatePostViewModel
            {
                PostTags = await _repotags.GetTags()
            };
            _logger.LogInformation("открываем страницу создания новой статьи");
            return View(model);
        }
        // GET: PostController/Create
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromForm] CreatePostViewModel newPost, [FromForm] List<string> postTags)
        {
            var searchuser = _userManager.Users.FirstOrDefault(u => u.Email == newPost.PostAuthorEmail);
            if (searchuser != null)
            {
                List<Tag> _posttags = new List<Tag>();

                foreach (var tag in postTags)
                {
                    Tag newtag = await _repotags.GetTagByName(tag);
                    if (newtag != null)
                        _posttags.Add(newtag);
                }

                Post post = new Post
                {
                    postName = newPost.PostName,
                    postText = newPost.PostText,
                    User = searchuser,
                    Tags = _posttags
                };
                
                await _repo.CreatePost(post);
                _logger.LogInformation("новая статья добавлена: " + post.postName);
            }

            return RedirectToAction("GetPosts");
        }


        [HttpGet]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id)
        {
            var post = await _repo.GetPostById(id);

            //редактировать статью может только автор или администратор
            if ((User.Identity.Name == post.User.UserName) || User.IsInRole("Администратор"))
            {

                EditPostViewModel model = new EditPostViewModel
                {
                    PostName = post.postName,
                    PostText = post.postText,
                    PostTagsCurrent = post.Tags,
                    PostTagsAll = await _repotags.GetTags(),
                    PostId = id
                };
                _logger.LogInformation("Статья отредактирована: " + post.postName);
                return View(model);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home");
            }
        }
        // Put: PostController/Edit/5
        [HttpPost]
        [Route("Edit/{id})]
        public async Task<IActionResult> Edit([FromForm] EditPostViewModel newPost, [FromForm] List<string> postTags, [FromRoute] int id)
        {

            List<Tag> _posttags = new List<Tag>();

            foreach (var tag in postTags)
            {
                Tag newtag = await _repotags.GetTagByName(tag);
                if (newtag != null)
                    _posttags.Add(newtag);
            }

            var post = await _repo.GetPostById(id);

            post.postName = newPost.PostName;
            post.postText = newPost.PostText;
            post.Tags = _posttags;

            
            await _repo.EditPost(post, id);
            _logger.LogInformation("Статья отредактирована: " + post.postName);

            return RedirectToAction("GetPosts");

        }


        // GET: PostController/Delete/5
        [HttpPost]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {

            var post = await _repo.GetPostById(id);
            //удалить статью может только автор или администратор
            if ((User.Identity.Name == post.User.UserName) || User.IsInRole("Администратор"))
            {
                if (post == null) { return RedirectToAction(nameof(Index)); }
                else
                {
                    await _repo.DelPost(post);
                    _logger.LogInformation("Статья удалена: " + post.postName);
                    return RedirectToAction("GetPosts");
                }
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home");
            }

        }

    }
}
