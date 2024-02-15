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

        public bool EditDept(Dept dept, int deptno) 
        {


            bool operationStatus = false;
            string str = ConfigurationManager.ConnectionStrings["HRConnectionString"].ConnectionString;
            SqlConnection cn = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand("[dbo].[sp_UpdateDept]", cn);

            try
            {

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@p_Deptno", deptno);
                cmd.Parameters.AddWithValue("@p_Dname", dept.Dname);
                cmd.Parameters.AddWithValue("@p_Loc", dept.Loc);
                cmd.Parameters.AddWithValue("@p_Mgr", dept.MgrName);
                cn.Open();
                cmd.ExecuteNonQuery();
                operationStatus = true;
            }
            catch (Exception ex)
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
        public bool RemoveDept(int deptno) 
        {
            bool operationStatus = false;
            string str = ConfigurationManager.ConnectionStrings["HRConnectionString"].ConnectionString;
            SqlConnection cn = new SqlConnection(str);

            SqlCommand cmd = new SqlCommand("delete  from dept where deptno= " + deptno, cn);
            cn.Open();
            cmd.ExecuteNonQuery();
                
            operationStatus = true;
            cn.Close();
            cn.Dispose();

            return operationStatus;
        
        }

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
            string str = ConfigurationManager.ConnectionStrings["HRConnectionString"].ConnectionString;
            SqlConnection cn = new SqlConnection(str);

            SqlCommand cmd = new SqlCommand("select * from dept where deptno= " + deptno, cn);
            cn.Open();
            SqlDataReader dr=cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
            
                dept.Deptno = Convert.ToInt32(dr["Deptno"]);
                dept.Dname = dr["Dname"].ToString();
                dept.Loc = dr["Loc"].ToString();
                dept.MgrName = dr["MgrName"].ToString();

            }
            cn.Close();
            cn.Dispose();
            return dept;
        }

    }

   
}
