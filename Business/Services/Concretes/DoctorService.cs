using Business.Exceptions;
using Business.Services.Abstracts;
using Core.Models;
using Core.RepositoryAbstracts;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concretes
{
    public class DoctorService : IDoctorService
    { 

        private readonly IDoctorRepository _doctorRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public DoctorService(IDoctorRepository doctorRepository, IWebHostEnvironment webHostEnvironment)
        {
            _doctorRepository = doctorRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public void AddDoctor(Doctor doctor)
        {
            if (!doctor.ImageFile.ContentType.Contains("image/"))
                throw new FileContentTypeException("ImageFile", "File content type errror");
            if (doctor == null) throw new DoctorNullException("", "Doctor null ola bilmez ");
            if (doctor.ImageFile.Length > 2097152) throw new FileSizeException("ImageFile", "Size error");
            string path = _webHostEnvironment.WebRootPath + @"\Upload\Doctor\" + doctor.ImageFile.FileName;
            using(FileStream stream=new FileStream(path, FileMode.Create))
            {
                doctor.ImageFile.CopyTo(stream);
            }
            doctor.ImageUrl = doctor.ImageFile.FileName;
            _doctorRepository.Add(doctor);
            _doctorRepository.Commit();
        }

        public void DeleteDoctor(int id)
        {
            var existdoctor = _doctorRepository.Get(x => x.Id == id);
            if (existdoctor == null) throw new EntitynotoundException("", "world not found ");

            string path = _webHostEnvironment.WebRootPath + @"\Upload\Doctor\" + existdoctor.ImageUrl;
            if (!File.Exists(path)) throw new Business.Exceptions.FileNotFoundException("", "File not found");
            File.Delete(path);
            _doctorRepository.Delete(existdoctor);
            _doctorRepository.Commit();
        }

        public List<Doctor> GetAllDoctors(Func<Doctor, bool>? func = null)
        {
            return _doctorRepository.GetAll(func);
        }

        public Doctor GetDoctor(Func<Doctor, bool>? func = null)
        {
           return _doctorRepository.Get(func);
        }

        public void UpdateDoctor(int id, Doctor doctor)
        {
            var existdoctor= _doctorRepository.Get(x=>x.Id == id);
            if (existdoctor == null) throw new EntitynotoundException("", "entity not found ");
            if(doctor==null) throw new DoctorNullException("", "Doctor null ola bilmez ");
            if (doctor.ImageFile!= null)
            {
                if (!doctor.ImageFile.ContentType.Contains("image/"))
                    throw new FileContentTypeException("ImageFile", "File content type errror");
                if (doctor.ImageFile.Length > 2097152) throw new FileSizeException("ImageFile", "Size error");

                string path = _webHostEnvironment.WebRootPath + @"\Upload\Doctor\" + doctor.ImageFile.FileName;
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    doctor.ImageFile.CopyTo(stream);
                }
                string path1 = _webHostEnvironment.WebRootPath + @"\Upload\Doctor\" + existdoctor.ImageUrl;
                FileInfo fileInfo = new FileInfo(path1);
                fileInfo.Delete();
                existdoctor.ImageUrl = doctor.ImageFile.FileName;
            }
            existdoctor.Name = doctor.Name;
            existdoctor.Description= doctor.Description;
            _doctorRepository.Commit();



        }
    }
}
