﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Capture.Workflow.Core;
using Capture.Workflow.Core.Classes;
using Capture.Workflow.Core.Classes.Attributes;
using Capture.Workflow.Core.Interface;
using MaterialDesignThemes.Wpf;

namespace Capture.Workflow.Plugins.ViewElements
{
    [Description("")]
    [PluginType(PluginType.ViewElement)]
    [DisplayName("Button")]
    public class Button:IViewElementPlugin
    {
        public string Name { get; set; }
        public WorkFlowViewElement CreateElement(WorkFlowView view)
        {
            WorkFlowViewElement element = new WorkFlowViewElement();
            element.Properties.Items.Add(new CustomProperty()
            {
                Name = "Caption",
                PropertyType = CustomPropertyType.String
            });
            element.Properties.Items.Add(new CustomProperty()
            {
                Name = "Position",
                PropertyType = CustomPropertyType.ValueList,
                ValueList = view.Instance.GetPositions(),
                Value = view.Instance.GetPositions()[0]
            });
            element.Properties.Items.Add(new CustomProperty()
            {
                Name = "Style",
                PropertyType = CustomPropertyType.ValueList,
                ValueList = { "Default", "Rounded" },
                Value = "Default"
            });
            element.Properties.Items.Add(new CustomProperty()
            {
                Name = "Icon",
                PropertyType = CustomPropertyType.Icon,
                Value = "(None)"
            });
            element.Properties.Items.Add(new CustomProperty()
            {
                Name = "Width",
                PropertyType = CustomPropertyType.Number,
                RangeMin = 0,
                RangeMax = 9999,
                Value = "150"
            });

            element.Properties.Items.Add(new CustomProperty()
            {
                Name = "Height",
                PropertyType = CustomPropertyType.Number,
                RangeMin = 0,
                RangeMax = 9999,
                Value = "50"
            });
            element.Properties.Items.Add(new CustomProperty()
            {
                Name = "Margins",
                PropertyType = CustomPropertyType.Number,
                RangeMin = 0,
                RangeMax = 9999,
                Value = "5"
            });
            element.Properties.Items.Add(new CustomProperty()
            {
                Name = "FontSize",
                PropertyType = CustomPropertyType.Number,
                RangeMin = 6,
                RangeMax = 400,
                Value = "15"
            });
            element.Properties.Items.Add(new CustomProperty()
            {
                Name = "BackgroundColor",
                PropertyType = CustomPropertyType.Color,
                Value = "Transparent"
            });
            element.Properties.Items.Add(new CustomProperty()
            {
                Name = "ForegroundColor",
                PropertyType = CustomPropertyType.Color,
                Value = "Transparent"
            });
            element.Events.Add(new CommandCollection("Click"));
            return element;
        }

        public FrameworkElement GetControl(WorkFlowViewElement viewElement)
        {
            var button = new System.Windows.Controls.Button()
            {
                Width = viewElement.Properties["Width"].ToInt(),
                Height = viewElement.Properties["Height"].ToInt(),
                Content = viewElement.Properties["Caption"].Value,
                Margin = new Thickness(viewElement.Properties["Margins"].ToInt()),
                FontSize = viewElement.Properties["FontSize"].ToInt(),
            };
            button.Click += (sender, args) =>
            {
                WorkflowManager.Execute(viewElement.GetEventCommands("Click"));
            };
            if (viewElement.Properties["Style"].Value == "Rounded")
            {
                button.Style = Application.Current.Resources["MaterialDesignFloatingActionButton"] as Style;
            }

            PackIconKind kind;
            if (Enum.TryParse(viewElement.Properties["Icon"].Value, out kind))
                button.Content = new PackIcon() {Kind = kind, Width = button.Width /2, Height = button.Height /2};

            if (viewElement.Properties["BackgroundColor"].Value != "Transparent" && viewElement.Properties["BackgroundColor"].Value != "#00FFFFFF")
                button.Background =
                    new SolidColorBrush(
                        (Color) ColorConverter.ConvertFromString(viewElement.Properties["BackgroundColor"].Value));
            if (viewElement.Properties["ForegroundColor"].Value != "Transparent" && viewElement.Properties["ForegroundColor"].Value != "#00FFFFFF")
                button.Foreground =
                    new SolidColorBrush(
                        (Color)ColorConverter.ConvertFromString(viewElement.Properties["ForegroundColor"].Value));

            return button;
        }


    }
}
