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
    public class MicrostripGraphic : StructureImage
    {
        private Geometry _geometry;

        private double numberOfLine;
        private double stripsThicknees;
        private double substrateHeight;
        private List<double> _stripWidths = new List<double>();

        private double stripWidth1;
        private double stripWidth2;
        private double stripWidth3;
        private double stripWidth4;
        private double stripWidth5;
        private double stripWidth6;
        private List<double> _slots = new List<double>();

        private double slot1;
        private double slot2;
        private double slot3;
        private double slot4;
        private double slot5;



        private const double gap = 10;
        private const double ground = 5;
        private const double textSize = 5;
        private FormattedText text;


        private double zoomt;
        private double zoomh;
        private double zoomw1;
        private double zoomw2;
        private double zoomw3;
        private double zoomw4;
        private double zoomw5;
        private double zoomw6;
        private double zooms1;
        private double zooms2;
        private double zooms3;
        private double zooms4;
        private double zooms5;



        public MicrostripGraphic(Geometry geometry)
        {
            _geometry = geometry;
            numberOfLine = _geometry[ParameterName.StripsNumber].Value;
            stripsThicknees = _geometry[ParameterName.StripsThickness].Value;
            substrateHeight = _geometry[ParameterName.SubstrateHeight].Value;

            Canvas.SetLeft(this, 120);
            Canvas.SetTop(this, 120);
        }

        private double ZoomTH(double zoom)
        {
            if (zoom > 1.6)
            {
                zoom = 1.6;
            }
            else if (zoom < 0.2)
            {
                zoom = 0.2;
            }

            return zoom;
        }



        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            var z = 0.5;
            double n = numberOfLine;

            var myPen = new Pen(Brushes.Black, 0.1);

            var widthBrush = new SolidColorBrush(Color.FromRgb(80, 80, 230));
            var substrateHeightBrush = new SolidColorBrush(Color.FromRgb(140, 137, 126));
            var groundBrush = new SolidColorBrush(Colors.Black);
            var penLine = new Pen(Brushes.Red, 0.5);

            var textWidth = Text("W1", textSize);
            var textWidth2 = Text("W2", textSize);
            var textWidth3 = Text("W3", textSize);
            var textWidth4 = Text("W4", textSize);
            var textWidth5 = Text("W5", textSize);
            var textWidth6 = Text("W6", textSize);
            var textS1 = Text("S1", textSize);
            var textS2 = Text("S2", textSize);
            var textS3 = Text("S3", textSize);
            var textS4 = Text("S4", textSize);
            var textS5 = Text("S5", textSize);
            var textSubstrateHeight = Text("h", textSize);
            var textThickness = Text("t", textSize);


            if (n == 1)
            {
                _stripWidths.Add(_geometry[ParameterName.StripWidth, 0].Value);

                zoomw1 = _stripWidths[0] / ((substrateHeight + stripsThicknees) / 2);
                zoomh = substrateHeight / _stripWidths[0];
                zoomt = stripsThicknees / ((_stripWidths[0] + substrateHeight) / 2);

                double W1 = 40 * ZoomTH(zoomw1);
                double h = 15 * ZoomTH(zoomh);
                double t = 15 * ZoomTH(zoomt);

                var substrateRect = new Rect(-(W1 + gap * 2) / 2, 0, W1 + gap * 2, h);
                var groundRect = new Rect(-(W1 + gap * 2) / 2, h, W1 + gap * 2, ground);
                var widthRect = new Rect(-W1 / 2, -t, W1, t);

                dc.DrawRectangle(widthBrush, myPen, widthRect);
                dc.DrawRectangle(substrateHeightBrush, myPen, substrateRect);
                dc.DrawRectangle(groundBrush, myPen, groundRect);

                //Линии для разделения ширин и зазоров
                dc.DrawLine(penLine, new Point(-W1 / 2, -t), new Point(-W1 / 2, -(t + 10)));
                dc.DrawLine(penLine, new Point(W1 / 2, -t), new Point(W1 / 2, -(t + 10)));
                dc.DrawLine(penLine, new Point(-5 - W1 / 2, -(t + 5)), new Point(W1 / 2 + 5, -(t + 5)));

                //Линии для разделения толщин линии и подложки
                dc.DrawLine(penLine, new Point(-10 - (W1 + gap * 2) / 2, 0), new Point(-(W1 + gap * 2) / 2, 0));
                dc.DrawLine(penLine, new Point(-10 - (W1 + gap * 2) / 2, h), new Point(-(W1 + gap * 2) / 2, h));
                dc.DrawLine(penLine, new Point(-10 - (W1 + gap * 2) / 2, -t), new Point(-W1 / 2, -t));
                dc.DrawLine(penLine, new Point(-5 - (W1 + gap * 2) / 2, -t - 5), new Point(-5 - (W1 + gap * 2) / 2, h + 5));

                //Подписи зазоров и ширин линий
                dc.DrawText(textWidth, new Point(-2, -(t + 15)));

                //Подписи толщин линии и подложки
                dc.DrawText(textSubstrateHeight, new Point(-15 - (W1 + gap * 2) / 2, h / 2 - 4));
                dc.DrawText(textThickness, new Point(-15 - (W1 + gap * 2) / 2, -(t / 2) - 4));

            }
            else if (n == 2)
            {
                _stripWidths.Add(_geometry[ParameterName.StripWidth, 0].Value);
                _stripWidths.Add(_geometry[ParameterName.StripWidth, 1].Value);
                _slots.Add(_geometry[ParameterName.Slot, 0].Value);
                stripWidth1 = _geometry[ParameterName.StripWidth, 0].Value;
                stripWidth2 = _geometry[ParameterName.StripWidth, 1].Value;
                slot1 = _geometry[ParameterName.Slot, 0].Value;

                zoomw1 = _stripWidths[0] / ((_slots[0] + _stripWidths[1] + substrateHeight) / 3);
                zoomw2 = _stripWidths[1] / ((_stripWidths[0] + _slots[0] + substrateHeight) / 3);
                zooms1 = _slots[0] / ((_stripWidths[0] + _stripWidths[1] + substrateHeight) / 3);
                zoomh = substrateHeight / ((_stripWidths[0] + _stripWidths[1] + _slots[0]) / 3);
                zoomt = stripsThicknees / ((substrateHeight + _stripWidths[0] + _stripWidths[1] + _slots[0]) / 4);

                double W1 = 30 * ZoomTH(zoomw1);
                double W2 = 30 * ZoomTH(zoomw2);
                double S1 = 20 * ZoomTH(zooms1);
                double h = 20 * ZoomTH(zoomh);
                double t = 15 * ZoomTH(zoomt);

                var substrateRect = new Rect(-(gap + W1), 0, W1 + S1 + W2 + gap * 2, h);
                var groundRect = new Rect(-(gap + W1), h, W1 + S1 + W2 + gap * 2, ground);

                var widthRect = new Rect(-W1, -t, W1, t);
                var widthRect2 = new Rect(S1, -t, W2, t);
                dc.DrawRectangle(widthBrush, myPen, widthRect);
                dc.DrawRectangle(widthBrush, myPen, widthRect2);
                dc.DrawRectangle(substrateHeightBrush, myPen, substrateRect);
                dc.DrawRectangle(groundBrush, myPen, groundRect);

                //Линии для разделения ширин и зазоров
                dc.DrawLine(penLine, new Point(-W1, -t), new Point(-W1, -(t + 10)));
                dc.DrawLine(penLine, new Point(-z, -t), new Point(-z, -(t + 10)));
                dc.DrawLine(penLine, new Point(S1 + z, -t), new Point(S1 + z, -(t + 10)));
                dc.DrawLine(penLine, new Point(S1 - z + W2, -t), new Point(S1 - z + W2, -(t + 10)));

                dc.DrawLine(penLine, new Point(-(gap - 5 + W1), -(t + 5)), new Point(S1 + W2 + gap - 5, -(t + 5)));

                //Линии для разделения толщин линии и подложки
                dc.DrawLine(penLine, new Point(-(W1 + 20), 0), new Point(-(gap + W1), 0));
                dc.DrawLine(penLine, new Point(-(W1 + 20), h), new Point(-(gap + W1), h));
                dc.DrawLine(penLine, new Point(-(W1 + 20), -t), new Point(-W1, -t));
                dc.DrawLine(penLine, new Point(-(W1 + 15), -t - 5), new Point(-(W1 + 15), h + 5));

                //Подписи зазоров и ширин линий
                dc.DrawText(textWidth, new Point(-(W1 / 2 + 3), -(t + 15)));
                dc.DrawText(textWidth2, new Point(S1 + W2 / 2 - 3, -(t + 15)));
                dc.DrawText(textS1, new Point(S1 / 2 - 2, -(t + 15)));

                //Подписи толщин линии и подложки
                dc.DrawText(textSubstrateHeight, new Point(-(W1 + 20), h / 2 - 3));
                dc.DrawText(textThickness, new Point(-(W1 + 20), -(t / 2) - 3));
            }
            else if (n == 3)
            {
                _stripWidths.Add(_geometry[ParameterName.StripWidth, 0].Value);
                _stripWidths.Add(_geometry[ParameterName.StripWidth, 1].Value);
                _stripWidths.Add(_geometry[ParameterName.StripWidth, 2].Value);
                _slots.Add(_geometry[ParameterName.Slot, 0].Value);
                _slots.Add(_geometry[ParameterName.Slot, 1].Value);

                zoomw1 = _stripWidths[0] / ((_slots[0] + _slots[1] + _stripWidths[1] + _stripWidths[2] + substrateHeight) / 5);
                zoomw2 = _stripWidths[1] / ((_stripWidths[0] + _stripWidths[2] + _slots[0] + _slots[1] + substrateHeight) / 5);
                zoomw3 = _stripWidths[2] / ((_stripWidths[0] + _stripWidths[1] + _slots[0] + _slots[1] + substrateHeight) / 5);
                zooms1 = _slots[0] / ((_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _slots[1] + substrateHeight) / 5);
                zooms2 = _slots[1] / ((_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _slots[0] + substrateHeight) / 5);
                zoomh = substrateHeight / ((_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + stripsThicknees) / 4);
                zoomt = stripsThicknees / ((substrateHeight + _stripWidths[0] + _stripWidths[1] + _stripWidths[2]) / 4);

                double W1 = 20 * ZoomTH(zoomw1);
                double W2 = 20 * ZoomTH(zoomw2);
                double W3 = 20 * ZoomTH(zoomw3);
                double S1 = 10 * ZoomTH(zooms1);
                double S2 = 10 * ZoomTH(zooms2);
                double h = 15 * ZoomTH(zoomh);
                double t = 15 * ZoomTH(zoomt);


                var substrateRect = new Rect(-(W1 + S1 + gap), 0, W1 + S1 + W2 + W3 + S2 + gap * 2, h);
                var groundRect = new Rect(-(W1 + S1 + gap), h, W1 + S1 + W2 + W3 + S2 + gap * 2, ground);
                var widthRect = new Rect(-(W1 + S1), -t, W1, t);
                var widthRect2 = new Rect(0, -t, W2, t);
                var widthRect3 = new Rect(W2 + S2, -t, W3, t);
                dc.DrawRectangle(widthBrush, myPen, widthRect);
                dc.DrawRectangle(widthBrush, myPen, widthRect2);
                dc.DrawRectangle(widthBrush, myPen, widthRect3);
                dc.DrawRectangle(substrateHeightBrush, myPen, substrateRect);
                dc.DrawRectangle(groundBrush, myPen, groundRect);

                //Линии для разделения ширин и зазоров
                dc.DrawLine(penLine, new Point(-(W1 + S1 - z), -t), new Point(-(W1 + S1 - z), -(t + 10)));
                dc.DrawLine(penLine, new Point(-S1 - z, -t), new Point(-S1 - z, -(t + 10)));
                dc.DrawLine(penLine, new Point(z, -t), new Point(z, -(t + 10)));
                dc.DrawLine(penLine, new Point(W2 - z, -t), new Point(W2 - z, -(t + 10)));
                dc.DrawLine(penLine, new Point(S2 + W2 + z, -t), new Point(S2 + W2 + z, -(t + 10)));
                dc.DrawLine(penLine, new Point(S2 + W2 + W3 - z, -t), new Point(S2 + W2 + W3 - z, -(t + 10)));

                dc.DrawLine(penLine, new Point(-(gap - 5 + W1 + S1), -(t + 5)), new Point(S2 + W2 + W3 + gap - 5, -(t + 5)));

                //Линии для разделения толщин линии и подложки
                dc.DrawLine(penLine, new Point(-(W1 + S1 + gap + 10), 0), new Point(-(W1 + S1 + gap), 0));
                dc.DrawLine(penLine, new Point(-(W1 + S1 + gap + 10), h), new Point(-(W1 + S1 + gap), h));
                dc.DrawLine(penLine, new Point(-(W1 + S1 + gap + 10), -t), new Point(-(W1 + S1), -t));
                dc.DrawLine(penLine, new Point(-(W1 + S1 + gap + 5), -t - 5), new Point(-(W1 + S1 + gap + 5), h + 5));

                //Подписи зазоров и ширин линий
                dc.DrawText(textWidth, new Point(-(W1 / 2 + S1 + 2), -(t + 15)));
                dc.DrawText(textWidth2, new Point(W2 / 2 - 4, -(t + 15)));
                dc.DrawText(textWidth3, new Point(W2 + S2 + W3 / 2 - 4, -(t + 15)));
                dc.DrawText(textS1, new Point(-(S1 / 2 + 2), -(t + 15)));
                dc.DrawText(textS2, new Point(W2 + S2 / 2 - 2, -(t + 15)));

                //Подписи толщин линии и подложки
                dc.DrawText(textSubstrateHeight, new Point(-(W1 + S1 + gap + 10), h / 2 - 3));
                dc.DrawText(textThickness, new Point(-(W1 + S1 + gap + 10), -(t / 2) - 3));
            }
            else if (n == 4)
            {
                _stripWidths.Add(_geometry[ParameterName.StripWidth, 0].Value);
                _stripWidths.Add(_geometry[ParameterName.StripWidth, 1].Value);
                _stripWidths.Add(_geometry[ParameterName.StripWidth, 2].Value);
                _stripWidths.Add(_geometry[ParameterName.StripWidth, 3].Value);
                _slots.Add(_geometry[ParameterName.Slot, 0].Value);
                _slots.Add(_geometry[ParameterName.Slot, 1].Value);
                _slots.Add(_geometry[ParameterName.Slot, 2].Value);

                zoomw1 = _stripWidths[0] / ((_slots[0] + _slots[1] + _slots[2] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3]+ substrateHeight) / 7);
                zoomw2 = _stripWidths[1] / ((_stripWidths[0] + _stripWidths[2] + _stripWidths[3] + _slots[0] + _slots[1] + _slots[2] + substrateHeight) / 7);
                zoomw3 = _stripWidths[2] / ((_stripWidths[0] + _stripWidths[1] + _stripWidths[3] + _slots[0] + _slots[1] + _slots[2] + substrateHeight) / 7);
                zoomw4 = _stripWidths[3] / ((_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _slots[0] + _slots[1] + _slots[2] + substrateHeight) / 7);
                zooms1 = _slots[0] / ((_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _slots[1] + _slots[2]) / 6);
                zooms2 = _slots[1] / ((_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _slots[0] + _slots[2]) / 6);
                zooms3 = _slots[2] / ((_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _slots[0] + _slots[1]) / 6);
                zoomh = substrateHeight / ((_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + stripsThicknees) / 5);
                zoomt = stripsThicknees / ((substrateHeight + _stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3]) / 5);

                double W1 = 15 * ZoomTH(zoomw1);
                double W2 = 15 * ZoomTH(zoomw2);
                double W3 = 15 * ZoomTH(zoomw3);
                double W4 = 15 * ZoomTH(zoomw4);
                double S1 = 10 * ZoomTH(zooms1);
                double S2 = 10 * ZoomTH(zooms2);
                double S3 = 10 * ZoomTH(zooms3);
                double h = 15 * ZoomTH(zoomh);
                double t = 15 * ZoomTH(zoomt);

                var substrateRect = new Rect(-(gap + W1 + S1 + W2), 0, W1 + S1 + W2 + W3 + W4 + S2 + S3 + gap * 2, h);
                var groundRect = new Rect(-(gap + W1 + S1 + W2), h, W1 + S1 + W2 + W3 + W4 + S2 + S3 + gap * 2, ground);

                var widthRect = new Rect(-(W1 + S1 + W2), -t, W1, t);
                var widthRect2 = new Rect(-W2, -t, W2, t);
                var widthRect3 = new Rect(S2, -t, W3, t);
                var widthRect4 = new Rect(S2 + S3 + W3, -t, W4, t);

                dc.DrawRectangle(widthBrush, myPen, widthRect);
                dc.DrawRectangle(widthBrush, myPen, widthRect2);
                dc.DrawRectangle(widthBrush, myPen, widthRect3);
                dc.DrawRectangle(widthBrush, myPen, widthRect4);
                dc.DrawRectangle(substrateHeightBrush, myPen, substrateRect);
                dc.DrawRectangle(groundBrush, myPen, groundRect);

                //Линии для разделения ширин и зазоров
                dc.DrawLine(penLine, new Point(-(W1 + S1 + W2 - z), -t), new Point(-(W1 + S1 + W2 - z), -(t + 10)));
                dc.DrawLine(penLine, new Point(-(W2 + S1 + z), -t), new Point(-(W2 + S1 + z), -(t + 10)));
                dc.DrawLine(penLine, new Point(-W2 + z, -t), new Point(-W2 + z, -(t + 10)));
                dc.DrawLine(penLine, new Point(-z, -t), new Point(-z, -(t + 10)));
                dc.DrawLine(penLine, new Point(S2 + z, -t), new Point(S2 + z, -(t + 10)));
                dc.DrawLine(penLine, new Point(S2 + W3 - z, -t), new Point(S2 + W3 - z, -(t + 10)));
                dc.DrawLine(penLine, new Point(S2 + W3 + S3 + z, -t), new Point(S2 + W3 + S3 + z, -(t + 10)));
                dc.DrawLine(penLine, new Point(S2 + W3 + S3 + W4 - z, -t), new Point(S2 + W3 + S3 + W4 - z, -(t + 10)));

                dc.DrawLine(penLine, new Point(-(gap + W1 + S1 + W2 - 5), -(t + 5)), new Point(S2 + W3 + S3 + W4 + gap - 5, -(t + 5)));

                //Линии для разделения толщин линии и подложки
                dc.DrawLine(penLine, new Point(-(gap + W1 + S1 + W2 + 10), 0), new Point(-(W1 + S1 + W2 + gap), 0));
                dc.DrawLine(penLine, new Point(-(gap + W1 + S1 + W2 + 10), h), new Point(-(W1 + S1 + W2 + gap), h));
                dc.DrawLine(penLine, new Point(-(gap + W1 + S1 + W2 + 10), -t), new Point(-(W1 + S1 + W2), -t));

                dc.DrawLine(penLine, new Point(-(gap + W1 + S1 + W2 + 5), -t - 5), new Point(-(gap + W1 + S1 + W2 + 5), h + 5));

                //Подписи зазоров и ширин линий
                dc.DrawText(textWidth, new Point(-(W1 / 2 + S1 + W2 + 3), -(t + 15)));
                dc.DrawText(textWidth2, new Point(-(W2 / 2 + 3), -(t + 15)));
                dc.DrawText(textWidth3, new Point(S2 + W3 / 2 - 3, -(t + 15)));
                dc.DrawText(textWidth4, new Point(S2 + S3 + W3 + W4 / 2 - 3, -(t + 15)));
                dc.DrawText(textS1, new Point(-(S1 / 2 + W2 + 3), -(t + 15)));
                dc.DrawText(textS2, new Point(S2 / 2 - 3, -(t + 15)));
                dc.DrawText(textS3, new Point(W3 + S2 + S3 / 2 - 3, -(t + 15)));

                //Подписи толщин линии и подложки
                dc.DrawText(textSubstrateHeight, new Point(-(gap + W1 + S1 + W2 + 10), h / 2 - 3));
                dc.DrawText(textThickness, new Point(-(gap + W1 + S1 + W2 + 10), -(t / 2) - 3));
            }
            else if (n == 5)
            {
                _stripWidths.Add(_geometry[ParameterName.StripWidth, 0].Value);
                _stripWidths.Add(_geometry[ParameterName.StripWidth, 1].Value);
                _stripWidths.Add(_geometry[ParameterName.StripWidth, 2].Value);
                _stripWidths.Add(_geometry[ParameterName.StripWidth, 3].Value);
                _stripWidths.Add(_geometry[ParameterName.StripWidth, 4].Value);
                _slots.Add(_geometry[ParameterName.Slot, 0].Value);
                _slots.Add(_geometry[ParameterName.Slot, 1].Value);
                _slots.Add(_geometry[ParameterName.Slot, 2].Value);
                _slots.Add(_geometry[ParameterName.Slot, 3].Value);


                zoomw1 = _stripWidths[0] / ((_stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _stripWidths[4] + _slots[0] + _slots[1] + _slots[2] + _slots[3] + substrateHeight) / 8);
                zoomw2 = _stripWidths[1] / ((_stripWidths[0] + _stripWidths[2] + _stripWidths[3] + _stripWidths[4] + _slots[0] + _slots[1] + _slots[2] + _slots[3] + substrateHeight) / 8);
                zoomw3 = _stripWidths[2] / ((_stripWidths[0] + _stripWidths[1] + _stripWidths[3] + _stripWidths[4] + _slots[0] + _slots[1] + _slots[2] + _slots[3] + substrateHeight) / 8);
                zoomw4 = _stripWidths[3] / ((_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[4] + _slots[0] + _slots[1] + _slots[2] + _slots[3] + substrateHeight) / 8);
                zoomw5 = _stripWidths[4] / ((_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _slots[0] + _slots[1] + _slots[2] + _slots[3] + substrateHeight) / 8);
                zooms1 = _slots[0] / ((_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _stripWidths[4] + _slots[1] + _slots[2] + _slots[3]) / 8);
                zooms2 = _slots[1] / ((_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _stripWidths[4] + _slots[0] + _slots[2] + _slots[3]) / 8);
                zooms3 = _slots[2] / ((_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _stripWidths[4] + _slots[0] + _slots[1] + _slots[3]) / 8);
                zooms4 = _slots[3] / ((_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _stripWidths[4] + _slots[0] + _slots[1] + _slots[2]) / 8);
                zoomh = substrateHeight / ((_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _stripWidths[4] + stripsThicknees) / 5);
                zoomt = stripsThicknees / ((substrateHeight + _stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _stripWidths[4]) / 5);


                double W1 = 15 * ZoomTH(zoomw1);
                double W2 = 15 * ZoomTH(zoomw2);
                double W3 = 15 * ZoomTH(zoomw3);
                double W4 = 15 * ZoomTH(zoomw4);
                double W5 = 15 * ZoomTH(zoomw5);
                double S1 = 10 * ZoomTH(zooms1);
                double S2 = 10 * ZoomTH(zooms2);
                double S3 = 10 * ZoomTH(zooms3);
                double S4 = 10 * ZoomTH(zooms4);
                double h = 15 * ZoomTH(zoomh);
                double t = 15 * ZoomTH(zoomt);

                var substrateRect = new Rect(-(W1 + S1 + W2 + S2 + gap), 0, W1 + S1 + W2 + W3 + W4 + W5 + S2 + S3 + S4 + gap * 2, h);
                var groundRect = new Rect(-(W1 + S1 + W2 + S2 + gap), h, W1 + S1 + W2 + W3 + W4 + W5 + S2 + S3 + S4 + gap * 2, ground);

                var widthRect = new Rect(-(W1 + S1 + W2 + S2), -t, W1, t);
                var widthRect2 = new Rect(-(W2 + S2), -t, W2, t);
                var widthRect3 = new Rect(0, -t, W3, t);
                var widthRect4 = new Rect(W3 + S3, -t, W4, t);
                var widthRect5 = new Rect(W3 + S3 + W4 + S4, -t, W5, t);

                dc.DrawRectangle(widthBrush, myPen, widthRect);
                dc.DrawRectangle(widthBrush, myPen, widthRect2);
                dc.DrawRectangle(widthBrush, myPen, widthRect3);
                dc.DrawRectangle(widthBrush, myPen, widthRect4);
                dc.DrawRectangle(widthBrush, myPen, widthRect5);
                dc.DrawRectangle(substrateHeightBrush, myPen, substrateRect);
                dc.DrawRectangle(groundBrush, myPen, groundRect);

                //Линии для разделения ширин и зазоров
                dc.DrawLine(penLine, new Point(-(W1 + S1 + W2 + S2 - z), -t), new Point(-(W1 + S1 + W2 + S2 - z), -(t + 10)));
                dc.DrawLine(penLine, new Point(-(S1 + W2 + S2 + z), -t), new Point(-(S1 + W2 + S2 + z), -(t + 10)));
                dc.DrawLine(penLine, new Point(-(S2 + W2 - z), -t), new Point(-(S2 + W2 - z), -(t + 10)));
                dc.DrawLine(penLine, new Point(-(S2 + z), -t), new Point(-(S2 + z), -(t + 10)));
                dc.DrawLine(penLine, new Point(z, -t), new Point(z, -(t + 10)));
                dc.DrawLine(penLine, new Point(W3 - z, -t), new Point(W3 - z, -(t + 10)));
                dc.DrawLine(penLine, new Point(W3 + S3 + z, -t), new Point(W3 + S3 + z, -(t + 10)));
                dc.DrawLine(penLine, new Point(W3 + S3 + W4 - z, -t), new Point(W3 + S3 + W4 - z, -(t + 10)));
                dc.DrawLine(penLine, new Point(W3 + S3 + W4 + S4 + z, -t), new Point(W3 + S3 + W4 + S4 + z, -(t + 10)));
                dc.DrawLine(penLine, new Point(W3 + S3 + W4 + S4 + W5 - z, -t), new Point(W3 + S3 + W4 + S4 + W5 - z, -(t + 10)));

                dc.DrawLine(penLine, new Point(-(gap + W1 + S1 + W2 + S2 - 5), -(t + 5)), new Point(W3 + S3 + W4 + S4 + W5 + gap - 5, -(t + 5)));

                //Линии для разделения толщин линии и подложки
                dc.DrawLine(penLine, new Point(-(gap + W1 + S1 + W2 + S2 + 10), 0), new Point(-(W1 + S1 + W2 + S2 + gap), 0));
                dc.DrawLine(penLine, new Point(-(gap + W1 + S1 + W2 + S2 + 10), h), new Point(-(W1 + S1 + W2 + S2 + gap), h));
                dc.DrawLine(penLine, new Point(-(gap + W1 + S1 + W2 + S2 + 10), -t), new Point(-(W1 + S1 + W2 + S2), -t));

                dc.DrawLine(penLine, new Point(-(gap + W1 + S1 + W2 + S2 + 5), -t - 5), new Point(-(gap + W1 + S1 + W2 + S2 + 5), h + 5));

                //Подписи зазоров и ширин линий
                dc.DrawText(textWidth, new Point(-(W1 / 2 + S1 + W2 + S2 + 3), -(t + 15)));
                dc.DrawText(textWidth2, new Point(-(W2 / 2 + S2 + 3), -(t + 15)));
                dc.DrawText(textWidth3, new Point(W3 / 2 - 3, -(t + 15)));
                dc.DrawText(textWidth4, new Point(S3 + W3 + W4 / 2 - 3, -(t + 15)));
                dc.DrawText(textWidth5, new Point(S3 + W3 + W4 + S4 + W5 / 2 - 3, -(t + 15)));
                dc.DrawText(textS1, new Point(-(S1 / 2 + W2 + S2 + 2), -(t + 15)));
                dc.DrawText(textS2, new Point(-S2 / 2 - 2, -(t + 15)));
                dc.DrawText(textS3, new Point(W3 + S3 / 2 - 2, -(t + 15)));
                dc.DrawText(textS4, new Point(W3 + S3 + W4 + S4 / 2 - 2, -(t + 15)));

                //Подписи толщин линии и подложки
                dc.DrawText(textSubstrateHeight, new Point(-(gap + W1 + S1 + W2 + S2 + 10), h / 2 - 3));
                dc.DrawText(textThickness, new Point(-(gap + W1 + S1 + W2 + S2 + 10), -(t / 2) - 3));

            }
            else if (n == 6)
            {
                _stripWidths.Add(_geometry[ParameterName.StripWidth, 0].Value);
                _stripWidths.Add(_geometry[ParameterName.StripWidth, 1].Value);
                _stripWidths.Add(_geometry[ParameterName.StripWidth, 2].Value);
                _stripWidths.Add(_geometry[ParameterName.StripWidth, 3].Value);
                _stripWidths.Add(_geometry[ParameterName.StripWidth, 4].Value);
                _stripWidths.Add(_geometry[ParameterName.StripWidth, 5].Value);
                _slots.Add(_geometry[ParameterName.Slot, 0].Value);
                _slots.Add(_geometry[ParameterName.Slot, 1].Value);
                _slots.Add(_geometry[ParameterName.Slot, 2].Value);
                _slots.Add(_geometry[ParameterName.Slot, 3].Value);
                _slots.Add(_geometry[ParameterName.Slot, 4].Value);

                zoomw1 = _stripWidths[0] / ((_stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _stripWidths[4] + _stripWidths[5] + _slots[0] + _slots[1] + _slots[2] + _slots[3] + _slots[4] + substrateHeight) / 11);
                zoomw2 = _stripWidths[1] / ((_stripWidths[0] + _stripWidths[2] + _stripWidths[3] + _stripWidths[4] + _stripWidths[5] + _slots[0] + _slots[1] + _slots[2] + _slots[3] + _slots[4] + substrateHeight) / 11);
                zoomw3 = _stripWidths[2] / ((_stripWidths[0] + _stripWidths[1] + _stripWidths[3] + _stripWidths[4] + _stripWidths[5] + _slots[0] + _slots[1] + _slots[2] + _slots[3] + _slots[4] + substrateHeight) / 11);
                zoomw4 = _stripWidths[3] / ((_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[4] + _stripWidths[5] + _slots[0] + _slots[1] + _slots[2] + _slots[3] + _slots[4] + substrateHeight) / 11);
                zoomw5 = _stripWidths[4] / ((_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _stripWidths[5] + _slots[0] + _slots[1] + _slots[2] + _slots[3] + _slots[4] + substrateHeight) / 11);
                zoomw6 = _stripWidths[5] / ((_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _stripWidths[4] + _slots[0] + _slots[1] + _slots[2] + _slots[3] + _slots[4] + substrateHeight) / 11);

                zooms1 = _slots[0] / ((_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _stripWidths[4] + _slots[1] + _slots[2] + _slots[3] + _slots[4]) / 9);
                zooms2 = _slots[1] / ((_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _stripWidths[4] + _slots[0] + _slots[2] + _slots[3] + _slots[4]) / 9);
                zooms3 = _slots[2] / ((_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _stripWidths[4] + _slots[0] + _slots[1] + _slots[3] + _slots[4]) / 9);
                zooms4 = _slots[3] / ((_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _stripWidths[4] + _slots[0] + _slots[1] + _slots[2] + _slots[4]) / 9);
                zooms5 = _slots[4] / ((_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _stripWidths[4] + _slots[0] + _slots[1] + _slots[2] + _slots[3]) / 9);
                zoomh = substrateHeight / ((_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _stripWidths[4] + _stripWidths[5] + stripsThicknees) / 7);
                zoomt = stripsThicknees / ((substrateHeight + _stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _stripWidths[4] + _stripWidths[5]) / 7);

                double W1 = 12 * ZoomTH(zoomw1);
                double W2 = 12 * ZoomTH(zoomw2);
                double W3 = 12 * ZoomTH(zoomw3);
                double W4 = 12 * ZoomTH(zoomw4);
                double W5 = 12 * ZoomTH(zoomw5);
                double W6 = 12 * ZoomTH(zoomw6);
                double S1 = 8 * ZoomTH(zooms1);
                double S2 = 8 * ZoomTH(zooms2);
                double S3 = 8 * ZoomTH(zooms3);
                double S4 = 8 * ZoomTH(zooms4); 
                double S5 = 8 * ZoomTH(zooms5);
                double h = 15 * ZoomTH(zoomh);
                double t = 10 * ZoomTH(zoomt);


                var substrateRect = new Rect(-(gap + W1 + S1 + W2 + S2 + W3), 0, W1 + S1 + W2 + W3 + W4 + W5 + W6 + S2 + S3 + S4 + S5 + gap * 2, h);
                var groundRect = new Rect(-(gap + W1 + S1 + W2 + S2 + W3), h, W1 + S1 + W2 + W3 + W4 + W5 + W6 + S2 + S3 + S4 + S5 + gap * 2, ground);
                var widthRect = new Rect(-(W1 + S1 + W2 + S2 + W3), -t, W1, t);
                var widthRect2 = new Rect(-(W2 + S2 + W3), -t, W2, t);
                var widthRect3 = new Rect(-W3, -t, W3, t);
                var widthRect4 = new Rect(S3, -t, W4, t);
                var widthRect5 = new Rect(S3 + W4 + S4, -t, W5, t);
                var widthRect6 = new Rect(S3 + W4 + S4 + W5 + S5, -t, W6, t);

                dc.DrawRectangle(widthBrush, myPen, widthRect);
                dc.DrawRectangle(widthBrush, myPen, widthRect2);
                dc.DrawRectangle(widthBrush, myPen, widthRect3);
                dc.DrawRectangle(widthBrush, myPen, widthRect4);
                dc.DrawRectangle(widthBrush, myPen, widthRect5);
                dc.DrawRectangle(widthBrush, myPen, widthRect6);

                dc.DrawRectangle(substrateHeightBrush, myPen, substrateRect);
                dc.DrawRectangle(groundBrush, myPen, groundRect);

                //Линии для разделения ширин и зазоров
                dc.DrawLine(penLine, new Point(-(W1 + S1 + W2 + S2 + W3 - z), -t), new Point(-(W1 + S1 + W2 + S2 + W3 - z), -(t + 10)));
                dc.DrawLine(penLine, new Point(-(S1 + W2 + S2 + W3 + z), -t), new Point(-(S1 + W2 + S2 + W3 + z), -(t + 10)));
                dc.DrawLine(penLine, new Point(-(W2 + S2 + W3 - z), -t), new Point(-(W2 + S2 + W3 - z), -(t + 10)));
                dc.DrawLine(penLine, new Point(-(S2 + W3 + z), -t), new Point(-(S2 + W3 + z), -(t + 10)));
                dc.DrawLine(penLine, new Point(-(W3 - z), -t), new Point(-(W3 - z), -(t + 10)));
                dc.DrawLine(penLine, new Point(-z, -t), new Point(-z, -(t + 10)));
                dc.DrawLine(penLine, new Point(S3 + z, -t), new Point(S3 + z, -(t + 10)));
                dc.DrawLine(penLine, new Point(S3 + W4 - z, -t), new Point(S3 + W4 - z, -(t + 10)));
                dc.DrawLine(penLine, new Point(S3 + W4 + S4 + z, -t), new Point(S3 + W4 + S4 + z, -(t + 10)));
                dc.DrawLine(penLine, new Point(S3 + W4 + S4 + W5 - z, -t), new Point(S3 + W4 + S4 + W5 - z, -(t + 10)));
                dc.DrawLine(penLine, new Point(S3 + W4 + S4 + W5 + S5 + z, -t), new Point(S3 + W4 + S4 + W5 + S5 + z, -(t + 10)));
                dc.DrawLine(penLine, new Point(S3 + W4 + S4 + W5 + S5 + W6 - z, -t), new Point(S3 + W4 + S4 + W5 + S5 + W6 - z, -(t + 10)));

                dc.DrawLine(penLine, new Point(-(gap + W1 + S1 + W2 + S2 + W3-5), -(t + 5)), new Point(S3 + W4 + S4 + W5 + S5 + W6 + gap-5, -(t + 5)));

                //Линии для разделения толщин линии и подложки
                dc.DrawLine(penLine, new Point(-(gap + W1 + S1 + W2 + S2 + W3 + 10), 0), new Point(-(W1 + S1 + W2 + S2 + W3 + gap), 0));
                dc.DrawLine(penLine, new Point(-(gap + W1 + S1 + W2 + S2 + W3 + 10), h), new Point(-(W1 + S1 + W2 + S2 + W3 + gap), h));
                dc.DrawLine(penLine, new Point(-(gap + W1 + S1 + W2 + S2 + W3 + 10), -t), new Point(-(W1 + S1 + W2 + S2 + W3), -t));

                dc.DrawLine(penLine, new Point(-(gap + W1 + S1 + W2 + S2 + W3 + 5), -t - 5), new Point(-(gap + W1 + S1 + W2 + S2 + W3 + 5), h + 5));

                //Подписи зазоров и ширин линий
                dc.DrawText(textWidth, new Point(-(W1 / 2 + S1 + W2 + S2 + W3 + 3), -(t + 15)));
                dc.DrawText(textWidth2, new Point(-(W2 / 2 + S2 + W3 + 3), -(t + 15)));
                dc.DrawText(textWidth3, new Point(-W3 / 2 - 3, -(t + 15)));
                dc.DrawText(textWidth4, new Point(S3 + W4 / 2 - 3, -(t + 15)));
                dc.DrawText(textWidth5, new Point(S3 + W4 + S4 + W5 / 2 - 3, -(t + 15)));
                dc.DrawText(textWidth6, new Point(S3 + W4 + S4 + W5 + S5 + W6 / 2 - 3, -(t + 15)));
                dc.DrawText(textS1, new Point(-(S1 / 2 + W2 + S2 + W3 + 3), -(t + 15)));
                dc.DrawText(textS2, new Point(-(S2 / 2 + W3 + 3), -(t + 15)));
                dc.DrawText(textS3, new Point(S3 / 2 - 3, -(t + 15)));
                dc.DrawText(textS4, new Point(S3 + W4 + S4 / 2 - 3, -(t + 15)));
                dc.DrawText(textS5, new Point(S3 + W4 + S4 + W5 + S5 / 2 - 3, -(t + 15)));

                //Подписи толщин линии и подложки
                dc.DrawText(textSubstrateHeight, new Point(-(gap + W1 + S1 + W2 + S2 + W3 + 10), h / 2 - 3));
                dc.DrawText(textThickness, new Point(-(gap + W1 + S1 + W2 + S2 + W3 + 10), -(t / 2) - 3));

            }
        }
        private FormattedText Text(string measure, double textSize) => text = new FormattedText(measure, CultureInfo.GetCultureInfo("en-Us"), FlowDirection.LeftToRight,
            new Typeface("verdana"), textSize, Brushes.DarkBlue);



    }
}
