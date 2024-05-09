using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;


namespace ImageDisplay
{
    /// <summary>
    /// Showing images from a folder in fullscreen window
    /// </summary>

    public partial class MainWindow : Window
    {
        // Path to the AI generated image

        //private readonly string imagePath = @"C:\Users\johan\source\repos\images\img.png";
        private readonly string imagePath = @"\\wsl.localhost\Ubuntu\home\johan\code\mazu\mazu-webapp\mazusea\output\img.png";

        private DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();
            InitializeTimer();

            // Set the WindowState to Maximized
            //this.WindowState = WindowState.Maximized;

            // Set the WindowStyle to None
            //this.WindowStyle = WindowStyle.None;

            // Optionally, set the Window as topmost
            //this.Topmost = true;
        }

        private void InitializeTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(2);
            timer.Tick += DisplayImage;
            timer.Start();
        }

        private void DisplayImage(object sender, EventArgs e)
        {
            try
            {
                Uri uri = new Uri(imagePath);

                // Load the original square image
                BitmapImage originalImage = new BitmapImage(uri);

                double originalWidth = originalImage.PixelWidth;
                double originalHeight = originalImage.PixelHeight;

                // Calculate 16:9 ratio from square image
                double newWidth = originalWidth * 16;
                double newHeight = originalHeight * 9;

                // Create a new BitmapImage with adjusted dimensions
                BitmapImage stretchedImage = new BitmapImage();
                stretchedImage.BeginInit();
                stretchedImage.UriSource = uri;
                stretchedImage.DecodePixelWidth = (int)newWidth;
                stretchedImage.DecodePixelHeight = (int)newHeight;
                stretchedImage.EndInit();

                imageControl.Source = stretchedImage;
            }
            catch
            {
                Console.WriteLine("No file found");
            }
        }
    }
}