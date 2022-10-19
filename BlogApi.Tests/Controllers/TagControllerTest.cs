// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using Blog.MockData.Repositories;
using BlogAPI.Controllers;
using BlogAPI.DATA.Repositories;
using NUnit.Framework;

namespace BlogApi.Tests.Controllers
{
    // тут надо реализовать интеграционные тесты

    internal class TagControllerTest
    {
        // Mock CommentRepository
        private readonly TagRepository _repo;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentRepositoryTest"/> class.
        /// </summary>
        public TagControllerTest()
        {
            //_repo = new TagRepository();
        }

        //[TestFixture]
        //public Task Get_ReturnsTags()
        //{
        //    //Arrange

        //    //Act

        //    //Assert
        //    //...
        //}

    }
}
