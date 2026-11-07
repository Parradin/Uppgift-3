using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Client.Models;
using Client.Services;

namespace Client.ViewModels;

public partial class AddStudentViewModel : ViewModelBase
{
    private readonly StudentStore _studentStore;

    [ObservableProperty]
    private string personalId = string.Empty;

    [ObservableProperty]
    private string firstName = string.Empty;

    [ObservableProperty]
    private string lastName = string.Empty;

    [ObservableProperty]
    private string email = string.Empty;

    [ObservableProperty]
    private string phoneNr = string.Empty;

    [ObservableProperty]
    private string streetAdress = string.Empty;

    [ObservableProperty]
    private string city = string.Empty;

    [ObservableProperty]
    private string postalCode = string.Empty;

    public AddStudentViewModel(StudentStore studentStore)
    {
        _studentStore = studentStore;
    }

    [RelayCommand]
    public void SaveStudent()
    {
        if (string.IsNullOrWhiteSpace(PersonalId) ||
            string.IsNullOrWhiteSpace(FirstName) ||
            string.IsNullOrWhiteSpace(LastName) ||
            string.IsNullOrWhiteSpace(Email) ||
            string.IsNullOrWhiteSpace(PhoneNr) ||
            string.IsNullOrWhiteSpace(StreetAdress) ||
            string.IsNullOrWhiteSpace(City) ||
            string.IsNullOrWhiteSpace(PostalCode))
        {
            return;
        }

        var student = new Student
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
            }
        };

        _studentStore.Students.Add(student);
    }
}