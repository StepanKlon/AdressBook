using AdressBook.Core.IConfiguration;
using AdressBook.Models;
using AdressBook.Models.Entities;
using AdressBook.Models.ViewModels;
using AdressBook.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AdressBook.Controllers
{
    public class HomeController : Controller
    {
        private readonly IContactService _contactService;

        public HomeController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet("/contacts")]
        public async Task<IActionResult> Index()
        {
            var contacts = await _contactService.GetAllContactAsync();
            if (contacts is null)
                return View("Error");
            return View(new IndexViewModel { Contacts = contacts });
        }

        [HttpGet("/contacts/search")]
        public IActionResult Search(IndexViewModel model)
        {
            var search = model.Search;
            if (search is null || search.Count() == 0)
                return RedirectToAction("Index");
            var contacts = _contactService.GetAllContact(search);
            if (contacts is null)
                return View("Error");
            return View("Index",new IndexViewModel { Contacts = contacts });
        }

        [HttpGet("/contacts/{id}")]
        public async Task<IActionResult> GetContact(long id)
        {
            var contact = await _contactService.GetContactAsync(id);
            if (contact is null)
                return View("Error");
            return View("ContactView", new ContactViewModel(contact));
        }

        [HttpPost("/contacts/{id}/delete")]
        public async Task<IActionResult> Delete(long id)
        {
            var isRemoved = await _contactService.RemoveContactAsync(id);
            if (!isRemoved)
                return View("Error");
            return RedirectToAction("Index");
        }

        [HttpGet("/contacts/{id}/edit")]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id is null || id == 0)
                return View("UpdateView", new ContactViewModel());

            var contact = await _contactService.GetContactAsync(id.Value);
            if (contact is null)
                return View("Error");
            return View("UpdateView", new ContactViewModel(contact));
        }

        [HttpPost("/contacts/{id}/edit")]
        public async Task<IActionResult> Edit(ContactViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Error");
            bool result;
            if (model.Id > 0)
            {
                result = await _contactService.UpdateContactAsync(model);
            }
            else
            {
                result = await _contactService.AddContactAsync(model);
            }
            if (!result)
                return View("Error");
            return RedirectToAction("Index");
        }
    }
}