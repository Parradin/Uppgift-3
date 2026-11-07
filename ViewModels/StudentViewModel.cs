
using System.Collections.ObjectModel;
using Client.Models;
using Client.Services;
using CommunityToolkit.Mvvm.Input;

namespace Client.ViewModels;

public partial class StudentViewModel : ViewModelBase
{
    private readonly StudentStore _studentStore;

    public ObservableCollection<Student> Students => _studentStore.Students;

    public StudentViewModel(StudentStore studentStore)
    {
        _studentStore = studentStore;
    }

    [RelayCommand]
    private void RemoveStudent(Student student)
    {
        _studentStore.Students.Remove(student);
    }
}
