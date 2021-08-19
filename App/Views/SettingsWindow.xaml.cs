﻿using Hurl.Browser;
using Hurl.Controls;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Hurl.Views
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();

            LoadSystemBrowserList();
            InstallPathTextBox.Text = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Hurl";
        }

        public void LoadSystemBrowserList()
        {
            BList x = BList.InitalGetList();

            foreach (BrowserObject i in x)
            {
                if (i.Name != null)
                {
                    var comp = new BrowserStatusComponent
                    {
                        BrowserName = i.Name,
                        BrowserPath = i.ExePath,
                        EditEnabled = false,
                        Margin = new Thickness(0, 4, 0, 0),
                    };
                    _ = StackSystemBrowsers.Children.Add(comp);
                }

            }
        }
    }
}
