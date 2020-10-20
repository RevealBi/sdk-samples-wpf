using Microsoft.Win32;
using Reveal.Sdk;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DataSource_OneDrive
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            RevealSdkSettings.AuthenticationProvider = new SampleAuthenticationProvider();

            revealView.DataSourcesRequested += RevealView_DataSourcesRequested;
            revealView.SaveDashboard += RevealView_SaveDashboard;
        }

        private async void RevealView_SaveDashboard(object sender, DashboardSaveEventArgs e)
        {
            var dlg = new SaveFileDialog();
            dlg.DefaultExt = ".rdash";
            dlg.FileName = e.Name + ".rdash";
            if (dlg.ShowDialog() == true)
            {
                var stream = new FileStream(dlg.FileName, FileMode.OpenOrCreate, FileAccess.Write);
                var data = await e.SerializeWithNewName(System.IO.Path.GetFileNameWithoutExtension(dlg.FileName));
                await stream.WriteAsync(data, 0, data.Length);
                stream.Close();
            }
            e.SaveFinished();
        }

        private void RevealView_DataSourcesRequested(object sender, DataSourcesRequestedEventArgs e)
        {
            var dataSources = new List<RVDashboardDataSource>();
            var items = new List<RVDataSourceItem>();

            var oneDrive = new RVOneDriveDataSource();
            oneDrive.Title = "One Drive";
            oneDrive.Id = "onedrive";
            dataSources.Add(oneDrive);

            //use this code to show an Excel file from OneDrive, oneDriveFileId needs to be set to the id of the file in OneDrive
            /*
            var oneDriveFileId = "";
            var oneDriveItem = new RVOneDriveDataSourceItem(oneDrive);
            oneDriveItem.Identifier = "__" + oneDriveFileId;
            oneDriveItem.Id = oneDriveItem.Identifier;

            var excelItem = new RVExcelDataSourceItem(oneDriveItem);
            excelItem.Title = "HR_Dataset 2016.xlsx";
            excelItem.Id = oneDriveItem.Id + "_excel";
            items.Add(excelItem);
            */

            e.Callback(new RevealDataSources(dataSources, items, true));
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog();
            dlg.DefaultExt = ".rdash";
            if (dlg.ShowDialog() == true)
            {
                var stream = new FileStream(dlg.FileName, FileMode.Open, FileAccess.Read);
                var dashboard = new RVDashboard(stream);
                revealView.StartInEditMode = true;
                revealView.Dashboard = dashboard;
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
