using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Squircle.Wpf.Sample;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        UpdateBorderCornerRadius();
    }

    private void CornerRadiusSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
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
        Squircle.Wpf.Clip.SetCornerRadius(border2, cornerRadius);
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
        Checkbox_Border1Visible.IsChecked = true;
        Checkbox_Border2Visible.IsChecked = true;
    }

    private void Checkbox_BorderVisible_Click(object sender, RoutedEventArgs e)
    {
        if (Checkbox_Border1Visible.IsChecked is not true
            && Checkbox_Border2Visible.IsChecked is not true)
        {
            ((CheckBox)sender).IsChecked = true;
        }
    }
}