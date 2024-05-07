using System.Diagnostics.CodeAnalysis;
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
        // Make sure imagePath points to the directory of AI generated images
        private readonly string imagePath = @"C:\Users\johan\source\repos\ImageDisplay\ImageDisplay\images\";

        private List<string>? imagePaths;
        private int currentIndex = 0;
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
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += DisplayNextImage;
            timer.Start();
        }

        private void LoadImagePaths()
        {
            // Load image paths from a folder
            imagePaths = new List<string>(Directory.GetFiles(imagePath, "*.png"));
        }

        private void DisplayNextImage(object sender, EventArgs e)
        {
            // Acquire all image paths, including newly added
            LoadImagePaths();

            // Display image in the imageControl window
            BitmapImage stretchedBitmap = StretchImage();
            imageControl.Source = stretchedBitmap;

            // Stop iterating currentIndex when at last image in imagePaths
            if (currentIndex < imagePaths.Count - 1)
                currentIndex++;
        }

        private BitmapImage StretchImage()
        {
            Uri uri = new Uri(imagePaths[currentIndex]);

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

            return stretchedImage;
        }
    }
}