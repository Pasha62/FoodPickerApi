using BCrypt.Net;

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
                    Parent parent = new()
                    {
                        MiddleName = table.Rows[i][0] != null ? (string)table.Rows[i][0] : "-1",
                        Name = table.Rows[i][1] != null ? (string)table.Rows[i][1] : "-1",
                        Surname = table.Rows[i][2] != null ? (string)table.Rows[i][2] : "-1",
                        Username = table.Rows[i][3] != null ? (string)table.Rows[i][3] : "-1",
                        Password = table.Rows[i][4] != null ? BCrypt.Net.BCrypt.HashPassword((string)table.Rows[i][4], 12) : "-1"
                    };
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


        [HttpPost("teachers")]
        public async Task<HttpStatusCode> UploadTeacher(string path)
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
                    Teacher teacher = new()
                    {
                        MiddleName = table.Rows[i][0] != null ? (string)table.Rows[i][0] : "-1",
                        Name = table.Rows[i][1] != null ? (string)table.Rows[i][1] : "-1",
                        Surname = table.Rows[i][2] != null ? (string)table.Rows[i][2] : "-1",
                        Username = table.Rows[i][3] != null ? (string)table.Rows[i][3] : "-1",
                        Password = table.Rows[i][4] != null ? BCrypt.Net.BCrypt.HashPassword((string)table.Rows[i][4], 12) : "-1"
                    };
                    DBContext.Teachers.Add(teacher);
                    await DBContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return HttpStatusCode.Created;
        }

        [HttpPost("students")]
        public async Task<HttpStatusCode> UploadStudent(string path)
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
                    Student student = new()
                    {
                        Name = table.Rows[i][0] != null ? (string)table.Rows[i][1] : "-1",
                        Surname = table.Rows[i][1] != null ? (string)table.Rows[i][2] : "-1",
                        MiddleName = table.Rows[i][2] != null ? (string)table.Rows[i][0] : "-1",
                        GradeId = DBContext.Grades.FirstOrDefaultAsync(g => g.Id == (int)table.Rows[i][3]) != null ? (int)table.Rows[i][3] : -1
                    };

                    DBContext.Students.Add(student);
                    await DBContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return HttpStatusCode.Created;
        }

        [HttpPost("workers")]
        public async Task<HttpStatusCode> UploadWorker(string path)
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
                    Worker worker = new()
                    {
                        MiddleName = table.Rows[i][0] != null ? (string)table.Rows[i][0]: "-1",
                        Name = table.Rows[i][1] != null ? (string)table.Rows[i][1] : "-1",
                        Surname = table.Rows[i][2] != null ? (string)table.Rows[i][2] : "-1",
                        Username = table.Rows[i][3] != null ? (string)table.Rows[i][3] : "-1",
                        Password = table.Rows[i][4] != null ? BCrypt.Net.BCrypt.HashPassword((string)table.Rows[i][4], 12) : "-1",
                        Role = table.Rows[i][5] != null ? (string)table.Rows[i][5] : "-1"
                    };
                    DBContext.Workers.Add(worker);
                    await DBContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return HttpStatusCode.Created;
        }


        [HttpPost("dishes")]
        public async Task<HttpStatusCode> UploadDish(string path)
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
                    Dish dish = new()
                    {
                        Name = table.Rows[i][0] != null ? (string)table.Rows[i][0] : "-1",
                        ImgUrl = table.Rows[i][1] != null ? (string)table.Rows[i][1] : "-1",
                        Price = table.Rows[i][2] != null ? (int)table.Rows[i][2] : -1,
                        Proteins = table.Rows[i][3] != null ? (int)table.Rows[i][3] : -1,
                        Carbs = table.Rows[i][4] != null ? (int)table.Rows[i][4] : -1,
                        Fats = table.Rows[i][5] != null ? (int)table.Rows[i][5] : -1,
                        WeightGrams = table.Rows[i][6] != null ? (int)table.Rows[i][6] : -1,
                        Ingredients = table.Rows[i][7] != null ? (string)table.Rows[i][7] : "-1",
                        Calories = table.Rows[i][8] != null ? (int)table.Rows[i][8] : -1,
                        Type = table.Rows[i][9] != null ? (string)table.Rows[i][9] : "-1"
                    };
                    DBContext.Dishes.Add(dish);
                    await DBContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return HttpStatusCode.Created;
        }

        [HttpPost("parentstudents")]
        public async Task<HttpStatusCode> UploadParentStudent(string path)
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
                    ParentStudent parentStudent = new()
                    {
                        ParentId = DBContext.Parents.FirstOrDefaultAsync(g => g.Id == (int)table.Rows[i][0]) != null ? (int)table.Rows[i][0] : null,
                        StudentId = DBContext.Students.FirstOrDefaultAsync(g => g.Id == (int)table.Rows[i][1]) != null ? (int)table.Rows[i][1] : null
                    };

                    DBContext.ParentStudents.Add(parentStudent);
                    await DBContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return HttpStatusCode.Created;
        }

        [HttpPost("grades")]
        public async Task<HttpStatusCode> UploadGrade(string path)
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
                    Grade grade = new()
                    {
                        Number = (int)table.Rows[i][0],
                        Letter = (string)table.Rows[i][1],
                        TeacherId = DBContext.Teachers.FirstOrDefaultAsync(g => g.Id == (int)table.Rows[i][2]) != null ? (int)table.Rows[i][2] : null
                    };

                    DBContext.Grades.Add(grade);
                    await DBContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return HttpStatusCode.Created;
        }

        [HttpPost("preferences")]
        public async Task<HttpStatusCode> UploadPreference(string path)
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
                    Preference preference = new()
                    {
                        DayOfWeek = (int)table.Rows[i][0],
                        DishId = DBContext.Dishes.FirstOrDefaultAsync(g => g.Id == (int)table.Rows[i][1]) != null ? (int)table.Rows[i][1] : null,
                        StudentId = DBContext.Students.FirstOrDefaultAsync(g => g.Id == (int)table.Rows[i][2]) != null ? (int)table.Rows[i][2] : null
                    };

                    DBContext.Preferences.Add(preference);
                    await DBContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return HttpStatusCode.Created;
        }

        [HttpPost("orders")]
        public async Task<HttpStatusCode> UploadOrder(string path)
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
                    Order order = new()
                    {
                        Date = (DateTime)table.Rows[i][0],
                        Cost = (int)table.Rows[i][1],
                        DishId = DBContext.Dishes.FirstOrDefaultAsync(g => g.Id == (int)table.Rows[i][2]) != null ? (int)table.Rows[i][2] : null,
                        StudentId = DBContext.Students.FirstOrDefaultAsync(g => g.Id == (int)table.Rows[i][3]) != null ? (int)table.Rows[i][3] : null
                    };

                    DBContext.Orders.Add(order);
                    await DBContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return HttpStatusCode.Created;
        }
    }
}
