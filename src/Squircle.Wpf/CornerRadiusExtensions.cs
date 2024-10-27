using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Squircle.Wpf
{
    public static class CornerRadiusExtensions
    {
        public static Squircle.Core.CornerRadius ToCornerRadius(this System.Windows.CornerRadius cornerRadius)
        {
            return new Squircle.Core.CornerRadius(cornerRadius.TopLeft, cornerRadius.TopRight, cornerRadius.BottomRight, cornerRadius.BottomLeft);
        }

        public static System.Windows.CornerRadius ToCornerRadius(this Squircle.Core.CornerRadius cornerRadius)
        {
            return new System.Windows.CornerRadius(cornerRadius.TopLeft, cornerRadius.TopRight, cornerRadius.BottomRight, cornerRadius.BottomLeft);
        }
    }
}
