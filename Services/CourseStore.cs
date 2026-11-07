using System.Collections.ObjectModel;
using Client.Models;

namespace Client.Services;

public class CourseStore
{
    public ObservableCollection<Course> Courses { get; set; } = new();
}