using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ImageDisplay
{
    /// <summary>
    /// Showing images from a folder in fullscreen window
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<string>? imagePaths;
        private int currentIndex = 0;

        public MainWindow()
        {
            InitializeComponent();
            LoadImagePaths();
            DisplayCurrentImage();

            // Set the WindowState to Maximized
            this.WindowState = WindowState.Maximized;

            // Set the WindowStyle to None
            this.WindowStyle = WindowStyle.None;

            // Optionally, set the Window as topmost
            //this.Topmost = true;
        }

        private void LoadImagePaths()
        {
            // Load image paths from a folder (adjust the path as needed)
            string imagePath = @"C:\Users\johan\source\repos\ImageDisplay\ImageDisplay\images\";
            imagePaths = new List<string>(Directory.GetFiles(imagePath, "*.png"));
        }

        private void DisplayCurrentImage()
        {
            if (imagePaths is not null && currentIndex >= 0 && currentIndex < imagePaths.Count)
            {
                Uri uri = new Uri(imagePaths[currentIndex]);
                BitmapImage bitmap = new BitmapImage(new Uri(imagePaths[currentIndex]));
                BitmapImage stretchedBitmap = StretchImage(uri);
                imageControl.Source = stretchedBitmap;
            }
        }

        private BitmapImage StretchImage(Uri uri)
        {
            try
            {
                // Load the original image (replace with your image path)
                BitmapImage originalImage = new BitmapImage(uri);

                // Calculate new dimensions for 16:9 aspect ratio
                //double desiredWidth = 1920; // Adjust as needed
                //double desiredHeight = 1080; // Adjust as needed

                double originalWidth = originalImage.PixelWidth;
                double originalHeight = originalImage.PixelHeight;

                //double scaleFactor = Math.Min(desiredWidth / originalWidth, desiredHeight / originalHeight);

                //double newWidth = originalWidth * scaleFactor;
                double newWidth = originalWidth * 16;
                //double newHeight = originalHeight * scaleFactor;
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
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading or stretching the image: {ex.Message}");
                return new BitmapImage(uri);
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (imagePaths is not null)
            {
                currentIndex = (currentIndex + 1) % imagePaths.Count;
                DisplayCurrentImage();
            }
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            if (imagePaths is not null)
            {
                currentIndex = (currentIndex - 1 + imagePaths.Count) % imagePaths.Count;
                DisplayCurrentImage();
            }
        }
    }
}