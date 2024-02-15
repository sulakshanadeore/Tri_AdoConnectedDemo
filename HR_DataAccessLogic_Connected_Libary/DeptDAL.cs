using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net.Http.Headers;
using System.Xml.Linq;
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


            //  SqlCommand cmd = new SqlCommand("select * from dept", cn);
            //Inline table valued function in sql server

//            CREATE FUNCTION[dbo].ShowDeptData
//()
//RETURNS TABLE AS RETURN
//            (
//    SELECT Deptno, Dname, Loc, MgrName from Dept
//)




            SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[ShowDeptData]()", cn);
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

        public int DeptNumber() {
            string str = ConfigurationManager.ConnectionStrings["HRConnectionString"].ConnectionString;
            SqlConnection cn = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand("Select count(Deptno) from Dept",cn);
            cn.Open();
           int cnt=Convert.ToInt32(cmd.ExecuteScalar());
            cn.Close();
            cn.Dispose();

           // SqlCommand cmd = new SqlCommand("select [dbo].CountNumberOfDepts()", cn);
           // cn.Open();
           // SqlDataReader dr=cmd.ExecuteReader();
           // dr.Read();
           //int cnt= Convert.ToInt32(dr[0]);
            

           // cn.Close();
           // cn.Dispose();
           

            return cnt;



        }


       public Dept FindDept(int deptno) {

            Dept dept = new Dept();
            string str = ConfigurationManager.ConnectionStrings["HRConnectionString"].ConnectionString;
            SqlConnection cn = new SqlConnection(str);

            //SqlCommand cmd = new SqlCommand("select * from dept where deptno= " + deptno, cn);
            //Stored Procedure Code:
//            CREATE PROCEDURE[dbo].FindDeptByDeptno
//    @p_Deptno int,
//    @p_Dname varchar(25) output,
//	@p_Loc varchar(25) output,
//	@p_MgrName varchar(25) output

//AS

//    SELECT
//    @p_Dname = Dname,
//    @p_Loc = Loc,
//    @p_MgrName = MgrName from Dept

//    where
//    Deptno = @p_Deptno
//RETURN 0



            SqlCommand cmd = new SqlCommand("[dbo].[FindDeptByDeptno]", cn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_Deptno", deptno);
            cmd.Parameters.Add("@p_Dname",System.Data.SqlDbType.VarChar,25);
          cmd.Parameters["@p_Dname"].Direction = System.Data.ParameterDirection.Output;

            cmd.Parameters.Add("@p_Loc", System.Data.SqlDbType.VarChar, 25);
             cmd.Parameters["@p_Loc"].Direction = System.Data.ParameterDirection.Output;

            cmd.Parameters.Add("@p_MgrName", System.Data.SqlDbType.VarChar, 25);
           cmd.Parameters["@p_MgrName"].Direction = System.Data.ParameterDirection.Output;
            cn.Open();
            cmd.ExecuteNonQuery();
            dept.Deptno = deptno;
            dept.Dname = cmd.Parameters["@p_Dname"].Value.ToString();
            dept.Loc = cmd.Parameters["@p_Loc"].Value.ToString();
            dept.MgrName = cmd.Parameters["@p_MgrName"].Value.ToString();

            //if (dr.HasRows)
            //{
            //    dr.Read();


            //    //dept.Deptno = Convert.ToInt32(dr["Deptno"]);
            //dept.Dname = dr["Dname"].ToString();
            //dept.Loc = dr["Loc"].ToString();
            //dept.MgrName = dr["MgrName"].ToString();

            //    }
            cn.Close();
            cn.Dispose();
            return dept;
        }

    }

   
}
