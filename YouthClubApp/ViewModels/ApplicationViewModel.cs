using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using YouthClubApp.Helpers;
using YouthClubApp.Models;
using YouthClubApp.Services;

namespace YouthClubApp.ViewModels
{
    public class ApplicationViewModel : ViewModelBase
    {

        private ICommand changePageCommand;
        private IPageViewModel currentPageViewModel;
        private List<IPageViewModel> pageViewModels;

        public ApplicationViewModel()
        {
            var players = new[]
            {
                new PlayerViewModel("Isaac", Key.A, Colors.LightSteelBlue),
                new PlayerViewModel("Lucas", Key.B, Colors.LightSeaGreen),
                new PlayerViewModel("Elsabe", Key.C, Colors.LightPink),
                new PlayerViewModel("Ilana", Key.D, Colors.LightGoldenrodYellow),
                new PlayerViewModel("Johan", Key.E, Colors.Orange),
            };
            // Add available pages
            PageViewModels.Add(new TargetGameViewModel(players, new SoundEffect("Loud_Gunshot.wav"), new GunAimPhysics()));
            PageViewModels.Add(new ScoresViewModel(PageViewModels[0].Name, players));
            PageViewModels.Add(new ScoresViewModel(players));

            for (int i = 0; i < PageViewModels.Count - 1; i++)
            {
                var nextIndex = i + 1;
                PageViewModels[i].Close += (o, e) => ChangeViewModel(PageViewModels[nextIndex]);
            }

            // Set starting page
            CurrentPageViewModel = PageViewModels[0];
        }

        public ICommand ChangePageCommand
        {
            get
            {
                if (changePageCommand == null)
                {
                    changePageCommand = new RelayCommand(
                        p => ChangeViewModel((IPageViewModel)p),
                        p => p is IPageViewModel);
                }

                return changePageCommand;
            }
        }

        public List<IPageViewModel> PageViewModels
        {
            get
            {
                if (pageViewModels == null)
                    pageViewModels = new List<IPageViewModel>();

                return pageViewModels;
            }
        }

        public IPageViewModel CurrentPageViewModel
        {
            get
            {
                return currentPageViewModel;
            }
            set
            {
                if (currentPageViewModel != value)
                {
                    currentPageViewModel = value;
                    OnPropertyChanged(nameof(CurrentPageViewModel));
                }
            }
        }

        private void ChangeViewModel(IPageViewModel viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
                PageViewModels.Add(viewModel);

            CurrentPageViewModel = PageViewModels
                .FirstOrDefault(vm => vm == viewModel);
        }

    }
}
