using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Crud_Net8.Data;
using MVC_Crud_Net8.Models.Entities;
using MVC_Crud_Net8.Models.ViewModel;

namespace MVC_Crud_Net8.Controllers
{
    public class StudentController : Controller
    {
        private readonly AppDbContext _context;

        public StudentController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel model)
        {
            var student = new Student
            {
                Name = model.Name,
                Email = model.Email,
                Phone = model.Phone,
                Subscribed = model.Subscribed
            };

            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var students = await _context.Students.ToListAsync();
            return View(students);
        }
    }
}
