// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DesktopWinUI3.Controls;

public sealed partial class PageHeader : UserControl
{
    public object Title
    {
        get { return GetValue(TitleProperty); }
        set { SetValue(TitleProperty, value); }
    }

    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register("Title", typeof(object), typeof(PageHeader), new PropertyMetadata(null));


    public string ApiNamespace
    {
        get { return (string)GetValue(ApiNamespaceProperty); }
        set { SetValue(ApiNamespaceProperty, value); }
    }

    public static readonly DependencyProperty ApiNamespaceProperty =
        DependencyProperty.Register("ApiNamespace", typeof(string), typeof(PageHeader), new PropertyMetadata(null));

    public object Subtitle
    {
        get { return GetValue(SubtitleProperty); }
        set { SetValue(SubtitleProperty, value); }
    }

    public static readonly DependencyProperty SubtitleProperty =
        DependencyProperty.Register("Subtitle", typeof(object), typeof(PageHeader), new PropertyMetadata(null));



    public Thickness HeaderPadding
    {
        get { return (Thickness)GetValue(HeaderPaddingProperty); }
        set { SetValue(HeaderPaddingProperty, value); }
    }

    // Using a DependencyProperty as the backing store for BackgroundColorOpacity.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty HeaderPaddingProperty =
        DependencyProperty.Register("HeaderPadding", typeof(Thickness), typeof(PageHeader), new PropertyMetadata((Thickness)App.Current.Resources["PageHeaderDefaultPadding"]));


    public double BackgroundColorOpacity
    {
        get { return (double)GetValue(BackgroundColorOpacityProperty); }
        set { SetValue(BackgroundColorOpacityProperty, value); }
    }

    // Using a DependencyProperty as the backing store for BackgroundColorOpacity.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty BackgroundColorOpacityProperty =
        DependencyProperty.Register("BackgroundColorOpacity", typeof(double), typeof(PageHeader), new PropertyMetadata(0.0));


    public double AcrylicOpacity
    {
        get { return (double)GetValue(AcrylicOpacityProperty); }
        set { SetValue(AcrylicOpacityProperty, value); }
    }

    // Using a DependencyProperty as the backing store for BackgroundColorOpacity.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty AcrylicOpacityProperty =
        DependencyProperty.Register("AcrylicOpacity", typeof(double), typeof(PageHeader), new PropertyMetadata(0.3));

    public double ShadowOpacity
    {
        get { return (double)GetValue(ShadowOpacityProperty); }
        set { SetValue(ShadowOpacityProperty, value); }
    }

    // Using a DependencyProperty as the backing store for BackgroundColorOpacity.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ShadowOpacityProperty =
        DependencyProperty.Register("ShadowOpacity", typeof(double), typeof(PageHeader), new PropertyMetadata(0.0));

    public CommandBar TopCommandBar
    {
        get { return topCommandBar; }
    }

    public UIElement TitlePanel
    {
        get { return pageTitle; }
    }

    public PageHeader()
    {
        InitializeComponent();
    }
}
