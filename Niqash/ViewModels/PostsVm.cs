using Niqash.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Niqash.ViewModels
{
    public class PostsVm
    {
        public Post NewPost { get; set; }

        public Comment NewComment { get; set; }

        public IEnumerable<Post> Posts { get; set; }
    }
}