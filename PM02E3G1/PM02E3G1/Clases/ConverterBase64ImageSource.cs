using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace PM02E3G1.Clases
{
    internal class ConverterBase64ImageSource : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string base64Image = null;
            Byte[] imageBytes = null;

            if (value != null)
            {
                base64Image = value.ToString();
                Regex regex = new Regex(@"^[\w/\:.-]+;base64,");
                base64Image = regex.Replace(base64Image, string.Empty);

                if (base64Image == null)
                    return null;

                // Convert base64Image from string to byte-array
                imageBytes = System.Convert.FromBase64String(base64Image);

                // Return a new ImageSource

            }
            if (imageBytes == null)
            {
                return null;
            }
            return ImageSource.FromStream(() => { return new MemoryStream(imageBytes); });

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Not implemented as we do not convert back
            throw new NotSupportedException();
        }
    }
}
