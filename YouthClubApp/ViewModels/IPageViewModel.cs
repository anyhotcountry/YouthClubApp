using System;

namespace YouthClubApp.ViewModels
{
    public interface IPageViewModel
    {
        event EventHandler Close;

        string Name { get; }
    }
}
