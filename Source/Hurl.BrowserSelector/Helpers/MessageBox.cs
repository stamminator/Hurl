﻿using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Wpf.Ui.Common;
using MessageBox = Wpf.Ui.Controls.MessageBox;
using TextBox = Wpf.Ui.Controls.TextBox;

namespace Hurl.BrowserSelector.Helpers;

public static class MessageBoxHelper
{
    public static Task<bool> ShowAsync(DependencyObject dependencyObject,
        string title,
        string message,
        string? leftButton = null,
        string? rightButton = null
    )
    {
        var window = Window.GetWindow(dependencyObject) ?? Application.Current.MainWindow;
        if (window is null)
            throw new InvalidOperationException("Cannot show message without window.");
        return ShowAsync(window, title, message, leftButton, rightButton);
    }

    public static Task<bool> ShowAsync(Window window,
        string title,
        string message,
        string? primaryButton = null,
        string? secondaryButton = null)
    {
        var tcs = new TaskCompletionSource<bool>();

        var messageBox = new MessageBox
        {
            Owner = window,
            Title = title,
            Content = new TextBlock
            {
                Text = message,
                TextWrapping = TextWrapping.Wrap,
            },
            ButtonLeftName = "OK",
            ButtonRightName = "Cancel",
            ShowInTaskbar = false,
            Topmost = false,
            ResizeMode = ResizeMode.NoResize,
        };
        messageBox.ButtonLeftClick += (s, e) =>
        {
            tcs.SetResult(true);
            messageBox.Close();
        };
        messageBox.ButtonRightClick += (s, e) =>
        {
            tcs.SetResult(false);
            messageBox.Close();
        };
        messageBox.Closing += (s, e) =>
        {
            tcs.TrySetResult(false);
        };
        messageBox.Show();

        return tcs.Task;
    }

    public static Task<string?> ShowInputAsync(
        DependencyObject dependencyObject,
        string title,
        string? placeholder = null,
        string? text = null,
        string? primaryButton = null,
        string? secondaryButton = null,
        bool allowEmpty = false
    )
    {
        var window = Window.GetWindow(dependencyObject) ?? Application.Current.MainWindow;
        if (window is null)
            throw new InvalidOperationException("Cannot show message without window.");
        return ShowInputAsync(window, title, placeholder, text, primaryButton, secondaryButton, allowEmpty);
    }

    public static Task<string?> ShowInputAsync(
        Window window,
        string title,
        string? placeholder = null,
        string? text = null,
        string? primaryButton = null,
        string? secondaryButton = null,
        bool allowEmpty = false
    )
    {
        var tcs = new TaskCompletionSource<string?>();

        var textBox = new TextBox
        {
            MaxLines = 1,
            MaxLength = 50,
            PlaceholderText = placeholder,
            Text = text,
            TextWrapping = TextWrapping.Wrap,
            SelectionStart = text?.Length ?? 0,
            SelectionLength = 0
        };
        var messageBox = new MessageBox
        {
            Owner = window,
            Title = title,
            Content = textBox,
            ButtonLeftAppearance = ControlAppearance.Transparent,
            ButtonLeftName = "OK",
            ButtonRightName = "Cancel",
            ShowInTaskbar = false,
            Topmost = false,
            MinHeight = 160,
            MaxHeight = 160,
            ResizeMode = ResizeMode.NoResize,
        };

        textBox.TextChanged += (s, e) =>
        {
            var isEmpty = !allowEmpty && string.IsNullOrWhiteSpace(textBox.Text);
            messageBox.ButtonLeftAppearance = isEmpty ? ControlAppearance.Transparent : ControlAppearance.Primary;
        };
        messageBox.ButtonLeftClick += (s, e) =>
        {
            var content = textBox.Text?.Trim();
            var newText = string.IsNullOrWhiteSpace(content) ? null : content;
            if (!allowEmpty && newText is null)
                return;
            tcs.SetResult(newText);
            messageBox.Close();
        };
        messageBox.ButtonRightClick += (s, e) =>
        {
            tcs.SetResult("");
            messageBox.Close();
        };
        messageBox.Closing += (s, e) =>
        {
            tcs.TrySetResult("");
        };
        messageBox.Show();

        FocusManager.SetFocusedElement(window, textBox);

        return tcs.Task;
    }
}
