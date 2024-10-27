using Microsoft.UI.Composition;
using Squircle.Core;

namespace Squircle.WinUI3
{
    internal class CompositionPathBuilder : PathBuilder
    {
        public CompositionPath? CreateGeometry()
        {
            var win2dPathBuilder = new Win2DPathBuilder();
            this.CopyTo(win2dPathBuilder);

            using var geometry = win2dPathBuilder.CreateGeometry(null);
            if (geometry != null)
            {
                return new CompositionPath(geometry);
            }
            return null;
        }
    }
}
