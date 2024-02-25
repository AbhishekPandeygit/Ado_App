using ado_app.Data;
using ado_app.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace ado_app.Controllers
{
    public class CustomerController : Controller
    {
        CustomerService service = new CustomerService();

        public Microsoft.AspNetCore.Hosting.IHostingEnvironment Environment { get; }

        public CustomerController(Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            Environment = environment;
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCustomer(Customer customer)
        {
            var result = service.CreateCustomer(customer);
            if (result == Models.Action.Success)
            {
                return Json(new { ok = true, message = "customer created successfully!" });

            }
            else if (result == Models.Action.EmailExist)
            {
                return Json(new { ok = false, message = "customer email already registered!" });
            }
            else
            {
                return Json(new { ok = false, message = "something went wrong!" });

            }
        }


        [HttpPost]
        public IActionResult UpdateCustomer(Customer customer)
        {
            var result = service.UpdateCustomer(customer);
            if (result == Models.Action.Success)
            {
                return Json(new { ok = true, message = "customer updated successfully!" });

            }
            else if (result == Models.Action.EmailExist)
            {
                return Json(new { ok = false, message = "customer nnot exist!" });
            }
            else
            {
                return Json(new { ok = false, message = "something went wrong!" });

            }
        }


        public IActionResult DeleteCustomer(int id)
        {
            var result = service.DeleteCustomer(id);
            if (result)
            {
                return Json(new { ok = true, message = "customer deleted successfully!" });

            }
            else
            {
                return Json(new { ok = false, message = "customer not deleted successfully!" });

            }
        }

        public IActionResult GetCustomers(Customer customer)
        {
            return Json(service.GetCustomers(customer));
        }
         
        public IActionResult GetCustomer(int id)
        {
            return Json(service.GetCustomer(id));
        }

        public IActionResult GetCountry()
        {
            return Json(service.GetCountryList());
        }

        public IActionResult GetState(int id)
        {
            return Json(service.GetStateList(id));
        }

        public IActionResult GetCity(int id)
        {
            return Json(service.GetCityList(id));
        }

        [HttpPost]
        public IActionResult UpdateProfileImage()
        {
            var id = Request.Form["id"];
            var file = Request.Form.Files[0];
            var rootpath = Environment.WebRootPath;
            var fullPath = Path.Combine(rootpath, "Images", file.FileName);
            FileStream stream = new FileStream(fullPath, FileMode.Create);
            file.CopyTo(stream);

            //   var result = service.UpdateprofileImage(Convert.ToInt32(id), "Images/" + file.FileName);
            service.UpdateprofileImage(Convert.ToInt32(id), "Images/" + file.FileName);

            //if (result == true)
            //{

            //    return Json(new { ok = true, message = "image uploaded successfully !" });
            //}
            //else
            //{
            //    return Json(new { ok = false, message = "upload failed!" });
            //}

            return Json(new { ok = true, message = "image uploaded successfully !" });
        }


    }
}
