using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Client.Models;
using Client.Services;
using CommunityToolkit.Mvvm.Input;

namespace Client.ViewModels;

public partial class TeacherViewModel : ViewModelBase
{
    private readonly TeacherStore _teacherStore;
    private readonly RestDbService _restDbService;

    public ObservableCollection<Teacher> Teachers => _teacherStore.Teachers;

    public TeacherViewModel(
        TeacherStore teacherStore,
        RestDbService restDbService)
    {
        _teacherStore = teacherStore;
        _restDbService = restDbService;

        _ = LoadTeachersAsync();
    }

    private async Task LoadTeachersAsync()
    {
        var teachers = await _restDbService.GetTeachersAsync();

        _teacherStore.Teachers.Clear();

        foreach (var teacher in teachers)
        {
            _teacherStore.Teachers.Add(teacher);
        }
    }
    [RelayCommand]
    private async Task RemoveTeacherAsync(Teacher teacher)
    {
        if (teacher == null)
            return;

        var deleted = await _restDbService.DeleteTeacherAsync(teacher);

        if (deleted)
        {
            _teacherStore.Teachers.Remove(teacher);
        }
    }
}