### Squircle for WinUI3

在 WinUI3 中创建平滑圆角矩形控件。

算法来自 [phamfoo/figma-squircle](https://github.com/phamfoo/figma-squircle)

使用方法
```xml
xmlns:squircle="using:Squircle.WinUI3" 
```

```xml
<Border Background="#eb5757"
        squircle:Clip.CornerRadius="24,24,24,24"
        squircle:Clip.CornerSmoothing="0.8">
</Border>
```