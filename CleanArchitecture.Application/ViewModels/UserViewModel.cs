using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Application.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }

        public string Email { get; set; }

        public int Rating { get; set; }

        public string ProfileImageUrl { get; set; }

        [Display(Name = "Member Since")]
        public DateTime MemberSince { get; set; }

        public bool IsActive { get; set; }

        [Display(Name = "Role")]
        public string RoleId { get; set; }

        public IEnumerable<SelectListItem> RoleList { get; set; }
    }
}
