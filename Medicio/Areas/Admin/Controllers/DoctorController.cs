using Business.Exceptions;
using Business.Services.Abstracts;
using Core.Models;
using Core.RepositoryAbstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Medicio.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class DoctorController : Controller
    {
        private readonly IDoctorService _doctorService;
        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }
        public IActionResult Index()
        {
            var doctors=_doctorService.GetAllDoctors();
            return View(doctors);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Doctor doctor)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                _doctorService.AddDoctor(doctor);
            }
            catch(FileContentTypeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch(FileSizeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch(FileNullException ex)
            {
                ModelState.AddModelError(ex.Propertyname, ex.Message);
                return View();
            }
            catch(DoctorNullException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                BadRequest(ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            var existdoctor = _doctorService.GetDoctor(x => x.Id == id);
            if (existdoctor == null)
            {
                return NotFound();
            }
            return View(existdoctor);
        }
        [HttpPost]
        public IActionResult DeleteDoctor(int id)
        {
            try
            {
                _doctorService.DeleteDoctor(id);
            }
            catch(EntitynotoundException ex)
            {
                return NotFound();
            }
            catch(Business.Exceptions.FileNotFoundException ex)
            {
                return NotFound();
            }
            catch(Exception ex)
            {
                BadRequest(ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult  Update(int id)
        {
            var doctor = _doctorService.GetDoctor(x => x.Id == id);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }
        [HttpPost]
        public IActionResult Update(Doctor doctor)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                _doctorService.UpdateDoctor(doctor.Id, doctor);
            }
            catch(FileSizeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch(FileContentTypeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch(EntitynotoundException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch(DoctorNullException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();

            }
            catch(Exception ex)
            {
                BadRequest(ex.Message);
            }
            return RedirectToAction(nameof(Index));

        }
    }
}
