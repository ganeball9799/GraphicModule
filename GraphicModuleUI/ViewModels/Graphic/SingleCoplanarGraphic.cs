using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using GraphicModule.Models;
using Geometry = GraphicModule.Models.Geometry;

namespace GraphicModuleUI.ViewModels.Graphic
{
    public class SingleCoplanarGraphic : StructureImage
    {
        private List<Parameter> _parameters = new List<Parameter>();
        private Geometry _geometry;
         
        public SingleCoplanarGraphic(List<Parameter> parameters, Geometry geometry)
        {
            _geometry = geometry;
            _parameters = parameters;
            Canvas.SetLeft(this, 100);
            Canvas.SetTop(this, 200);
        }

        protected override void OnRender(DrawingContext dc)
        {
            double h;
            double t;
            double S1;
            double W1;
            double S2;
            S1 = _geometry[ParameterName.Slot, 0].Value*2;
            //S1 = _parameters[0].Value * 2;
            //S2 = _parameters[1].Value * 2;
            S2 = _geometry[ParameterName.Slot, 1].Value;
            W1 = _parameters[2].Value * 2;
            t = _parameters[3].Value * 2;
            h = _geometry[ParameterName.SubstrateHeight].Value*2;
            //h = _parameters[4].Value * 2;
            var g = 20;
            var S = _parameters[1].ParameterName+ _parameters[1].Number.ToString();

            base.OnRender(dc);

            var wSolidBrush = new SolidColorBrush(Colors.Blue);
            var hSolidBrsh = new SolidColorBrush(Colors.LimeGreen);
            var groundBrush = new SolidColorBrush(Colors.Black);
            var text = new FormattedText(S, CultureInfo.GetCultureInfo("en-Us"), FlowDirection.LeftToRight,
                new Typeface("verdana"), 10, Brushes.Red);

            var myPen = new Pen(Brushes.Black, 0.1);
            var substrateRect = new Rect(0, 0, W1+ S1 + S2+g*2, h);
            var widthRect = new Rect(g+ S1, -t,W1,t);
            var groundLeft = new Rect(0, -t,g,t);
            var groundRight = new Rect(g+ S1 + S2+W1,-t,g,t);

            dc.DrawRectangle(wSolidBrush, myPen, widthRect);
            dc.DrawRectangle(hSolidBrsh, myPen, substrateRect);
            dc.DrawRectangle(groundBrush, myPen, groundLeft);
            dc.DrawRectangle(groundBrush, myPen, groundRight);
            dc.DrawText(text, new Point(50, 50));


        }
    }
}
