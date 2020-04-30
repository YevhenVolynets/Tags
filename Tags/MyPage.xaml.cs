using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Tags
{
    public partial class MyPage : ContentPage
    {
        public MyPage()
        {
            InitializeComponent();
            shape.Points = new ObservableCollection<Point>
                {
                       new Point(50, 5),
                    new Point(370, 5),
                    new Point(420, 40),
                    new Point(0, 40),
                };
        }
    }
}
