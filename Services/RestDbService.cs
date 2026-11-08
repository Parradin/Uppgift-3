using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text.Json;
using Client.Models;
using System.Net.Http.Json;
using System.Linq;

namespace Client.Services;

public class RestDbService
{
    private readonly HttpClient _httpClient;

    private const string BaseUrl =
        "https://westcoasteducation-be50.restdb.io/rest/";

    public RestDbService()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(BaseUrl)
        };

        _httpClient.DefaultRequestHeaders.Add(
            "x-apikey",
            "f008b4b87208ba41c611264757f9550bf7d50"
        );

        _httpClient.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json")
        );
    }

    private string ReadValue(JsonElement item, string property)
    {
        return item.GetProperty(property).ToString();
    }

    public async Task<List<Course>> GetCoursesAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("courses");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var courses = JsonSerializer.Deserialize<List<Course>>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            return courses ?? new List<Course>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"RestDB-fel kurser: {ex.Message}");
            return new List<Course>();
        }
    }

    public async Task<List<Student>> GetStudentsAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("students");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            using var document = JsonDocument.Parse(json);

            var students = new List<Student>();

            foreach (var item in document.RootElement.EnumerateArray())
            {
                var student = new Student
                {
                    Id = ReadValue(item, "_id"),
                    PersonalId = ReadValue(item, "personalId"),
                    FirstName = ReadValue(item, "firstName"),
                    LastName = ReadValue(item, "lastName"),
                    Email = ReadValue(item, "email"),
                    PhoneNr = ReadValue(item, "phoneNr"),

                    Address = new Address
                    {
                        StreetAdress = ReadValue(item, "streetAdress"),
                        City = ReadValue(item, "city"),
                        PostalCode = ReadValue(item, "postalCode")
                    }
                };

                students.Add(student);
            }
            return students;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"RestDB-fel studenter: {ex.Message}");
            return new List<Student>();
        }
    }
    public async Task<List<Teacher>> GetTeachersAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("teachers");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            using var document = JsonDocument.Parse(json);

            var teachers = new List<Teacher>();

            // Hämta kurserna från RestDB
            var courses = await GetCoursesAsync();

            foreach (var item in document.RootElement.EnumerateArray())
            {
                var teacher = new Teacher
                {
                    Id = ReadValue(item, "_id"),
                    PersonalId = ReadValue(item, "personalId"),
                    FirstName = ReadValue(item, "firstName"),
                    LastName = ReadValue(item, "lastName"),
                    Email = ReadValue(item, "email"),
                    PhoneNr = ReadValue(item, "phoneNr"),

                    Address = new Address
                    {
                        StreetAdress = ReadValue(item, "streetAdress"),
                        City = ReadValue(item, "city"),
                        PostalCode = ReadValue(item, "postalCode")
                    },

                    KnowledgeArea = ReadValue(item, "knowledgeArea")
                };

                if (item.TryGetProperty("courses", out var courseNumbers))
                {
                    foreach (var courseNumber in courseNumbers.EnumerateArray())
                    {
                        var courseNr = courseNumber.ToString();

                        var matchingCourse = courses.Find(
                            c => c.CourseNr == courseNr);

                        if (matchingCourse != null)
                        {
                            teacher.Courses.Add(matchingCourse);
                        }
                    }
                }

                teachers.Add(teacher);
            }
            return teachers;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"RestDB-fel lärare: {ex.Message}");
            return new List<Teacher>();
        }
    }
    public async Task<bool> AddStudentAsync(Student student)
    {
        try
        {
            var studentData = new
            {
                personalId = student.PersonalId,
                firstName = student.FirstName,
                lastName = student.LastName,
                email = student.Email,
                phoneNr = student.PhoneNr,
                streetAdress = student.Address.StreetAdress,
                city = student.Address.City,
                postalCode = student.Address.PostalCode
            };

            var response = await _httpClient.PostAsJsonAsync(
                "students",
                studentData
            );

            response.EnsureSuccessStatusCode();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Kunde inte spara student: {ex.Message}");
            return false;
        }
    }
    public async Task<bool> AddTeacherAsync(Teacher teacher)
    {
        try
        {
            var teacherData = new
            {
                personalId = teacher.PersonalId,
                firstName = teacher.FirstName,
                lastName = teacher.LastName,
                email = teacher.Email,
                phoneNr = teacher.PhoneNr,
                streetAdress = teacher.Address.StreetAdress,
                city = teacher.Address.City,
                postalCode = teacher.Address.PostalCode,
                knowledgeArea = teacher.KnowledgeArea,
                courses = teacher.Courses
                    .Select(course => course.CourseNr)
                    .ToList()
            };

            var response = await _httpClient.PostAsJsonAsync(
                "teachers", teacherData);

            response.EnsureSuccessStatusCode();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Kunde inte spara lärare: {ex.Message}");
            return false;
        }
    }
    public async Task<bool> AddCourseAsync(Course course)
    {
        try
        {
            var courseData = new
            {
                courseNr = course.CourseNr,
                courseTitel = course.CourseTitel,
                startDate = course.StartDate,
                endDate = course.EndDate,
                isDistance = course.IsDistance
            };

            var response = await _httpClient.PostAsJsonAsync(
                "courses", courseData);

            response.EnsureSuccessStatusCode();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Kunde inte spara kurs: {ex.Message}");
            return false;
        }
    }
    public async Task<bool> DeleteCourseAsync(Course course)
    {
        try
        {
            var response = await _httpClient.DeleteAsync(
                $"courses/{course.Id}");

            response.EnsureSuccessStatusCode();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Kunde inte ta bort kurs: {ex.Message}");
            return false;
        }
    }
    public async Task<bool> DeleteStudentAsync(Student student)
    {
        try
        {
            var response = await _httpClient.DeleteAsync(
                $"students/{student.Id}");

            response.EnsureSuccessStatusCode();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Kunde inte ta bort student: {ex.Message}");
            return false;
        }
    }
    public async Task<bool> DeleteTeacherAsync(Teacher teacher)
    {
        try
        {
            var response = await _httpClient.DeleteAsync(
                $"teachers/{teacher.Id}");

            response.EnsureSuccessStatusCode();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Kunde inte ta bort lärare: {ex.Message}");
            return false;
        }
    }

}