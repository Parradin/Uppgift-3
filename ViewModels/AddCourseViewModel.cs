using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Client.ViewModels;

public partial class AddCourseViewModel : ViewModelBase
{
    [ObservableProperty]
    private string courseNr = string.Empty;

    [ObservableProperty]
    private string courseTitel = string.Empty;

    [ObservableProperty]
    private DateTime? startDate;

    [ObservableProperty]
    private DateTime? endDate;
}