using Microsoft.AspNetCore.Mvc;
using BasicAuthSample.Repository.Models;
using System.Collections.Generic;
using BasicAuthSample.Repository;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;

namespace BasicAuthSample.Controllers
{
    public class AppointmentController : Controller
    {
        [Authorize]
        public IActionResult List()
        {
            List<Appointment> appointments = null;
            Login login = LoginRepository.GetUserByUserName(User.Identity.Name);

            if(login.UserRoles.ToList().Where(ur => ur.Role.Name == "Admin").Count() > 0)
            {
                appointments = AppointmentRepository.GetAppointments();
            }
            else // is a patient
            {
                appointments = AppointmentRepository.GetAppointmentsByPatientId(login.Id);
            }
            // Get Appointments from db
            return View(appointments);
        }

        [HttpGet]
        [Authorize(Roles = "HR")]
        public ActionResult Create()
        {
            ViewBag.Patients = LoginRepository.GetUsers(UserTypeEnum.PATIENT);
            ViewBag.Doctors = LoginRepository.GetUsers(UserTypeEnum.DOCTOR);
            return View();
        }

        [HttpPost]
        public ActionResult Create(Appointment appointment)
        {
            Login login = LoginRepository.GetUserByUserName(User.Identity.Name);
            appointment.CreatedBy = login.Id;
            appointment.CreatedDate = DateTime.Now;
            appointment.ModifiedBy = login.Id;
            appointment.ModifiedDate = DateTime.Now;
            appointment = AppointmentRepository.Create(appointment);
            TempData["key"] = "ravi";
            return RedirectToAction("List");
        }
    }
}
