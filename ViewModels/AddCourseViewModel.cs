
using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Client.Services;
using Client.Models;

namespace Client.ViewModels;

public partial class AddCourseViewModel : ViewModelBase
{
    private readonly CourseStore _courseStore;
    private readonly RestDbService _restDbService;

    [ObservableProperty]
    private string courseNr = string.Empty;

    [ObservableProperty]
    private string courseTitel = string.Empty;

    [ObservableProperty]
    private DateTimeOffset? startDate;

    [ObservableProperty]
    private DateTimeOffset? endDate;

    public AddCourseViewModel(
        CourseStore courseStore,
        RestDbService restDbService)
    {
        _courseStore = courseStore;
        _restDbService = restDbService;
    }

    public async Task<bool> SaveCourseAsync()
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

        var saved = await _restDbService.AddCourseAsync(course);

        if (!saved)
            return false;

        _courseStore.Courses.Add(course);
        return true;
    }
}

