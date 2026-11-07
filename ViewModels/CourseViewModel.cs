using System.Collections.ObjectModel;
using Client.Models;
using Client.Services;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Client.ViewModels;

public partial class CourseViewModel : ViewModelBase
{
    private readonly CourseStore _courseStore;

    public ObservableCollection<Course> Courses => _courseStore.Courses;

    public CourseViewModel(CourseStore courseStore)
    {
        _courseStore = courseStore;
    }
    [RelayCommand]
    private void RemoveCourse(Course course)
    {
        _courseStore.Courses.Remove(course);
    }
}
