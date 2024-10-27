using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Squircle.WinUI3
{
    public static class CornerRadiusExtensions
    {
        public static Squircle.Core.CornerRadius ToCornerRadius(this Microsoft.UI.Xaml.CornerRadius cornerRadius)
        {
            return new Squircle.Core.CornerRadius(cornerRadius.TopLeft, cornerRadius.TopRight, cornerRadius.BottomRight, cornerRadius.BottomLeft);
        }

        public static Microsoft.UI.Xaml.CornerRadius ToCornerRadius(this Squircle.Core.CornerRadius cornerRadius)
        {
            return new Microsoft.UI.Xaml.CornerRadius(cornerRadius.TopLeft, cornerRadius.TopRight, cornerRadius.BottomRight, cornerRadius.BottomLeft);
        }
    }
}
