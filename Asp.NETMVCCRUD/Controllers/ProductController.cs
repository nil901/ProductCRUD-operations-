using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Asp.NETMVCCRUD.Models;
using System.Data.Entity;

namespace Asp.NETMVCCRUD.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /Product/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData()
        {
            using (DBModels db = new DBModels())
            {
                List<Productlist> empList = db.Employees.ToList<Produclist>();
                return Json(new { data = empList }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new Productlist());
            else
            {
                using (DBModels db = new DBModels())
                {
                    return View(db.Product.Where(x => x.ProductID==id).FirstOrDefault<Product>());
                }
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(Productlist emp)
        {
            using (DBModel db = new DBModel())
            {
                if (emp.ProductID == 0)
                {
                    db.Product.Add(emp);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
                }
                else {
                    db.Entry(emp).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
                }
            }


        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (DBModel db = new DBModel())
            {
                Product emp = db.Employees.Where(x => x.ProductID == id).FirstOrDefault<Product>();
                db.Employees.Remove(emp);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}