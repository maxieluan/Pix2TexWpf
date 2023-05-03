using System;
using System.Collections.Generic;
using System.Drawing;
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

namespace Pix2TexWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CaptureButton_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;

            CaptureWindow captureWindow = new CaptureWindow();
            captureWindow.ImageCaptured += CaptureWindow_ImageCaptured;
        }

        private void CaptureWindow_ImageCaptured(object? sender, CapturedImageEventArgs e)
        {
            Bitmap bitmap = e.Image;

            // write bitmap to file
            string fileName = "test.png";
            bitmap.Save(fileName);

            this.Visibility = Visibility.Visible;
        }
    }
}
