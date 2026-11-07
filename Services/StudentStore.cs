using System.Collections.ObjectModel;
using Client.Models;

namespace Client.Services;

public class StudentStore
{
    public ObservableCollection<Student> Students { get; set; } = new();
}