using Niqash.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Niqash.Dtos
{
    public class UserDto
    {
        public string Id { get; set; }

        [Required(ErrorMessageResourceName = "requiredMsg", ErrorMessageResourceType = typeof(ModelsResource))]
        [MinLength(2, ErrorMessageResourceName = "min2", ErrorMessageResourceType = typeof(ModelsResource))]
        [MaxLength(25, ErrorMessageResourceName = "max25", ErrorMessageResourceType = typeof(ModelsResource))]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceName = "requiredMsg", ErrorMessageResourceType = typeof(ModelsResource))]
        [MinLength(2, ErrorMessageResourceName = "min2", ErrorMessageResourceType = typeof(ModelsResource))]
        [MaxLength(25, ErrorMessageResourceName = "max25", ErrorMessageResourceType = typeof(ModelsResource))]
        public string LastName { get; set; }

        [MinLength(2, ErrorMessageResourceName = "min2", ErrorMessageResourceType = typeof(ModelsResource))]
        [MaxLength(500, ErrorMessageResourceName = "max500", ErrorMessageResourceType = typeof(ModelsResource))]
        public string AboutMe { get; set; }

        public string ProfilePicSrc { get; set; }

        //public IEnumerable<PostDto> Posts { get; set; }

        //public IEnumerable<CommentDto> Comments { get; set; }

        //public IEnumerable<LikeDto> Likes { get; set; }

        //public IEnumerable<LoveDto> Loves { get; set; }

    }
}