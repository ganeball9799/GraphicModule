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
        /// <summary>
        /// Экземпляр линии
        /// </summary>
        private Geometry _geometry;

        /// <summary>
        /// Лист с зазорами линий
        /// </summary>
        private List<double> _slots = new List<double>();

        /// <summary>
        /// Ширина линии
        /// </summary>
        private double _stripWidth;

        /// <summary>
        /// Толщина линии
        /// </summary>
        private double _stripsThicknees;

        /// <summary>
        /// Толщина подложки
        /// </summary>
        private double _substrateHeight;

        /// <summary>
        /// Конструктор класса
        /// </summary>
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

        /// <summary>
        /// Метод отрисовки
        /// </summary>
        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            var screenWidth = 8;
            var zoomt = _stripsThicknees / ((_substrateHeight + _stripWidth + _slots[0] + _slots[1]) / 4);
            var zoomh = _substrateHeight / ((_stripWidth + _slots[0] + _slots[1] + _stripsThicknees) / 4);
            var zooms1 = _slots[0] / (_stripWidth + _slots[0] + _slots[1]);
            var zooms2 = _slots[1] / (_stripWidth + _slots[0] + _slots[1]);
            var zoomw = _stripWidth / (_stripWidth + _slots[0] + _slots[1]);

            var S1 = 180 * zooms1;
            var S2 = 180 * zooms2;
            var W1 = 180 * zoomw;
            var t = 40 * ZoomIn(zoomt);
            var h = 40 * ZoomIn(zoomh);

            DrawRectangle(dc, WidthColor, PenColor, -95 + screenWidth + S1, -t, W1, t);
            DrawRectangle(dc, SubstrateColor, PenColor, -95, 0, 190, h);
            DrawRectangle(dc, GroundColor, PenColor, -95, -t, screenWidth, t);
            DrawRectangle(dc, GroundColor, PenColor, -screenWidth + 95, -t, screenWidth, t);

            //Линии разделения для слотов и ширины
            DrawLine(dc, new Point(-95 + screenWidth, -t), new Point(-95 + screenWidth, -(t + 15)));
            DrawLine(dc, new Point(-95 + screenWidth + S1, -t), new Point(-95 + screenWidth + S1, -(t + 15)));
            DrawLine(dc, new Point(-95 + screenWidth + S1 + W1, -t), new Point(-95 + screenWidth + S1 + W1, -(t + 15)));
            DrawLine(dc, new Point(-screenWidth + 95, -t), new Point(-screenWidth + 95, -(t + 15)));
            DrawLine(dc, new Point(-95, -(t + 10)), new Point(95, -(t + 10)));

            //Подписи ширины и зазоров
            DrawText(dc, "W1", 12, new Point(-95 + screenWidth + S1 + W1 / 2 - W1 / 7, -(t + 30)));
            DrawText(dc, "S1", 12, new Point(-95 + screenWidth + S1 / 2 - S1 / 7, -(t + 30)));
            DrawText(dc, "S2", 12, new Point(95 - screenWidth - S2 / 2 - S2 / 7, -(t + 30)));

            //Линии для разделения толщины линии и подложки
            DrawLine(dc, new Point(-110, -t), new Point(-(screenWidth + S1 + W1 / 2), -t));
            DrawLine(dc, new Point(-110, 0), new Point(-95, 0));
            DrawLine(dc, new Point(-110, h), new Point(-95, h));
            DrawLine(dc, new Point(-105, -t - 5), new Point(-105, h + 5));

            //Подписи линий разделения толщины линии и подложки
            DrawText(dc, "h", 12, new Point(-115, h / 2 - 9));
            DrawText(dc, "t", 12, new Point(-115, -(t / 2) - 9));
        }
    }
}
