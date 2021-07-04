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
        /// <summary>
        /// Экземпляр линии
        /// </summary>
        private Geometry _geometry;

        /// <summary>
        /// Количество линий
        /// </summary>
        private double _linesNumber;

        /// <summary>
        /// Толщина линии
        /// </summary>
        private double _stripsThicknees;

        /// <summary>
        /// Толщина подложки
        /// </summary>
        private double _substrateHeight;

        /// <summary>
        /// Список ширин линий
        /// </summary>
        private List<double> _stripWidths = new List<double>();

        /// <summary>
        /// Список зазоров между линиями
        /// </summary>
        private List<double> _slots = new List<double>();

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public MicrostripGraphic(Geometry geometry)
        {
            _geometry = geometry;
            _linesNumber = _geometry[ParameterName.StripsNumber].Value;
            _stripsThicknees = _geometry[ParameterName.StripsThickness].Value;
            _substrateHeight = _geometry[ParameterName.SubstrateHeight].Value;

            Canvas.SetLeft(this, 120);
            Canvas.SetTop(this, 120);
        }

        /// <summary>
        /// Метод отрисовки
        /// </summary>
        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            double n = _linesNumber;
            var ground = 5;

            if (n == 1)
            {
                _stripWidths.Add(_geometry[ParameterName.StripWidth, 0].Value);

                var zoomw1 = _stripWidths[0] / ((_substrateHeight + _stripsThicknees) / 2);
                var zoomh = _substrateHeight / ((_stripWidths[0] + _stripsThicknees) / 2);
                var zoomt = _stripsThicknees / ((_stripWidths[0] + _substrateHeight) / 2);
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
                DrawText(dc, "W1", 13, new Point(-9, -(t + 25)));

                //Подписи толщин линии и подложки
                DrawText(dc, "h", 12, new Point(-115, h / 2 - 9));
                DrawText(dc, "t", 12, new Point(-115, -(t / 2) - 9));
            }
            else if (n == 2)
            {
                _stripWidths.Add(_geometry[ParameterName.StripWidth, 0].Value);
                _stripWidths.Add(_geometry[ParameterName.StripWidth, 1].Value);
                _slots.Add(_geometry[ParameterName.Slot, 0].Value);

                var zoomw1 = _stripWidths[0] / (_stripWidths[0] + _stripWidths[1] + _slots[0]);
                var zoomw2 = _stripWidths[1] / (_stripWidths[0] + _stripWidths[1] + _slots[0]);
                var zooms1 = _slots[0] / (_stripWidths[0] + _stripWidths[1] + _slots[0]);
                var zoomh = _substrateHeight / ((_stripWidths[0] + _stripWidths[1] + _slots[0] + _stripsThicknees) / 4);
                var zoomt = _stripsThicknees / ((_substrateHeight + _stripWidths[0] + _stripWidths[1] + _slots[0]) / 4);

                double W1 = 150 * zoomw1;
                double W2 = 150 * zoomw2;
                double S1 = 150 * zooms1;
                double h = 40 * ZoomIn(zoomh);
                double t = 40 * ZoomIn(zoomt);

                DrawRectangle(dc, WidthColor, PenColor, -75, -t, W1, t);
                DrawRectangle(dc, WidthColor, PenColor, -(W2 - 75), -t, W2, t);
                DrawRectangle(dc, SubstrateColor, PenColor, -95, 0, 190, h);
                DrawRectangle(dc, GroundColor, PenColor, -95, h, 190, ground);

                //Линии для разделения ширин и зазоров
                DrawLine(dc, new Point(-75, -t), new Point(-75, -(t + 10)));
                DrawLine(dc, new Point(-(75 - W1), -t), new Point(-(75 - W1), -(t + 10)));
                DrawLine(dc, new Point(-(+W2 - 75), -t), new Point(-(+W2 - 75), -(t + 10)));
                DrawLine(dc, new Point(75, -t), new Point(75, -(t + 10)));
                DrawLine(dc, new Point(-80, -(t + 5)), new Point(80, -(t + 5)));

                //Линии для разделения толщин линии и подложки
                DrawLine(dc, new Point(-110, -t), new Point(-75, -t));
                DrawLine(dc, new Point(-110, 0), new Point(-95, 0));
                DrawLine(dc, new Point(-110, h), new Point(-95, h));
                DrawLine(dc, new Point(-105, -t - 5), new Point(-105, h + 5));

                //Подписи зазоров и ширин линий
                DrawText(dc, "W1", 12, new Point(-(75 - W1 / 2) - 5, -(t + 25)));
                DrawText(dc, "W2", 12, new Point(-(W2 / 2 - 75) - 5, -(t + 25)));
                DrawText(dc, "S1", 12, new Point(-(75 - W1 - S1 / 2 + S1 / 10), -(t + 25)));

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

                var zoomw1 = _stripWidths[0] / (_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _slots[0] + _slots[1]);
                var zoomw2 = _stripWidths[1] / (_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _slots[0] + _slots[1]);
                var zoomw3 = _stripWidths[2] / (_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _slots[0] + _slots[1]);
                var zooms1 = _slots[0] / (_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _slots[0] + _slots[1]);
                var zooms2 = _slots[1] / (_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _slots[0] + _slots[1]);
                var zoomh = _substrateHeight / ((_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripsThicknees) / 4);
                var zoomt = _stripsThicknees / ((_substrateHeight + _stripWidths[0] + _stripWidths[1] + _slots[0] + _slots[1] + _stripWidths[2]) / 6);

                double W1 = 150 * zoomw1;
                double W2 = 150 * zoomw2;
                double W3 = 150 * zoomw3;
                double S1 = 150 * zooms1;
                double S2 = 150 * zooms2;
                double h = 40 * ZoomIn(zoomh);
                double t = 40 * ZoomIn(zoomt);

                DrawRectangle(dc, WidthColor, PenColor, -75, -t, W1, t);
                DrawRectangle(dc, WidthColor, PenColor, (W1 / 2 + S1 / 2) - (W2 / 2 + S2 / 2 + W3 / 2), -t, W2, t);
                DrawRectangle(dc, WidthColor, PenColor, -(W3 - 75), -t, W3, t);
                DrawRectangle(dc, SubstrateColor, PenColor, -95, 0, 190, h);
                DrawRectangle(dc, GroundColor, PenColor, -95, h, 190, ground);

                //Линии для разделения ширин и зазоров
                DrawLine(dc, new Point(-75, -t), new Point(-75, -(t + 10)));
                DrawLine(dc, new Point(-75 + W1, -t), new Point(-75 + W1, -(t + 10)));
                DrawLine(dc, new Point((S1 / 2 + W1 / 2) - (W2 / 2 + S2 / 2 + W3 / 2), -t), new Point((S1 / 2 + W1 / 2) - (W2 / 2 + S2 / 2 + W3 / 2), -(t + 10)));
                DrawLine(dc, new Point((S1 / 2 + W1 / 2 + W2) - (W2 / 2 + S2 / 2 + W3 / 2), -t), new Point((S1 / 2 + W1 / 2 + W2) - (W2 / 2 + S2 / 2 + W3 / 2), -(t + 10)));
                DrawLine(dc, new Point(75 - W3, -t), new Point(75 - W3, -(t + 10)));
                DrawLine(dc, new Point(75, -t), new Point(75, -(t + 10)));
                DrawLine(dc, new Point(-80, -(t + 5)), new Point(80, -(t + 5)));

                //Линии для разделения толщин линии и подложки
                DrawLine(dc, new Point(-110, -t), new Point(-75, -t));
                DrawLine(dc, new Point(-110, 0), new Point(-95, 0));
                DrawLine(dc, new Point(-110, h), new Point(-95, h));
                DrawLine(dc, new Point(-105, -t - 5), new Point(-105, h + 5));

                //Подписи зазоров и ширин линий
                DrawText(dc, "W1", 9, new Point(-75 + W1 / 2 - W1 / 10, -(t + 25)));
                DrawText(dc, "W2", 9, new Point((S1 / 2 + W1 / 2 + W2 / 2) - (W2 / 2 + W2 / 10 + S2 / 2 + W3 / 2), -(t + 25)));
                DrawText(dc, "W3", 9, new Point(75 - W3 / 2 - W3 / 10, -(t + 25)));
                DrawText(dc, "S1", 9, new Point(-75 + W1 + S1 / 2 - S1 / 10, -(t + 25)));
                DrawText(dc, "S2", 9, new Point(-(W3 - 75 + S2 / 2 + S2 / 10), -(t + 25)));

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

                var zoomw1 = _stripWidths[0] / (_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _slots[0] + _slots[1] + _slots[2]);
                var zoomw2 = _stripWidths[1] / (_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _slots[0] + _slots[1] + _slots[2]);
                var zoomw3 = _stripWidths[2] / (_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _slots[0] + _slots[1] + _slots[2]);
                var zoomw4 = _stripWidths[3] / (_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _slots[0] + _slots[1] + _slots[2]);
                var zooms1 = _slots[0] / (_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _slots[0] + _slots[1] + _slots[2]);
                var zooms2 = _slots[1] / (_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _slots[0] + _slots[1] + _slots[2]);
                var zooms3 = _slots[2] / (_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _slots[0] + _slots[1] + _slots[2]);
                var zoomh = _substrateHeight / ((_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _stripsThicknees) / 5);
                var zoomt = _stripsThicknees / ((_substrateHeight + _stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _slots[0] + _slots[1] + _slots[2] + _stripWidths[3]) / 8);

                double W1 = 150 * zoomw1;
                double W2 = 150 * zoomw2;
                double W3 = 150 * zoomw3;
                double W4 = 150 * zoomw4;
                double S1 = 150 * zooms1;
                double S2 = 150 * zooms2;
                double S3 = 150 * zooms3;
                double h = 40 * ZoomIn(zoomh);
                double t = 40 * ZoomIn(zoomt);

                DrawRectangle(dc, WidthColor, PenColor, -75, -t, W1, t);
                DrawRectangle(dc, WidthColor, PenColor, (W1 / 2 + S1 / 2) - (W2 / 2 + S2 / 2 + W3 / 2 + S3 / 2 + W4 / 2), -t, W2, t);
                DrawRectangle(dc, WidthColor, PenColor, (W1 / 2 + S1 / 2 + W2 / 2 + S2 / 2) - (W3 / 2 + S3 / 2 + W4 / 2), -t, W3, t);
                DrawRectangle(dc, WidthColor, PenColor, -(W4 - 75), -t, W4, t);
                DrawRectangle(dc, SubstrateColor, PenColor, -95, 0, 190, h);
                DrawRectangle(dc, GroundColor, PenColor, -95, h, 190, ground);

                //Линии для разделения ширин и зазоров
                DrawLine(dc, new Point(-75, -t), new Point(-75, -(t + 10)));
                DrawLine(dc, new Point(-75 + W1, -t), new Point(-75 + W1, -(t + 10)));
                DrawLine(dc, new Point((S1 / 2 + W1 / 2 + W2 / 2) - (W2 + S2 / 2 + W3 / 2 + S3 / 2 + W4 / 2), -t), new Point((S1 / 2 + W1 / 2 + W2 / 2) - (W2 + S2 / 2 + W3 / 2 + S3 / 2 + W4 / 2), -(t + 10)));
                DrawLine(dc, new Point((S1 / 2 + W1 / 2 + W2) - (W2 / 2 + S2 / 2 + W3 / 2 + S3 / 2 + W4 / 2), -t), new Point((S1 / 2 + W1 / 2 + W2) - (W2 / 2 + S2 / 2 + W3 / 2 + S3 / 2 + W4 / 2), -(t + 10)));
                DrawLine(dc, new Point((S1 / 2 + W1 / 2 + W2 / 2 + S2 / 2 + W3 / 2) - (W3 + S3 / 2 + W4 / 2), -t), new Point((S1 / 2 + W1 / 2 + W2 / 2 + S2 / 2 + W3 / 2) - (W3 + S3 / 2 + W4 / 2), -(t + 10)));
                DrawLine(dc, new Point((S1 / 2 + W1 / 2 + W2 / 2 + S2 / 2 + W3) - (W3 / 2 + S3 / 2 + W4 / 2), -t), new Point((S1 / 2 + W1 / 2 + W2 / 2 + S2 / 2 + W3) - (W3 / 2 + S3 / 2 + W4 / 2), -(t + 10)));
                DrawLine(dc, new Point(75 - W4, -t), new Point(75 - W4, -(t + 10)));
                DrawLine(dc, new Point(75, -t), new Point(75, -(t + 10)));
                DrawLine(dc, new Point(-80, -(t + 5)), new Point(80, -(t + 5)));

                //Линии для разделения толщин линии и подложки
                DrawLine(dc, new Point(-110, -t), new Point(-75, -t));
                DrawLine(dc, new Point(-110, 0), new Point(-95, 0));
                DrawLine(dc, new Point(-110, h), new Point(-95, h));
                DrawLine(dc, new Point(-105, -t - 5), new Point(-105, h + 5));

                //Подписи зазоров и ширин линий
                DrawText(dc, "W1", 9, new Point(-75 + W1 / 2 - W1 / 6, -(t + 25)));
                DrawText(dc, "W2", 9, new Point((S1 / 2 + W1 / 2 + W2 / 2) - (W2 / 2 + W2 / 6 + S2 / 2 + W3 / 2 + S3 / 2 + W4 / 2), -(t + 25)));
                DrawText(dc, "W3", 9, new Point((S1 / 2 + W1 / 2 + W2 / 2 + S2 / 2 + W3 / 2) - (W3 / 2 + W3 / 6 + S3 / 2 + W4 / 2), -(t + 25)));
                DrawText(dc, "W4", 9, new Point(75 - W4 / 2 - W4 / 6, -(t + 25)));
                DrawText(dc, "S1", 9, new Point(-75 + W1 + S1 / 2 - S1 / 6, -(t + 25)));
                DrawText(dc, "S2", 9, new Point((S1 / 2 + W1 / 2 + W2 / 2 + S2 / 2) - (S2 / 2 + S2 / 6 + W3 / 2 + S3 / 2 + W4 / 2), -(t + 25)));
                DrawText(dc, "S3", 9, new Point(75 - W4 - S3 / 2 - S3 / 6, -(t + 25)));

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


                var zoomw1 = _stripWidths[0] / (_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _stripWidths[4] + _slots[0] + _slots[1] + _slots[2] + _slots[3]);
                var zoomw2 = _stripWidths[1] / (_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _stripWidths[4] + _slots[0] + _slots[1] + _slots[2] + _slots[3]);
                var zoomw3 = _stripWidths[2] / (_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _stripWidths[4] + _slots[0] + _slots[1] + _slots[2] + _slots[3]);
                var zoomw4 = _stripWidths[3] / (_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _stripWidths[4] + _slots[0] + _slots[1] + _slots[2] + _slots[3]);
                var zoomw5 = _stripWidths[4] / (_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _stripWidths[4] + _slots[0] + _slots[1] + _slots[2] + _slots[3]);
                var zooms1 = _slots[0] / (_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _stripWidths[4] + _slots[0] + _slots[1] + _slots[2] + _slots[3]);
                var zooms2 = _slots[1] / (_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _stripWidths[4] + _slots[0] + _slots[1] + _slots[2] + _slots[3]);
                var zooms3 = _slots[2] / (_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _stripWidths[4] + _slots[0] + _slots[1] + _slots[2] + _slots[3]);
                var zooms4 = _slots[3] / (_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _stripWidths[4] + _slots[0] + _slots[1] + _slots[2] + _slots[3]);
                var zoomh = _substrateHeight / ((_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _stripWidths[4] + _slots[0] + _slots[1] + _slots[2] + _slots[3] + _stripsThicknees) / 10);
                var zoomt = _stripsThicknees / ((_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _stripWidths[4] + _slots[0] + _slots[1] + _slots[2] + _slots[3] + _substrateHeight) / 10);

                double W1 = 150 * zoomw1;
                double W2 = 150 * zoomw2;
                double W3 = 150 * zoomw3;
                double W4 = 150 * zoomw4;
                double W5 = 150 * zoomw5;
                double S1 = 150 * zooms1;
                double S2 = 150 * zooms2;
                double S3 = 150 * zooms3;
                double S4 = 150 * zooms4;
                double h = 40 * ZoomIn(zoomh);
                double t = 40 * ZoomIn(zoomt);

                DrawRectangle(dc, WidthColor, PenColor, -75, -t, W1, t);
                DrawRectangle(dc, WidthColor, PenColor, (W1 / 2 + S1 / 2) - (W2 / 2 + S2 / 2 + W3 / 2 + S3 / 2 + W4 / 2 + S4 / 2 + W5 / 2), -t, W2, t);
                DrawRectangle(dc, WidthColor, PenColor, (W1 / 2 + S1 / 2 + W2 / 2 + S2 / 2) - (W3 / 2 + S3 / 2 + W4 / 2 + S4 / 2 + W5 / 2), -t, W3, t);
                DrawRectangle(dc, WidthColor, PenColor, (W1 / 2 + S1 / 2 + W2 / 2 + S2 / 2 + W3 / 2 + S3 / 2) - (W4 / 2 + S4 / 2 + W5 / 2), -t, W4, t);
                DrawRectangle(dc, WidthColor, PenColor, -W5 + 75, -t, W5, t);
                DrawRectangle(dc, SubstrateColor, PenColor, -95, 0, 190, h);
                DrawRectangle(dc, GroundColor, PenColor, -95, h, 190, ground);

                //Линии для разделения ширин и зазоров
                DrawLine(dc, new Point(-75, -t), new Point(-75, -(t + 10)));
                DrawLine(dc, new Point(-75 + W1, -t), new Point(-75 + W1, -(t + 10)));
                DrawLine(dc, new Point((S1 / 2 + W1 / 2 + W2 / 2) - (W2 + S2 / 2 + W3 / 2 + S3 / 2 + W4 / 2 + S4 / 2 + W5 / 2), -t), new Point((S1 / 2 + W1 / 2 + W2 / 2) - (W2 + S2 / 2 + W3 / 2 + S3 / 2 + W4 / 2 + S4 / 2 + W5 / 2), -(t + 10)));
                DrawLine(dc, new Point((S1 / 2 + W1 / 2 + W2) - (W2 / 2 + S2 / 2 + W3 / 2 + S3 / 2 + W4 / 2 + S4 / 2 + W5 / 2), -t), new Point((S1 / 2 + W1 / 2 + W2) - (W2 / 2 + S2 / 2 + W3 / 2 + S3 / 2 + W4 / 2 + S4 / 2 + W5 / 2), -(t + 10)));
                DrawLine(dc, new Point((S1 / 2 + W1 / 2 + S2 / 2 + W2 / 2 + W3 / 2) - (W3 + S3 / 2 + W4 / 2 + S4 / 2 + W5 / 2), -t), new Point((S1 / 2 + W1 / 2 + S2 / 2 + W2 / 2 + W3 / 2) - (W3 + S3 / 2 + W4 / 2 + S4 / 2 + W5 / 2), -(t + 10)));
                DrawLine(dc, new Point((S1 / 2 + W1 / 2 + S2 / 2 + W2 / 2 + W3) - (W3 / 2 + S3 / 2 + W4 / 2 + S4 / 2 + W5 / 2), -t), new Point((S1 / 2 + W1 / 2 + S2 / 2 + W2 / 2 + W3) - (W3 / 2 + S3 / 2 + W4 / 2 + S4 / 2 + W5 / 2), -(t + 10)));
                DrawLine(dc, new Point((S1 / 2 + W1 / 2 + W2 / 2 + S2 / 2 + W3 / 2 + S3 / 2 + W4 / 2) - (W4 + S4 / 2 + W5 / 2), -t), new Point((S1 / 2 + W1 / 2 + W2 / 2 + S2 / 2 + W3 / 2 + S3 / 2 + W4 / 2) - (W4 + S4 / 2 + W5 / 2), -(t + 10)));
                DrawLine(dc, new Point((S1 / 2 + W1 / 2 + W2 / 2 + S2 / 2 + W3 / 2 + S3 / 2 + W4) - (W4 / 2 + S4 / 2 + W5 / 2), -t), new Point((S1 / 2 + W1 / 2 + W2 / 2 + S2 / 2 + W3 / 2 + S3 / 2 + W4) - (W4 / 2 + S4 / 2 + W5 / 2), -(t + 10)));
                DrawLine(dc, new Point(75 - W5, -t), new Point(75 - W5, -(t + 10)));
                DrawLine(dc, new Point(75, -t), new Point(75, -(t + 10)));
                DrawLine(dc, new Point(-80, -(t + 5)), new Point(80, -(t + 5)));

                //Линии для разделения толщин линии и подложки
                DrawLine(dc, new Point(-110, -t), new Point(-75, -t));
                DrawLine(dc, new Point(-110, 0), new Point(-95, 0));
                DrawLine(dc, new Point(-110, h), new Point(-95, h));
                DrawLine(dc, new Point(-105, -t - 5), new Point(-105, h + 5));

                //Подписи зазоров и ширин линий
                DrawText(dc, "W1", 7, new Point(-75 + W1 / 2 - W1 / 9, -(t + 20)));
                DrawText(dc, "W2", 7, new Point((S1 / 2 + W1 / 2) - (W2 / 9 + S2 / 2 + W3 / 2 + S3 / 2 + W4 / 2 + S4 / 2 + W5 / 2), -(t + 20)));
                DrawText(dc, "W3", 7, new Point((S1 / 2 + W1 / 2 + S2 / 2 + W2 / 2) - (W3 / 9 + S3 / 2 + W4 / 2 + S4 / 2 + W5 / 2), -(t + 20)));
                DrawText(dc, "W4", 7, new Point((S1 / 2 + W1 / 2 + W2 / 2 + S2 / 2 + W3 / 2 + S3 / 2) - (W4 / 9 + S4 / 2 + W5 / 2), -(t + 20)));
                DrawText(dc, "W5", 7, new Point(75 - W5 / 2 - W5 / 9, -(t + 20)));
                DrawText(dc, "S1", 7, new Point(-75 + W1 + S1 / 2 - S1 / 9, -(t + 20)));
                DrawText(dc, "S2", 7, new Point((S1 / 2 + W1 / 2 + W2 / 2) - (S2 / 9 + W3 / 2 + S3 / 2 + W4 / 2 + S4 / 2 + W5 / 2), -(t + 20)));
                DrawText(dc, "S3", 7, new Point((S1 / 2 + W1 / 2 + W2 / 2 + S2 / 2 + W3 / 2) - (S3 / 9 + W4 / 2 + S4 / 2 + W5 / 2), -(t + 20)));
                DrawText(dc, "S4", 7, new Point(75 - W5 - S4 / 2 - S4 / 9, -(t + 20)));

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

                var zoomw1 = _stripWidths[0] / (_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _stripWidths[4] + _stripWidths[5] + _slots[0] + _slots[1] + _slots[2] + _slots[3] + _slots[4]);
                var zoomw2 = _stripWidths[1] / (_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _stripWidths[4] + _stripWidths[5] + _slots[0] + _slots[1] + _slots[2] + _slots[3] + _slots[4]);
                var zoomw3 = _stripWidths[2] / (_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _stripWidths[4] + _stripWidths[5] + _slots[0] + _slots[1] + _slots[2] + _slots[3] + _slots[4]);
                var zoomw4 = _stripWidths[3] / (_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _stripWidths[4] + _stripWidths[5] + _slots[0] + _slots[1] + _slots[2] + _slots[3] + _slots[4]);
                var zoomw5 = _stripWidths[4] / (_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _stripWidths[4] + _stripWidths[5] + _slots[0] + _slots[1] + _slots[2] + _slots[3] + _slots[4]);
                var zoomw6 = _stripWidths[5] / (_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _stripWidths[4] + _stripWidths[5] + _slots[0] + _slots[1] + _slots[2] + _slots[3] + _slots[4]);

                var zooms1 = _slots[0] / (_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _stripWidths[4] + _stripWidths[5] + _slots[0] + _slots[1] + _slots[2] + _slots[3] + _slots[4]);
                var zooms2 = _slots[1] / (_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _stripWidths[4] + _stripWidths[5] + _slots[0] + _slots[1] + _slots[2] + _slots[3] + _slots[4]);
                var zooms3 = _slots[2] / (_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _stripWidths[4] + _stripWidths[5] + _slots[0] + _slots[1] + _slots[2] + _slots[3] + _slots[4]);
                var zooms4 = _slots[3] / (_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _stripWidths[4] + _stripWidths[5] + _slots[0] + _slots[1] + _slots[2] + _slots[3] + _slots[4]);
                var zooms5 = _slots[4] / (_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _stripWidths[4] + _stripWidths[5] + _slots[0] + _slots[1] + _slots[2] + _slots[3] + _slots[4]);
                var zoomh = _substrateHeight / ((_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _stripWidths[4] + _stripWidths[5] + _slots[0] + _slots[1] + _slots[2] + _slots[3] + _slots[4] + _stripsThicknees) / 12);
                var zoomt = _stripsThicknees / ((_stripWidths[0] + _stripWidths[1] + _stripWidths[2] + _stripWidths[3] + _stripWidths[4] + _stripWidths[5] + _slots[0] + _slots[1] + _slots[2] + _slots[3] + _slots[4] + _substrateHeight) / 12);

                double W1 = 150 * zoomw1;
                double W2 = 150 * zoomw2;
                double W3 = 150 * zoomw3;
                double W4 = 150 * zoomw4;
                double W5 = 150 * zoomw5;
                double W6 = 150 * zoomw6;
                double S1 = 150 * zooms1;
                double S2 = 150 * zooms2;
                double S3 = 150 * zooms3;
                double S4 = 150 * zooms4;
                double S5 = 150 * zooms5;
                double h = 40 * ZoomIn(zoomh);
                double t = 40 * ZoomIn(zoomt);

                DrawRectangle(dc, WidthColor, PenColor, -75, -t, W1, t);
                DrawRectangle(dc, WidthColor, PenColor, (W1 / 2 + S1 / 2) - (W2 / 2 + S2 / 2 + W3 / 2 + S3 / 2 + W4 / 2 + S4 / 2 + W5 / 2 + S5 / 2 + W6 / 2), -t, W2, t);
                DrawRectangle(dc, WidthColor, PenColor, (W1 / 2 + S1 / 2 + W2 / 2 + S2 / 2) - (W3 / 2 + S3 / 2 + W4 / 2 + S4 / 2 + W5 / 2 + S5 / 2 + W6 / 2), -t, W3, t);
                DrawRectangle(dc, WidthColor, PenColor, (W1 / 2 + S1 / 2 + W2 / 2 + S2 / 2 + W3 / 2 + S3 / 2) - (W4 / 2 + S4 / 2 + W5 / 2 + S5 / 2 + W6 / 2), -t, W4, t);
                DrawRectangle(dc, WidthColor, PenColor, (W1 / 2 + S1 / 2 + W2 / 2 + S2 / 2 + W3 / 2 + S3 / 2 + W4 / 2 + S4 / 2) - (W5 / 2 + S5 / 2 + W6 / 2), -t, W5, t);
                DrawRectangle(dc, WidthColor, PenColor, -W6 + 75, -t, W6, t);
                DrawRectangle(dc, SubstrateColor, PenColor, -95, 0, 190, h);
                DrawRectangle(dc, GroundColor, PenColor, -95, h, 190, ground);

                //Линии для разделения ширин и зазоров
                DrawLine(dc, new Point(-75, -t), new Point(-75, -(t + 10)));
                DrawLine(dc, new Point(-75 + W1, -t), new Point(-75 + W1, -(t + 10)));
                DrawLine(dc, new Point((W1 / 2 + S1 / 2 + W2 / 2) - (W2 + S2 / 2 + W3 / 2 + S3 / 2 + W4 / 2 + S4 / 2 + W5 / 2 + S5 / 2 + W6 / 2), -t), new Point((W1 / 2 + S1 / 2 + W2 / 2) - (W2 + S2 / 2 + W3 / 2 + S3 / 2 + W4 / 2 + S4 / 2 + W5 / 2 + S5 / 2 + W6 / 2), -(t + 10)));
                DrawLine(dc, new Point((W1 / 2 + S1 / 2 + W2) - (W2 / 2 + S2 / 2 + W3 / 2 + S3 / 2 + W4 / 2 + S4 / 2 + W5 / 2 + S5 / 2 + W6 / 2), -t), new Point((W1 / 2 + S1 / 2 + W2) - (W2 / 2 + S2 / 2 + W3 / 2 + S3 / 2 + W4 / 2 + S4 / 2 + W5 / 2 + S5 / 2 + W6 / 2), -(t + 10)));
                DrawLine(dc, new Point((W1 / 2 + S1 / 2 + W2 / 2 + S2 / 2 + W3 / 2) - (W3 + S3 / 2 + W4 / 2 + S4 / 2 + W5 / 2 + S5 / 2 + W6 / 2), -t), new Point((W1 / 2 + S1 / 2 + W2 / 2 + S2 / 2 + W3 / 2) - (W3 + S3 / 2 + W4 / 2 + S4 / 2 + W5 / 2 + S5 / 2 + W6 / 2), -(t + 10)));
                DrawLine(dc, new Point((W1 / 2 + S1 / 2 + W2 / 2 + S2 / 2 + W3) - (W3 / 2 + S3 / 2 + W4 / 2 + S4 / 2 + W5 / 2 + S5 / 2 + W6 / 2), -t), new Point((W1 / 2 + S1 / 2 + W2 / 2 + S2 / 2 + W3) - (W3 / 2 + S3 / 2 + W4 / 2 + S4 / 2 + W5 / 2 + S5 / 2 + W6 / 2), -(t + 10)));
                DrawLine(dc, new Point((W1 / 2 + S1 / 2 + W2 / 2 + S2 / 2 + W3 / 2 + S3 / 2 + W4 / 2) - (W4 + S4 / 2 + W5 / 2 + S5 / 2 + W6 / 2), -t), new Point((W1 / 2 + S1 / 2 + W2 / 2 + S2 / 2 + W3 / 2 + S3 / 2 + W4 / 2) - (W4 + S4 / 2 + W5 / 2 + S5 / 2 + W6 / 2), -(t + 10)));
                DrawLine(dc, new Point((W1 / 2 + S1 / 2 + W2 / 2 + S2 / 2 + W3 / 2 + S3 / 2 + W4) - (W4 / 2 + S4 / 2 + W5 / 2 + S5 / 2 + W6 / 2), -t), new Point((W1 / 2 + S1 / 2 + W2 / 2 + S2 / 2 + W3 / 2 + S3 / 2 + W4) - (W4 / 2 + S4 / 2 + W5 / 2 + S5 / 2 + W6 / 2), -(t + 10)));
                DrawLine(dc, new Point((W1 / 2 + S1 / 2 + W2 / 2 + S2 / 2 + W3 / 2 + S3 / 2 + W4 / 2 + S4 / 2 + W5 / 2) - (W5 + S5 / 2 + W6 / 2), -t), new Point((W1 / 2 + S1 / 2 + W2 / 2 + S2 / 2 + W3 / 2 + S3 / 2 + W4 / 2 + S4 / 2 + W5 / 2) - (W5 + S5 / 2 + W6 / 2), -(t + 10)));
                DrawLine(dc, new Point((W1 / 2 + S1 / 2 + W2 / 2 + S2 / 2 + W3 / 2 + S3 / 2 + W4 / 2 + S4 / 2 + W5) - (W5 / 2 + S5 / 2 + W6 / 2), -t), new Point((W1 / 2 + S1 / 2 + W2 / 2 + S2 / 2 + W3 / 2 + S3 / 2 + W4 / 2 + S4 / 2 + W5) - (W5 / 2 + S5 / 2 + W6 / 2), -(t + 10)));
                DrawLine(dc, new Point(75 - W6, -t), new Point(75 - W6, -(t + 10)));
                DrawLine(dc, new Point(75, -t), new Point(75, -(t + 10)));
                DrawLine(dc, new Point(-80, -(t + 5)), new Point(80, -(t + 5)));

                //Линии для разделения толщин линии и подложки
                DrawLine(dc, new Point(-110, -t), new Point(-75, -t));
                DrawLine(dc, new Point(-110, 0), new Point(-95, 0));
                DrawLine(dc, new Point(-110, h), new Point(-95, h));
                DrawLine(dc, new Point(-105, -t - 5), new Point(-105, h + 5));

                //Подписи зазоров и ширин линий
                DrawText(dc, "W1", 7, new Point(-75 + W1 / 2 - W1 / 9, -(t + 25)));
                DrawText(dc, "W2", 7, new Point(-75 + W1 + S1 + W2 / 2 - W2 / 9, -(t + 25)));
                DrawText(dc, "W3", 7, new Point(-75 + W1 + S1 + W2 + S2 + W3 / 2 - W3 / 9, -(t + 25)));
                DrawText(dc, "W4", 7, new Point(75 - W6 - S5 - W5 - S4 - W4 / 2 - W4 / 9, -(t + 25)));
                DrawText(dc, "W5", 7, new Point(75 - W6 - S5 - W5 / 2 - W5 / 9, -(t + 25)));
                DrawText(dc, "W6", 7, new Point(75 - W6 / 2 - W6 / 9, -(t + 25)));
                DrawText(dc, "S1", 7, new Point(-75 + W1 + S1 / 2 - S1 / 9, -(t + 25)));
                DrawText(dc, "S2", 7, new Point(-75 + W1 + S1 + W2 + S2 / 2 - S2 / 9, -(t + 25)));
                DrawText(dc, "S3", 7, new Point(-75 + W1 + S1 + W2 + S2 + W3 + S3 / 2 - S3 / 9, -(t + 25)));
                DrawText(dc, "S4", 7, new Point(75 - W6 - S5 - W5 - S4 / 2 - S4 / 9, -(t + 25)));
                DrawText(dc, "S5", 7, new Point(75 - W6 - S5 / 2 - S5 / 9, -(t + 25)));

                //Подписи толщин линии и подложки
                DrawText(dc, "h", 12, new Point(-115, h / 2 - 9));
                DrawText(dc, "t", 12, new Point(-115, -(t / 2) - 9));
            }
        }
    }
}
