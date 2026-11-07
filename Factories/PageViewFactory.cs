using System;
using Client.Enums;
using Client.Interfaces;
using Client.ViewModels;
using Client.Services;

namespace Client.Factories;

public class PageViewFactory : IPageViewFactory
{
    private readonly CourseStore _courseStore;

    public PageViewFactory(CourseStore courseStore, StudentStore studentStore, TeacherStore teacherStore)
    {
        _courseStore = courseStore;
        _studentStore = studentStore;
        _teacherStore = teacherStore;
    }
    private readonly StudentStore _studentStore;

    private readonly TeacherStore _teacherStore;

    public ViewModelBase LoadPageViewModel(PageViewsEnum page)
    {
        return page switch
        {
            PageViewsEnum.Home => new HomeViewModel(),
            PageViewsEnum.Courses => new CourseViewModel(_courseStore),
            PageViewsEnum.AddCourse => new AddCourseViewModel(_courseStore),
            PageViewsEnum.Students => new StudentViewModel(_studentStore),
            PageViewsEnum.AddStudent => new AddStudentViewModel(_studentStore),
            PageViewsEnum.Teachers => new TeacherViewModel(_teacherStore),
            PageViewsEnum.AddTeacher => new AddTeacherViewModel(_teacherStore, _courseStore),
            _ => throw new InvalidOperationException("Det finns inte det du ville ha"),
        };
    }
}

