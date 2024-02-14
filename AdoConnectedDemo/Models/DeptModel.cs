using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdoConnectedDemo.Models
{
    public class DeptModel
    {
        
        public int Deptno { get;set; }


        string _dname;
        public string Dname {
            get 
            {
                return _dname; 
            } 
            set { 
                if (value.Length > 0) 
                {
                _dname = value;
                }
                else
                {
                    throw new ArgumentNullException("Dname cannot be null");
                }
            } }
        public string Loc { get; set; }
        public string MgrName { get; set; }


    }
}