using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using GraphicModule.Models;
using Geometry = GraphicModule.Models.Geometry;

namespace GraphicModuleUI.ViewModels.Graphic
{
    public class SingleCoplanarGraphic : StructureImage
    {
        private Geometry _geometry;

        public double S1 { get; private set; }
        public double S2 { get; private set; }

        public double W1 { get; private set; }
        public double t { get; private set; }
        public double h { get; private set; }
        public double g { get; private set; }
        public double textSize { get; private set; }
        public double zoomt { get; private set; }
        public double zoomh { get; private set; }
        public double zoomw { get; private set; }
        public double zooms1 { get; private set; }
        public double zooms2 { get; private set; }

        public SingleCoplanarGraphic(Geometry geometry)
        {
            S1 = 20;
            S2 = 20;
            W1 = 30;
            t = 20;
            h = 20;
            g = 10;
            textSize = 5;
            zoomt = 0;
            zoomh = 0;
            zoomw = 0;
            zooms1 = 0;
            zooms2 = 0;
            _geometry = geometry;
            Canvas.SetLeft(this, 120);
            Canvas.SetTop(this, 120);
        }



        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            zoomt = (_geometry[ParameterName.StripsThickness].Value / _geometry[ParameterName.SubstrateHeight].Value);
            zoomh = (_geometry[ParameterName.SubstrateHeight].Value / _geometry[ParameterName.StripsThickness].Value);
            zooms1 = _geometry[ParameterName.Slot, 0].Value /
                         ((_geometry[ParameterName.Slot, 1].Value + _geometry[ParameterName.StripWidth].Value) / 2);
            zooms2 = _geometry[ParameterName.Slot, 1].Value /
                         ((_geometry[ParameterName.Slot, 0].Value + _geometry[ParameterName.StripWidth].Value) / 2);
            zoomw = _geometry[ParameterName.StripWidth].Value /
                        ((_geometry[ParameterName.Slot, 1].Value + _geometry[ParameterName.Slot, 0].Value) / 2);

            if (zoomh > 3)
            {
                zoomh = 3;
            }
            else if (zoomh < 0.3)
            {
                zoomh = 0.3;
            }
            if (zoomt > 3)
            {
                zoomt = 3;
            }
            else if (zoomt < 0.3)
            {
                zoomt = 0.3;
            }

            S1 *= zooms1;
            S2 *= zooms2;
            W1 *= zoomw;
            t *= zoomt;
            h *= zoomh;


            var wSolidBrush = new SolidColorBrush(Color.FromRgb(80, 80, 230));
            var hSolidBrsh = new SolidColorBrush(Color.FromRgb(140, 137, 126));
            var groundBrush = new SolidColorBrush(Colors.Black);

            var textWidth = new FormattedText("W", CultureInfo.GetCultureInfo("en-Us"), FlowDirection.LeftToRight,
                new Typeface("verdana"), textSize, Brushes.Red);
            var textS1 = new FormattedText("S1", CultureInfo.GetCultureInfo("en-Us"), FlowDirection.LeftToRight,
                new Typeface("verdana"), textSize, Brushes.Red);
            var textS2 = new FormattedText("S2", CultureInfo.GetCultureInfo("en-Us"), FlowDirection.LeftToRight,
                new Typeface("verdana"), textSize, Brushes.Red);
            var textSubstrateHeight = new FormattedText("h", CultureInfo.GetCultureInfo("en-Us"), FlowDirection.LeftToRight,
                new Typeface("verdana"), textSize, Brushes.Red);
            var textThickness = new FormattedText("t", CultureInfo.GetCultureInfo("en-Us"), FlowDirection.LeftToRight,
                new Typeface("verdana"), textSize, Brushes.Red);


            var myPen = new Pen(Brushes.Black, 0.1);
            var penLine = new Pen(Brushes.Red, 0.5);
            var substrateRect = new Rect(-(g + S1 + W1 / 2), 0, W1 + S1 + S2 + g * 2, h);
            var widthRect = new Rect(-W1 / 2, -t, W1, t);
            var groundLeft = new Rect(-(g + S1 + W1 / 2), -t, g, t);
            var groundRight = new Rect(W1 / 2 + S2, -t, g, t);

            dc.DrawRectangle(wSolidBrush, myPen, widthRect);
            dc.DrawRectangle(hSolidBrsh, myPen, substrateRect);
            dc.DrawRectangle(groundBrush, myPen, groundLeft);
            dc.DrawRectangle(groundBrush, myPen, groundRight);

            //Линии разделения для слотов и ширины
            dc.DrawLine(penLine, new Point(-(S1 + W1 / 2), -t), new Point(-(S1 + W1 / 2), -(t + 20)));
            dc.DrawLine(penLine, new Point(-W1 / 2, -t), new Point(-W1 / 2, -(t + 20)));
            dc.DrawLine(penLine, new Point(W1 / 2, -t), new Point(W1 / 2, -(t + 20)));
            dc.DrawLine(penLine, new Point(W1 / 2 + S2, -t), new Point(W1 / 2 + S2, -(t + 20)));
            dc.DrawLine(penLine, new Point(-(S1 + W1 / 2 + 5), -(t + 15)), new Point(W1 / 2 + S2 + 5, -(t + 15)));

            dc.DrawText(textWidth, new Point(-2, -(t + 25)));
            dc.DrawText(textS1, new Point(-(S1 / 2 + W1 / 2 + 4), -(t + 25)));
            dc.DrawText(textS2, new Point(W1 / 2 + S2 / 2 - 4, -(t + 25)));

            //Линии для разделения толщины подложки
            dc.DrawLine(penLine, new Point(-(g + S1 + W1 / 2), 0), new Point(-(25 + g + S1 + W1 / 2), 0));
            dc.DrawLine(penLine, new Point(-(g + S1 + W1 / 2), h), new Point(-(25 + g + S1 + W1 / 2), h));
            dc.DrawText(textSubstrateHeight, new Point(-(30 + g + S1 + W1 / 2), h / 2 - 3));

            //Линии для разделения толщины линии
            dc.DrawLine(penLine, new Point(-(g + S1 + W1 / 2), -t), new Point(-(25 + g + S1 + W1 / 2), -t));
            dc.DrawLine(penLine, new Point(-(20 + g + S1 + W1 / 2), -5 - t), new Point(-(20 + g + S1 + W1 / 2), h + 5));
            dc.DrawText(textThickness, new Point(-(30 + g + S1 + W1 / 2), -t / 2 - 5));
        }
    }
}
