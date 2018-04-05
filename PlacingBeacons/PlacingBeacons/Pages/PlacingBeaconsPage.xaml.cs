using PlacingBeacons.Pages;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace PlacingBeacons
{
    public partial class PlacingBeaconsPage : ContentPage
    {
        private Slider sliderX, sliderY;
        private Image circleImage;

        private List<Beacon> beacons = new List<Beacon>();

        private double imageScale = 0.1;

        private Button deleteButton;

        private AbsoluteLayout layout = new AbsoluteLayout();

        public PlacingBeaconsPage()
        {
            InitializeComponent();

            var mapImage = new Image
            {
                Source = "szkolarzut.png",
                Aspect = Aspect.Fill
            };

            var tapGestureRecogniser = new TapGestureRecognizer();
            tapGestureRecogniser.Tapped += OnBackgroundClick;
            mapImage.GestureRecognizers.Add(tapGestureRecogniser);

            AbsoluteLayout.SetLayoutBounds(mapImage, new Rectangle(0, 0, 1, 1));
            AbsoluteLayout.SetLayoutFlags(mapImage, AbsoluteLayoutFlags.All);

            circleImage = new Image
            {
                Source = "circleblue.png",

            };

            AbsoluteLayout.SetLayoutBounds(circleImage, new Rectangle(0.5, 0.5, imageScale, imageScale));
            AbsoluteLayout.SetLayoutFlags(circleImage, AbsoluteLayoutFlags.All);

            layout.Children.Add(mapImage);
            layout.Children.Add(circleImage);

            sliderX = new Slider
            {
                Minimum = -imageScale / 2,
                Maximum = 1 + imageScale / 2,
                Value = 0.5
            };

            AbsoluteLayout.SetLayoutBounds(sliderX, new Rectangle(0.5, 0.9, 1, 0.01));
            AbsoluteLayout.SetLayoutFlags(sliderX, AbsoluteLayoutFlags.All);
            sliderX.ValueChanged += OnValueChange;

            sliderY = new Slider
            {
                Minimum = -imageScale / 2,
                Maximum = 1 + imageScale / 2,
                Value = 0.5,
            };

            AbsoluteLayout.SetLayoutBounds(sliderY, new Rectangle(0.5, 0.95, 1, 0.01));
            AbsoluteLayout.SetLayoutFlags(sliderY, AbsoluteLayoutFlags.All);
            sliderY.ValueChanged += OnValueChange;

            layout.Children.Add(sliderX);
            layout.Children.Add(sliderY);

            var addButton = new Button
            {
                Text = "add"
            };

            AbsoluteLayout.SetLayoutBounds(addButton, new Rectangle(0.1, 0.1, 0.3, 0.1));
            AbsoluteLayout.SetLayoutFlags(addButton, AbsoluteLayoutFlags.All);
            addButton.Clicked += OnAddButtonPress;

            layout.Children.Add(addButton);

            deleteButton = new Button
            {
                Text = "delete"
            };

            AbsoluteLayout.SetLayoutBounds(deleteButton, new Rectangle(0.9, 0.1, 0.3, 0.1));
            AbsoluteLayout.SetLayoutFlags(deleteButton, AbsoluteLayoutFlags.All);
            deleteButton.Clicked += OnDeleteButtonPress;

            layout.Children.Add(deleteButton);

            var dataButton = new Button
            {
                Text = "data"
            };

            AbsoluteLayout.SetLayoutBounds(dataButton, new Rectangle(0.5, 0.1, 0.3, 0.1));
            AbsoluteLayout.SetLayoutFlags(dataButton, AbsoluteLayoutFlags.All);
            dataButton.Clicked += OnDataButtonPress;

            layout.Children.Add(dataButton);

            Content = layout;
        }

        private void OnDataButtonPress(object o, EventArgs e)
        {
            if (circleImage.IsVisible)
            {
                OnAddButtonPress(null, null);
                OnDeleteButtonPress(null, null);
            }
            Navigation.PushModalAsync(new DataPage(beacons, imageScale));
        }

        private void OnValueChange(object o, EventArgs e)
        {
            AbsoluteLayout.SetLayoutBounds(circleImage, new Rectangle(sliderX.Value, sliderY.Value, imageScale, imageScale));
        }

        private void OnAddButtonPress(object o, EventArgs e)
        {
            if (circleImage.IsVisible)
            {
                Beacon newBeacon = new Beacon(sliderX.Value, sliderY.Value);
                newBeacon.Source = "circle.png";
                beacons.Add(newBeacon);
                var tapGestureRecogniser = new TapGestureRecognizer();
                tapGestureRecogniser.Tapped += OnImageClick;
                newBeacon.GestureRecognizers.Add(tapGestureRecogniser);
                AbsoluteLayout.SetLayoutBounds(newBeacon, new Rectangle(newBeacon.x, newBeacon.y, imageScale, imageScale));
                AbsoluteLayout.SetLayoutFlags(newBeacon, AbsoluteLayoutFlags.All);
                layout.Children.Add(newBeacon);
            }

            sliderX.Value = 0.5;
            sliderY.Value = 0.5;

            ChangeMainCircleVisibility(true);

            Content = layout;
        }

        private void OnBackgroundClick(object o, EventArgs e)
        {
            if (circleImage.IsVisible)
            {
                OnAddButtonPress(null, null);
            }

            ChangeMainCircleVisibility(false);
        }

        private void OnDeleteButtonPress(object o, EventArgs e)
        {
            sliderX.Value = 0.5;
            sliderY.Value = 0.5;

            ChangeMainCircleVisibility(false);
        }

        private void OnImageClick(object ob, EventArgs e)
        {
            Beacon o = (Beacon)ob;

            if (circleImage.IsVisible)
            {
                OnAddButtonPress(null, null);
            }

            ChangeMainCircleVisibility(true);

            sliderX.Value = o.x;
            sliderY.Value = o.y;

            beacons.Remove(o);
            layout.Children.Remove(o);
        }

        private void ChangeMainCircleVisibility(Boolean b)
        {
            circleImage.IsVisible = b;
            sliderX.IsVisible = b;
            sliderY.IsVisible = b;
            deleteButton.IsVisible = b;
        }
    }

    public class Beacon : Image
    {
        public double x, y;

        public Beacon(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
