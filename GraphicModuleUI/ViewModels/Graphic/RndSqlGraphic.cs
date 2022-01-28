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
            _slot = _geometry[ParameterName.Slot].Value;

            Canvas.SetLeft(this, 125);
            Canvas.SetTop(this, 125);

        }

        /// <summary>
        /// Метод отрисовки
        /// </summary>
        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);

            var zoomd1 = _diameters[0] / (_slot + _diameters[0] + _diameters[1] + _b + _a);
            var zoomd2 = _diameters[1] / (_slot + _diameters[0] + _diameters[1] + _b + _a);
            var zooms = _slot / (_slot + _diameters[0] + _diameters[1] + _b + _a);
            var zooma = _a / ((_slot + _diameters[0] + _diameters[1] + _b+_a) / 5);
            var zoomb = _b / ((_slot + _diameters[0] + _diameters[1] + _a+_b) / 5);
            var zoomh1 = _height[0] / (_a/_diameters[0]+_a/_height[0]);
            var zoomh2 = _height[1] / (_a / _diameters[1] + _a / _height[1]);

            var d1 = 250 * zoomd1;
            var d2 = 250 * zoomd2;
            var s = 250 * zooms;
            var a = 80 * ZoomIn(zooma, 2.5, 0.5);
            var b = 80 * ZoomIn(zoomb, 2.5, 0.5);
            var h1 = 70 *zoomh1;
            var h2 = 70 * zoomh2;

            //Вторая версия коэффициентов
            //var a = 180 * zooma;
            //var b = 180 * zoomb;
            //var d1 = 150 * ZoomIn(zoomd1, 1.5, 0.02);
            //var d2 = 150 * ZoomIn(zoomd2, 1.5, 0.02);
            //var s = 300 * ZoomIn(zooms, 1, 0.01);
            //var a = 300 * ZoomIn(zooma, 2.5, 0.4);
            //var b = 300 * ZoomIn(zoomb, 2.5, 0.4);
            //var h1 = 300 * ZoomIn(zoomh1, 1, 0.01);
            //var h2 = 300 * ZoomIn(zoomh2, 1, 0.01);
            
            DrawRectangle(dc, SubstrateColor, PenColor, -b / 2, -a / 2, b, a);
            DrawEllipse(dc, WidthColor, new Point(-s / 2 - d1 / 2, a / 2 - h1 - d1 / 2), d1 / 2, d1 / 2);
            DrawEllipse(dc, WidthColor, new Point(s / 2 + d2 / 2, a / 2 - h2 - d2 / 2), d2 / 2, d2 / 2);

            //Линии подписи высоты диэлектрика
            DrawLine(dc, new Point(-b/2, -a/2), new Point(-b/2-15, -a/2));
            DrawLine(dc, new Point(-b/2, a/2), new Point(-b/2-15, a/2));
            DrawLine(dc, new Point(-b/2-10, -a/2-5), new Point(-b/2-10, a/2+5));

            //Подписи высоты диэлектрика
            DrawText(dc, "а", 13, new Point(-b/2-20, -3));

            //Линии подписи ширины диэлектрика
            DrawLine(dc, new Point(-b / 2, a / 2), new Point(-b / 2, a / 2+15));
            DrawLine(dc, new Point(b / 2, a / 2), new Point(b / 2, a / 2 + 15));
            DrawLine(dc, new Point(-b / 2-5, a / 2+10), new Point(b / 2+5, a / 2 + 10));

            //Подписи высоты диэлектрика
            DrawText(dc, "b", 13, new Point(-3, a/2+10));

            //Линии подписи левого проводника
            DrawLine(dc, new Point(-s / 2 - d1 / 2, a / 2 - h1 - d1), new Point(-s / 2 - d1 / 2-15, a / 2 - h1 - d1));
            DrawLine(dc, new Point(-s / 2 - d1 / 2, a / 2 - h1 ), new Point(-s / 2 - d1 / 2 - 15, a / 2 - h1));
            DrawLine(dc, new Point(-s / 2 - d1 / 2-10, a / 2 - h1 - d1-5), new Point(-s / 2 - d1 / 2 - 10, a / 2));

            //Подписи левого проводника
            DrawText(dc, "d1", 9, new Point(-s / 2 - d1 / 2 - 25, a / 2 - h1 - d1 / 2-5));

            //Линии подписи правого проводника
            DrawLine(dc, new Point(s / 2 + d1 / 2, a / 2 - h1 - d1), new Point(s / 2 + d1 / 2 + 15, a / 2 - h1 - d1));
            DrawLine(dc, new Point(s / 2 + d1 / 2, a / 2 - h1), new Point(s / 2 + d1 / 2 + 15, a / 2 - h1));
            DrawLine(dc, new Point(s / 2 + d1 / 2 + 10, a / 2 - h1 - d1 - 5), new Point(s / 2 + d1 / 2 + 10, a / 2));

            //Подписи правого проводника
            DrawText(dc, "d2", 9, new Point(s / 2 + d1 / 2 + 25, a / 2 - h1 - d1 / 2 - 5));

            //Подпись h1
            DrawText(dc, "h1", 9, new Point(-s / 2 - d1 / 2 - 25, a / 2 - h1  + 3));

            //Подпись h2
            DrawText(dc, "h2", 9, new Point(s / 2 + d1 / 2 + 25, a / 2 - h1 + 3));
        }
    }
}
