using System;
using System.Reactive.Linq;
using System.Threading.Tasks;

using ReactiveUI;

namespace ReactiveUiMahAppsDialog
{
    public class TestViewModel : ReactiveObject
    {
        public TestViewModel()
        {
            ShowDialog = ReactiveCommand.CreateFromTask(DoShowDialog);
        }

        
        public ReactiveCommand ShowDialog { get; }

        public async Task DoShowDialog()
        {
            try
            {
                var fbb = await this.Interaction.Handle("Testing...");

                if (fbb == null)
                {
                    System.Diagnostics.Debug.Print("Rejected");
                }
                else
                {
                    System.Diagnostics.Debug.Print($"Accepted: Foo = '{fbb.Foo}', BarBaz='{fbb.BarBaz}'.");
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.ToString());
            }
        }


        public Interaction<String, FooBarBaz> Interaction { get; } = new Interaction<string, FooBarBaz>();
    }


    public class FooBarBaz
    {
        public string Foo { get; set; }

        public string BarBaz { get; set; }
    }
}
