using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC_Project.Models;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data.SqlClient;

namespace MVC_Project.Controllers
{
    public class EmployeesController : Controller
    {
        [Authorize]
        // GET: Employees
        public ActionResult Index()
        {
            using (EmployeeEntities db = new EmployeeEntities())
            {


                return View(db.tblEmployees.ToList());
            }
        }

        // GET: Teachers/Details/5
        public ActionResult Details(int? id)
        {

            using (EmployeeEntities db = new EmployeeEntities())
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                tblEmployee emp = db.tblEmployees.Find(id);
                if (emp == null)
                {
                    return HttpNotFound();
                }
                return View(emp);
            }
            
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            using (EmployeeEntities db=new EmployeeEntities())
            {
                
                


                return View();
            }
        }

        // POST: Employees/Create
        [HttpPost]
        public ActionResult Create(tblEmployee emp,HttpPostedFileBase FileUpload)
        {
            using (EmployeeEntities db = new EmployeeEntities())
            {
                if (ModelState.IsValid)
                {
                    //Save image in folder

                    string FileName = Path.GetFileName(FileUpload.FileName);
                    string SaveLocation = Server.MapPath("~/Upload/" + FileName);
                    FileUpload.SaveAs(SaveLocation);

                    //save image name in database

                    emp.Picture = "~/Upload/" + FileName;


                    // byte image Save


                    emp.EmployeeImage = new byte[FileUpload.ContentLength];
                    FileUpload.InputStream.Read(emp.EmployeeImage, 0, FileUpload.ContentLength);

                    db.tblEmployees.Add(emp);
                    db.SaveChanges();


                    return RedirectToAction("Index");

                }

               

                return View(emp);

            }
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            using (EmployeeEntities db = new EmployeeEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                tblEmployee emp = db.tblEmployees.Find(id);
                if (emp == null)
                {
                    return HttpNotFound();
                }
                return View(emp);
            }
           
        }

        // POST: Employees/Edit/5
        [HttpPost]
        public ActionResult Edit(tblEmployee emp, HttpPostedFileBase FileUploadEdit)
        {
            using (EmployeeEntities db = new EmployeeEntities())
            {
                if (ModelState.IsValid)
                {

                    //Save image in folder

                    string FileName = Path.GetFileName(FileUploadEdit.FileName);
                    string SaveLocation = Server.MapPath("~/Upload/" + FileName);
                    FileUploadEdit.SaveAs(SaveLocation);

                    //save image name in database

                    emp.Picture = "~/Upload/" + FileName;


                    // byte image Save


                    emp.EmployeeImage = new byte[FileUploadEdit.ContentLength];
                    ViewBag.TeacherPicture = FileUploadEdit.InputStream.Read(emp.EmployeeImage,0, FileUploadEdit.ContentLength);


                    db.Entry(emp).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(emp);
            }
            
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            using (EmployeeEntities db = new EmployeeEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                tblEmployee emp = db.tblEmployees.Find(id);
                if (emp == null)
                {
                    return HttpNotFound();
                }
                return View(emp);
            }
            
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            using (EmployeeEntities db = new EmployeeEntities())
            {
                tblEmployee emp = db.tblEmployees.Find(id);
                db.tblEmployees.Remove(emp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           
        }


        [Authorize]
        public ActionResult PrintReport(int id)
        {

            using (EmployeeEntities db = new EmployeeEntities())
            {


                ReportDocument rpt = new ReportDocument();
                rpt.Load(Path.Combine(Server.MapPath("~/EmployeeReport/"), "EmployeeReport.rpt"));

                rpt.SetDataSource(db.tblEmployees.Where(e => e.Id == id));

                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();


                Stream stream = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "EmployeeReport.pdf");


            }

           
        }














    }
}
