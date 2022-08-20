﻿using BlogWebApp.DAL.Entities;
using BlogWebApp.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogWebApp.BLL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TagController : Controller
    {

        private readonly ITagRepository _repo;

        public TagController(ITagRepository repo)
        {
            _repo = repo;
        }

        //получить все комментарии
        // GET: TagController
        [HttpGet]
        [Route("GetTags")]
        public async Task<IActionResult> GetTagss()
        {
            var tags = await _repo.GetTags();
            return View(tags);
        }


        //получить комментарий по id
        // GET: TagController
        [HttpGet]
        [Route("GetTagById")]
        public async Task<IActionResult> GetTagById(int id)
        {
            var tag = await _repo.GetTagById(id);

            return View(tag);
        }

        // GET: TagController/Create
        [HttpPost]
        public async Task<IActionResult> Create(TagEntity newTag)
        {
            await _repo.CreateTag(newTag);
            return View(newTag);
        }

        // GET: TagController/Edit
        [HttpPut]
        [Route("Edit")]
        public async Task<IActionResult> Edit(TagEntity newTag)
        {
            await _repo.EditTag(newTag);
            return View(newTag);
        }

        // GET: TagController/Delete/5
        [HttpDelete]
        [Route("Delete/{Id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {

            var tag = await _repo.GetTagById(id);
            if (tag == null) { return RedirectToAction(nameof(Index)); }
            else
            {
                await _repo.DelTag(tag);
                return View(tag);
            }
        }
    }
}
