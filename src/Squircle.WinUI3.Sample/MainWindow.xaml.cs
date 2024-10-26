using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Squircle.WinUI3.Sample
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            UpdateBorderCornerRadius();
        }

        private void CornerRadiusSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            UpdateBorderCornerRadius();
        }

        private void UpdateBorderCornerRadius()
        {
            if (border1 == null) return;
            if (border2 == null) return;
            if (Slider_TopLeftCornerRadius == null) return;
            if (Slider_TopRightCornerRadius == null) return;
            if (Slider_BottomRightCornerRadius == null) return;
            if (Slider_BottomLeftCornerRadius == null) return;

            var cornerRadius = new CornerRadius(
                Slider_TopLeftCornerRadius.Value,
                Slider_TopRightCornerRadius.Value,
                Slider_BottomRightCornerRadius.Value,
                Slider_BottomLeftCornerRadius.Value);

            border1.CornerRadius = cornerRadius;
            Clip.SetCornerRadius(border2, cornerRadius);
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            Slider_Width.Value = 150;
            Slider_Height.Value = 150;
            Slider_TopLeftCornerRadius.Value = 24;
            Slider_TopRightCornerRadius.Value = 24;
            Slider_BottomLeftCornerRadius.Value = 24;
            Slider_BottomRightCornerRadius.Value = 24;
            Slider_CornerSmoothing.Value = 0.8;
        }

        private void Checkbox_BorderVisible_Click(object sender, RoutedEventArgs e)
        {
            if (Checkbox_Border1Visible.IsChecked is not true
                && Checkbox_Border2Visible.IsChecked is not true)
            {
                ((CheckBox)sender).IsChecked = true;
            }
        }

        public Visibility ToVisibility(bool? isChecked) => isChecked is true ? Visibility.Visible : Visibility.Collapsed;
    }
}
