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
    }

}
