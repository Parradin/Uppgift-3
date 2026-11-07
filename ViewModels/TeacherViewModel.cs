
using System.Collections.ObjectModel;
using Client.Models;
using Client.Services;
using CommunityToolkit.Mvvm.Input;

namespace Client.ViewModels;

public partial class TeacherViewModel : ViewModelBase
{
    private readonly TeacherStore _teacherStore;

    public ObservableCollection<Teacher> Teachers => _teacherStore.Teachers;

    public TeacherViewModel(TeacherStore teacherStore)
    {
        _teacherStore = teacherStore;
    }

    [RelayCommand]
    private void RemoveTeacher(Teacher teacher)
    {
        _teacherStore.Teachers.Remove(teacher);
    }
}
