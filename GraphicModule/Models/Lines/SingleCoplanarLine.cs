using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using GraphicModule.Models.Enums;

namespace GraphicModule.Models
{
    public class SingleCoplanarLine : Geometry
    {
        public LinesStructure Type = LinesStructure.SingleCoplanar;

        private List<Parameter> _parameters = new List<Parameter>
        {
            new Parameter(ParameterName.StripsWidth, 30),
            new Parameter(ParameterName.Slot,20),
            new Parameter(ParameterName.Slot,20),
            new Parameter(ParameterName.StripsNumber,1),
            new Parameter(ParameterName.StripsThickness,10),
            new Parameter(ParameterName.SubstrateHeight,20)
        };

        public SingleCoplanarLine()
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
