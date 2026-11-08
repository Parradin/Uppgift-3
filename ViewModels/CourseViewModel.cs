using System;
using System.Collections.ObjectModel;
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

        LoadCoursesAsync();
    }

    private async void LoadCoursesAsync()
    {
        try
        {
            var courses = await _restDbService.GetCoursesAsync();
            _courseStore.LoadCourses(courses);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Kunde inte hämta kurser: {ex.Message}");
        }
    }

    [RelayCommand]
    private void RemoveCourse(Course course)
    {
        _courseStore.Courses.Remove(course);
    }
}