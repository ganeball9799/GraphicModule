using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Baml2006;
using System.Windows.Controls;
using System.Windows.Media;
using GraphicModule.Models;
using Geometry = GraphicModule.Models.Geometry;

namespace GraphicModuleUI.ViewModels.Graphic
{
    public class RndSqlGraphic : StructureImage
    {
        /// <summary>
        /// Экземпляр линии
        /// </summary>
        private Geometry _geometry;

        /// <summary>
        /// Лист с диаметрами линий
        /// </summary>
        private List<double> _diameters = new List<double>();

        /// <summary>
        /// Зазор между проводниками
        /// </summary>
        private double _slot;

        /// <summary>
        /// Зазор для левогопроводника
        /// </summary>
        private double _slot0;

        /// <summary>
        /// Лист с высотами
        /// </summary>
        private List<double> _height = new List<double>();

        /// <summary>
        /// Высота диэлектрика
        /// </summary>
        private double _a;

        /// <summary>
        /// Ширина диэлектрика
        /// </summary>
        private double _b;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public RndSqlGraphic(Geometry geometry)
        {
            _geometry = geometry;

            _height.Add(_geometry[ParameterName.Height].Value);
            _height.Add(_geometry[ParameterName.Height, 1].Value);
            _a = _geometry[ParameterName.HeightDielectric].Value;
            _b = _geometry[ParameterName.WidthDielectric].Value;
            _diameters.Add(_geometry[ParameterName.DiameterLine].Value);
            _diameters.Add(_geometry[ParameterName.DiameterLine, 1].Value);
            _slot0 = _geometry[ParameterName.Slot].Value;
            _slot = _geometry[ParameterName.Slot, 1].Value;

            Canvas.SetLeft(this, 125);
            Canvas.SetTop(this, 125);

        }

        /// <summary>
        /// Метод отрисовки
        /// </summary>
        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);

            var zoomd1 = _diameters[0] / (_slot + _diameters[1] + _slot0 + _diameters[0]);
            var zoomd2 = _diameters[1] / (_slot + _diameters[0] + _slot0 + _diameters[1]);
            var zooms = _slot / (_slot0 + _diameters[0] + _diameters[1] + _slot);
            var zooms0 = _slot0 / (_slot + _diameters[0] + _diameters[1] + _slot0);
            var zooma = _a / ((_slot + _diameters[0] + _diameters[1] + _b + _a + _slot0) / 6);
            var zoomb = _b / ((_slot + _diameters[0] + _diameters[1] + _a + _b + _slot0) / 6);
            var zoomh1 = (_height[0] / _b) * zoomb;
            var zoomh2 = (_height[1] / _b) * zoomb;

            var d1 = 100 * ZoomIn(zoomd1, 2, 0.05);
            var d2 = 100 * ZoomIn(zoomd2, 2, 0.05);
            var s = 50 * ZoomIn(zooms, 2, 0.05);
            var s0 = 50 * ZoomIn(zooms0, 2, 0.05);
            var a = 50 * ZoomIn(zooma, 3, 0.6);
            var b = 50 * ZoomIn(zoomb, 3, 0.6);
            var h1 = 50 * zoomh1;
            var h2 = 50 * zoomh2;

            DrawRectangle(dc, SubstrateColor, PenColor, -a / 2, -b / 2, a, b);
            DrawEllipse(dc, WidthColor, new Point(-a / 2 + s0 + d1 / 2, b / 2 - h1 - d1 / 2), d1 / 2, d1 / 2);
            DrawEllipse(dc, WidthColor, new Point(-a / 2 + s0 + d1 + s + d2 / 2, b / 2 - h2 - d2 / 2), d2 / 2, d2 / 2);

            //Линии подписи высоты диэлектрика
            DrawLine(dc, new Point(-a / 2, -b / 2), new Point(-a / 2 - 15, -b / 2));
            DrawLine(dc, new Point(-a / 2, b / 2), new Point(-a / 2 - 15, b / 2));
            DrawLine(dc, new Point(-a / 2 - 10, -b / 2 - 5), new Point(-a / 2 - 10, b / 2 + 5));

            //Подписи высоты диэлектрика
            DrawText(dc, "b", 13, new Point(-a / 2 - 20, -3));

            //Линии подписи ширины диэлектрика
            DrawLine(dc, new Point(-a / 2, b / 2), new Point(-a / 2, b / 2 + 15));
            DrawLine(dc, new Point(a / 2, b / 2), new Point(a / 2, b / 2 + 15));
            DrawLine(dc, new Point(-a / 2 - 5, b / 2 + 10), new Point(a / 2 + 5, b / 2 + 10));

            //Подписи высоты диэлектрика
            DrawText(dc, "a", 13, new Point(-3, b / 2 + 10));

            //Линии подписи левого проводника
            DrawLine(dc, new Point(-a / 2 + s0 + d1 / 2, b / 2 - h1 - d1), new Point(-a / 2 + s0 - 5, b / 2 - h1 - d1));
            DrawLine(dc, new Point(-a / 2 + s0 + d1 / 2, b / 2 - h1), new Point(-a / 2 + s0 - 5, b / 2 - h1));
            DrawLine(dc, new Point(-a / 2 + s0 - 3, b / 2 - h1 - d1 - 5), new Point(-a / 2 + s0 - 3, b / 2));

            //Линии подписи правого проводника
            DrawLine(dc, new Point(-a / 2 + s0 + d1 + s + d2 / 2, b / 2 - h1 - d1), new Point(-a / 2 + s0 + d1 + s + d2 +5, b / 2 - h1 - d1));
            DrawLine(dc, new Point(-a / 2 + s0 + d1 + s + d2 / 2, b / 2 - h1), new Point(-a / 2 + s0 + d1 + s + d2 +5, b / 2 - h1));
            DrawLine(dc, new Point(-a / 2 + s0 + d1 + s + d2  + 3, b / 2 - h1 - d1 - 5), new Point(-a / 2 + s0 + d1 + s + d2 + 3, b / 2));

            //Подписи левого проводника
            DrawText(dc, "d1", 9, new Point(-a / 2 + s0 - 15, b / 2 - h1 - d1 / 2 - 5));

            //Подписи правого проводника
            DrawText(dc, "d2", 9, new Point(-a / 2 + s0 + d1 + s + d2+5, b / 2 - h1 - d1 / 2 - 5));

            //Подпись h1
            DrawText(dc, "h1", 9, new Point(-a / 2 + s0 - 15, b / 2 - h1 + 3));

            //Подпись h2
            DrawText(dc, "h2", 9, new Point(-a / 2 + s0 + d1 + s + d2+5 , b / 2 - h1 + 3));
        }
    }
}
