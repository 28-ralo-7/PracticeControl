using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Drawing;
using Plugin.Media;

namespace PracticeControl.XamarinClient.Helpers
{
    public class ImageConvert
    {
        public byte[] ImageToByteArray(Image imageIn)
        {
            var photo = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions() { });

            byte[] imageArray = null;

            using (MemoryStream memory = new MemoryStream())
            {

                Stream stream = photo.GetStream();
                stream.CopyTo(memory);
                imageArray = memory.ToArray();
            }
        }
    }

}
