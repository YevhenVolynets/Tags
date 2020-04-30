using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Tags
{
    public partial class HackPage : ContentPage
    {
        public HackPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Go(true);
        }

        private async void Go(bool goRight)
        {
            var currentX = sv.ScrollX;
            var contentSize = sv.ContentSize.Width;
            var width = sv.Width;
            if (contentSize < width)
                return;

            var needLeft = currentX + width <= contentSize;

            if (goRight && !needLeft) //scroll right
            {
                await sv.ScrollToAsync(currentX + 10, 0, true);
                goRight = true;
            }
            else // scroll left
            {
                await sv.ScrollToAsync(currentX - 10, 0, true);
                goRight = sv.ScrollX < 10;
            }
            Go(goRight);
        }
    }
}
