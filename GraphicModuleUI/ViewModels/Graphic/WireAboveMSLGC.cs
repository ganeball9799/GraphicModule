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
    public class WireAboveMSLGC : StructureImage
    {
        /// <summary>
        /// Экземпляр линии
        /// </summary>
        private Geometry _geometry;

        /// <summary>
        /// Высота над проводником
        /// </summary>
        private double _height;

        /// <summary>
        /// Диаметр проводника
        /// </summary>
        private double _diameter;

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
        public WireAboveMSLGC(Geometry geometry)
        {
            _geometry = geometry;

            _height = _geometry[ParameterName.Height].Value;
            _diameter = _geometry[ParameterName.DiameterLine].Value;
            _stripWidth = _geometry[ParameterName.StripWidth].Value;
            _stripsThicknees = _geometry[ParameterName.StripsThickness].Value;
            _substrateHeight = _geometry[ParameterName.SubstrateHeight].Value;
        }

        /// <summary>
        /// Метод отрисовки
        /// </summary>
        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            var ground = 5;
            var zoomt = _stripsThicknees / (_substrateHeight + _stripWidth + _diameter + _stripsThicknees + _height);
            var zoomh = _substrateHeight / (_stripWidth + _diameter + _stripsThicknees + _substrateHeight + _height);
            var zoomw = _stripWidth / (_stripWidth + _diameter * 2);
            var zoomd = _diameter / (_stripWidth + _diameter * 2);
            var zoomh1 = _height / (_substrateHeight + _diameter + _stripsThicknees + _height);


            var d = 100 * zoomd;
            var W1 = 100 * zoomw;
            var t = 100 * ZoomIn(zoomt);
            var h = 100 * ZoomIn(zoomh);
            var h1 = 100 * zoomh1;

            //Отрисовка фигур
            DrawEllipse(dc, WidthColor, new Point(0, -(t + d / 2+h1) ), d / 2, d / 2);
            DrawRectangle(dc, WidthColor, PenColor, -W1 / 2, -t, W1, t);
            DrawRectangle(dc, SubstrateColor, PenColor, -95, 0, 190, h);
            DrawRectangle(dc, GroundColor, PenColor, -95, h, 190, ground);

            //Линии разделения для ширины
            DrawLine(dc, new Point(-W1 / 2, 0), new Point(-W1 / 2, h + 15));
            DrawLine(dc, new Point(W1 / 2, 0), new Point(W1 / 2, h + 15));
            DrawLine(dc, new Point(-W1 / 2 - 5, h + 10), new Point(W1 / 2 + 5, h + 10));

            //Линии разделения для диаметра
            DrawLine(dc, new Point(-d / 2, -(d/2 + t+h1)), new Point(-d / 2, -(d + t + 10 + h1)));
            DrawLine(dc, new Point(d / 2, -(d/2 + t + h1)), new Point(d / 2, -(d + t + 10 + h1)));
            DrawLine(dc, new Point(-d / 2 - 5, -(d + t + 5 + h1)), new Point(d / 2 + 5, -(d + t + 5 + h1)));
            
            //Подписи ширины и зазоров
            DrawText(dc, "W", 12, new Point(-5, h+15));
            DrawText(dc, "d", 12, new Point(-5, -(t + d + 20+h1)));

            //Линии для разделения толщины линии и подложки
            DrawLine(dc, new Point(-110, -(t+h1)), new Point(0, -(t+h1)));
            DrawLine(dc, new Point(-110, -t), new Point(-W1/2, -t));
            DrawLine(dc, new Point(-110, 0), new Point(-95, 0));
            DrawLine(dc, new Point(-110, h), new Point(-95, h));
            DrawLine(dc, new Point(-105, -t -h1- 5), new Point(-105, h + 5));

            //Подписи линий разделения толщины линии, высоты и подложки
            DrawText(dc, "h1", 12, new Point(-125, -t-h1/2-9));
            DrawText(dc, "h2", 12, new Point(-125, h / 2 - 9));
            DrawText(dc, "t", 12, new Point(-115, -(t / 2) - 9));

            //Подписи Er
            DrawText(dc, "Er", 12, new Point(-90, h / 2));
        }
    }
}
