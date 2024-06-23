﻿using PersonalBloggingPlatform.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBloggingPlatform.Core.Domain.RepositoryContracts
{
    public interface IBlogRepository
    {
        /// <summary>
        /// Takes a blog object and adds it to the database.
        /// </summary>
        /// <param name="blog">Blog object to add.</param>
        /// <returns>Returns the blog object after addding it to the database blogs table.</returns>
        public Task<Blog> AddBlog(Blog blog);
    }
}