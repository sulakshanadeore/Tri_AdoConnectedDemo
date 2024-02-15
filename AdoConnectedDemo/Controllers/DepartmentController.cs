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
        public ActionResult Index()//List and Model Class
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
        public ActionResult Details(int id)//Details and Model Class
        {


            int deptno = id;

            DeptDAL dal = new DeptDAL();
            Dept dept = new Dept();
            dept = dal.FindDept(deptno);
            DeptModel model = new DeptModel();
            model.Deptno = dept.Deptno;
            model.Dname = dept.Dname;
            model.Loc = dept.Loc;
            model.MgrName = dept.MgrName;
            return View(model);
        }

        // GET: Department/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Department/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)//Create and ModelClass
        {
            bool completed = false;
            try
            {
                // TODO: Add insert logic here
                DeptDAL dal=new DeptDAL();
                

                Dept dept=new Dept();
                dept.Dname = collection["Dname"];
                dept.Loc = collection["Loc"];
                dept.MgrName = collection["MgrName"];
                completed = dal.AddDept(dept);
                           }
            catch(Exception ex)
            {

                ViewBag.ErrorMsg=ex.Message;
                return View();
            }
            if (completed)
                return RedirectToAction("Index");
            else 
                return View();

        }

        // GET: Department/Edit/5
        public ActionResult Edit(int id)
        {
            int deptno = id;

            DeptDAL dal = new DeptDAL();
            Dept dept = new Dept();
            dept=dal.FindDept(deptno);
DeptModel model=new DeptModel();
            model.Deptno = dept.Deptno;
            model.Dname = dept.Dname;
            model.Loc = dept.Loc; 
            model.MgrName= dept.MgrName;
            return View(model);
        }

        // POST: Department/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)//Edit view and Model Class
        {
            bool completed = false;
            try
            {
                DeptDAL dal = new DeptDAL();
                Dept dept = new Dept();
                dept.Deptno = id;
                dept.Dname = collection["Dname"];
                dept.Loc = collection["Loc"];
                dept.MgrName = collection["MgrName"];
                 completed = dal.EditDept(dept, id);
            
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMsg = ex.Message;
                return View();
            }
            if (completed)
                return RedirectToAction("Index");
            else
            
                return View();
            
        }

        // GET: Department/Delete/5
        public ActionResult Delete(int id) //Delete View and Model class
        {

            int deptno = id;

            DeptDAL dal = new DeptDAL();
            Dept dept = new Dept();
            dept = dal.FindDept(deptno);
            DeptModel model = new DeptModel();
            model.Deptno = dept.Deptno;
            model.Dname = dept.Dname;
            model.Loc = dept.Loc;
            model.MgrName = dept.MgrName;
            return View(model);

            
        }

        // POST: Department/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            bool completed=false;
            try
            {
                // TODO: Add delete logic here
                DeptDAL dal=new DeptDAL();
                int deptno = id;
                completed=dal.RemoveDept(deptno);
                if (completed)
                {
                    return RedirectToAction("Index");
                }
               
            }
            catch
            {
                return View();
            }
            
           
          
                return View();
            
        }
    }
}
