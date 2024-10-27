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

            PathFigure? figure = null;
            var pathGeometry = new PathGeometry();

            var currentPoint = Vector2.Zero;

            for (int i = 0; i < Elements.Count; i++)
            {
                var element = Elements[i];

                if (element is MoveElement moveTo)
                {
                    if (moveTo.IsRelative) currentPoint += moveTo.MoveTo;
                    else currentPoint = moveTo.MoveTo;

                    if (figure != null) pathGeometry.Figures.Add(figure);
                    figure = new PathFigure()
                    {
                        StartPoint = new Point(currentPoint.X, currentPoint.Y),
                        IsClosed = true
                    };
                }
                else if (element is LineElement lineTo)
                {
                    if (lineTo.IsRelative) currentPoint = lineTo.LineTo;
                    else currentPoint = lineTo.LineTo;

                    (figure ??= new PathFigure() { IsClosed = true }).Segments.Add(new LineSegment(new Point(currentPoint.X, currentPoint.Y), isStroked));
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

                    (figure ??= new PathFigure() { IsClosed = true }).Segments.Add(new BezierSegment(
                        new Point(cp1.X, cp1.Y),
                        new Point(cp2.X, cp2.Y),
                        new Point(ep.X, ep.Y),
                        isStroked));
                }
                else if (element is ArcElement arcTo)
                {
                    if (arcTo.IsRelative) currentPoint += arcTo.EndPoint;
                    else currentPoint = arcTo.EndPoint;

                    (figure ??= new PathFigure() { IsClosed = true }).Segments.Add(new ArcSegment(
                        new Point(currentPoint.X, currentPoint.Y),
                        new Size(arcTo.RadiusX, arcTo.RadiusY),
                        arcTo.Angle,
                        arcTo.IsLargeFlag,
                        arcTo.SweepDirectionClockwise ? SweepDirection.Clockwise : SweepDirection.Counterclockwise,
                        isStroked));
                }
            }

            if (figure != null)
            {
                pathGeometry.Figures.Add(figure);
                return pathGeometry;
            }

            return null;
        }
    }
}
