using System.Diagnostics;
using System.Windows;

using ReactiveUI;

using Splat;

namespace ReactiveUiMahAppsDialog
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            PresentationTraceSources.Refresh();

            PresentationTraceSources.DataBindingSource.Listeners.Add(new ConsoleTraceListener());

            PresentationTraceSources.DataBindingSource.Switch.Level = SourceLevels.Warning | SourceLevels.Error;

            Locator.CurrentMutable.Register(() => new TestView(), typeof(IViewFor<TestViewModel>) );


            Locator.CurrentMutable.Register(() => new TestDialog(), typeof(IViewFor<TestDialogViewModel>));
        }
    }
}
