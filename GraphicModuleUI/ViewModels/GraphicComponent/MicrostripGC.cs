using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GraphicModuleUI.ViewModels.GraphicComponent
{
    public class MicrostripGC:GeometryVM
    {
        public MicrostripGC()
        {
            Canvas.SetLeft(this, 80);
            Canvas.SetTop(this, 40);
        }
        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            mySolidColorBrush.Color = Colors.LimeGreen;
            Pen myPen = new Pen(Brushes.Red, 10);
            Rect myRect = new Rect(10, 10, 50, 50);
            dc.DrawRectangle(mySolidColorBrush, myPen, myRect);
        }
    }
}
