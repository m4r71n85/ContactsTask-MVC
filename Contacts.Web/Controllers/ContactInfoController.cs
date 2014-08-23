using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Contacts.Models;
using Contacts.Data;
using Contacts.Web.Models;

namespace Contacts.Web.Controllers
{
    public class ContactInfoController : BaseController
    {
        // GET: /ContactInfo/
        public ActionResult Index(int? page, int? sort, int? type)
        {
            if (!String.IsNullOrEmpty(Request["page"])
                && !String.IsNullOrEmpty(Request["sort"])
                && !String.IsNullOrEmpty(Request["type"]))
            {
                return RedirectToAction("Index", new { page = Request["page"], sort = Request["sort"], type=Request["type"] });
            }

            IQueryable<ContactInformation> contactsQuarable = this.Data.ContactInformations.All()
                                                                .Where(x => x.Status != Status.Delete);
            int pageNumber = page.GetValueOrDefault(1);
            int currentSort = sort.GetValueOrDefault(0);
            int currentType = type.GetValueOrDefault(0);
            
            List<SelectListItem> orderType = new List<SelectListItem>();
            orderType.Add(new SelectListItem() { Text = "Ascending", Value = "0", Selected = currentType == 0 });
            orderType.Add(new SelectListItem() { Text = "Descending", Value = "1", Selected = currentType == 1 });

            contactsQuarable = GetSorting(currentSort, currentType, contactsQuarable);

            List<ContactInformation> contacts = contactsQuarable.Skip((pageNumber - 1) * this.PageSize).Take(this.PageSize).ToList();

            ViewBag.OrderType = orderType;
            ViewBag.Pages = Math.Ceiling((double)contactsQuarable.Count() / this.PageSize);
            ViewBag.CurrentPage = pageNumber;
            ViewBag.CurrentSort = currentSort;
            ViewBag.CurrentType = currentType;
            return View(contacts);
        }

        public ActionResult ToggleStatus(int? id)
        {
            ContactInformation contact = this.Data.ContactInformations.GetById(id);
            contact.Status = contact.Status == Status.Active ? Status.Inactive : Status.Active;
            this.Data.SaveChanges();

            return Content(contact.Status.ToString());
        }
        // GET: /ContactInfo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactInformation contactInformation = this.Data.ContactInformations.GetById(id);
            if (contactInformation == null)
            {
                return HttpNotFound();
            }
            return View(contactInformation);
        }

        // GET: /ContactInfo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /ContactInfo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,FirstName,LastName,Sex,Phone,Status")] ContactInformation contactInformation)
        {
            if (ModelState.IsValid)
            {
                this.Data.ContactInformations.Add(contactInformation);
                this.Data.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contactInformation);
        }

        // POST: /ContactInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            ContactInformation contactInformation = this.Data.ContactInformations.GetById(id);
            contactInformation.Status = Status.Delete;
            Data.SaveChanges();
            return Json("ok");
        }

        private IQueryable<ContactInformation> GetSorting(int currentSort, int sortType, IQueryable<ContactInformation> contactsQuarable)
        {
            if (sortType == 0)
            {
                switch (currentSort)
                {
                    case (int)ContactColumns.FirstName:
                        return contactsQuarable.OrderBy(x => x.FirstName);
                    case (int)ContactColumns.LastName:
                        return contactsQuarable.OrderBy(x => x.LastName);
                    case (int)ContactColumns.Sex:
                        return contactsQuarable.OrderBy(x => x.Sex);
                    case (int)ContactColumns.Phone:
                        return contactsQuarable.OrderBy(x => x.Phone);
                    case (int)ContactColumns.Status:
                        return contactsQuarable.OrderBy(x => x.Status);
                    default:
                        return contactsQuarable.OrderBy(x => x.FirstName);
                }
            }
            else
            {
                switch (currentSort)
                {
                    case (int)ContactColumns.FirstName:
                        return contactsQuarable.OrderByDescending(x => x.FirstName);
                    case (int)ContactColumns.LastName:
                        return contactsQuarable.OrderByDescending(x => x.LastName);
                    case (int)ContactColumns.Sex:
                        return contactsQuarable.OrderByDescending(x => x.Sex);
                    case (int)ContactColumns.Phone:
                        return contactsQuarable.OrderByDescending(x => x.Phone);
                    case (int)ContactColumns.Status:
                        return contactsQuarable.OrderByDescending(x => x.Status);
                    default:
                        return contactsQuarable.OrderByDescending(x => x.FirstName);
                }
            }
        }

    }
}
