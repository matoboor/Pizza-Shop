using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace PizzaShop.Areas.Admin.Models
{
    public class UserViewModel
    {
        public IList<MembershipUser> Employers { get; set; }
        public IList<MembershipUser> Customers { get; set; }

        public UserViewModel()
        {
            Employers = new List<MembershipUser>();
            Customers = new List<MembershipUser>();
        }
    }
}