using Niqash.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Niqash.Dtos
{
    public class LoveDto
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        [Required(ErrorMessageResourceName = "requiredMsg", ErrorMessageResourceType = typeof(ModelsResource))]
        public int PostId { get; set; }

        public UserDto User { get; set; }
    }
}