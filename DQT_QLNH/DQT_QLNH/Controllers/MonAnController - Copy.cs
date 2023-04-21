using DQT_QLNH.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using DQT_QLNH.Model;
using System.IO;
using Microsoft.AspNetCore.Hosting;


namespace DQT_QLNH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonAnController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env; 

        public MonAnController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        [HttpGet]
        public JsonResult Get() 
        {
            string query = "select MaMonAn, TenMonAn, ThucDon,NgayTao, AnhMonAn  from MonAn";
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

        public JsonResult Post(MonAn monAn) {
            String query = @"Insert into MonAn values (N'"+monAn.TenMonAn+"' , N'"+monAn.ThucDon+"',  '"+monAn.NgayTao+"',  N'"+monAn.AnhMonAn+"')";
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

        public JsonResult Put (MonAn MonAn)
        {
            String query = @"Update MonAn set 
                    TenMonAn = N'"+ MonAn.TenMonAn+ 
                "' ,ThucDon = N'" + MonAn.ThucDon + "'"  +
                " ,NgayTao = '" + MonAn.NgayTao + "'" +
                " ,AnhMonAn = N'" + MonAn.AnhMonAn + "'" +
                "where MaMonAn = " +MonAn.MaMonAn;
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

        [HttpDelete("{ma}")]

        public JsonResult Delete(int  ma)
        {
            String query = @"Delete from MonAn" + " where MaMonAn = " + ma;
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

        [Route("SaveFile")]
        [HttpPost]

        public JsonResult SaveFile()
        {
            try {

                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "//Photos/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }
                return new JsonResult("Them moi thanh cong");
            }
            catch (Exception e) {

                Console.WriteLine(e.ToString());
                return new JsonResult("com.jpg");
            }

            
        }

        [Route("GetAllTenThucDon")]
        [HttpGet]
        public JsonResult GetAllTenThucDon()
        {
            string query = "select TenThucDon  from ThucDon";
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

            return new JsonResult(table);

        }

    }
}
