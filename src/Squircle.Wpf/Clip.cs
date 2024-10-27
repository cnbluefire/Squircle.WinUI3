using Squircle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Squircle.Wpf
{
    public static class Clip
    {
        private readonly static object BoxedCornerRadius = new System.Windows.CornerRadius(0);
        private readonly static object BoxedDouble = 0d;

        public static System.Windows.CornerRadius GetCornerRadius(FrameworkElement obj)
        {
            return (System.Windows.CornerRadius)obj.GetValue(CornerRadiusProperty);
        }

        public static void SetCornerRadius(FrameworkElement obj, System.Windows.CornerRadius value)
        {
            obj.SetValue(CornerRadiusProperty, value);
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.RegisterAttached("CornerRadius", typeof(System.Windows.CornerRadius), typeof(Clip), new PropertyMetadata(BoxedCornerRadius, OnPropertyChanged));



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
                    cornerRadius.ToCornerRadius(),
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
                            _cornerRadius.ToCornerRadius(),
                            _cornerSmoothing,
                            true));
                }
            }

            static void UpdateElementCorner(FrameworkElement? _element, in SquircleProperties _props)
            {
                if (_element == null) return;

                var _pathBuilder = SquircleFactory.CreateGeometry(in _props, () => new WpfPathBuilder());
                _element.Clip = _pathBuilder?.CreateGeometry(false);
            }
        }

    }
}
