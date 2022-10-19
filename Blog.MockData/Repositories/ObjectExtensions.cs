// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.MockData.Repositories
{
    /// <summary>
    /// The object extensions.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// As the list task.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A Task.</returns>
        public static Task<List<T>> AsListTask<T>(this T value)
            => new List<T> { value }.AsTask();

        /// <summary>
        /// As the task.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A Task.</returns>
        public static Task<T> AsTask<T>(this T value)
            => Task.FromResult(value);
    }
}