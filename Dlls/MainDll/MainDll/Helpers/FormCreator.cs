using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Lab_4.Helpers
{
    public class FormCreator
    {
        public static Button CreateButton(string name, string content, Thickness margin, Action<object, RoutedEventArgs> btnClick)
        {
            Button b = new Button()
            {
                Margin = margin,
                Name = name,
                Content = content,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Width = 60
            };
            b.Click += new RoutedEventHandler(btnClick);
            return b;
        }

        public static CheckBox CreateCheckBox(string name, string content, Thickness margin, bool val)
        {
            CheckBox ch = new CheckBox()
            {
                Margin = margin,
                Name = name,
                Content = content,
                VerticalAlignment = VerticalAlignment.Top,
                Height = 18,
                IsChecked = val
            };
            return ch;
        }

        public static Grid CreateGrid(Thickness margin)
        {
            Grid g = new Grid()
            {
                Margin = margin
            };
            return g;
        }

        public static GroupBox CreateGroupBox(string name, string header, Thickness margin, int width, int height)
        {
            GroupBox g = new GroupBox()
            {
                Name = name,
                Header = header,
                Height = height,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Width = width,
                Margin = margin
            };
            return g;
        }

        public static ComboBox CreateComboBox(string name, Thickness margin, List<string> items)
        {
            ComboBox c = new ComboBox()
            {
                Name = name,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Height = 22,
                Width = 134,
                Margin = margin,
                ItemsSource = items,
                SelectedIndex = -1
            };
            return c;
        }
        public static Label CreateLabel(string content, Thickness margin)
        {
            Label l = new Label()
            {
                Content = content,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Height = 28,
                Width = 200,
                Margin = margin
            };
            return l;
        }

        public static TextBox CreateTextBox(string name, string text, Thickness margin)
        {
            TextBox t = new TextBox()
            {
                Name = name,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Height = 22,
                Width = 134,
                Margin = margin,
                TextWrapping = TextWrapping.Wrap,
                MaxLines = 1,
                MaxLength = 36,
                Text = text
            };
            return t;
        }
    }
}
