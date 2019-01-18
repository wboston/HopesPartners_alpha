using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HopesPartners_alpha.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HopesPartners_alpha.Controllers
{
    public class UserModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UserModels
        public ActionResult Index()
        {
            return View(db.ApplicationUsers.ToList());
        }

        // GET: UserModels/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserModels userModels = db.ApplicationUsers.Find(id);
            if (userModels == null)
            {
                return HttpNotFound();
            }
            return View(userModels);
        }

        // GET: UserModels/Create
        [Authorize(Roles = "SuperAdmin, Admin, Account-Manager")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FamilyName,Email,Role")] UserModels userModels)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (ModelState.IsValid)
            {
                userModels.UserName = userModels.Email;
                userModels.EmailConfirmed = true;
                userModels.DateTime = DateTime.Now.ToLongDateString();

                var UsrCheck = userManager.Create(userModels, "Password1");
                if(UsrCheck.Succeeded)
                {
                    var check = userManager.AddToRole(userModels.Id, userModels.Role);
                }

                userManager.UpdateAsync(userModels);
                context.SaveChangesAsync();

                //db.Users.Add(userModels);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userModels);
        }

        // GET: UserModels/Edit/5
        [Authorize(Roles = "SuperAdmin, Admin, Account-Manager")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserModels userModels = db.ApplicationUsers.Find(id);
            if (userModels == null)
            {
                return HttpNotFound();
            }
            return View(userModels);
        }

        // POST: UserModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FamilyName,Address,City,State,ZipCode,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,DateTime,Role")] UserModels userModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userModels);
        }

        // GET: UserModels/Delete/5
        [Authorize(Roles = "SuperAdmin, Admin, Account-Manager")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserModels userModels = db.ApplicationUsers.Find(id);
            if (userModels == null)
            {
                return HttpNotFound();
            }
            return View(userModels);
        }

        // POST: UserModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            UserModels userModels = db.ApplicationUsers.Find(id);
            db.Users.Remove(userModels);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
