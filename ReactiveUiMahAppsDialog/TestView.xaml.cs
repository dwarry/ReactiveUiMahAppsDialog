using System;
using System.Reactive.Disposables;
using System.Windows.Controls;

using MahApps.Metro.Controls.Dialogs;

using ReactiveUI;

namespace ReactiveUiMahAppsDialog
{
    /// <summary>
    /// Interaction logic for TestView.xaml
    /// </summary>
    public partial class TestView : UserControl, IViewFor<TestViewModel>
    {
        public TestView()
        {
            InitializeComponent();

            this.WhenActivated(d =>
            {
                this.BindCommand(ViewModel, vm => vm.ShowDialog, view => view.ShowDialog).DisposeWith(d);

                new DialogParticipationRegistration(this).DisposeWith(d);

                this.ViewModel.Interaction.RegisterHandler(async interaction =>
                {
                    var dlg = new CustomDialog {Title = interaction.Input};

                    var dlgvm = new TestDialogViewModel((TestDialogViewModel vm, bool wasAccepted) =>
                        {
                            DialogCoordinator.Instance.HideMetroDialogAsync(this, dlg);

                            if (wasAccepted)
                            {
                                interaction.SetOutput(new DialogData{Foo = vm.Foo, BarBaz = vm.BarBaz});
                            }
                            else
                            {
                                interaction.SetOutput(null);
                            }
                        });

                    dlg.Content = new ViewModelViewHost {ViewModel = dlgvm};

                    await DialogCoordinator.Instance.ShowMetroDialogAsync(this, dlg);

                    await dlg.WaitUntilUnloadedAsync();
                }).DisposeWith(d);

            });
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = value as TestViewModel;
        }


        public TestViewModel ViewModel { get; set; }


        private class DialogParticipationRegistration : IDisposable
        {
            private readonly TestView _view;

            public DialogParticipationRegistration(TestView view)
            {
                _view = view;
                DialogParticipation.SetRegister(view, view);
            }

            public void Dispose()
            {
                DialogParticipation.SetRegister(_view, null);
            }
        }
    }


}
