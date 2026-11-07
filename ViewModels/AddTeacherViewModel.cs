
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using Client.Models;
using Client.Services;

namespace Client.ViewModels;

public partial class AddTeacherViewModel : ViewModelBase
{
    private readonly TeacherStore _teacherStore;

    public ObservableCollection<CourseSelectionViewModel> CourseOptions { get; } = new();

    [ObservableProperty] private string personalId = string.Empty;
    [ObservableProperty] private string firstName = string.Empty;
    [ObservableProperty] private string lastName = string.Empty;
    [ObservableProperty] private string email = string.Empty;
    [ObservableProperty] private string phoneNr = string.Empty;
    [ObservableProperty] private string streetAdress = string.Empty;
    [ObservableProperty] private string city = string.Empty;
    [ObservableProperty] private string postalCode = string.Empty;
    [ObservableProperty] private string knowledgeArea = string.Empty;

    public AddTeacherViewModel(TeacherStore teacherStore, CourseStore courseStore)
    {
        _teacherStore = teacherStore;

        foreach (var course in courseStore.Courses)
        {
            CourseOptions.Add(new CourseSelectionViewModel(course));
        }
    }

    public bool SaveTeacher()
    {
        if (string.IsNullOrWhiteSpace(PersonalId) ||
            string.IsNullOrWhiteSpace(FirstName) ||
            string.IsNullOrWhiteSpace(LastName) ||
            string.IsNullOrWhiteSpace(Email) ||
            string.IsNullOrWhiteSpace(PhoneNr) ||
            string.IsNullOrWhiteSpace(StreetAdress) ||
            string.IsNullOrWhiteSpace(City) ||
            string.IsNullOrWhiteSpace(PostalCode) ||
            string.IsNullOrWhiteSpace(KnowledgeArea))
        {
            return false;
        }

        var teacher = new Teacher
        {
            PersonalId = PersonalId,
            FirstName = FirstName,
            LastName = LastName,
            Email = Email,
            PhoneNr = PhoneNr,
            Address = new Address
            {
                StreetAdress = StreetAdress,
                City = City,
                PostalCode = PostalCode
            },
            KnowledgeArea = KnowledgeArea,

            Courses = CourseOptions
                .Where(c => c.IsSelected)
                .Select(c => c.Course)
                .ToList()
        };

        _teacherStore.Teachers.Add(teacher);

        return true;
    }
}

