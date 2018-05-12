using ReactiveUI;

namespace ReactiveUiMahAppsDialog
{
    class MainWindowViewModel : ReactiveObject
    {
        public MainWindowViewModel()
        {
            ViewModel = new TestViewModel();
        }


        private ReactiveObject _viewModel;


        public ReactiveObject ViewModel
        {
            get => _viewModel;
            set => this.RaiseAndSetIfChanged(ref _viewModel, value);
        }
    }
}
