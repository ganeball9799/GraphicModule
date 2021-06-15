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
        private List<double> _slots = new List<double>();


        public MicrostripGraphic(Geometry geometry)
        {
            _geometry = geometry;
            numberOfLine = _geometry[ParameterName.StripsNumber].Value;
            stripsThicknees = _geometry[ParameterName.StripsThickness].Value;
            substrateHeight = _geometry[ParameterName.SubstrateHeight].Value;

            Canvas.SetLeft(this, 120);
            Canvas.SetTop(this, 120);
        }


        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            var z = 0.5;
            double n = numberOfLine;
            var gap = 10;
            var ground = 5;


            if (n == 1)
            {
                _stripWidths.Add(_geometry[ParameterName.StripWidth, 0].Value);

                var zoomw1 = _stripWidths[0] / ((substrateHeight + stripsThicknees) / 2);
                var zoomh = substrateHeight / ((_stripWidths[0] + stripsThicknees) / 2);
                var zoomt = stripsThicknees / ((_stripWidths[0] + substrateHeight) / 2);

                double W1 = 100 * ZoomIn(zoomw1);
                double h = 40 * ZoomIn(zoomh);
                double t = 40 * ZoomIn(zoomt);

                DrawRectangle(dc, WidthColor, PenColor, -W1 / 2, -t, W1, t);
                DrawRectangle(dc, SubstrateColor, PenColor, -95, 0, 190, h);
                DrawRectangle(dc, GroundColor, PenColor, -95, h, 190, ground);

                //Линии для разделения ширин и зазоров
                DrawLine(dc, new Point(-W1 / 2, -t), new Point(-W1 / 2, -(t + 10)));
                DrawLine(dc, new Point(W1 / 2, -t), new Point(W1 / 2, -(t + 10)));
                DrawLine(dc, new Point(-5 - W1 / 2, -(t + 5)), new Point(W1 / 2 + 5, -(t + 5)));

                //Линии для разделения толщин линии и подложки
                DrawLine(dc, new Point(-110, -t), new Point(-W1 / 2, -t));
                DrawLine(dc, new Point(-110, 0), new Point(-95, 0));
                DrawLine(dc, new Point(-110, h), new Point(-95, h));
                DrawLine(dc, new Point(-105, -t - 5), new Point(-105, h + 5));

                //Подписи зазоров и ширин линий
                DrawText(dc, "W", 13, new Point(-5, -(t + 25)));

                //Подписи толщин линии и подложки
                DrawText(dc, "h", 12, new Point(-115, h / 2 - 9));
                DrawText(dc, "t", 12, new Point(-115, -(t / 2) - 9));
            }
            else if (n == 2)
            {
                _stripWidths.Add(_geometry[ParameterName.StripWidth, 0].Value);
                _stripWidths.Add(_geometry[ParameterName.StripWidth, 1].Value);
                _slots.Add(_geometry[ParameterName.Slot, 0].Value);

                var zoomw1 = _stripWidths[0] / (_slots[0] + _stripWidths[0] + _stripWidths[1]);
                var zoomw2 = _stripWidths[1] / (_slots[0] + _stripWidths[0] + _stripWidths[1]);
                var zooms1 = _slots[0] / (_slots[0] + _stripWidths[0] + _stripWidths[1]); ;

                //var zoomw1 = _stripWidths[0] / ((_slots[0] + _stripWidths[1] + substrateHeight + stripsThicknees) / 4);
                //var zoomw2 = _stripWidths[1] / ((_stripWidths[0] + _slots[0] + substrateHeight + stripsThicknees) / 4);
                //var zooms1 = _slots[0] / ((_stripWidths[0] + _stripWidths[1] + substrateHeight + stripsThicknees) / 4);
                var zoomh = substrateHeight / ((_stripWidths[0] + _stripWidths[1] + _slots[0] + stripsThicknees) / 4);
                var zoomt = stripsThicknees / ((substrateHeight + _stripWidths[0] + _stripWidths[1] + _slots[0]) / 4);

                double W1 = 80 * ZoomIn(zoomw1);
                double W2 = 80 * ZoomIn(zoomw2);
                double S1 = 40 * ZoomIn(zooms1);
                double h = 30 * ZoomIn(zoomh);
                double t = 30 * ZoomIn(zoomt);

                DrawRectangle(dc, WidthColor, PenColor, -W1 - S1 / 2, -t, W1, t);
                DrawRectangle(dc, WidthColor, PenColor, S1 / 2, -t, W2, t);
                DrawRectangle(dc, SubstrateColor, PenColor, -95, 0, 190, h);
                DrawRectangle(dc, GroundColor, PenColor, -95, h, 190, ground);

                //Линии для разделения ширин и зазоров
                DrawLine(dc, new Point(-W1 - S1 / 2, -t), new Point(-W1 - S1 / 2, -(t + 10)));
                DrawLine(dc, new Point(-z - S1 / 2, -t), new Point(-z - S1 / 2, -(t + 10)));
                DrawLine(dc, new Point(S1 / 2 + z, -t), new Point(S1 / 2 + z, -(t + 10)));
                DrawLine(dc, new Point(S1 / 2 - z + W2, -t), new Point(S1 / 2 - z + W2, -(t + 10)));
                DrawLine(dc, new Point(-(gap - 5 + W1 + S1 / 2), -(t + 5)), new Point(S1 / 2 + W2 + gap - 5, -(t + 5)));

                //Линии для разделения толщин линии и подложки
                DrawLine(dc, new Point(-110, -t), new Point(-W1 - S1 / 2, -t));
                DrawLine(dc, new Point(-110, 0), new Point(-95, 0));
                DrawLine(dc, new Point(-110, h), new Point(-95, h));
                DrawLine(dc, new Point(-105, -t - 5), new Point(-105, h + 5));

                //Подписи зазоров и ширин линий
                DrawText(dc, "W1", 12, new Point(-(W1 / 2 + S1 / 2 + 5), -(t + 25)));
                DrawText(dc, "W2", 12, new Point(W2 / 2 + S1 / 2 - 5, -(t + 25)));
                DrawText(dc, "S1", 12, new Point(-5, -(t + 25)));

                //Подписи толщин линии и подложки
                DrawText(dc, "h", 12, new Point(-115, h / 2 - 9));
                DrawText(dc, "t", 12, new Point(-115, -(t / 2) - 9));
            }
            else if (n == 3)
            {
                _stripWidths.Add(_geometry[ParameterName.StripWidth, 0].Value);
                _stripWidths.Add(_geometry[ParameterName.StripWidth, 1].Value);
                _stripWidths.Add(_geometry[ParameterName.StripWidth, 2].Value);
                _slots.Add(_geometry[ParameterName.Slot, 0].Value);
                _slots.Add(_geometry[ParameterName.Slot, 1].Value);

                var zoomw1 = _stripWidths[0] / ((_slots[0] + _slots[1] + _stripWidths[1] + _stripWidths[2] + substrateHeight + stripsThicknees) / 6);
                var zoomw2 = _stripWidths[1] / ((_stripWidths[0] + _stripWidths[2] + _slots[0] + _slots[1] + substrateHeight + stripsThicknees) / 6);
                var zoomw3 = _stripWidths[2] / ((_stripWidths[0] + _stripWidths[1] + _slots[0] + _slots[1] + substrateHeight + stripsThicknees) / 6);
                var zooms1 = _slots[0] / ((_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _slots[1] + substrateHeight + stripsThicknees) / 6);
                var zooms2 = _slots[1] / ((_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _slots[0] + substrateHeight + stripsThicknees) / 6);
                var zoomh = substrateHeight / ((_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _slots[0] + _slots[1] + stripsThicknees) / 6);
                var zoomt = stripsThicknees / ((substrateHeight + _stripWidths[0] + _stripWidths[1] + _slots[0] + _slots[1] + _stripWidths[2]) / 6);

                double W1 = 40 * ZoomIn(zoomw1);
                double W2 = 40 * ZoomIn(zoomw2);
                double W3 = 40 * ZoomIn(zoomw3);
                double S1 = 10 * ZoomIn(zooms1);
                double S2 = 10 * ZoomIn(zooms2);
                double h = 20 * ZoomIn(zoomh);
                double t = 20 * ZoomIn(zoomt);

                DrawRectangle(dc, WidthColor, PenColor, -(W1 + S1 + W2 / 2), -t, W1, t);
                DrawRectangle(dc, WidthColor, PenColor, -W2 / 2, -t, W2, t);
                DrawRectangle(dc, WidthColor, PenColor, W2 / 2 + S2, -t, W3, t);
                DrawRectangle(dc, SubstrateColor, PenColor, -95, 0, 190, h);
                DrawRectangle(dc, GroundColor, PenColor, -95, h, 190, ground);

                //Линии для разделения ширин и зазоров
                DrawLine(dc, new Point(-(W1 + S1 + W2 / 2 - z), -t), new Point(-(W1 + S1 + W2 / 2 - z), -(t + 10)));
                DrawLine(dc, new Point(-S1 - W2 / 2 - z, -t), new Point(-S1 - W2 / 2 - z, -(t + 10)));
                DrawLine(dc, new Point(-W2 / 2, -t), new Point(-W2 / 2, -(t + 10)));
                DrawLine(dc, new Point(W2 / 2 - z, -t), new Point(W2 / 2 - z, -(t + 10)));
                DrawLine(dc, new Point(S2 + W2 / 2 + z, -t), new Point(S2 + W2 / 2 + z, -(t + 10)));
                DrawLine(dc, new Point(S2 + W2 / 2 + W3 - z, -t), new Point(S2 + W2 / 2 + W3 - z, -(t + 10)));
                DrawLine(dc, new Point(-(gap - 5 + W1 + S1 + W2 / 2), -(t + 5)), new Point(S2 + W2 / 2 + W3 + gap - 5, -(t + 5)));

                //Линии для разделения толщин линии и подложки
                DrawLine(dc, new Point(-110, -t), new Point(-(W1 + S1 + W2 / 2), -t));
                DrawLine(dc, new Point(-110, 0), new Point(-95, 0));
                DrawLine(dc, new Point(-110, h), new Point(-95, h));
                DrawLine(dc, new Point(-105, -t - 5), new Point(-105, h + 5));

                //Подписи зазоров и ширин линий
                DrawText(dc, "W", 11, new Point(-(W1 / 2 + S1 + W2 / 2 + 4), -(t + 25)));
                DrawText(dc, "W2", 11, new Point(-5, -(t + 25)));
                DrawText(dc, "W3", 11, new Point(W2 / 2 + S2 + W3 / 2 - 5, -(t + 25)));
                DrawText(dc, "S", 11, new Point(-(S1 / 2 + W2 / 2 + 3), -(t + 25)));
                DrawText(dc, "S2", 11, new Point(W2 / 2 + S2 / 2 - 5, -(t + 25)));

                //Подписи толщин линии и подложки
                DrawText(dc, "h", 12, new Point(-115, h / 2 - 9));
                DrawText(dc, "t", 12, new Point(-115, -(t / 2) - 9));
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

                var zoomw1 = _stripWidths[0] / ((_stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _slots[0] + _slots[1] + _slots[2] + substrateHeight + stripsThicknees) / 8);
                var zoomw2 = _stripWidths[1] / ((_stripWidths[0] + _stripWidths[2] + _stripWidths[3] + _slots[0] + _slots[1] + _slots[2] + substrateHeight + stripsThicknees) / 8);
                var zoomw3 = _stripWidths[2] / ((_stripWidths[0] + _stripWidths[1] + _stripWidths[3] + _slots[0] + _slots[1] + _slots[2] + substrateHeight + stripsThicknees) / 8);
                var zoomw4 = _stripWidths[3] / ((_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _slots[0] + _slots[1] + _slots[2] + substrateHeight + stripsThicknees) / 8);
                var zooms1 = _slots[0] / ((substrateHeight + _stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _slots[1] + _slots[2] + stripsThicknees) / 8);
                var zooms2 = _slots[1] / ((substrateHeight + _stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _slots[0] + _slots[2] + stripsThicknees) / 8);
                var zooms3 = _slots[2] / ((substrateHeight + _stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _slots[0] + _slots[1] + stripsThicknees) / 8);
                var zoomh = substrateHeight / ((_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _slots[0] + _slots[1] + _slots[2] + stripsThicknees) / 8);
                var zoomt = stripsThicknees / ((substrateHeight + _stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _slots[0] + _slots[1] + _slots[2] + _stripWidths[3]) / 8);

                double W1 = 25 * ZoomIn(zoomw1);
                double W2 = 25 * ZoomIn(zoomw2);
                double W3 = 25 * ZoomIn(zoomw3);
                double W4 = 25 * ZoomIn(zoomw4);
                double S1 = 15 * ZoomIn(zooms1);
                double S2 = 15 * ZoomIn(zooms2);
                double S3 = 15 * ZoomIn(zooms3);
                double h = 20 * ZoomIn(zoomh);
                double t = 20 * ZoomIn(zoomt);

                DrawRectangle(dc, WidthColor, PenColor, -(W1 + S1 + W2), -t, W1, t);
                DrawRectangle(dc, WidthColor, PenColor, -W2, -t, W2, t);
                DrawRectangle(dc, WidthColor, PenColor, S2, -t, W3, t);
                DrawRectangle(dc, WidthColor, PenColor, S2 + S3 + W3, -t, W4, t);
                DrawRectangle(dc, SubstrateColor, PenColor, -95, 0, 190, h);
                DrawRectangle(dc, GroundColor, PenColor, -95, h, 190, ground);

                //Линии для разделения ширин и зазоров
                DrawLine(dc, new Point(-(W1 + S1 + W2 - z), -t), new Point(-(W1 + S1 + W2 - z), -(t + 10)));
                DrawLine(dc, new Point(-(W2 + S1 + z), -t), new Point(-(W2 + S1 + z), -(t + 10)));
                DrawLine(dc, new Point(-W2 + z, -t), new Point(-W2 + z, -(t + 10)));
                DrawLine(dc, new Point(-z, -t), new Point(-z, -(t + 10)));
                DrawLine(dc, new Point(S2 + z, -t), new Point(S2 + z, -(t + 10)));
                DrawLine(dc, new Point(S2 + W3 - z, -t), new Point(S2 + W3 - z, -(t + 10)));
                DrawLine(dc, new Point(S2 + W3 + S3 + z, -t), new Point(S2 + W3 + S3 + z, -(t + 10)));
                DrawLine(dc, new Point(S2 + W3 + S3 + W4 - z, -t), new Point(S2 + W3 + S3 + W4 - z, -(t + 10)));
                DrawLine(dc, new Point(-(gap + W1 + S1 + W2 - 5), -(t + 5)), new Point(S2 + W3 + S3 + W4 + gap - 5, -(t + 5)));

                //Линии для разделения толщин линии и подложки
                DrawLine(dc, new Point(-110, -t), new Point(-(W1 + S1 + W2 + S2 / 2), -t));
                DrawLine(dc, new Point(-110, 0), new Point(-95, 0));
                DrawLine(dc, new Point(-110, h), new Point(-95, h));
                DrawLine(dc, new Point(-105, -t - 5), new Point(-105, h + 5));

                //Подписи зазоров и ширин линий
                DrawText(dc, "W", 7, new Point(-(W1 / 2 + S1 + W2 + 9), -(t + 25)));
                DrawText(dc, "W2", 7, new Point(-(W2 / 2 + 9), -(t + 25)));
                DrawText(dc, "W3", 7, new Point(S2 + W3 / 2 - 9, -(t + 25)));
                DrawText(dc, "W4", 7, new Point(S2 + S3 + W3 + W4 / 2 - 9, -(t + 25)));
                DrawText(dc, "S", 7, new Point(-(S1 / 2 + W2 + 9), -(t + 25)));
                DrawText(dc, "S2", 7, new Point(S2 / 2 - 9, -(t + 25)));
                DrawText(dc, "S3", 7, new Point(W3 + S2 + S3 / 2 - 9, -(t + 25)));

                //Подписи толщин линии и подложки
                DrawText(dc, "h", 12, new Point(-115, h / 2 - 9));
                DrawText(dc, "t", 12, new Point(-115, -(t / 2) - 9));
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


                var zoomw1 = _stripWidths[0] / ((_stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _stripWidths[4] + _slots[0] + _slots[1] + _slots[2] + _slots[3] + substrateHeight + stripsThicknees) / 11);
                var zoomw2 = _stripWidths[1] / ((_stripWidths[0] + _stripWidths[2] + _stripWidths[3] + _stripWidths[4] + _slots[0] + _slots[1] + _slots[2] + _slots[3] + substrateHeight + stripsThicknees) / 11);
                var zoomw3 = _stripWidths[2] / ((_stripWidths[0] + _stripWidths[1] + _stripWidths[3] + _stripWidths[4] + _slots[0] + _slots[1] + _slots[2] + _slots[3] + substrateHeight + stripsThicknees) / 11);
                var zoomw4 = _stripWidths[3] / ((_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[4] + _slots[0] + _slots[1] + _slots[2] + _slots[3] + substrateHeight + stripsThicknees) / 11);
                var zoomw5 = _stripWidths[4] / ((_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _slots[0] + _slots[1] + _slots[2] + _slots[3] + substrateHeight + stripsThicknees) / 11);
                var zooms1 = _slots[0] / ((_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _stripWidths[4] + _slots[1] + _slots[2] + _slots[3] + substrateHeight + stripsThicknees) / 11);
                var zooms2 = _slots[1] / ((_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _stripWidths[4] + _slots[0] + _slots[2] + _slots[3] + substrateHeight + stripsThicknees) / 11);
                var zooms3 = _slots[2] / ((_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _stripWidths[4] + _slots[0] + _slots[1] + _slots[3] + substrateHeight + stripsThicknees) / 11);
                var zooms4 = _slots[3] / ((_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _stripWidths[4] + _slots[0] + _slots[1] + _slots[2] + substrateHeight + stripsThicknees) / 11);
                var zoomh = substrateHeight / ((_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _stripWidths[4] + _slots[0] + _slots[1] + _slots[2] + _slots[3] + stripsThicknees) / 11);
                var zoomt = stripsThicknees / ((substrateHeight + _stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _stripWidths[4] + _slots[0] + _slots[1] + _slots[2] + _slots[3]) / 11);


                double W1 = 20 * ZoomIn(zoomw1);
                double W2 = 20 * ZoomIn(zoomw2);
                double W3 = 20 * ZoomIn(zoomw3);
                double W4 = 20 * ZoomIn(zoomw4);
                double W5 = 20 * ZoomIn(zoomw5);
                double S1 = 10 * ZoomIn(zooms1);
                double S2 = 10 * ZoomIn(zooms2);
                double S3 = 10 * ZoomIn(zooms3);
                double S4 = 10 * ZoomIn(zooms4);
                double h = 20 * ZoomIn(zoomh);
                double t = 20 * ZoomIn(zoomt);

                DrawRectangle(dc, WidthColor, PenColor, -(W1 + S1 + W2 + S2), -t, W1, t);
                DrawRectangle(dc, WidthColor, PenColor, -(W2 + S2), -t, W2, t);
                DrawRectangle(dc, WidthColor, PenColor, 0, -t, W3, t);
                DrawRectangle(dc, WidthColor, PenColor, W3 + S3, -t, W4, t);
                DrawRectangle(dc, WidthColor, PenColor, W3 + S3 + W4 + S4, -t, W5, t);
                DrawRectangle(dc, SubstrateColor, PenColor, -95, 0, 190, h);
                DrawRectangle(dc, GroundColor, PenColor, -95, h, 190, ground);


                //Линии для разделения ширин и зазоров
                DrawLine(dc, new Point(-(W1 + S1 + W2 + S2 - z), -t), new Point(-(W1 + S1 + W2 + S2 - z), -(t + 10)));
                DrawLine(dc, new Point(-(S1 + W2 + S2 + z), -t), new Point(-(S1 + W2 + S2 + z), -(t + 10)));
                DrawLine(dc, new Point(-(S2 + W2 - z), -t), new Point(-(S2 + W2 - z), -(t + 10)));
                DrawLine(dc, new Point(-(S2 + z), -t), new Point(-(S2 + z), -(t + 10)));
                DrawLine(dc, new Point(z, -t), new Point(z, -(t + 10)));
                DrawLine(dc, new Point(W3 - z, -t), new Point(W3 - z, -(t + 10)));
                DrawLine(dc, new Point(W3 + S3 + z, -t), new Point(W3 + S3 + z, -(t + 10)));
                DrawLine(dc, new Point(W3 + S3 + W4 - z, -t), new Point(W3 + S3 + W4 - z, -(t + 10)));
                DrawLine(dc, new Point(W3 + S3 + W4 + S4 + z, -t), new Point(W3 + S3 + W4 + S4 + z, -(t + 10)));
                DrawLine(dc, new Point(W3 + S3 + W4 + S4 + W5 - z, -t), new Point(W3 + S3 + W4 + S4 + W5 - z, -(t + 10)));
                DrawLine(dc, new Point(-(gap + W1 + S1 + W2 + S2 - 5), -(t + 5)), new Point(W3 + S3 + W4 + S4 + W5 + gap - 5, -(t + 5)));

                //Линии для разделения толщин линии и подложки
                DrawLine(dc, new Point(-110, -t), new Point(-(W1 + S1 + W2 + S2 + W3 / 2), -t));
                DrawLine(dc, new Point(-110, 0), new Point(-95, 0));
                DrawLine(dc, new Point(-110, h), new Point(-95, h));
                DrawLine(dc, new Point(-105, -t - 5), new Point(-105, h + 5));

                //Подписи зазоров и ширин линий
                DrawText(dc, "W", 7, new Point(-(W1 / 2 + S1 + W2 + S2 + 10), -(t + 25)));
                DrawText(dc, "W2", 7, new Point(-(W2 / 2 + S2 + 10), -(t + 25)));
                DrawText(dc, "W3", 7, new Point(W3 / 2 - 10, -(t + 25)));
                DrawText(dc, "W4", 7, new Point(S3 + W3 + W4 / 2 - 10, -(t + 25)));
                DrawText(dc, "W5", 7, new Point(S3 + W3 + W4 + S4 + W5 / 2 - 10, -(t + 25)));
                DrawText(dc, "S", 7, new Point(-(S1 / 2 + W2 + S2 + 10), -(t + 25)));
                DrawText(dc, "S2", 7, new Point(-S2 / 2 - 10, -(t + 25)));
                DrawText(dc, "S3", 7, new Point(W3 + S3 / 2 - 10, -(t + 25)));
                DrawText(dc, "S4", 7, new Point(W3 + S3 + W4 + S4 / 2 - 10, -(t + 25)));

                //Подписи толщин линии и подложки
                DrawText(dc, "h", 12, new Point(-115, h / 2 - 9));
                DrawText(dc, "t", 12, new Point(-115, -(t / 2) - 9));

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

                var zoomw1 = _stripWidths[0] / ((_stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _stripWidths[4] + _stripWidths[5] + _slots[0] + _slots[1] + _slots[2] + _slots[3] + _slots[4] + substrateHeight + stripsThicknees) / 12);
                var zoomw2 = _stripWidths[1] / ((_stripWidths[0] + _stripWidths[2] + _stripWidths[3] + _stripWidths[4] + _stripWidths[5] + _slots[0] + _slots[1] + _slots[2] + _slots[3] + _slots[4] + substrateHeight + stripsThicknees) / 12);
                var zoomw3 = _stripWidths[2] / ((_stripWidths[0] + _stripWidths[1] + _stripWidths[3] + _stripWidths[4] + _stripWidths[5] + _slots[0] + _slots[1] + _slots[2] + _slots[3] + _slots[4] + substrateHeight + stripsThicknees) / 12);
                var zoomw4 = _stripWidths[3] / ((_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[4] + _stripWidths[5] + _slots[0] + _slots[1] + _slots[2] + _slots[3] + _slots[4] + substrateHeight + stripsThicknees) / 12);
                var zoomw5 = _stripWidths[4] / ((_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _stripWidths[5] + _slots[0] + _slots[1] + _slots[2] + _slots[3] + _slots[4] + substrateHeight + stripsThicknees) / 12);
                var zoomw6 = _stripWidths[5] / ((_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _stripWidths[4] + _slots[0] + _slots[1] + _slots[2] + _slots[3] + _slots[4] + substrateHeight + stripsThicknees) / 12);

                var zooms1 = _slots[0] / ((_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _stripWidths[4] + _stripWidths[5] + _slots[1] + _slots[2] + _slots[3] + _slots[4] + substrateHeight + stripsThicknees) / 12);
                var zooms2 = _slots[1] / ((_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _stripWidths[4] + _stripWidths[5] + _slots[0] + _slots[2] + _slots[3] + _slots[4] + substrateHeight + stripsThicknees) / 12);
                var zooms3 = _slots[2] / ((_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _stripWidths[4] + _stripWidths[5] + _slots[0] + _slots[1] + _slots[3] + _slots[4] + substrateHeight + stripsThicknees) / 12);
                var zooms4 = _slots[3] / ((_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _stripWidths[4] + _stripWidths[5] + _slots[0] + _slots[1] + _slots[2] + _slots[4] + substrateHeight + stripsThicknees) / 12);
                var zooms5 = _slots[4] / ((_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _stripWidths[4] + _stripWidths[5] + _slots[0] + _slots[1] + _slots[2] + _slots[3] + substrateHeight + stripsThicknees) / 12);
                var zoomh = substrateHeight / ((_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _stripWidths[4] + _stripWidths[5] + _slots[0] + _slots[1] + _slots[2] + _slots[3] + _slots[4] + stripsThicknees) / 12);
                var zoomt = stripsThicknees / ((substrateHeight + _stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _stripWidths[4] + _stripWidths[5] + _slots[0] + _slots[1] + _slots[2] + _slots[3] + _slots[4]) / 12);

                double W1 = 20 * ZoomIn(zoomw1);
                double W2 = 20 * ZoomIn(zoomw2);
                double W3 = 20 * ZoomIn(zoomw3);
                double W4 = 20 * ZoomIn(zoomw4);
                double W5 = 20 * ZoomIn(zoomw5);
                double W6 = 20 * ZoomIn(zoomw6);
                double S1 = 10 * ZoomIn(zooms1);
                double S2 = 10 * ZoomIn(zooms2);
                double S3 = 10 * ZoomIn(zooms3);
                double S4 = 10 * ZoomIn(zooms4);
                double S5 = 10 * ZoomIn(zooms5);
                double h = 15 * ZoomIn(zoomh);
                double t = 15 * ZoomIn(zoomt);


                DrawRectangle(dc, WidthColor, PenColor, -(W1 + S1 + W2 + S2 + W3), -t, W1, t);
                DrawRectangle(dc, WidthColor, PenColor, -(W2 + S2 + W3), -t, W2, t);
                DrawRectangle(dc, WidthColor, PenColor, -W3, -t, W3, t);
                DrawRectangle(dc, WidthColor, PenColor, S3, -t, W4, t);
                DrawRectangle(dc, WidthColor, PenColor, S3 + W4 + S4, -t, W5, t);
                DrawRectangle(dc, WidthColor, PenColor, S3 + W4 + S4 + W5 + S5, -t, W6, t);
                DrawRectangle(dc, SubstrateColor, PenColor, -95, 0, 190, h);
                DrawRectangle(dc, GroundColor, PenColor, -95, h, 190, ground);


                //Линии для разделения ширин и зазоров
                DrawLine(dc, new Point(-(W1 + S1 + W2 + S2 + W3 - z), -t), new Point(-(W1 + S1 + W2 + S2 + W3 - z), -(t + 10)));
                DrawLine(dc, new Point(-(S1 + W2 + S2 + W3 + z), -t), new Point(-(S1 + W2 + S2 + W3 + z), -(t + 10)));
                DrawLine(dc, new Point(-(W2 + S2 + W3 - z), -t), new Point(-(W2 + S2 + W3 - z), -(t + 10)));
                DrawLine(dc, new Point(-(S2 + W3 + z), -t), new Point(-(S2 + W3 + z), -(t + 10)));
                DrawLine(dc, new Point(-(W3 - z), -t), new Point(-(W3 - z), -(t + 10)));
                DrawLine(dc, new Point(-z, -t), new Point(-z, -(t + 10)));
                DrawLine(dc, new Point(S3 + z, -t), new Point(S3 + z, -(t + 10)));
                DrawLine(dc, new Point(S3 + W4 - z, -t), new Point(S3 + W4 - z, -(t + 10)));
                DrawLine(dc, new Point(S3 + W4 + S4 + z, -t), new Point(S3 + W4 + S4 + z, -(t + 10)));
                DrawLine(dc, new Point(S3 + W4 + S4 + W5 - z, -t), new Point(S3 + W4 + S4 + W5 - z, -(t + 10)));
                DrawLine(dc, new Point(S3 + W4 + S4 + W5 + S5 + z, -t), new Point(S3 + W4 + S4 + W5 + S5 + z, -(t + 10)));
                DrawLine(dc, new Point(S3 + W4 + S4 + W5 + S5 + W6 - z, -t), new Point(S3 + W4 + S4 + W5 + S5 + W6 - z, -(t + 10)));
                DrawLine(dc, new Point(-(gap + W1 + S1 + W2 + S2 + W3 - 5), -(t + 5)), new Point(S3 + W4 + S4 + W5 + S5 + W6 + gap - 5, -(t + 5)));

                //Линии для разделения толщин линии и подложки
                DrawLine(dc, new Point(-110, -t), new Point(-(W1 + S1 + W2 + S2 + W3 + S3 / 2), -t));
                DrawLine(dc, new Point(-110, 0), new Point(-95, 0));
                DrawLine(dc, new Point(-110, h), new Point(-95, h));
                DrawLine(dc, new Point(-105, -t - 5), new Point(-105, h + 5));

                //Подписи зазоров и ширин линий
                DrawText(dc, "W", 7, new Point(-(W1 / 2 + S1 + W2 + S2 + W3 + 9), -(t + 25)));
                DrawText(dc, "W2", 7, new Point(-(W2 / 2 + S2 + W3 + 9), -(t + 25)));
                DrawText(dc, "W3", 7, new Point(-W3 / 2 - 9, -(t + 25)));
                DrawText(dc, "W4", 7, new Point(S3 + W4 / 2 - 9, -(t + 25)));
                DrawText(dc, "W5", 7, new Point(S3 + W4 + S4 + W5 / 2 - 9, -(t + 25)));
                DrawText(dc, "W6", 7, new Point(S3 + W4 + S4 + W5 + S5 + W6 / 2 - 9, -(t + 25)));
                DrawText(dc, "S1", 7, new Point(-(S1 / 2 + W2 + S2 + W3 + 9), -(t + 25)));
                DrawText(dc, "S2", 7, new Point(-(S2 / 2 + W3 + 9), -(t + 25)));
                DrawText(dc, "S3", 7, new Point(S3 / 2 - 9, -(t + 25)));
                DrawText(dc, "S4", 7, new Point(S3 + W4 + S4 / 2 - 9, -(t + 25)));
                DrawText(dc, "S5", 7, new Point(S3 + W4 + S4 + W5 + S5 / 2 - 9, -(t + 25)));

                //Подписи толщин линии и подложки
                DrawText(dc, "h", 12, new Point(-115, h / 2 - 9));
                DrawText(dc, "t", 12, new Point(-115, -(t / 2) - 9));
            }
        }
    }
}
