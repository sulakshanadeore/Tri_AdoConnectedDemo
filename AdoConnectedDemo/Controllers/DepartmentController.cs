using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdoConnectedDemo.Models;
using HR_DataAccessLogic_Connected_Libary;

namespace AdoConnectedDemo.Controllers
{
    public class DepartmentController : Controller
    {
        // GET: Department
        public ActionResult Index()
        {
            DeptDAL dal=new DeptDAL();
            List<Dept> deptlist=dal.GetDeptList();
            List<DeptModel> deptModels = new List<DeptModel>();
            foreach (Dept dept in deptlist) 
            {
                deptModels.Add(new DeptModel {Deptno=dept.Deptno, Dname=dept.Dname,Loc=dept.Loc,MgrName=dept.MgrName });
            
            }



            return View(deptModels);
        }

        // GET: Department/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Department/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Department/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                DeptDAL dal=new DeptDAL();
                

                Dept dept=new Dept();
                dept.Dname = collection["Dname"];
                dept.Loc = collection["Loc"];
                dept.MgrName = collection["MgrName"];
                dal.AddDept(dept);
                                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {

                ViewBag.ErrorMsg=ex.Message;
                return View();
            }
        }

        // GET: Department/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Department/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Department/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Department/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
