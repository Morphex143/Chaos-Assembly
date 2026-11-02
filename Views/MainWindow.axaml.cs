using System;
using System.IO;
using Avalonia;
using System.Runtime;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using static Chaos_Assembly.References;
using Eremex.AvaloniaUI.Controls;
using System.Threading.Tasks;
using System.Linq;
using static System.Threading.Tasks.Task;
using static MsBox.Avalonia.MessageBoxManager;
using System.Globalization;
using System.Text.Json.Nodes;
using System.Text.Json;


namespace Chaos_Assembly.Views;

public partial class MainWindow : Window
{

    private MxTabItem CurrentTab;
    private string CurrentSite;
    private string CurrentURL;
    private NativeWebView TabBrowser;
    public MainWindow()
    {
        InitializeComponent();

        StartupTab();
        CurrentTab = BrowserTab.SelectedItem as MxTabItem;
        TabBrowser = CurrentTab.Content as NativeWebView;
        Bookmark1.IsVisible = false;
        
        }

    private void StartupTab()
    {
        var browser = new NativeWebView
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Top,
            Margin = new Thickness(0, 60, 0, 0),
            Source = StartPage
        };
        browser.NavigationCompleted += WebView_NavigationCompleted;
        var tab = new MxTabItem
        {
            Header = "StartPage",
            Content = browser,
            MinHeight = 15,
            FontSize = 15,
        };
        BrowserTab.Items.Add(tab);
        BrowserTab.SelectedItem = tab;
        CurrentSite = "StartPage";
        
    }
    private void TabChanged(object sender, SelectionChangedEventArgs args)
    {
        if (CurrentTab != null)
        {
            CurrentTab = BrowserTab.SelectedItem as MxTabItem;
            TabBrowser = CurrentTab.Content as NativeWebView;
        }
    }

    private void WebView_NavigationCompleted(object sender, WebViewNavigationCompletedEventArgs args)
    {
        CurrentTab.Header = CurrentSite;
        if (TabBrowser.Source == Download)
        {
            MoveButton();
        }
        if (TabBrowser.Source == Download2)
        {
            ProtectButton();
        }
    }

    private void EnterClicked(object sender, KeyEventArgs e)
    {

        if (e.Key == Key.Return)
        {
            if (URL_Bar.Text != null)
            {
                foreach (String site in Sites)
                {
                    if (URL_Bar.Text.EndsWith(".com"))
                    {
                        int SiteURL = Sites.IndexOf(site);
                        if (URL_Bar.Text.Equals(URL[SiteURL], StringComparison.OrdinalIgnoreCase))
                        {
                            Uri uri = new(Path.GetFullPath($"HTML/{site}.html"));
                            TabBrowser.Navigate(uri);
                            CurrentSite = site;
                            CurrentURL = URL[SiteURL];
                        }
                    }
                }
            }
        }
    }
    private void NewBookmark()
    {
        if (BookmarkCount < 8)
        {
            var bookmark = new Button()
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(90, 60, 0, 0),

            };
            bookmark.Click += Book1;
            BookmarkDistance = BookmarkDistance + 30;
        }
        else
        {
            MsgBox("Not Allowed", "You can only have 8 Bookmarks");
        }

    }


    private void TestBTN(object sender, RoutedEventArgs args)
    {
        //NewPopup(true);


        MoveButton();

    }

    private async void MsgBox(string Title, string Message)
    {
        var box = MessageBoxManager
            .GetMessageBoxStandard(Title, Message, ButtonEnum.Ok);
            

        await box.ShowAsync();
    }
    private void NewTab(object sender, RoutedEventArgs args)
    {
        AddTab("StartPage", StartPage, BrowserTab);
    }
        private void Front(object sender, RoutedEventArgs args)
    {
        TabBrowser.GoForward();
        CurrentTab.Header = CurrentSite;
    }
    private void Back(object sender, RoutedEventArgs args)
    {
        TabBrowser.GoBack();
        CurrentTab.Header = CurrentSite;
    }
    private void Refresh(object sender, RoutedEventArgs args)
    {
        TabBrowser.Refresh();
        CurrentTab.Header = CurrentSite;
    }
        private void Menu(object sender, RoutedEventArgs args)
    {
        TabBrowser.Refresh();
        
    }

    private void Dmenu(object sender, RoutedEventArgs args)
    {
        TabBrowser.Refresh(); 
    }
    
    private void Book1(object sender, RoutedEventArgs args)
    {
        TabBrowser.Navigate(Bookmark1uri);
    }


    private void BookmarkSite(object sender, RoutedEventArgs args)
    {

        if (Bookmarks != null)
        {
            foreach (string bookmark in Bookmarks)
            {
                if (CurrentSite != bookmark)
                {
                    NewBookmark();
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
        }


    }

    private async void MoveButton()
    {

        while (true)
        {
            string result2 = await TabBrowser.InvokeScript("JSON.stringify([window.mouseX, window.mouseY])");
            double MouseX = System.Text.Json.JsonDocument.Parse(result2).RootElement[0].ValueKind == JsonValueKind.Number ? System.Text.Json.JsonDocument.Parse(result2).RootElement[0].GetDouble() : 0;
            double MouseY = System.Text.Json.JsonDocument.Parse(result2).RootElement[1].ValueKind == JsonValueKind.Number ? System.Text.Json.JsonDocument.Parse(result2).RootElement[1].GetDouble() : 0;
            if (MouseX != null && MouseY != null)
            {
                string result = await TabBrowser.InvokeScript("JSON.stringify([document.getElementById('download').getBoundingClientRect().x, document.getElementById('download').getBoundingClientRect().y])");
                string Size = await TabBrowser.InvokeScript("JSON.stringify([document.getElementById('download').getBoundingClientRect().width, document.getElementById('download').getBoundingClientRect().height])");
                double X = System.Text.Json.JsonDocument.Parse(result).RootElement[0].GetDouble();
                double Y = System.Text.Json.JsonDocument.Parse(result).RootElement[1].GetDouble();
                double Width = System.Text.Json.JsonDocument.Parse(Size).RootElement[0].GetDouble();
                double Height = System.Text.Json.JsonDocument.Parse(Size).RootElement[1].GetDouble();
                double XD = X + (Width / 2) - MouseX;
                double YD = Y + (Height / 2) - MouseY;
                double Range = Math.Sqrt(XD * XD + YD * YD);

                if (Range < 100 && Range > 0)
                {
                    double strenght = (100 - Range) / 90;
                    X += XD / Range * strenght * 3;
                    Y += YD / Range * strenght * 3;

                    string xText = X.ToString(CultureInfo.InvariantCulture);
                    string yText = Y.ToString(CultureInfo.InvariantCulture);
                    await TabBrowser.InvokeScript($"document.getElementById('download').style.left = '{xText}px';");
                    await TabBrowser.InvokeScript($"document.getElementById('download').style.top = '{yText}px';");

                }
            }
            await Task.Delay(1);
        }
    }

    private async void ProtectButton()
    {
        while (IsProtected)
        {
            string result2 = await TabBrowser.InvokeScript("JSON.stringify([window.mouseX, window.mouseY])");
            double WMouseX = System.Text.Json.JsonDocument.Parse(result2).RootElement[0].ValueKind == JsonValueKind.Number ? System.Text.Json.JsonDocument.Parse(result2).RootElement[0].GetDouble() : 0;
            double WMouseY = System.Text.Json.JsonDocument.Parse(result2).RootElement[1].ValueKind == JsonValueKind.Number ? System.Text.Json.JsonDocument.Parse(result2).RootElement[1].GetDouble() : 0;
            string Size = await TabBrowser.InvokeScript("JSON.stringify([document.getElementById('download').getBoundingClientRect().width, document.getElementById('download').getBoundingClientRect().height])");
            double Width = System.Text.Json.JsonDocument.Parse(Size).RootElement[0].GetDouble();
            double Height = System.Text.Json.JsonDocument.Parse(Size).RootElement[1].GetDouble();
            string result = await TabBrowser.InvokeScript("JSON.stringify([document.getElementById('download').getBoundingClientRect().x, document.getElementById('download').getBoundingClientRect().y])");
            double X = System.Text.Json.JsonDocument.Parse(result).RootElement[0].GetDouble();
            double Y = System.Text.Json.JsonDocument.Parse(result).RootElement[1].GetDouble();
            double XD = X + (Width / 2) - WMouseX;
            double YD = Y + (Height / 2) - WMouseY;
            double Range = Math.Sqrt(XD * XD + YD * YD); 
            var screen = Screens.ScreenFromWindow(this);
            var area = screen.WorkingArea;
            
          
            if (Range < 60)
            {
                NewPopup(false, Width + 60, Height + 60, this.Position.X + WMouseX - Width + 50, this.Position.Y + WMouseY + 60 - Height + 50);
                IsProtected = false;
            }
        }
    }

    private void AddTab(string Title, Uri Page, MxTabControl TabBar)
    {
        var browser = new NativeWebView
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Top,
            Margin = new Thickness(0, 60, 0, 0),

        };
        browser.NavigationCompleted += WebView_NavigationCompleted;
        var tab = new MxTabItem
        {
            Header = Title,
            Content = browser,
            MinHeight = 15,
            FontSize = 15,

        };
        TabBar.Items.Add(tab);
        TabBar.SelectedItem = tab;
        TabBrowser.Navigate(Page);
    }
    private void NewPopup(bool Jump, double width, double height, double PosX, double PosY)
    {
        Window PW = new Popup(PosX, PosY, width, height, Jump, this)
        {
            CanResize = false,
            WindowDecorations = WindowDecorations.None
        };
        PW.Show();
    }
    private void CloseTab(object sender, RoutedEventArgs args)
    {

        BrowserTab.Items.Remove(CurrentTab);
        BrowserTab.SelectedItem = BrowserTab.Items.LastOrDefault() as MxTabItem;
    }
        private void WPM(object? sender, PointerEventArgs e)
    {
        var pos = e.GetPosition(this);
        MouseX = pos.X;
        MouseY = pos.Y;
    }
}