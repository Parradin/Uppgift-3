using System.Collections.ObjectModel;
using Client.Models;

namespace Client.Services;

public class TeacherStore
{
    public ObservableCollection<Teacher> Teachers { get; set; } = new();
}