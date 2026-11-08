using System;
using Client.Factories;
using Client.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Client.Services;

namespace Client.Helpers;

public static class ServiceCollectionExtensions
{


    public static void AddServices(this IServiceCollection collection)
    {
        collection.AddSingleton<CourseStore>();
        collection.AddSingleton<StudentStore>();
        collection.AddSingleton<TeacherStore>();
        collection.AddSingleton<MainWindowViewModel>();
        collection.AddSingleton<PageViewFactory>();
        collection.AddTransient<HomeViewModel>();
        collection.AddTransient<CourseViewModel>();
        collection.AddTransient<AddCourseViewModel>();
        collection.AddTransient<StudentViewModel>();
        collection.AddTransient<AddStudentViewModel>();
        collection.AddTransient<TeacherViewModel>();
        collection.AddTransient<AddTeacherViewModel>();
        collection.AddSingleton<RestDbService>();
    }
}
