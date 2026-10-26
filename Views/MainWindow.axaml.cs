using System;
using System.IO;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;
using Microsoft.VisualBasic;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace Chaos_Assembly.Views;

public partial class MainWindow : Window
{
    Uri Google = new Uri(Path.GetFullPath("HTML/Google.html"));
    Uri Amazon = new Uri(Path.GetFullPath("HTML/Amazon.html"));
    Uri Download = new Uri(Path.GetFullPath("HTML/Download.html"));
    Uri Twitter = new Uri(Path.GetFullPath("HTML/Twitter.html"));
    Uri StartPage = new Uri(Path.GetFullPath("HTML/StartPage.html"));
    List<String> History;

    Uri Bookmark1uri;
    Uri Bookmark2uri;

  
    public MainWindow()
    {
        InitializeComponent();
        AddTab("StartPage", StartPage, BrowserTab);
        Bookmark1.IsVisible = false;
         

    } 

    private void WebView_NavigationCompleted(object sender, WebViewNavigationCompletedEventArgs args)
    {
        var   CurrentTab = BrowserTab.SelectedItem as TabItem;
        var TabBrowser = CurrentTab.Content as NativeWebView;

        CurrentTab.Header = Path.GetFileNameWithoutExtension(TabBrowser.Source.ToString());

        History.Add(Path.GetFileNameWithoutExtension(TabBrowser.Source.ToString()) + ".com");
    }
    private void EnterClicked(object sender, KeyEventArgs e)
    {
        var   CurrentTab = BrowserTab.SelectedItem as TabItem;
        var TabBrowser = CurrentTab.Content as NativeWebView;
        if (e.Key == Key.Return)
        {

            if (URL_Bar.Text == "Amazon.com" | URL_Bar.Text == "amazon.com")
            {

                TabBrowser.Navigate(Amazon);
                        CurrentTab.Header = "Amazon";
            } 
            else if (URL_Bar.Text == "Google.com" | URL_Bar.Text == "google.com")
            {

                TabBrowser.Navigate(Google);
                CurrentTab.Header = "Google";
            }
            else if (URL_Bar.Text == "Twitter.com" | URL_Bar.Text == "twitter.com")
            {

                TabBrowser.Navigate(Twitter);
                CurrentTab.Header = "Twitter";
            }

            else
            {
                TabBrowser.Navigate(StartPage);
                CurrentTab.Header = "404 Page Not Found";
            }
         

        }

    }


    private void TestBTN(object sender, RoutedEventArgs args)
    {

        Bookmark1.Content = "Google";

    }
    private void NewTab(object sender, RoutedEventArgs args)
    {
        AddTab("StartPage", StartPage, BrowserTab);


    }
        private void Front(object sender, RoutedEventArgs args)
    {
         var   CurrentTab = BrowserTab.SelectedItem as TabItem;
        var TabBrowser = CurrentTab.Content as NativeWebView;

        TabBrowser.GoForward();
    }
   
    private void Back(object sender, RoutedEventArgs args)
    {
        var CurrentTab = BrowserTab.SelectedItem as TabItem;
        var TabBrowser = CurrentTab.Content as NativeWebView;


        TabBrowser.GoBack();

    }
    private void Refresh(object sender, RoutedEventArgs args)
    {
        var CurrentTab = BrowserTab.SelectedItem as TabItem;
        var TabBrowser = CurrentTab.Content as NativeWebView;


        TabBrowser.Refresh();

    }
          private void Menu(object sender, RoutedEventArgs args)
    { 
        var   CurrentTab = BrowserTab.SelectedItem as TabItem;
        var TabBrowser = CurrentTab.Content as NativeWebView;


        TabBrowser.Refresh();

    }

    private void Dmenu(object sender, RoutedEventArgs args)
    {
        var CurrentTab = BrowserTab.SelectedItem as TabItem;
        var TabBrowser = CurrentTab.Content as NativeWebView;


        TabBrowser.Refresh(); 

    }
    
    private void Book1(object sender, RoutedEventArgs args)
    {
        var CurrentTab = BrowserTab.SelectedItem as TabItem;
        var TabBrowser = CurrentTab.Content as NativeWebView;


        TabBrowser.Navigate(Bookmark1uri);


    }


    private void BookmarkSite(object sender, RoutedEventArgs args)
    {
        var CurrentTab = BrowserTab.SelectedItem as TabItem;
        var TabBrowser = CurrentTab.Content as NativeWebView;

        if (TabBrowser.Source != Bookmark1uri)
        {
            Bookmark1.Content = CurrentTab.Header;
            Bookmark1uri = TabBrowser.Source;
            Star.Content = "★";
            Bookmark1.IsVisible = true;
        }
        else
        {
            Bookmark1uri = Bookmark2uri;
            Star.Content = "☆";
            Bookmark1.IsVisible = false;
        }


    }



    private void AddTab(string Title, Uri Page, TabControl TabBar)
    {
        var browser = new NativeWebView
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Top,
            Margin = new Thickness(0, 60, 0, 0),
        };
        browser.Source = Page;
        var tab = new TabItem
        {
            Header = Title,
            Content = browser,
            MinHeight = 15,
            FontSize = 15,

        };
        TabBar.Items.Add(tab);
        TabBar.SelectedItem = tab;
    }
    private void NewPopup(bool Jump)
    {
        Window PW = new AD(25, 25, Jump, this)
        {
            CanResize = false,
            WindowDecorations = WindowDecorations.None
        };
        PW.Show();
    }

}