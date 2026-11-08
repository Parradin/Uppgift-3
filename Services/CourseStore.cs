using System.Collections.Generic;
using System.Collections.ObjectModel;
using Client.Models;

namespace Client.Services;

public class CourseStore
{
    public ObservableCollection<Course> Courses { get; set; } = new();

    public void LoadCourses(List<Course> courses)
    {
        Courses.Clear();

        foreach (var course in courses)
        {
            Courses.Add(course);
        }
    }
}
