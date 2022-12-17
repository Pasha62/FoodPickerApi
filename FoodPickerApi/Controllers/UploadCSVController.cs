using BCrypt.Net;

using FoodPickerApi.Entities;
using LumenWorks.Framework.IO.Csv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Metrics;
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
                using (var csvReader = new CsvReader(new StreamReader(path), true))
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
                using (var csvReader = new CsvReader(new StreamReader(path), true))
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
                    if (teacher != null)
                    {
                        List<char> letters = table.Rows[i][5].ToString().ToList();
                        int number = Convert.ToInt32(String.Join("", letters.SkipLast(1).ToArray()));
                        string letter = letters.Last().ToString();

                        Grade findgrade = await DBContext.Grades.Select(s => new Grade()
                        {
                            Id = s.Id,
                            Letter = s.Letter,
                            Number = s.Number,
                            TeacherId = s.TeacherId,
                            breakIndex = s.breakIndex
                        }).Where(s => s.Number == number  && s.Letter == letter ).FirstOrDefaultAsync();

                        if(findgrade != null)
                        {  

                        }
                        else
                        {
                            Grade grade = new Grade()
                            {
                                Letter = letter,
                                Number = number,
                                TeacherId = teacher.Id,
                                breakIndex = 0
                            };
                            DBContext.Grades.Add(grade);
                            await DBContext.SaveChangesAsync();
                        }
                    }
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
                using (var csvReader = new CsvReader(new StreamReader(path), true))
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
                    };
                    List<char> letters = table.Rows[i][3].ToString().ToList();
                    int number = Convert.ToInt32(String.Join("", letters.SkipLast(1).ToArray()));
                    string letter = letters.Last().ToString();

                    Grade findgrade = await DBContext.Grades.Select(s => new Grade()
                    {
                        Id = s.Id,
                        Letter = s.Letter,
                        Number = s.Number,
                        TeacherId = s.TeacherId,
                        breakIndex = s.breakIndex
                    }).Where(s => s.Number == number && s.Letter == letter).FirstOrDefaultAsync();

                    if (findgrade != null)
                    {
                        student.GradeId = findgrade.Id;
                        DBContext.Students.Add(student);
                        await DBContext.SaveChangesAsync();
                    }
                    Parent parent = await DBContext.Parents.Select(s => new Parent()
                    {
                        Id = s.Id,
                        Username = s.Username
                    }).Where(s => s.Username == table.Rows[i][4].ToString()).FirstOrDefaultAsync();
                    if (parent != null)
                    {
                        ParentStudent parentStudent = await DBContext.ParentStudents.Select(s => new ParentStudent()
                        {
                            ParentId = s.ParentId,
                            StudentId = s.StudentId
                        }).Where(s => s.ParentId == parent.Id && s.StudentId == student.Id).FirstOrDefaultAsync();
                        if( parentStudent != null)
                        {
                        }
                        else
                        {
                            ParentStudent parentStudent1 = new ParentStudent()
                            {
                                StudentId = student.Id,
                                ParentId = parent.Id
                            };
                            DBContext.ParentStudents.Add(parentStudent1);
                            await DBContext.SaveChangesAsync();
                        }
                    }


                    
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
                using (var csvReader = new CsvReader(new StreamReader(path), true))
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
                using (var csvReader = new CsvReader(new StreamReader(path), true))
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
                        Calories = table.Rows[i][8] != null ? (int)table.Rows[i][8] : -1
                    };
                    if ((int)table.Rows[i][9] == 0) dish.Type = Dish.DishType.PRIMARY;
                    if ((int)table.Rows[i][9] == 1) dish.Type = Dish.DishType.SIDE;
                    if ((int)table.Rows[i][9] == 2) dish.Type = Dish.DishType.SECONDARY;
                    if ((int)table.Rows[i][9] == 3) dish.Type = Dish.DishType.DRINK;
                    if ((int)table.Rows[i][9] == 4) dish.Type = Dish.DishType.EXTRA;
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
                using (var csvReader = new CsvReader(new StreamReader(path), true))
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
                using (var csvReader = new CsvReader(new StreamReader(path), true))
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
                using (var csvReader = new CsvReader(new StreamReader(path), true))
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
                using (var csvReader = new CsvReader(new StreamReader(path), true))
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
