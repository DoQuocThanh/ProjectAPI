using DQT_QLNH.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;



namespace DQT_QLNH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThucDonController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ThucDonController(IConfiguration configuration) {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get() 
        {
            string query = "select MaThucDon, TenThucDon from ThucDon";
            DataTable table = new DataTable();
            String sqlDataSource = _configuration.GetConnectionString("DQT");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            { 
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon)) {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);

        }

        [HttpPost]

        public JsonResult Post(ThucDon thucdon) {
            String query = @"Insert into ThucDon values (N'"+thucdon.TenThucDon+"')";
            DataTable table = new DataTable();
            String sqlDataSource = _configuration.GetConnectionString("DQT");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Them moi thanh cong");
        }

        [HttpPut]

        public JsonResult Put (ThucDon thucdon)
        {
            String query = @"Update ThucDon set TenThucDon = N'"+ thucdon.TenThucDon+"'"+"where MaThucDon = "+thucdon.MaThucDon;
            DataTable table = new DataTable();
            String sqlDataSource = _configuration.GetConnectionString("DQT");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Update Thanh Cong");

        }

        [HttpDelete]

        public JsonResult Delete(ThucDon thucdon)
        {
            String query = @"Delete from ThucDon" + "where MaThucDon = " + thucdon.MaThucDon;
            DataTable table = new DataTable();
            String sqlDataSource = _configuration.GetConnectionString("DQT");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Xoa Thanh Cong");

        }
    }
}
