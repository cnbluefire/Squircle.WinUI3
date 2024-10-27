### Squircle for WinUI3 and WPF

在 WinUI3 或 WPF 中创建平滑圆角矩形控件。

算法来自 [phamfoo/figma-squircle](https://github.com/phamfoo/figma-squircle)


#### WinUI3
1. 在 xaml 中使用
    ```xml
    xmlns:squircle="using:Squircle.WinUI3" 
    ```

    ```xml
    <Border Background="#eb5757"
            squircle:Clip.CornerRadius="24,24,24,24"
            squircle:Clip.CornerSmoothing="0.8">
    </Border>
    ```
2. 在 Win2D 中使用
    ```csharp
    public void CanvasControl_Draw(Microsoft.Graphics.Canvas.UI.Xaml.CanvasControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasDrawEventArgs args)
    {
        var pathBuilder = SquircleFactory.CreateGeometry(
            new SquircleProperties(
                Width: 100,
                Height: 100,
                CornerRadius: new(24),
                CornerSmoothing: 0.8,
                PreserveSmoothing: true),
            () => Win2DPathBuilder());

        var geometry = pathBuilder?.CreateGeometry(sender);

        if (geometry != null) args.DrawingSession.FillGeometry(geometry, Colors.Red);
    }
    ```

#### WPF
1. 在 xaml 中使用
    ```xml
    xmlns:squircle="clr-namespace:Squircle.Wpf;assembly=Squircle.Wpf"
    ```

    ```xml
    <Border Background="#eb5757"
            squircle:Clip.CornerRadius="24,24,24,24"
            squircle:Clip.CornerSmoothing="0.8">
    </Border>
    ```
2. 直接创建 Geometry
    ```csharp
    var path = new Path();
    var pathBuilder = SquircleFactory.CreateGeometry(
        new SquircleProperties(
                Width: 100,
                Height: 100,
                CornerRadius: new(24),
                CornerSmoothing: 0.8,
                PreserveSmoothing: true), 
        () => new WpfPathBuilder());

        path.Data = pathBuilder?.CreateGeometry(false);
    ```