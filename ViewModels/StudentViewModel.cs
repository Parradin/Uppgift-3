using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Client.Models;
using Client.Services;
using CommunityToolkit.Mvvm.Input;

namespace Client.ViewModels;

public partial class StudentViewModel : ViewModelBase
{
    private readonly StudentStore _studentStore;
    private readonly RestDbService _restDbService;

    public ObservableCollection<Student> Students => _studentStore.Students;

    public StudentViewModel(
        StudentStore studentStore,
        RestDbService restDbService)
    {
        _studentStore = studentStore;
        _restDbService = restDbService;

        _ = LoadStudentsAsync();
    }

    private async Task LoadStudentsAsync()
    {
        var students = await _restDbService.GetStudentsAsync();

        _studentStore.Students.Clear();

        foreach (var student in students)
        {
            _studentStore.Students.Add(student);
        }
    }
    [RelayCommand]
    private async Task RemoveStudentAsync(Student student)
    {
        if (student == null)
            return;

        var deleted = await _restDbService.DeleteStudentAsync(student);

        if (deleted)
        {
            _studentStore.Students.Remove(student);
        }
    }
}