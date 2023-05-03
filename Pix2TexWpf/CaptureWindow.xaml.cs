using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Pix2TexWpf
{
    /// <summary>
    /// Interaction logic for CaptureWindow.xaml
    /// </summary>
    public partial class CaptureWindow : Window
    {
        private System.Windows.Point initialMousePosition;
        private Rect capturedRegion;
        private bool isCapturing;
        public event EventHandler<CapturedImageEventArgs> ImageCaptured;

        public CaptureWindow()
        {
            InitializeComponent();

            this.Cursor = Cursors.Cross;
            
            this.Show();
            backgroundGeometry.Rect = new Rect(0, 0, ActualWidth, ActualHeight);

            this.MouseDown += CaptureWindow_MouseDown;
            this.MouseMove += CaptureWindow_MouseMove;
            this.MouseUp += CaptureWindow_MouseUp;
        }

        private void CaptureWindow_Closing(object? sender, CancelEventArgs e)
        {
            
        }

        private void CaptureWindow_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isCapturing = false;

            using (Bitmap bitmap = new Bitmap((int)capturedRegion.Width, (int)capturedRegion.Height))
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    graphics.CopyFromScreen((int)capturedRegion.X - 6, (int)capturedRegion.Y - 7, 0, 0, bitmap.Size);
                }

                CapturedImageEventArgs args = new CapturedImageEventArgs(bitmap);
                ImageCaptured?.Invoke(this, args);
            }
            this.Close();
        }

        private void CaptureWindow_MouseMove(object sender, MouseEventArgs e)
        {
            if (isCapturing)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    capturedRegion.X = Math.Min(e.GetPosition(this).X, initialMousePosition.X);
                    capturedRegion.Y = Math.Min(e.GetPosition(this).Y, initialMousePosition.Y);
                    capturedRegion.Width = Math.Abs(e.GetPosition(this).X - initialMousePosition.X);
                    capturedRegion.Height = Math.Abs(e.GetPosition(this).Y - initialMousePosition.Y);

                    excludeGeometry.Rect = capturedRegion;
                }
                else
                {
                    isCapturing = false;
                    this.Close();
                }
            }
        }

        private void CaptureWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isCapturing = true;
            initialMousePosition = e.GetPosition(this);
            capturedRegion = new Rect(initialMousePosition, initialMousePosition);
        }
    }
}
