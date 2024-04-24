using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Niqash.Models
{
    public class Follow
    {
        public int Id { get; set; }

        public string AccountId { get; set; }

        public virtual ApplicationUser Account { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}