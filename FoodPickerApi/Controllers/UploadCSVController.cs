using BCrypt.Net;
using FoodPickerApi.DTO;
using FoodPickerApi.Entities;
using LumenWorks.Framework.IO.Csv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net;
using System.Text;

namespace FoodPickerApi.Controllers
{
    [ApiController]
    [Route("api/import")]
    public class UploadCSVController : ControllerBase
    {
        private readonly foodpickerdbContext DBContext;
        public UploadCSVController(foodpickerdbContext DBContext) {
            this.DBContext = DBContext;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }


        [HttpPost("parents")]
        public async Task<HttpStatusCode> UploadParent(string path)
        {
            try
            {
                DataTable table = new DataTable();
                using (var csvReader = new CsvReader(new StreamReader(path, Encoding.GetEncoding(1251)), true))
                {
                    table.Load(csvReader);
                };
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    Parent parent = new();
                    parent.MiddleName = table.Rows[i][0] != null ? Convert.ToString(table.Rows[i][0]) : "-1";
                    parent.Name = table.Rows[i][1] != null ? Convert.ToString(table.Rows[i][1]) : "-1";
                    parent.Surname = table.Rows[i][2] != null ? Convert.ToString(table.Rows[i][2]) : "-1";
                    parent.Username = table.Rows[i][3] != null ? Convert.ToString(table.Rows[i][3]) : "-1";
                    parent.Password = table.Rows[i][4] != null ? BCrypt.Net.BCrypt.HashPassword(Convert.ToString(table.Rows[i][4]), 12) : "-1";
                    DBContext.Parents.Add(parent);
                    await DBContext.SaveChangesAsync();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return HttpStatusCode.Created;
        }
    }
}
