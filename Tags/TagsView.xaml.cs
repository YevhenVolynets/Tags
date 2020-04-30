using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Tags
{
    public partial class TagsView : ContentView
    {
        public TagsView()
        {
            InitializeComponent();
            tags.IsVisible = false;
            btn.BackgroundColor = Color.Gray;
            
        }

        public bool IsActive { get; set; }

        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            IsActive = !IsActive;
            tags.IsVisible = IsActive;
            if (IsActive)
            {
                btn.BackgroundColor = Color.Blue;
                tags.Parent = this.Parent.Parent.Parent;
            }
            else
                btn.BackgroundColor = Color.Gray;
        }
    }
}
