using System;
using System.Reactive.Linq;

using ReactiveUI;

namespace ReactiveUiMahAppsDialog
{
    public class TestDialogViewModel : ReactiveObject
    {
        private readonly Action<TestDialogViewModel, bool> _closeCallback;

        public TestDialogViewModel(Action<TestDialogViewModel, bool> closeCallback)
        {
            _closeCallback = closeCallback;

            var canAccept = this.WhenAnyValue(x => x.Foo, x => x.BarBaz)
                .Select(pair => !String.IsNullOrWhiteSpace(pair.Item1) 
                                && !String.IsNullOrWhiteSpace(pair.Item2));

            Accept = ReactiveCommand.Create(() =>  _closeCallback(this, true), canAccept);

            Reject = ReactiveCommand.Create(() => _closeCallback(this, false));
        }

        private string _foo;


        public string Foo
        {
            get => _foo;
            set => this.RaiseAndSetIfChanged(ref _foo, value);
        }


        private string _barBaz;


        public string BarBaz
        {
            get => _barBaz;
            set => this.RaiseAndSetIfChanged(ref _barBaz, value);
        }

        public ReactiveCommand Accept { get; }
        public ReactiveCommand Reject { get; }
    }
}
