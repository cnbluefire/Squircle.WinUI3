using Microsoft.UI.Composition;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Squircle.WinUI3
{
    public static class Clip
    {
        private readonly static object BoxedCornerRadius = new CornerRadius(0);
        private readonly static object BoxedDouble = 0d;

        public static CornerRadius GetCornerRadius(FrameworkElement obj)
        {
            return (CornerRadius)obj.GetValue(CornerRadiusProperty);
        }

        public static void SetCornerRadius(FrameworkElement obj, CornerRadius value)
        {
            obj.SetValue(CornerRadiusProperty, value);
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.RegisterAttached("CornerRadius", typeof(CornerRadius), typeof(Clip), new PropertyMetadata(BoxedCornerRadius, OnPropertyChanged));



        public static double GetCornerSmoothing(FrameworkElement obj)
        {
            return (double)obj.GetValue(CornerSmoothingProperty);
        }

        public static void SetCornerSmoothing(FrameworkElement obj, double value)
        {
            obj.SetValue(CornerSmoothingProperty, value);
        }

        public static readonly DependencyProperty CornerSmoothingProperty =
            DependencyProperty.RegisterAttached("CornerSmoothing", typeof(double), typeof(Clip), new PropertyMetadata(BoxedDouble, OnPropertyChanged));




        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FrameworkElement sender && !Equals(e.NewValue, e.OldValue))
            {
                var cornerRadius = GetCornerRadius(sender);
                var cornerSmoothing = GetCornerSmoothing(sender);

                var props = new SquircleProperties(
                    sender.ActualWidth,
                    sender.ActualHeight,
                    cornerRadius,
                    cornerSmoothing,
                    true);

                sender.SizeChanged -= OnSizeChanged;

                if (SquircleFactory.IsValidProperties(in props, false))
                {
                    sender.SizeChanged += OnSizeChanged;
                }

                UpdateElementCorner(sender, in props);
            }

            static void OnSizeChanged(object _sender, SizeChangedEventArgs _e)
            {
                if (_sender is FrameworkElement _element)
                {
                    var _cornerRadius = GetCornerRadius(_element);
                    var _cornerSmoothing = GetCornerSmoothing(_element);

                    UpdateElementCorner(
                        _element,
                        new SquircleProperties(
                            _e.NewSize.Width,
                            _e.NewSize.Height,
                            _cornerRadius,
                            _cornerSmoothing,
                            true));
                }
            }

            static void UpdateElementCorner(FrameworkElement? _element, in SquircleProperties _props)
            {
                const string SquircleClipCommit = "_SQUIRCLE_CLIP";

                if (_element == null) return;

                var _visual = ElementCompositionPreview.GetElementVisual(_element);
                var _compositor = _visual.Compositor;

                using var _geometry = SquircleFactory.CreateGeometry(null, in _props);
                if (_geometry == null)
                {
                    if (_visual.Clip is CompositionGeometricClip _clip
                        && _visual.Clip.Comment == SquircleClipCommit
                        && _clip.Geometry is CompositionPathGeometry _pathGeometry)
                    {
                        _pathGeometry.Path = null;
                    }
                }
                else
                {
                    var _path = new CompositionPath(_geometry);
                    if (_visual.Clip is CompositionGeometricClip _clip
                        && _visual.Clip.Comment == SquircleClipCommit
                        && _clip.Geometry is CompositionPathGeometry _pathGeometry)
                    {
                        _pathGeometry.Path = _path;
                    }
                    else
                    {
                        _pathGeometry = _compositor.CreatePathGeometry(_path);
                        _clip = _compositor.CreateGeometricClip(_pathGeometry);
                        _clip.Comment = SquircleClipCommit;
                        _visual.Clip = _clip;
                    }
                }
            }
        }

    }
}
