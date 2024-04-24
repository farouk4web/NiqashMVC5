//using Niqash.Resources;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Web;

//namespace Niqash.Models
//{
//    public class UniqueNickname : ValidationAttribute
//    {
//        private ApplicationDbContext context = new ApplicationDbContext();

//        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
//        {
//            // here  we are check if nickname are not dublicated
//            var nickName = value.ToString();

//            var user = context.Users.SingleOrDefault(m => m.Nickname == nickName);

//            if (user == null)
//                return ValidationResult.Success;

//            else
//                return new ValidationResult(ModelsResource.userNameNotAvailableMsg);

//        }
//    }
//}