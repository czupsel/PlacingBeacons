using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PlacingBeacons.Pages
{
    public partial class DataPage : ContentPage
    {
        private StackLayout layout = new StackLayout();

        public DataPage(List<Beacon> data, double imageScale)
        {
            InitializeComponent();

            layout.Children.Add(new Label
            {
                Text = "Rozmiar obrazu: " + imageScale
            });

            for (int i=0;i<data.Count;i++)
            {
                Beacon p = data[i];
                layout.Children.Add(new Label
                {
                    Text = i + ": " + p.x + ", " + p.y
                });
            }

            Content = new ScrollView()
            {
                Content = layout
            };
        }
    }
}