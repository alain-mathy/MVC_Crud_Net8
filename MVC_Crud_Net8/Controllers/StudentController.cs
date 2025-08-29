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
            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var students = await _context.Students.ToListAsync();
            return View(students);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var result = await _context.Students.FindAsync(id);
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Student model)
        {
            var student = await _context.Students.FindAsync(model.Id);
            if (student is not null)
            {
                student.Name = model.Name;
                student.Email = model.Email;
                student.Phone = model.Phone;
                student.Subscribed = model.Subscribed;
                await _context.SaveChangesAsync();
                return RedirectToAction("GetAll");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Student model)
        {
            var student = await _context.Students
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == model.Id);
            if (student is not null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("GetAll");
        }
    }
}
