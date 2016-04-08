using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WPFUtility {
    public static class MiscExtensions {
        public static BitmapImage ToBitmapImage(this Icon icon) {
            return icon?.ToBitmap().ToBitmapImage();
        }

        public static BitmapImage ToBitmapImage(this Bitmap bitmap) {
            if (bitmap == null) return null;
            using(var ms = new MemoryStream()) {
                bitmap.Save(ms, ImageFormat.Png);
                ms.Seek(0, SeekOrigin.Begin);
                var img = new BitmapImage();
                img.BeginInit();
                img.CacheOption = BitmapCacheOption.OnLoad;
                img.StreamSource = ms;
                img.EndInit();
                return img;
            }
        }

        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject([In] IntPtr hObject);
        public static BitmapSource ToBitmapSource(this Bitmap bmp)
        {
            var handle = bmp.GetHbitmap();
            try {
                return Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            } finally {
                DeleteObject(handle);
            }
        }
    }
}
