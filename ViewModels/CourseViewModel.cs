using System;
using System.Collections.ObjectModel;
using Client.Models;
namespace Client.ViewModels;

public partial class CourseViewModel : ViewModelBase
{
    public ObservableCollection<Course> Courses { get; set; } = new()
{
    new Course
    {
        CourseNr = "C001",
        CourseTitel = "Programmering 1",
        StartDate = new DateTime(2026, 11, 1),
        EndDate = new DateTime(2027, 1, 30),
        IsDistance = false
    }
};
}
