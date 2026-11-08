using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Client.Models;
using Client.Services;
using System.Threading.Tasks;

namespace Client.ViewModels;

public partial class AddStudentViewModel : ViewModelBase
{
    private readonly StudentStore _studentStore;
    private readonly RestDbService _restDbService;

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

    public AddStudentViewModel(
        StudentStore studentStore,
        RestDbService restDbService)
    {
        _studentStore = studentStore;
        _restDbService = restDbService;
    }

    public async Task<bool> SaveStudentAsync()
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
            return false;
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

        var saved = await _restDbService.AddStudentAsync(student);

        if (!saved)
            return false;

        _studentStore.Students.Add(student);

        return true;
    }
}