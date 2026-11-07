using System;
using Client.Enums;
using Client.Interfaces;
using Client.ViewModels;

namespace Client.Factories;

public class PageViewFactory : IPageViewFactory
{
    public ViewModelBase LoadPageViewModel(PageViewsEnum page)
    {
        return page switch
        {
            PageViewsEnum.Home => new HomeViewModel(),
            PageViewsEnum.Courses => new CourseViewModel(),
            PageViewsEnum.AddCourse => new AddCourseViewModel(),
            PageViewsEnum.Students => new StudentViewModel(),
            PageViewsEnum.AddStudent => new AddStudentViewModel(),
            PageViewsEnum.Teachers => new TeacherViewModel(),
            PageViewsEnum.AddTeacher => new AddTeacherViewModel(),
            _ => throw new InvalidOperationException("Det finns inte det du ville ha"),
        };
    }

}
