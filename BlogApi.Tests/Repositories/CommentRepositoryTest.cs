﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using NUnit.Framework;
using Blog.MockData.Repositories;

namespace BlogApi.Tests.Repositories
{
    /// <summary>
    /// The comment repository test.
    /// </summary>
    [TestFixture]
    public class CommentRepositoryTest
    {
        // Mock CommentRepository
        private readonly CommentRepository _repo;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentRepositoryTest"/> class.
        /// </summary>
        public CommentRepositoryTest()
        {
            _repo = new CommentRepository();
        }

        /// <summary>
        /// GetCommentById возвращает корректное значение комментария.
        /// </summary>
        [Test]
        public void GetCommentByIdShouldReturnComment()
        {
            // Arrange
            int id = 1;

            // Act
            var comment = _repo.GetCommentById(id);

            // Assert
            Assert.That(comment.Result.CommentTexte == "Комментарий");
        }
    }
}
