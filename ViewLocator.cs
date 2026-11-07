using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Client.ViewModels;

namespace Client;

public class ViewLocator : IDataTemplate
{
    public Control? Build(object? data)
    {
        if (data is null) return null;

        // Skapa en view(sida)...
        var viewName = data.GetType().FullName!
    .Replace("ViewModels", "Views", StringComparison.InvariantCulture)
    .Replace("AddCourseViewModel", "AddCourseView", StringComparison.InvariantCulture)
    .Replace("AddStudentViewModel", "AddStudentView", StringComparison.InvariantCulture)
    .Replace("AddTeacherViewModel", "AddTeacherView", StringComparison.InvariantCulture)
    .Replace("CourseViewModel", "CoursesView", StringComparison.InvariantCulture)
    .Replace("StudentViewModel", "StudentsView", StringComparison.InvariantCulture)
    .Replace("TeacherViewModel", "TeachersView", StringComparison.InvariantCulture)
    .Replace("ViewModel", "View", StringComparison.InvariantCulture);
        var view = Type.GetType(viewName);

        if (view is null) return null;

        var control = (Control)Activator.CreateInstance(view)!;
        control.DataContext = data;

        return control;
    }

    public bool Match(object? data) => data is ViewModelBase;
}