using Avalonia.Controls;
using System.Collections.Generic;
using System;
using System.IO;
namespace Chaos_Assembly;

public class References
{
        public static readonly string[] Sites =
        {
            "Google", "Amazon", "Download", "Download2", "Twitter", "StartPage"
    };
        public static readonly string[] URL =
        {
            "Google.com", "Amazon.com", "Download.com", "Download2.com", "Twitter.com", "StartPage.com"
        };


        public static Uri Google = new Uri(Path.GetFullPath("HTML/Google.html"));
        public static Uri Amazon = new Uri(Path.GetFullPath("HTML/Amazon.html"));
        public static Uri Download = new Uri(Path.GetFullPath("HTML/Download.html"));
        public static Uri Download2 = new Uri(Path.GetFullPath("HTML/Download2.html"));
        public static Uri Twitter = new Uri(Path.GetFullPath("HTML/Twitter.html"));
        public static Uri StartPage = new Uri(Path.GetFullPath("HTML/StartPage.html"));


        public static Uri Bookmark1uri;
        public static Uri Bookmark2uri;
        public static string[] Bookmarks =
        {

        };
        public static int BookmarkCount;
        public static int BookmarkDistance = 90;


        public static List<String> History;
        public static double MouseX, MouseY;

    public static double BrowserMouseY, BrowserMouseX;
    public static bool IsProtected = true;
}
