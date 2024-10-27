using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Geometry;
using Squircle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Squircle.WinUI3
{
    public class Win2DPathBuilder : PathBuilder
    {
        public CanvasGeometry? CreateGeometry(ICanvasResourceCreator? resourceCreator)
        {
            if (Elements.Count == 0) return null;

            using var canvasPathBuilder = new CanvasPathBuilder(resourceCreator);
            canvasPathBuilder.BeginFigure(0, 0);

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

                    canvasPathBuilder.AddLine(currentPoint);
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

                    canvasPathBuilder.AddCubicBezier(cp1, cp2, ep);
                }
                else if (element is ArcElement arcTo)
                {
                    if (arcTo.IsRelative) currentPoint += arcTo.EndPoint;
                    else currentPoint = arcTo.EndPoint;

                    canvasPathBuilder.AddArc(
                        currentPoint,
                        (float)arcTo.RadiusX,
                        (float)arcTo.RadiusY,
                        (float)arcTo.Angle,
                        arcTo.SweepDirectionClockwise ? CanvasSweepDirection.Clockwise : CanvasSweepDirection.CounterClockwise,
                        arcTo.IsLargeFlag ? CanvasArcSize.Large : CanvasArcSize.Small);
                }
            }

            canvasPathBuilder.EndFigure(CanvasFigureLoop.Closed);
            return CanvasGeometry.CreatePath(canvasPathBuilder);
        }

    }
}
