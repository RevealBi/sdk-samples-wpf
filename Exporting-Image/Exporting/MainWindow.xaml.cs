using Microsoft.Win32;
using Reveal.Sdk;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Exporting
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var filePath = Path.Combine(Environment.CurrentDirectory, "Dashboards/Sales.rdash");
            _revealView.Dashboard = new RVDashboard(filePath);
        }

        //Override default image export and perform a custom image export
        private void RevealView_ImageExported(object sender, ImageExportedEventArgs e)
        {
            var image = e.Image;

            var saveDialog = new SaveFileDialog()
            {
                DefaultExt = ".png",
                FileName = _revealView.Dashboard.Title + ".png",
            };

            if (saveDialog.ShowDialog() == true)
            {
                SaveImageToFile(image, saveDialog.FileName);
            }
            else
            {
                e.CloseExportDialog = false;
            }
        }

        //Programatic image export
        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            var image = _revealView.ToImage();
            SaveImageToFile(image, Path.Combine(path, $"{_revealView.Dashboard.Title}.png"));
        }

        public static void SaveImageToFile(BitmapSource image, string filePath)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));
                encoder.Save(fileStream);
            }
        }
    }
}
