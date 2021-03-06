﻿using System;
using Xamarin.Forms;
using DeviceOrientation.Forms.Plugin.Abstractions;

namespace DeviceOrientationDemo
{
    public class MainPage:ContentPage
    {
        IDeviceOrientation _deviceOrientationSvc;
        private Label _label;

        public MainPage()
        {
            Title = "Main";
            _deviceOrientationSvc = DependencyService.Get<IDeviceOrientation>();
            PrepareControls();
        }

        private void PrepareControls()
        {
            var stackLayout = new StackLayout()
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(5)
            };
           
            _label = new Label
            {
                
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };

            var button = new Button()
            {
                Text = "Navigate To Child",
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Command = new Command(() =>
                {
                    Navigation.PushAsync(new ChildPage());
                })
            };
            

            stackLayout.Children.Add(_label);
            stackLayout.Children.Add(button);

            Content = stackLayout;           
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            _label.Text = String.Format("Hello, Forms! My initial orientation is {0}", _deviceOrientationSvc.GetOrientation());
            
            MessagingCenter.Subscribe<DeviceOrientationChangeMessage>(this, DeviceOrientationChangeMessage.MessageId, (message) =>
                {
                    HandleOrientationChange(message);
                });
        }

        protected override void OnDisappearing()
        {
            MessagingCenter.Unsubscribe<DeviceOrientationChangeMessage>(this, DeviceOrientationChangeMessage.MessageId);
            base.OnDisappearing();
        }

        private void HandleOrientationChange(DeviceOrientationChangeMessage mesage)
        {
            _label.Text = String.Format("Orientation changed to {0}", mesage.Orientation.ToString());
        }
    }
}

