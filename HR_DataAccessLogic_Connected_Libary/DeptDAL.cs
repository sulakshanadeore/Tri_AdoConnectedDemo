using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace HR_DataAccessLogic_Connected_Libary
{
    public class DeptDAL
    {
        public bool AddDept(Dept dept) 
        {
            bool operationStatus = false;
            string str=ConfigurationManager.ConnectionStrings["HRConnectionString"].ConnectionString;
            SqlConnection cn = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand("[dbo].sp_InsertDept", cn);
          
            try
            {
                
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@p_Dname", dept.Dname);
                cmd.Parameters.AddWithValue("@p_Loc", dept.Loc);
                cmd.Parameters.AddWithValue("@p_MgrName", dept.MgrName);
                cn.Open();
                cmd.ExecuteNonQuery();
                operationStatus=true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally 
            {
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
                
            }
                        return operationStatus;
        
        }

        public bool EditDept(Dept dept, int deptno) { return true; }
        public bool RemoveDept(Dept dept, int deptno) { return true; }

        public List<Dept> GetDeptList() 
        {
            List<Dept> deptlist=new List<Dept>();
            string str = ConfigurationManager.ConnectionStrings["HRConnectionString"].ConnectionString;
            SqlConnection cn = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand("select * from dept", cn);
            cn.Open();
            SqlDataReader dr=cmd.ExecuteReader();
            while (dr.Read())
            {
                Dept d=new  Dept();
                d.Deptno = Convert.ToInt32(dr["Deptno"]);
                d.Dname = dr["Dname"].ToString();
                d.Loc = dr["Loc"].ToString();
                d.MgrName = dr["MgrName"].ToString();
                deptlist.Add(d);


            }
            cn.Close();
            cn.Dispose();


            return deptlist;
             
        }
       public Dept FindDept(int deptno) {

            Dept dept = new Dept();
            return dept;
        }

    }

   
}
