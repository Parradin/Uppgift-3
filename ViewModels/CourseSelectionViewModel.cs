
using CommunityToolkit.Mvvm.ComponentModel;
using Client.Models;

namespace Client.ViewModels;

public partial class CourseSelectionViewModel : ObservableObject
{
    public Course Course { get; }

    [ObservableProperty]
    private bool isSelected;

    public CourseSelectionViewModel(Course course)
    {
        Course = course;
    }
}
