using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
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
                new PlayerViewModel("Isaac", Key.A, 10),
                new PlayerViewModel("Lucas", Key.B, 7),
                new PlayerViewModel("Elsabe", Key.C, 6),
                new PlayerViewModel("Ilana", Key.D, 5),
                new PlayerViewModel("Johan", Key.E, 5),
                new PlayerViewModel("Dad", Key.Enter, 5),
                new PlayerViewModel("Mum", Key.Space, 5)
            };
            //// Add available pages
            PageViewModels.Add(new BadShooterViewModel("Bad Shooter", 90, players, new SoundEffect("Loud_Gunshot.wav"), new GunAimPhysics()));
            PageViewModels.Add(new ScoresViewModel(PageViewModels[0].Name, players));
            PageViewModels.Add(new BadShooterViewModel("Bad Shooter Far", 60, players, new SoundEffect("Loud_Gunshot.wav"), new GunAimPhysics()));
            PageViewModels.Add(new ScoresViewModel(PageViewModels[2].Name, players));
            PageViewModels.Add(new ScoresViewModel(players));
            PageViewModels.Add(new BadShooterViewModel("Bad Shooter, Seriously!", 30, players, new SoundEffect("Loud_Gunshot.wav"), new GunAimPhysics()));
            PageViewModels.Add(new ScoresViewModel(PageViewModels[5].Name, players));
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

        public List<IPageViewModel> PageViewModels
        {
            get
            {
                if (pageViewModels == null)
                {
                    pageViewModels = new List<IPageViewModel>();
                }

                return pageViewModels;
            }
        }

        private void ChangeViewModel(IPageViewModel viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
            {
                PageViewModels.Add(viewModel);
            }

            CurrentPageViewModel = PageViewModels
                .FirstOrDefault(vm => vm == viewModel);
        }
    }
}