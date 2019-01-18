using Microsoft.Owin;
using Owin;
using HopesPartners_alpha.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;


[assembly: OwinStartupAttribute(typeof(HopesPartners_alpha.Startup))]
namespace HopesPartners_alpha
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRoles(); 
        }

        public void CreateRoles()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if(!roleManager.RoleExists("SuperAdmin"))
            {
                //Create SuperAdmin Role
                var role = new IdentityRole();
                role.Name = "SuperAdmin";
                roleManager.Create(role);

                //Createing Default SuperAdmin UserAccount
                var user = new ApplicationUser
                {
                    UserName = "SuperAdmin",
                    Email = "SuperAdmin@su.com"
                };
                // adding user to the userManager (making a live user account)
                var UserCheck = userManager.Create(user, "Password1");

                // adding user into the SuperAdmin Role
                if (UserCheck.Succeeded) 
                {
                    var check = userManager.AddToRole(user.Id, "SuperAdmin");
                }
            }


            // List of desired users to be added to the Applications Roles
            List<string> roleList = new List<string>() { "Admin", "Content-Manager", "Account-Manager", "Partner" };
            /* Role Names and Descriptions
             * SuperAdmin: The Highest Admin role with no restrictions
             * 
             * Admin: Ability to controll(add/Delete) Manager Accounts + manager roles
             * 
             * Content-Manager:
             * 
             * Account-Manager:
             * 
             * Partner:  
             *
             */

            foreach (string roleName in roleList)
            {
                // checking of role curently exists
                if (!roleManager.RoleExists(roleName))
                {
                    var newRole = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                    newRole.Name = roleName;
                    roleManager.Create(newRole);
                }
            }
        }
    }
}
