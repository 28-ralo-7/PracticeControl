using System.IO;
using Xamarin.Essentials;

namespace PracticeControl.XamarinClient.Helpers
{
    public class ImageConvert
    {
        public static byte[] ImageToByteArray(FileResult photoFile)
        {
            var bytes = File.ReadAllBytes(photoFile.FullPath);
            return bytes;
        }


/*        public static BitmapImage ConvertByteArrayToImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;

            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }
*/
    }

}
