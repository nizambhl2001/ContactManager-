using AspNetCoreMvcJwtApp.Data;
using AspNetCoreMvcJwtApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreMvcJwtApp.Controllers
{
   
    public class PersonController : Controller
    {
        private readonly AppDbContext _context;

        public PersonController(AppDbContext context)
        {
            _context = context;
        }

      
        public IActionResult Index()
        {
            var people = _context.People
                .Include(p => p.Phones)
                .Include(p => p.Addresses)
                .ToList();
            return View(people);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Person person, List<string> phoneNumbers, List<string> addressLocations)
        {
            person.Phones = phoneNumbers.Select(p => new Phone { Number = p }).ToList();
            person.Addresses = addressLocations.Select(a => new Address { Location = a }).ToList();

            _context.People.Add(person);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var person = _context.People
                .Include(p => p.Phones)
                .Include(p => p.Addresses)
                .FirstOrDefault(p => p.Id == id);
            return View(person);
        }

        [HttpPost]
        public IActionResult Edit(Person updatedPerson, List<string> phoneNumbers, List<string> addressLocations)
        {
            var person = _context.People
                .Include(p => p.Phones)
                .Include(p => p.Addresses)
                .FirstOrDefault(p => p.Id == updatedPerson.Id);

            if (person == null) return NotFound();

            person.Name = updatedPerson.Name;
            person.Phones.Clear();
            person.Addresses.Clear();

            person.Phones = phoneNumbers.Select(p => new Phone { Number = p }).ToList();
            person.Addresses = addressLocations.Select(a => new Address { Location = a }).ToList();

            _context.Update(person);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
