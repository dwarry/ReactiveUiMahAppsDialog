# ReactiveUiMahAppsDialog

This is a minimal project which just demonstrates how to open a custom MahApps dialog using ReactiveUi's Interaction infrastructure. 

## MainWindow / MainWindowViewModel
This is just a MetroWindow shell to host the TestView

## TestDialog / TestDialogViewModel
The custom dialog window with a couple of editable fields. The "OK" button only becomes enabled when both fields contain non-whitespace values.

## TestView / TestViewModel
TestView just contains a button to trigger the dialog. TestViewModel uses ReactiveUi's Interaction class to trigger the dialog and collect the entered data. The TestView declares the handler for this interaction, and uses MahApp's DialogCoordinator to display the dialog. 

The interesting part is in TestView's WhenActivated block in its constructor: 

```csharp
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
```

The important bits:

* The view has to register itself with the DialogParticipation class - the MahApps docs show how to do this from the ViewModel, but I prefer ReactiveUi's approach of keeping this in the View. This is dealt with by the nested `DialogParticipationRegistration` class that also unregisters it when the TestView is unloaded. 
* The content of the Dialog is set to be a `ViewModelViewHost` that is initialized with a `TestDialogViewModel` instance, so that ReactiveUi can wire everything up in the normal way. 
* Once we have asked the DialogCoordinator to show the dialog, we have to explicitly wait for it to be unloaded, otherwise ReactiveUi will throw an `UnhandledInteractionException` because the interaction handler will end before the dialog has been closed. 
* The view is unregistered from the DialogParticipation when its closed; I've added a custom class that implements `IDisposable` that is added to the `CompositeDisposable` passed into WhenActivated. 
