using Niqash.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Niqash.ViewModels
{
    public class UserAccountVm
    {
        public Post NewPost { get; set; }

        public Comment NewComment { get; set; }

        public string UserId { get; set; }
    }
}