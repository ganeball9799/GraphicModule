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
    public class WireOnMSLGC : StructureImage
    {
        /// <summary>
        /// Экземпляр линии
        /// </summary>
        private Geometry _geometry;

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
        public WireOnMSLGC(Geometry geometry)
        {
            _geometry = geometry;

            _diameter = _geometry[ParameterName.DiameterLine].Value;
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
            var ground = 5;
            var zoomt = _stripsThicknees / (_substrateHeight + _stripWidth + _diameter + _stripsThicknees);
            var zoomh = _substrateHeight / (_stripWidth + _diameter + _stripsThicknees + _substrateHeight);
            var zoomw = _stripWidth / (_stripWidth + _diameter * 2);
            var zoomd = _diameter / (_stripWidth + _diameter * 2);


            var d = 100 * zoomd;
            var W1 = 100 * zoomw;
            var t = 100 * ZoomIn(zoomt);
            var h = 100 * ZoomIn(zoomh);

            DrawEllipse(dc, WidthColor, new Point(0, -t - d / 2), d / 2, d / 2);
            DrawRectangle(dc, WidthColor, PenColor, -W1 / 2, -t, W1, t);
            DrawRectangle(dc, SubstrateColor, PenColor, -95, 0, 190, h);
            DrawRectangle(dc, GroundColor, PenColor, -95, h, 190, ground);

            //Линии разделения для ширины
            DrawLine(dc, new Point(-W1 / 2, 0), new Point(-W1 / 2, h + 15));
            DrawLine(dc, new Point(W1 / 2, 0), new Point(W1 / 2, h + 15));
            DrawLine(dc, new Point(-W1 / 2 - 5, h + 10), new Point(W1 / 2 + 5, h + 10));

            //Линии разделения для диаметра
            DrawLine(dc, new Point(-d / 2, -(d/2 + t)), new Point(-d / 2, -(d + t + 10)));
            DrawLine(dc, new Point(d / 2, -(d/2 + t)), new Point(d / 2, -(d + t + 10)));
            DrawLine(dc, new Point(-d / 2 - 5, -(d + t + 5)), new Point(d / 2 + 5, -(d + t + 5)));
            
            //Подписи ширины и зазоров
            DrawText(dc, "W", 12, new Point(-5, h+15));
            DrawText(dc, "d", 12, new Point(-5, -(t + d + 20)));
            
            //Линии для разделения толщины линии и подложки
            DrawLine(dc, new Point(-110, -t), new Point(-W1/2, -t));
            DrawLine(dc, new Point(-110, 0), new Point(-95, 0));
            DrawLine(dc, new Point(-110, h), new Point(-95, h));
            DrawLine(dc, new Point(-105, -t - 5), new Point(-105, h + 5));

            //Подписи линий разделения толщины линии и подложки
            DrawText(dc, "h", 12, new Point(-115, h / 2 - 9));
            DrawText(dc, "t", 12, new Point(-115, -(t / 2) - 9));

            //Подписи Er
            DrawText(dc, "Er", 12, new Point(-90, h / 2));


        }
    }
}
