using System;
using CommunityToolkit.Mvvm.ComponentModel;
using Client.Services;
using CommunityToolkit.Mvvm.Input;
using Client.Models;
namespace Client.ViewModels;

public partial class AddCourseViewModel : ViewModelBase
{
    private readonly CourseStore _courseStore;

    [ObservableProperty]
    private string courseNr = string.Empty;

    [ObservableProperty]
    private string courseTitel = string.Empty;

    [ObservableProperty]
    private DateTimeOffset? startDate;

    [ObservableProperty]
    private DateTimeOffset? endDate;

    public AddCourseViewModel(CourseStore courseStore)
    {
        _courseStore = courseStore;
    }
    public bool SaveCourse()
    {
        if (string.IsNullOrWhiteSpace(CourseNr) ||
            string.IsNullOrWhiteSpace(CourseTitel) ||
            StartDate is null ||
            EndDate is null)
        {
            return false;
        }

        var course = new Course
        {
            CourseNr = CourseNr,
            CourseTitel = CourseTitel,
            StartDate = StartDate.Value.DateTime,
            EndDate = EndDate.Value.DateTime,
            IsDistance = false
        };

        if (!course.IsCourseDateValid())
            return false;

        _courseStore.Courses.Add(course);

        return true;
    }

}
