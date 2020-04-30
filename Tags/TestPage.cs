using System;

using Xamarin.Forms;
using Xamarin.Forms.PancakeView;

namespace Tags
{
    public class TestPage : ContentPage
    {
        public TestPage()
        {
            var sl = new StackLayout();
            var pv = new PancakeView();
            pv.BackgroundGradientAngle = 25;
            pv.BackgroundGradientStartColor = Color.Red;
            pv.BackgroundGradientEndColor = Color.Blue;
            pv.HeightRequest = 200;
            pv.WidthRequest = 400;

            sl.Children.Add(pv);
            Content = sl;
        }
    }
}

