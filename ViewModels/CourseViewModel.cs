
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Client.Models;
using Client.Services;
using CommunityToolkit.Mvvm.Input;

namespace Client.ViewModels;

public partial class CourseViewModel : ViewModelBase
{
    private readonly CourseStore _courseStore;
    private readonly RestDbService _restDbService;

    public ObservableCollection<Course> Courses => _courseStore.Courses;

    public CourseViewModel(
        CourseStore courseStore,
        RestDbService restDbService)
    {
        _courseStore = courseStore;
        _restDbService = restDbService;

        _ = LoadCoursesAsync();
    }

    private async Task LoadCoursesAsync()
    {
        var courses = await _restDbService.GetCoursesAsync();

        Console.WriteLine($"Kurser hämtade: {courses.Count}");

        _courseStore.LoadCourses(courses);

        Console.WriteLine($"Kurser i Store: {_courseStore.Courses.Count}");
    }

    [RelayCommand]
    private async Task RemoveCourseAsync(Course course)
    {
        if (course == null)
            return;

        var deleted = await _restDbService.DeleteCourseAsync(course);

        if (deleted)
        {
            _courseStore.Courses.Remove(course);
        }
    }
}
