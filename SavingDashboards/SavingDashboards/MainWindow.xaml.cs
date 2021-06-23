using Microsoft.Win32;
using Reveal.Sdk;
using System;
using System.IO;
using System.Windows;

namespace SavingDashboards
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string _defaultDirctory = Path.Combine(Environment.CurrentDirectory, "Dashboards");

        public MainWindow()
        {
            InitializeComponent();

            var filePath = Path.Combine(_defaultDirctory, "Sales.rdash");
            _revealView.Dashboard = new RVDashboard(filePath);
        }

        private async void RevealView_SaveDashboard(object sender, Reveal.Sdk.DashboardSaveEventArgs e)
        {
            if (e.IsSaveAs)
            {
                var saveDialog = new SaveFileDialog()
                {
                    DefaultExt = ".rdash",
                    FileName = e.Name + ".rdash",
                    Filter = "Reveal Dashboard (*.rdash)|*.rdash",
                    InitialDirectory = _defaultDirctory
                };

                if (saveDialog.ShowDialog() == true)
                {
                    using (var stream = new FileStream(saveDialog.FileName, FileMode.Create, FileAccess.Write))
                    {
                        var name = Path.GetFileNameWithoutExtension(saveDialog.FileName);
                        var data = await e.SerializeWithNewName(name);
                        await stream.WriteAsync(data, 0, data.Length);
                    }
                }
            }
            else
            {
                var path = Path.Combine(_defaultDirctory, $"{e.Name}.rdash");
                var data = await e.Serialize();
                using (var output = File.Open(path, FileMode.Open))
                {
                    output.Write(data, 0, data.Length);
                }                
            }

            e.SaveFinished();              
        }
    }
}
