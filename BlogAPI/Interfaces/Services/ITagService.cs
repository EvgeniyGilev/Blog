﻿using BlogAPI.DATA.Models;

namespace BlogAPI.Interfaces.Services
{
    public interface ITagService
    {
        Task<IEnumerable<Tag>> ListAsync();

        Task<Tag> GetTagById(int id);

        Task<bool> CreateTag(string name);
    }
}
