using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;

namespace Test
{
    /// <summary>
    /// Provides various image untilities, such as high quality resizing and the ability to save a JPEG.
    /// </summary>
    public static class ImageHelper
    {
        /// <summary>
        /// A quick lookup for getting image encoders
        /// </summary>
        private static Dictionary<string, ImageCodecInfo> encoders = null;

        /// <summary>
        /// A lock to prevent concurrency issues loading the encoders.
        /// </summary>
        private static object encodersLock = new object();

        /// <summary>
        /// A quick lookup for getting image encoders
        /// </summary>
        public static Dictionary<string, ImageCodecInfo> Encoders
        {
            //get accessor that creates the dictionary on demand
            get
            {
                //if the quick lookup isn't initialised, initialise it
                if (encoders == null)
                {
                    //protect against concurrency issues
                    lock (encodersLock)
                    {
                        //check again, we might not have been the first person to acquire the lock (see the double checked lock pattern)
                        if (encoders == null)
                        {
                            encoders = new Dictionary<string, ImageCodecInfo>();

                            //get all the codecs
                            foreach (ImageCodecInfo codec in ImageCodecInfo.GetImageEncoders())
                            {
                                //add each codec to the quick lookup
                                encoders.Add(codec.MimeType.ToLower(), codec);
                            }
                        }
                    }
                }

                //return the lookup
                return encoders;
            }
        }

        /// <summary>
        /// Resize the image to the specified width and height.
        /// 指定长宽裁剪
        /// 按模版比例最大范围的裁剪图片并缩放至模版尺寸
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public static System.Drawing.Bitmap CustomImage(System.Drawing.Image initImage, int maxWidth, int maxHeight)
        {
            //a holder for the result
            Bitmap result = new Bitmap(maxWidth, maxHeight);
            //set the resolutions the same to avoid cropping due to resolution differences
            result.SetResolution(initImage.HorizontalResolution, initImage.VerticalResolution);

            //use a graphics object to draw the resized image into the bitmap
            using (Graphics graphics = Graphics.FromImage(result))
            {
                //模版的宽高比例
                double templateRate = (double)maxWidth / maxHeight;
                //原图片的宽高比例
                double initRate = (double)initImage.Width / initImage.Height;
                //原图与模版比例相等，直接缩放
                if (templateRate == initRate)
                {
                    //按模版大小生成最终图片
                    graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                    graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    graphics.Clear(Color.White);
                    graphics.DrawImage(initImage, new System.Drawing.Rectangle(0, 0, maxWidth, maxHeight), new System.Drawing.Rectangle(0, 0, initImage.Width, initImage.Height), System.Drawing.GraphicsUnit.Pixel);
                }
                //原图与模版比例不等，裁剪后缩放
                else
                {
                    //裁剪对象
                    System.Drawing.Image pickedImage = null;
                    System.Drawing.Graphics pickedG = null;

                    //定位
                    Rectangle fromR = new Rectangle(0, 0, 0, 0);//原图裁剪定位
                    Rectangle toR = new Rectangle(0, 0, 0, 0);//目标定位

                    //宽为标准进行裁剪
                    if (templateRate > initRate)
                    {
                        //裁剪对象实例化
                        pickedImage = new System.Drawing.Bitmap(initImage.Width, (int)System.Math.Floor(initImage.Width / templateRate));
                        pickedG = System.Drawing.Graphics.FromImage(pickedImage);

                        //裁剪源定位
                        fromR.X = 0;
                        fromR.Y = (int)System.Math.Floor((initImage.Height - initImage.Width / templateRate) / 2);
                        fromR.Width = initImage.Width;
                        fromR.Height = (int)System.Math.Floor(initImage.Width / templateRate);

                        //裁剪目标定位
                        toR.X = 0;
                        toR.Y = 0;
                        toR.Width = initImage.Width;
                        toR.Height = (int)System.Math.Floor(initImage.Width / templateRate);
                    }
                    //高为标准进行裁剪
                    else
                    {
                        pickedImage = new System.Drawing.Bitmap((int)System.Math.Floor(initImage.Height * templateRate), initImage.Height);
                        pickedG = System.Drawing.Graphics.FromImage(pickedImage);

                        fromR.X = (int)System.Math.Floor((initImage.Width - initImage.Height * templateRate) / 2);
                        fromR.Y = 0;
                        fromR.Width = (int)System.Math.Floor(initImage.Height * templateRate);
                        fromR.Height = initImage.Height;

                        toR.X = 0;
                        toR.Y = 0;
                        toR.Width = (int)System.Math.Floor(initImage.Height * templateRate);
                        toR.Height = initImage.Height;
                    }

                    //设置质量
                    pickedG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    pickedG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                    //裁剪
                    pickedG.DrawImage(initImage, toR, fromR, System.Drawing.GraphicsUnit.Pixel);

                    //按模版大小生成最终图片
                    graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                    graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    graphics.Clear(Color.Transparent);
                    graphics.DrawImage(pickedImage, new System.Drawing.Rectangle(0, 0, maxWidth, maxHeight), new System.Drawing.Rectangle(0, 0, pickedImage.Width, pickedImage.Height), System.Drawing.GraphicsUnit.Pixel);

                    //释放资源
                    pickedImage.Dispose();
                    pickedG.Dispose();
                }
            }

            //return the resulting bitmap
            return result;
        }

        /// <summary>
        /// Resize the image to the specified width and height.
        /// 图片等比缩放
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public static System.Drawing.Bitmap ZoomAutoImage(System.Drawing.Image initImage, int maxWidth, int maxHeight)
        {
            //a holder for the result
            Bitmap result = new Bitmap(maxWidth, maxHeight);
            //set the resolutions the same to avoid cropping due to resolution differences
            result.SetResolution(initImage.HorizontalResolution, initImage.VerticalResolution);

            //use a graphics object to draw the resized image into the bitmap
            using (Graphics graphics = Graphics.FromImage(result))
            {
                //模版的宽高比例
                double templateRate = (double)maxWidth / maxHeight;
                //原图片的宽高比例
                double initRate = (double)initImage.Width / initImage.Height;
                //原图与模版比例相等，直接缩放
                if (templateRate == initRate)
                {
                    //按模版大小生成最终图片
                    graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                    graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    graphics.Clear(Color.White);
                    graphics.DrawImage(initImage, new System.Drawing.Rectangle(0, 0, maxWidth, maxHeight), new System.Drawing.Rectangle(0, 0, initImage.Width, initImage.Height), System.Drawing.GraphicsUnit.Pixel);
                }
                //原图与模版比例不等，裁剪后缩放
                else
                {
                    int x, y, w, h;
                    int twidth = maxWidth;
                    int height = maxHeight;
                    if (initImage.Width > twidth && initImage.Height > height)
                    {
                        w = twidth;
                        h = twidth * initImage.Height / initImage.Width;
                        if (h > height)
                        {
                            h = height;
                            w = height * initImage.Width / initImage.Height;
                            x = (twidth - w) / 2;
                            y = 0;
                        }
                        else
                        {
                            x = 0;
                            y = (height - h) / 2;
                        }
                    }
                    else if (initImage.Width > twidth)
                    {
                        w = twidth;
                        h = twidth * initImage.Height / initImage.Width;
                        x = 0;
                        y = (height - h) / 2;
                    }
                    else if (initImage.Height > height)
                    {
                        h = height;
                        w = height * initImage.Width / initImage.Height;
                        x = (twidth - w) / 2;
                        y = 0;
                    }
                    else
                    {
                        w = initImage.Width;
                        h = initImage.Height;
                        x = (twidth - w) / 2;
                        y = (height - h) / 2;
                    }

                    //按模版大小生成最终图片
                    graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                    graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    graphics.Clear(Color.White);
                    graphics.DrawImage(initImage, new Rectangle(x, y, w, h), 0, 0, initImage.Width, initImage.Height, GraphicsUnit.Pixel);

                }
            }
            //return the resulting bitmap
            return result;
        }

        /// <summary> 
        /// Saves an image as a jpeg image, with the given quality 
        /// </summary> 
        /// <param name="path">Path to which the image would be saved.</param> 
        /// <param name="quality">An integer from 0 to 100, with 100 being the 
        /// highest quality</param> 
        /// <exception cref="ArgumentOutOfRangeException">
        /// An invalid value was entered for image quality.
        /// </exception>
        public static void SaveJpeg(string path, Image image, int quality = 80)
        {
            //ensure the quality is within the correct range
            if ((quality < 0) || (quality > 100))
            {
                //create the error message
                string error = string.Format("Jpeg image quality must be between 0 and 100, with 100 being the highest quality.  A value of {0} was specified.", quality);
                //throw a helpful exception
                throw new ArgumentOutOfRangeException(error);
            }

            //create an encoder parameter for the image quality
            EncoderParameter qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            //get the jpeg codec
            ImageCodecInfo jpegCodec = GetEncoderInfo("image/jpeg");

            //create a collection of all parameters that we will pass to the encoder
            EncoderParameters encoderParams = new EncoderParameters(1);
            //set the quality parameter for the codec
            encoderParams.Param[0] = qualityParam;
            //save the image using the codec and the parameters
            image.Save(path, jpegCodec, encoderParams);
        }

        /// <summary> 
        /// Returns the image codec with the given mime type 
        /// </summary> 
        public static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            //do a case insensitive search for the mime type
            string lookupKey = mimeType.ToLower();

            //the codec to return, default to null
            ImageCodecInfo foundCodec = null;

            //if we have the encoder, get it to return
            if (Encoders.ContainsKey(lookupKey))
            {
                //pull the codec from the lookup
                foundCodec = Encoders[lookupKey];
            }

            return foundCodec;
        }
    }
}
