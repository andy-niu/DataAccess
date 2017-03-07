using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Drawing.Drawing2D;
using System.Configuration;

namespace Test
{
    public class ThumbsImage
    {
        /// <summary>
        /// 是否覆盖已存在的目标文件
        /// </summary>
        public bool IfBestrow
        {
            get;
            set;
        }
        /// <summary>
        /// 生成缩略图是否维持比例
        /// </summary>
        public bool LockRatio
        {
            get;
            set;
        }
        /// <summary>
        /// 图片质量（1-100）
        /// </summary>
        public int Quality
        {
            get;
            set;
        }
        /// <summary>
        /// 初始化
        /// </summary>
        public ThumbsImage()
        {
            this.Quality = 80;
            this.LockRatio = true;
            this.IfBestrow = true;
        }
        /// <summary>
        /// 生成缩略图(输入路径,输出路径,最大宽度,最大高度)
        /// </summary>
        /// <param name="inPath">源图片路径</param>
        /// <param name="outPath">输出图片路径</param>
        /// <param name="maxWidth">最大宽度</param>
        /// <param name="maxHeight">最大高度</param>
        public void SetThumbs(string inPath, string outPath, int maxWidth, int maxHeight)
        {
            System.Drawing.Image originalImage = System.Drawing.Image.FromFile(inPath);
            int x, y, w, h;
            int twidth = maxWidth;
            int height = maxHeight;
            if (originalImage.Width > twidth && originalImage.Height > height)
            {
                w = twidth;
                h = twidth * originalImage.Height / originalImage.Width;
                if (h > height)
                {
                    h = height;
                    w = height * originalImage.Width / originalImage.Height;
                    x = (twidth - w) / 2;
                    y = 0;
                }
                else
                {
                    x = 0;
                    y = (height - h) / 2;
                }
            }
            else if (originalImage.Width > twidth)
            {
                w = twidth;
                h = twidth * originalImage.Height / originalImage.Width;
                x = 0;
                y = (height - h) / 2;
            }
            else if (originalImage.Height > height)
            {
                h = height;
                w = height * originalImage.Width / originalImage.Height;
                x = (twidth - w) / 2;
                y = 0;
            }
            else
            {
                w = originalImage.Width;
                h = originalImage.Height;
                x = (twidth - w) / 2;
                y = (height - h) / 2;
            }
            Bitmap bm = new Bitmap(twidth, height);
            Graphics g = Graphics.FromImage(bm);

            // 指定高质量的双三次插值法。执行预筛选以确保高质量的收缩。此模式可产生质量最高的转换图像。
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

           
            // 指定高质量、低速度呈现。
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.Clear(Color.White);
            g.DrawImage(originalImage, new Rectangle(x, y, w, h), 0, 0, originalImage.Width, originalImage.Height, GraphicsUnit.Pixel);

            //这是用来提高质量的重点
            long[] quality = new long[1];
            quality[0] = 100;

            System.Drawing.Imaging.EncoderParameters encoderParams = new System.Drawing.Imaging.EncoderParameters();
            System.Drawing.Imaging.EncoderParameter encoderParam = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            encoderParams.Param[0] = encoderParam;
            ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();//获得包含有关内置图像编码解码器的信息的ImageCodecInfo 对象。
            ImageCodecInfo jpegICI = null;
            for (int i = 0; i < arrayICI.Length; i++)
            {
                if (arrayICI[i].FormatDescription.Equals("JPEG"))
                {
                    jpegICI = arrayICI[i];//设置JPEG编码
                    break;
                }
            }
            if (jpegICI != null)
            {
                bm.Save(outPath, jpegICI, encoderParams);
            }

            bm.Dispose();
            originalImage.Dispose();
            g.Dispose();
        }

        /// <summary>
        /// 返回保持比例的缩略图大小(输入宽度，输入高度，输出宽度，输出高度)
        /// </summary>
        /// <param name="InWidth"></param>
        /// <param name="InHeight"></param>
        /// <param name="OutWidth"></param>
        /// <param name="OutHeight"></param>
        /// <returns></returns>
        private Size ThumbNewSize(double InWidth, double InHeight, double OutWidth, double OutHeight)
        {
            int[] Writes = { 0, 0 };
            double i;
            if (InWidth / InHeight > OutWidth / OutHeight)
            {
                if (InWidth > OutWidth)
                {
                    Writes[0] = (int)(OutWidth);
                    i = InWidth / OutWidth;
                    i = InHeight / i;
                    Writes[1] = (int)(i);
                }
                else
                {
                    Writes[0] = (int)(InWidth);
                    Writes[1] = (int)(InHeight);
                }
            }
            else
            {
                if (InHeight > OutHeight)
                {
                    i = InHeight / OutHeight;
                    i = InWidth / i;
                    Writes[0] = (int)(i);
                    Writes[1] = (int)(OutHeight);
                }
                else
                {
                    Writes[0] = (int)(InWidth);
                    Writes[1] = (int)(InHeight);
                }
            }
            return new Size(Writes[0], Writes[1]);
        }

        /// <summary>
        /// 获取缩略后的图像在标准缩略图218×228中的位置
        /// </summary>
        /// <param name="newSize"></param>
        /// <param name="maxWidth"></param>
        /// <param name="maxHeight"></param>
        /// <returns></returns>
        private Point SetPointThumbPic(Size newSize, int maxWidth, int maxHeight)
        {
            int thumbWidth = newSize.Width;//缩略图宽
            int thumbHeight = newSize.Height;//缩略图高
            Point SitePoint = new Point(0, 0);
            if (thumbHeight < maxHeight)
            {
                SitePoint.X = 0;
                SitePoint.Y = (int)(maxHeight - thumbHeight) / 2;
            }
            else if (thumbWidth < maxWidth)
            {
                SitePoint.Y = 0;
                SitePoint.X = (int)(maxWidth - thumbWidth) / 2;
            }
            return SitePoint;
        }


        #region 自定义裁剪并缩放
        /// 指定长宽裁剪
        /// 按模版比例最大范围的裁剪图片并缩放至模版尺寸
        public void CutForCustom(string inPath, string outPath, int maxWidth, int maxHeight)
        {
            //从文件获取原始图片，并使用流中嵌入的颜色管理信息
            System.Drawing.Image initImage = System.Drawing.Image.FromFile(inPath);

            #region 处理质量和图片编码
            //关键质量控制
            EncoderParameters ep = new EncoderParameters();
            long[] _quality = new long[1];
            _quality[0] = this.Quality;
            ep.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, _quality);

            //获取系统编码类型数组,包含了jpeg,bmp,png,gif,tiff
            ImageCodecInfo[] icis = ImageCodecInfo.GetImageEncoders();
            ImageCodecInfo ici = null;
            foreach (ImageCodecInfo i in icis)
            {
                if (i.MimeType == "image/jpeg" || i.MimeType == "image/bmp" || i.MimeType == "image/png" || i.MimeType == "image/gif")
                {
                    ici = i;
                }
            }
            #endregion

            //原图宽高均小于模版，不作处理，直接保存
            if (initImage.Width <= maxWidth && initImage.Height <= maxHeight)
            {
                initImage.Save(outPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            else
            {
                //模版的宽高比例
                double templateRate = (double)maxWidth / maxHeight;
                //原图片的宽高比例
                double initRate = (double)initImage.Width / initImage.Height;

                //原图与模版比例相等，直接缩放
                if (templateRate == initRate)
                {
                    //按模版大小生成最终图片
                    System.Drawing.Image templateImage = new System.Drawing.Bitmap(maxWidth, maxHeight);
                    System.Drawing.Graphics templateG = System.Drawing.Graphics.FromImage(templateImage);
                    templateG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                    templateG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    templateG.Clear(Color.White);
                    templateG.DrawImage(initImage, new System.Drawing.Rectangle(0, 0, maxWidth, maxHeight), new System.Drawing.Rectangle(0, 0, initImage.Width, initImage.Height), System.Drawing.GraphicsUnit.Pixel);
                    templateImage.Save(outPath, System.Drawing.Imaging.ImageFormat.Jpeg);
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
                    System.Drawing.Image templateImage = new System.Drawing.Bitmap(maxWidth, maxHeight);
                    System.Drawing.Graphics templateG = System.Drawing.Graphics.FromImage(templateImage);
                    templateG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                    templateG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    templateG.Clear(Color.White);
                    templateG.DrawImage(pickedImage, new System.Drawing.Rectangle(0, 0, maxWidth, maxHeight), new System.Drawing.Rectangle(0, 0, pickedImage.Width, pickedImage.Height), System.Drawing.GraphicsUnit.Pixel);

                    //保存缩略图
                    if (ici != null)
                    {
                        templateImage.Save(outPath, ici, ep);
                    }
                    else
                    {
                        templateImage.Save(outPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }

                    //释放资源
                    templateG.Dispose();
                    templateImage.Dispose();

                    pickedG.Dispose();
                    pickedImage.Dispose();
                }
            }
            //释放资源
            initImage.Dispose();
        }
        #endregion

        #region 等比缩放
        // 图片等比缩放
        public void ZoomAuto(string inPath, string outPath, System.Double targetWidth, System.Double targetHeight, string watermarkText, string watermarkImage)
        {
            //创建目录
            string dir = Path.GetDirectoryName(outPath);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            //原始图片（获取原始图片创建对象，并使用流中嵌入的颜色管理信息）
            System.Drawing.Image initImage = System.Drawing.Image.FromFile(inPath);

            //原图宽高均小于模版，不作处理，直接保存
            if (initImage.Width <= targetWidth && initImage.Height <= targetHeight)
            {
                //文字水印
                if (watermarkText != "")
                {
                    using (System.Drawing.Graphics gWater = System.Drawing.Graphics.FromImage(initImage))
                    {
                        System.Drawing.Font fontWater = new Font("黑体", 10);
                        System.Drawing.Brush brushWater = new SolidBrush(Color.White);
                        gWater.DrawString(watermarkText, fontWater, brushWater, 10, 10);
                        gWater.Dispose();
                    }
                }

                //透明图片水印
                if (watermarkImage != "")
                {
                    if (File.Exists(watermarkImage))
                    {
                        //获取水印图片
                        using (System.Drawing.Image wrImage = System.Drawing.Image.FromFile(watermarkImage))
                        {
                            //水印绘制条件：原始图片宽高均大于或等于水印图片
                            if (initImage.Width >= wrImage.Width && initImage.Height >= wrImage.Height)
                            {
                                Graphics gWater = Graphics.FromImage(initImage);

                                //透明属性
                                ImageAttributes imgAttributes = new ImageAttributes();
                                ColorMap colorMap = new ColorMap();
                                colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
                                colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
                                ColorMap[] remapTable = { colorMap };
                                imgAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

                                float[][] colorMatrixElements = { 
                                   new float[] {1.0f,  0.0f,  0.0f,  0.0f, 0.0f},
                                   new float[] {0.0f,  1.0f,  0.0f,  0.0f, 0.0f},
                                   new float[] {0.0f,  0.0f,  1.0f,  0.0f, 0.0f},
                                   new float[] {0.0f,  0.0f,  0.0f,  0.5f, 0.0f},//透明度:0.5
                                   new float[] {0.0f,  0.0f,  0.0f,  0.0f, 1.0f}
                                };

                                ColorMatrix wmColorMatrix = new ColorMatrix(colorMatrixElements);
                                imgAttributes.SetColorMatrix(wmColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                                gWater.DrawImage(wrImage, new Rectangle(initImage.Width - wrImage.Width, initImage.Height - wrImage.Height, wrImage.Width, wrImage.Height), 0, 0, wrImage.Width, wrImage.Height, GraphicsUnit.Pixel, imgAttributes);

                                gWater.Dispose();
                            }
                            wrImage.Dispose();
                        }
                    }
                }

                //保存
                initImage.Save(outPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            else
            {
                //缩略图宽、高计算
                double newWidth = initImage.Width;
                double newHeight = initImage.Height;

                //宽大于高或宽等于高（横图或正方）
                if (initImage.Width > initImage.Height || initImage.Width == initImage.Height)
                {
                    //如果宽大于模版
                    if (initImage.Width > targetWidth)
                    {
                        //宽按模版，高按比例缩放
                        newWidth = targetWidth;
                        newHeight = initImage.Height * (targetWidth / initImage.Width);
                    }
                }
                //高大于宽（竖图）
                else
                {
                    //如果高大于模版
                    if (initImage.Height > targetHeight)
                    {
                        //高按模版，宽按比例缩放
                        newHeight = targetHeight;
                        newWidth = initImage.Width * (targetHeight / initImage.Height);
                    }
                }

                //生成新图
                //新建一个bmp图片
                System.Drawing.Image newImage = new System.Drawing.Bitmap((int)newWidth, (int)newHeight);
                //新建一个画板
                System.Drawing.Graphics newG = System.Drawing.Graphics.FromImage(newImage);

                //设置质量
                newG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                newG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                //置背景色
                newG.Clear(Color.White);
                //画图
                newG.DrawImage(initImage, new System.Drawing.Rectangle(0, 0, newImage.Width, newImage.Height), new System.Drawing.Rectangle(0, 0, initImage.Width, initImage.Height), System.Drawing.GraphicsUnit.Pixel);

                //文字水印
                if (watermarkText != "")
                {
                    using (System.Drawing.Graphics gWater = System.Drawing.Graphics.FromImage(newImage))
                    {
                        System.Drawing.Font fontWater = new Font("宋体", 10);
                        System.Drawing.Brush brushWater = new SolidBrush(Color.White);
                        gWater.DrawString(watermarkText, fontWater, brushWater, 10, 10);
                        gWater.Dispose();
                    }
                }

                //透明图片水印
                if (watermarkImage != "")
                {
                    if (File.Exists(watermarkImage))
                    {
                        //获取水印图片
                        using (System.Drawing.Image wrImage = System.Drawing.Image.FromFile(watermarkImage))
                        {
                            //水印绘制条件：原始图片宽高均大于或等于水印图片
                            if (newImage.Width >= wrImage.Width && newImage.Height >= wrImage.Height)
                            {
                                Graphics gWater = Graphics.FromImage(newImage);

                                //透明属性
                                ImageAttributes imgAttributes = new ImageAttributes();
                                ColorMap colorMap = new ColorMap();
                                colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
                                colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
                                ColorMap[] remapTable = { colorMap };
                                imgAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

                                float[][] colorMatrixElements = { 
                                   new float[] {1.0f,  0.0f,  0.0f,  0.0f, 0.0f},
                                   new float[] {0.0f,  1.0f,  0.0f,  0.0f, 0.0f},
                                   new float[] {0.0f,  0.0f,  1.0f,  0.0f, 0.0f},
                                   new float[] {0.0f,  0.0f,  0.0f,  0.5f, 0.0f},//透明度:0.5
                                   new float[] {0.0f,  0.0f,  0.0f,  0.0f, 1.0f}
                                };

                                ColorMatrix wmColorMatrix = new ColorMatrix(colorMatrixElements);
                                imgAttributes.SetColorMatrix(wmColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                                gWater.DrawImage(wrImage, new Rectangle(newImage.Width - wrImage.Width, newImage.Height - wrImage.Height, wrImage.Width, wrImage.Height), 0, 0, wrImage.Width, wrImage.Height, GraphicsUnit.Pixel, imgAttributes);
                                gWater.Dispose();
                            }
                            wrImage.Dispose();
                        }
                    }
                }

                //保存缩略图
                newImage.Save(outPath, System.Drawing.Imaging.ImageFormat.Jpeg);

                //释放资源
                newG.Dispose();
                newImage.Dispose();
                initImage.Dispose();
            }
        }

        #endregion


        #region 压缩图片
        public bool GetPicThumbnail(string sFile, string outPath, int flag)
        {
            System.Drawing.Image iSource = System.Drawing.Image.FromFile(sFile);
            ImageFormat tFormat = iSource.RawFormat;

            //以下代码为保存图片时，设置压缩质量  
            EncoderParameters ep = new EncoderParameters();
            long[] qy = new long[1];
            qy[0] = flag;//设置压缩的比例1-100  
            EncoderParameter eParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qy);
            ep.Param[0] = eParam;
            try
            {
                ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo jpegICIinfo = null;
                for (int x = 0; x < arrayICI.Length; x++)
                {
                    if (arrayICI[x].FormatDescription.Equals("JPEG"))
                    {
                        jpegICIinfo = arrayICI[x];
                        break;
                    }
                }
                if (jpegICIinfo != null)
                {
                    iSource.Save(outPath, jpegICIinfo, ep);//dFile是压缩后的新路径  
                }
                else
                {
                    iSource.Save(outPath, tFormat);
                }
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                iSource.Dispose();
                iSource.Dispose();
            }
        }
        #endregion

        #region
        public void GetCroppedImage(string inPath, string outPath, int maxWidth, int maxHeight)
        {

            using (var imgOriginal = System.Drawing.Image.FromFile(inPath))
            {
                ImageFormat tFormat = imgOriginal.RawFormat;
                //get original width and height of the incoming image
                var originalWidth = imgOriginal.Width; // 1000
                var originalHeight = imgOriginal.Height; // 800

                //裁剪对象
                System.Drawing.Image pickedImage = null;
                System.Drawing.Graphics pickedG = null;

                //定位
                Rectangle fromR = new Rectangle(0, 0, 0, 0);//原图裁剪定位
                Rectangle toR = new Rectangle(0, 0, 0, 0);//目标定位

                //模版的宽高比例
                double templateRate = (double)maxWidth / maxHeight;
                //原图片的宽高比例
                double initRate = (double)imgOriginal.Width / imgOriginal.Height;
                //宽为标准进行裁剪
                if (templateRate > initRate)
                {
                    //裁剪对象实例化
                    pickedImage = new System.Drawing.Bitmap(imgOriginal.Width, (int)System.Math.Floor(imgOriginal.Width / templateRate));
                    pickedG = System.Drawing.Graphics.FromImage(pickedImage);

                    //裁剪源定位
                    fromR.X = 0;
                    fromR.Y = (int)System.Math.Floor((imgOriginal.Height - imgOriginal.Width / templateRate) / 2);
                    fromR.Width = imgOriginal.Width;
                    fromR.Height = (int)System.Math.Floor(imgOriginal.Width / templateRate);

                    //裁剪目标定位
                    toR.X = 0;
                    toR.Y = 0;
                    toR.Width = imgOriginal.Width;
                    toR.Height = (int)System.Math.Floor(imgOriginal.Width / templateRate);
                }
                //高为标准进行裁剪
                else
                {
                    pickedImage = new System.Drawing.Bitmap((int)System.Math.Floor(imgOriginal.Height * templateRate), imgOriginal.Height);
                    pickedG = System.Drawing.Graphics.FromImage(pickedImage);

                    fromR.X = (int)System.Math.Floor((imgOriginal.Width - imgOriginal.Height * templateRate) / 2);
                    fromR.Y = 0;
                    fromR.Width = (int)System.Math.Floor(imgOriginal.Height * templateRate);
                    fromR.Height = imgOriginal.Height;

                    toR.X = 0;
                    toR.Y = 0;
                    toR.Width = (int)System.Math.Floor(imgOriginal.Height * templateRate);
                    toR.Height = imgOriginal.Height;
                }

                //设置质量
                pickedG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                pickedG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                //裁剪
                pickedG.DrawImage(imgOriginal, toR, fromR, System.Drawing.GraphicsUnit.Pixel);


                //按模版大小生成最终图片
                using (var resizedBmp = new System.Drawing.Bitmap(maxWidth, maxHeight))
                {
                    using (var graphics = Graphics.FromImage((Image)resizedBmp))
                    {
                        graphics.InterpolationMode = InterpolationMode.Default;
                        graphics.DrawImage(imgOriginal, 0, 0, maxWidth, maxHeight);
                    }


                    using (System.Drawing.Graphics templateG = System.Drawing.Graphics.FromImage(resizedBmp))
                    {
                        templateG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                        templateG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                        templateG.Clear(Color.White);
                        var rectangle = new Rectangle(0, 0, maxWidth, maxHeight);

                        //templateG.DrawImage(pickedImage, new System.Drawing.Rectangle(0, 0, maxWidth, maxHeight), new System.Drawing.Rectangle(0, 0, pickedImage.Width, pickedImage.Height), System.Drawing.GraphicsUnit.Pixel);


                        //crop
                        using (var croppedBmp = resizedBmp.Clone(rectangle, resizedBmp.PixelFormat))
                        {
                            //get the codec needed
                            var imgCodec = ImageCodecInfo.GetImageEncoders().First(c => c.FormatID == tFormat.Guid);

                            //make a paramater to adjust quality
                            var codecParams = new EncoderParameters(1);

                            //reduce to quality of 80 (from range of 0 (max compression) to 100 (no compression))
                            codecParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)this.Quality);

                            EncoderParameters ep = new EncoderParameters();
                            long[] qy = new long[1];
                            qy[0] = this.Quality;//设置压缩的比例1-100  
                            EncoderParameter eParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qy);
                            ep.Param[0] = eParam;

                            //save to the memorystream - convert it to an array and send it back as a byte[]
                            croppedBmp.Save(outPath, imgCodec, ep);
                        }

                    }                   
                }
            }
        }
        #endregion
    }
}
