using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PracticeControl.XamarinClient.Helpers
{
    public class ImageConvert
    {
        public static byte[] ImageToByteArray(FileResult photoFile)
        {
            var bytes = File.ReadAllBytes(photoFile.FullPath);
            return bytes;
        }


        public static ImageSource ConvertByteArrayToImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;

            var image = new Xamarin.Forms.Image();

            using (var mem = new MemoryStream(imageData))
            {
                image.Source = ImageSource.FromStream(() => mem);
            }

            return image.Source;
        }

    }
}
