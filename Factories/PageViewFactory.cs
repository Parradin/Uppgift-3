using System;
using Client.Enums;
using Client.Interfaces;
using Client.ViewModels;
using Client.Services;

namespace Client.Factories;

public class PageViewFactory : IPageViewFactory
{
    private readonly RestDbService _restDbService;
    private readonly CourseStore _courseStore;
    private readonly StudentStore _studentStore;
    private readonly TeacherStore _teacherStore;

    public PageViewFactory(
        CourseStore courseStore,
        StudentStore studentStore,
        TeacherStore teacherStore,
        RestDbService restDbService)
    {
        _courseStore = courseStore;
        _studentStore = studentStore;
        _teacherStore = teacherStore;
        _restDbService = restDbService;
    }

    public ViewModelBase LoadPageViewModel(PageViewsEnum page)
    {
        return page switch
        {
            PageViewsEnum.Home => new HomeViewModel(),
            PageViewsEnum.Courses => new CourseViewModel(_courseStore, _restDbService),
            PageViewsEnum.AddCourse => new AddCourseViewModel(_courseStore, _restDbService),
            PageViewsEnum.Students => new StudentViewModel(_studentStore, _restDbService),
            PageViewsEnum.AddStudent => new AddStudentViewModel(_studentStore, _restDbService),
            PageViewsEnum.Teachers => new TeacherViewModel(_teacherStore, _restDbService),
            PageViewsEnum.AddTeacher => new AddTeacherViewModel(_teacherStore, _courseStore, _restDbService),
            _ => throw new InvalidOperationException("Det finns inte det du ville ha"),
        };
    }
}