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
using System.Net.Http;
using System.Threading.Tasks;
using Avalonia.Platform;

namespace Chaos_Assembly.Views;

public partial class Popup : Window
{
    public Popup(double Width, double Height, bool Jump, Window MW)
    {
        InitializeComponent();
        LayoutSize.Height = Height;
        LayoutSize.Width = Width;
        if(Jump)
        {
            JumpWindow(MW);
        }       
    }
    private async void JumpWindow(Window MW)
    {
        
        var random = new Random();
        while (true)
        {
            var screen = Screens.ScreenFromWindow(MW);
            var area = screen.WorkingArea;

            int maxX = area.Right - (int)Width;
            int maxY = area.Bottom - (int)Height;
            int x = random.Next(area.X, maxX);
            int y = random.Next(area.Y, maxY);

            Position = new PixelPoint(x, y);

            await Task.Delay(600);
        }
        
    }
}
