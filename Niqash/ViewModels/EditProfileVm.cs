using Niqash.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Niqash.ViewModels
{
    public class EditProfileVm
    {
        public EditProfileModel GenralProfileInfo { get; set; }
        public ChangePasswordViewModel ChangePassword { get; set; }
    }
}