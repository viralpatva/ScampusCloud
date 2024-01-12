using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace ScampusCloud.Utility
{
    public static class ImageCompressor
    {
        private const int Width = 300;
        private const int Height = 300;

        //public static string REsizeImage { get; private set; }

        //public static async Task<string> GetBase64StringAsync(IFormFile formFile)
        //{
        //    string base64String = "";
        //    byte[] byteImage;
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        await formFile.OpenReadStream().CopyToAsync(ms);
        //        IImageFormat format;
        //        using (var image = Image.Load<Rgba32>(ms.ToArray(), out format))
        //        {
        //            image.Mutate(img => img.Resize(Width, Height));
        //            using (MemoryStream outputStream = new MemoryStream())
        //            {
        //                await image.SaveAsync(outputStream, format);
        //                byteImage = outputStream.ToArray();
        //            }
        //            base64String = Convert.ToBase64String(byteImage);
        //        }
        //    }
        //    return base64String;
        //}

        //public static async Task<string> GetBase64StringAsync(IFormFile formFile, int intWidth, int intHeight)
        //{
        //    string base64String = "";
        //    byte[] byteImage;
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        await formFile.OpenReadStream().CopyToAsync(ms);
        //        IImageFormat format;
        //        using (var image = Image.Load<Rgba32>(ms.ToArray(), out format))
        //        {
        //            //string newSize = ResizeImage(image, intWidth, intHeight);
        //            //    string[] aSize = newSize.Split(",");
        //            //    image.Mutate(img => img.Resize(Convert.ToInt32(aSize[1]), Convert.ToInt32(aSize[0])));
        //            image.Mutate(img => img.Resize(intWidth, intHeight));
        //            using (MemoryStream outputStream = new MemoryStream())
        //            {
        //                await image.SaveAsync(outputStream, format);
        //                byteImage = outputStream.ToArray();
        //            }
        //            base64String = Convert.ToBase64String(byteImage);
        //        }
        //    }
        //    return base64String;
        //}

        //private static string ResizeImage(Image image, int maxWidth, int maxHeight)
        //{
        //    if (image.Width > maxWidth || image.Height > maxHeight)
        //    {
        //        double widthRation = (double)image.Width / (double)maxWidth;
        //        double heightRation = (double)image.Height / (double)maxHeight;
        //        double ration = Math.Max(widthRation, heightRation);
        //        int newWidth = (int)(image.Width / ration);
        //        int newHeight = (int)(image.Height / ration);

        //        return string.Join(",", new[] { newHeight.ToString(), newWidth.ToString() });
        //    }
        //    else
        //    {
        //        return string.Join(",", new[] { image.Height.ToString(), image.Width.ToString() });
        //    }
        //}

        //public static async Task<string> GetResizedImage(byte[] formFile, int intWidth, int intHeight)
        //{
        //    string base64String = "";
        //    byte[] byteImage;
        //    IImageFormat format;
        //    using (var image = Image.Load<Rgba32>(formFile, out format))
        //    {
        //        image.Mutate(img => img.Resize(intWidth, intHeight));
        //        using (MemoryStream outputStream = new MemoryStream())
        //        {
        //            await image.SaveAsync(outputStream, format);
        //            byteImage = outputStream.ToArray();
        //        }
        //        base64String = Convert.ToBase64String(byteImage);
        //    }
        //    return base64String;
        //}

        //public static async Task<string> OriginalBase64String(IFormFile formfile)
        //{
        //    string base64String = "";
        //    if (formfile.Length > 0)
        //    {
        //        using (var ms = new MemoryStream())
        //        {
        //            await formfile.OpenReadStream().CopyToAsync(ms);
        //            var fileBytes = ms.ToArray();
        //            base64String = Convert.ToBase64String(fileBytes);
        //            // act on the Base64 data
        //        }
        //    }
        //    return base64String;
        //}
    }
}