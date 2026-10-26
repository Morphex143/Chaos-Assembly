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

public partial class AD : Window
{
    public AD(double Width, double Height, bool Jump, Window MW)
    {
        InitializeComponent();
        LayoutSize.Height = Height;
        LayoutSize.Width = Width;
        if (Jump)
        {
            JumpWindow(MW);
        }   
    }
    private async void JumpWindow(Window MW)
    {

        var random = new Random();
        while (true)
        {


            int x = random.Next(MW.Position.X + 50, (int)MW.Width + MW.Position.X);
            int y = random.Next(MW.Position.Y, (int)MW.Height + MW.Position.Y);
 
            Position = new PixelPoint(x, y);

            await Task.Delay(600);

        }


    }

}
