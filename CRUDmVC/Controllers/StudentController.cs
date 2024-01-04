using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using MvcCRUD.Models;

namespace CRUDmVC.Controllers
{
    public class StudentController : Controller
    {
        string connectionString = @"Data Source=DESKTOP-AV2PARO;Initial Catalog=Student_DB;Integrated Security=True";

        // GET: Student
        public ActionResult Index()
        {
            DataTable dbStu = new DataTable();
            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                sqlConn.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter("SELECT * FROM StudentMvc", sqlConn);
                sqlDA.Fill(dbStu);
            }
            return View(dbStu);
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View(new StudentModel());
        }

        // POST: Student/Create
        [HttpPost]
        public ActionResult Create(StudentModel studentModel)
        {
            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                sqlConn.Open();
                SqlCommand sqlCmd = new SqlCommand("INSERT INTO StudentMvc (StudentName, StudentAge, StudentDept) VALUES (@StudentName, @StudentAge, @StudentDept)", sqlConn);
                sqlCmd.Parameters.AddWithValue("@StudentName", studentModel.StudentName);
                sqlCmd.Parameters.AddWithValue("@StudentAge", studentModel.StudentAge);
                sqlCmd.Parameters.AddWithValue("@StudentDept", studentModel.StudentDept);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            StudentModel studentModel = new StudentModel();
            DataTable dt = new DataTable();
            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                sqlConn.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("Select * from StudentMvc where ID = @ID", sqlConn);
                sqlDa.SelectCommand.Parameters.AddWithValue("@ID", id);
                sqlDa.Fill(dt);
            }

            if (dt.Rows.Count == 1)
            {
                studentModel.StudentID = Convert.ToInt32(dt.Rows[0]["ID"]);
                studentModel.StudentName = dt.Rows[0]["StudentName"].ToString();
                studentModel.StudentAge = Convert.ToInt32(dt.Rows[0]["StudentAge"]);
                studentModel.StudentDept = dt.Rows[0]["StudentDept"].ToString();
                return View(studentModel);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Student/Edit/5
        [HttpPost]
        public ActionResult Edit(StudentModel studentModel)
        {
            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                sqlConn.Open();
                SqlCommand sqlCmd = new SqlCommand("UPDATE StudentMvc SET StudentName=@StudentName, StudentAge=@StudentAge, StudentDept=@StudentDept WHERE ID=@ID", sqlConn);
                sqlCmd.Parameters.AddWithValue("@ID", studentModel.StudentID);
                sqlCmd.Parameters.AddWithValue("@StudentName", studentModel.StudentName);
                sqlCmd.Parameters.AddWithValue("@StudentAge", studentModel.StudentAge);
                sqlCmd.Parameters.AddWithValue("@StudentDept", studentModel.StudentDept);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // GET: Student/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                sqlConn.Open();
                SqlCommand sqlCmd = new SqlCommand("Delete from StudentMvc WHERE ID=@ID", sqlConn);
                sqlCmd.Parameters.AddWithValue("@ID", id);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

    }
}
