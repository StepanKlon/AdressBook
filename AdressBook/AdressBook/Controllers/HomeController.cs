using AdressBook.Core.IConfiguration;
using AdressBook.Models;
using AdressBook.Models.Entities;
using AdressBook.Models.ViewModels;
using AdressBook.Services;
using AutoMapper;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AdressBook.Controllers
{
    public class HomeController : Controller
    {
        private readonly IContactService _contactService;
        private readonly IMapper _mapper;
        private readonly IExportService _exportService;

        public HomeController(IContactService contactService, IMapper mapper, IExportService exportService)
        {
            _contactService = contactService;
            _mapper = mapper;
            _exportService = exportService;
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
            var contactViewModel = _mapper.Map<ContactViewModel>(contact);
            return View("ContactView", contactViewModel);
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
            var contactViewModel = _mapper.Map<ContactViewModel>(contact);
            return View("UpdateView", contactViewModel);
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
        [HttpGet("/contacts/download")]
        public async Task<ActionResult> ExportData()
        {
            var dataTable = await _exportService.getData();
            var fileName = "adresses.xlsx";
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dataTable);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                }
            }
        }
    }
}