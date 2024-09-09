using Microsoft.AspNetCore.Mvc;
using Mvc_curso.Domain.Entities;
using Mvc_curso.Infrastructure.Context;

namespace Mvc_curso.Web.Controllers
{
    public class StudentController : Controller
    {
        private readonly AppDbContext _db;
        public StudentController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            //agegar la lista de estudiantes
            var students = _db.Students.ToList();
            return View(students);
        }

        //procedimiento para mostrar la vista de creacion
        public IActionResult Create()
        {
            return View();
        }
        //procedimiento para guardar los datos en la BD
        [HttpPost]
        public IActionResult Store(Student student)
        {
            if (ModelState.IsValid)
            {
                _db.Students.Add(student);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Create");
        }
        //Procedimiento para mostrar la vista de form Edit
        public IActionResult Edit(int id)
        {
            Student? student = _db.Students.SingleOrDefault(s => s.Id == id);
            //Student? student = _db.Students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        //procedimiento para actualzar los datos en la BD
        [HttpPost]
        public IActionResult Update(Student student)
        {
            if (ModelState.IsValid && student.Id>0)
            {
                _db.Students.Update(student);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Edit");
        }

        //procedimiento para eliminar un registro
        public IActionResult Delete(int id)
        {
            Student? student = _db.Students.SingleOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound();
            }
          
            return View(student);
        }

        [HttpPost, ActionName("Destroy")]
        public IActionResult Destroy(Student student)
        {
            if (ModelState.IsValid && student.Id > 0)
            {
                _db.Students.Remove(student);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Delete");
        }
    }
}
