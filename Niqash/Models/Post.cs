using Niqash.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Niqash.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Display(Name = "content", ResourceType =typeof(ModelsResource))]
        [Required(ErrorMessageResourceName = "requiredMsg", ErrorMessageResourceType = typeof(ModelsResource))]
        [MinLength(1, ErrorMessageResourceName = "min1", ErrorMessageResourceType = typeof(ModelsResource))]
        [MaxLength(500, ErrorMessageResourceName = "max500", ErrorMessageResourceType = typeof(ModelsResource))]
        public string Content { get; set; }

        [Display(Name = "publishDate", ResourceType = typeof(ModelsResource))]
        public DateTime? PublishDate { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Like> Likes { get; set; }

        public virtual ICollection<Love> Loves { get; set; }
    }
}