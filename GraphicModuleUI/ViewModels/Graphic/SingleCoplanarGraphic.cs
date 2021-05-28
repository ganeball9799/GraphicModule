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

        private List<double> _slots = new List<double>();

        private double _stripWidth;
        private double _stripsThicknees;
        private double _substrateHeight;
        
        public SingleCoplanarGraphic(Geometry geometry)
        {
            _geometry = geometry;

            _slots.Add(_geometry[ParameterName.Slot].Value);
            _slots.Add(_geometry[ParameterName.Slot, 1].Value);
            _stripWidth = _geometry[ParameterName.StripWidth].Value;
            _stripsThicknees = _geometry[ParameterName.StripsThickness].Value;
            _substrateHeight = _geometry[ParameterName.SubstrateHeight].Value;

            Canvas.SetLeft(this, 120);
            Canvas.SetTop(this, 120);
        }

        private double Zoom(double zoom)
        {
            if (zoom > 2.5)
            {
                zoom = 2.5;
            }
            else if (zoom < 0.3)
            {
                zoom = 0.3;
            }
            return zoom;
        }

        private FormattedText Text(string mesure,double textSize)
        {
            var text = new FormattedText(mesure, CultureInfo.GetCultureInfo("en-Us"), FlowDirection.LeftToRight,
                new Typeface("verdana"), textSize, Brushes.DarkBlue);
            return text;
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            var screen = 10;
            var textSize = 6;
            var zoomt = (_stripsThicknees / _substrateHeight);
            var zoomh = (_substrateHeight / _stripsThicknees);
            var zooms1 = _slots[0] / ((_slots[1] + _stripWidth) / 2);
            var zooms2 = _slots[1] / ((_slots[0] + _stripWidth) / 2);
            var zoomw = _stripWidth / ((_slots[0] + _slots[1]) / 2);

            var S1 = 20 * Zoom(zooms1) + _slots[0] / 5;
            var S2 = 20 * Zoom(zooms2) + _slots[1] / 5;
            var W1 = 30 * Zoom(zoomw) + _stripWidth / 5;
            var t = 20 * Zoom(zoomt) + _stripsThicknees / 5;
            var h = 20 * Zoom(zoomh) + _substrateHeight / 5;

            var wSolidBrush = new SolidColorBrush(Color.FromRgb(80, 80, 230));
            var hSolidBrsh = new SolidColorBrush(Color.FromRgb(140, 137, 126));
            var groundBrush = new SolidColorBrush(Colors.Black);


            var textWidth = Text("W", textSize);
            var textS1 = Text("S1", textSize);
            var textS2 = Text("S2", textSize);
            var textSubstrateHeight = Text("h", textSize);
            var textThickness = Text("t", textSize);

            var myPen = new Pen(Brushes.Black, 0.1);
            var penLine = new Pen(Brushes.Red, 0.5);
            var substrateRect = new Rect(-(screen + S1 + W1 / 2), 0, W1 + S1 + S2 + screen * 2, h);
            var widthRect = new Rect(-W1 / 2, -t, W1, t);
            var groundLeft = new Rect(-(screen + S1 + W1 / 2), -t, screen, t);
            var groundRight = new Rect(W1 / 2 + S2, -t, screen, t);

            dc.DrawRectangle(wSolidBrush, myPen, widthRect);
            dc.DrawRectangle(hSolidBrsh, myPen, substrateRect);
            dc.DrawRectangle(groundBrush, myPen, groundLeft);
            dc.DrawRectangle(groundBrush, myPen, groundRight);

            //Линии разделения для слотов и ширины
            dc.DrawLine(penLine, new Point(-(S1 + W1 / 2), -t), new Point(-(S1 + W1 / 2), -(t + 15)));
            dc.DrawLine(penLine, new Point(-W1 / 2, -t), new Point(-W1 / 2, -(t + 15)));
            dc.DrawLine(penLine, new Point(W1 / 2, -t), new Point(W1 / 2, -(t + 15)));
            dc.DrawLine(penLine, new Point(W1 / 2 + S2, -t), new Point(W1 / 2 + S2, -(t + 15)));
            dc.DrawLine(penLine, new Point(-(S1 + W1 / 2 + 5), -(t + 10)), new Point(W1 / 2 + S2 + 5, -(t + 10)));

            dc.DrawText(textWidth, new Point(-2, -(t + 20)));
            dc.DrawText(textS1, new Point(-(S1 / 2 + W1 / 2 + 4), -(t + 20)));
            dc.DrawText(textS2, new Point(W1 / 2 + S2 / 2 - 4, -(t + 20)));

            //Линии для разделения толщины подложки
            dc.DrawLine(penLine, new Point(-(screen + S1 + W1 / 2), 0), new Point(-(10 + screen + S1 + W1 / 2), 0));
            dc.DrawLine(penLine, new Point(-(screen + S1 + W1 / 2), h), new Point(-(10 + screen + S1 + W1 / 2), h));
            dc.DrawText(textSubstrateHeight, new Point(-(15 + screen + S1 + W1 / 2), h / 2 - 3));

            //Линии для разделения толщины линии
            dc.DrawLine(penLine, new Point(-(screen + S1 + W1 / 2), -t), new Point(-(10 + screen + S1 + W1 / 2), -t));
            dc.DrawLine(penLine, new Point(-(5 + screen + S1 + W1 / 2), -5 - t), new Point(-(5 + screen + S1 + W1 / 2), h + 5));
            dc.DrawText(textThickness, new Point(-(15 + screen + S1 + W1 / 2), -t / 2 - 5));
        }
    }
}
