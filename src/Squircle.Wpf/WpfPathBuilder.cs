using Squircle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Squircle.Wpf
{
    public class WpfPathBuilder : PathBuilder
    {
        public Geometry? CreateGeometry(bool isStroked)
        {
            if (Elements.Count == 0) return null;

            var figure = new PathFigure();
            var pathGeometry = new PathGeometry()
            {
                Figures = { figure }
            };

            var currentPoint = Vector2.Zero;

            foreach (var element in Elements)
            {
                if (element is MoveElement moveTo)
                {
                    if (moveTo.IsRelative) currentPoint += moveTo.MoveTo;
                    else currentPoint = moveTo.MoveTo;
                }
                else if (element is LineElement lineTo)
                {
                    if (lineTo.IsRelative) currentPoint = lineTo.LineTo;
                    else currentPoint = lineTo.LineTo;

                    figure.Segments.Add(new LineSegment(new Point(currentPoint.X, currentPoint.Y), isStroked));
                }
                else if (element is CubicBezierElement cubicBezierTo)
                {
                    var cp1 = cubicBezierTo.ControlPoint1;
                    var cp2 = cubicBezierTo.ControlPoint2;
                    var ep = cubicBezierTo.EndPoint;
                    if (cubicBezierTo.IsRelative)
                    {
                        cp1 += currentPoint;
                        cp2 += currentPoint;
                        ep += currentPoint;
                    }
                    currentPoint = ep;

                    figure.Segments.Add(new BezierSegment(
                        new Point(cp1.X, cp1.Y),
                        new Point(cp2.X, cp2.Y),
                        new Point(ep.X, ep.Y),
                        isStroked));
                }
                else if (element is ArcElement arcTo)
                {
                    if (arcTo.IsRelative) currentPoint += arcTo.EndPoint;
                    else currentPoint = arcTo.EndPoint;

                    figure.Segments.Add(new ArcSegment(
                        new Point(currentPoint.X, currentPoint.Y),
                        new Size(arcTo.RadiusX, arcTo.RadiusY),
                        arcTo.Angle,
                        arcTo.IsLargeFlag,
                        arcTo.SweepDirectionClockwise ? SweepDirection.Clockwise : SweepDirection.Counterclockwise,
                        isStroked));
                }
            }


            return pathGeometry;
        }
    }
}
