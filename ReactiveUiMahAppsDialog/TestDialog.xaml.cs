using System.Reactive.Disposables;
using System.Windows.Controls;

using ReactiveUI;

namespace ReactiveUiMahAppsDialog
{
    /// <summary>
    /// Interaction logic for TestDialog.xaml
    /// </summary>
    public partial class TestDialog : UserControl, IViewFor<TestDialogViewModel>
    {
        public TestDialog()
        {
            InitializeComponent();

            this.WhenActivated(d =>
            {
                this.Bind(ViewModel, vm => vm.Foo, view => view.Foo.Text).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.BarBaz, view => view.BarBaz.Text).DisposeWith(d);
                this.BindCommand(ViewModel, vm => vm.Accept, view => view.Accept).DisposeWith(d);
                this.BindCommand(ViewModel, vm => vm.Reject, view => view.Reject).DisposeWith(d);
            });
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (TestDialogViewModel)value;
        }


        public TestDialogViewModel ViewModel { get; set; }
    }
}
