using Niqash.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Niqash.Dtos
{
    public class CommentDto
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceName = "requiredMsg", ErrorMessageResourceType = typeof(ModelsResource))]
        [MinLength(1, ErrorMessageResourceName = "min1", ErrorMessageResourceType = typeof(ModelsResource))]
        [MaxLength(500, ErrorMessageResourceName = "max500", ErrorMessageResourceType = typeof(ModelsResource))]
        public string Content { get; set; }

        [Display(Name = "publishDate", ResourceType = typeof(ModelsResource))]
        public DateTime? PublishDate { get; set; }

        public int PostId { get; set; }

        public string UserId { get; set; }

        public UserDto User { get; set; }
    }
}