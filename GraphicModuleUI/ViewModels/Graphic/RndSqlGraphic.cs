using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
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
        /// Лист с зазорами линий
        /// </summary>
        private List<double> _diameters = new List<double>();


        private double _slot;

        private List<double> _height = new List<double>();

        private double _a;

        private double _b;

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

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);

            var zoomd1 = _diameters[0] / (_slot + _diameters[0] + _diameters[1] + _a + _b);
            var zoomd2 = _diameters[1] / (_slot + _diameters[0] + _diameters[1] + _a + _b);
            var zooms = _slot / (_slot + _diameters[0] + _diameters[1] + _a + _b);
            var zooma = _a / (_a + _b + _height[0] + _height[1]);
            var zoomb = _b / (_a + _b + _height[0] + _height[1]);
            var zoomh1 = _height[0] / (_a + _b + _height[0] + _height[1]);
            var zoomh2 = _height[1] / (_a + _b + _height[0] + _height[1]);

            var d1 = 170 * ZoomIn(zoomd1, 1.4, 0.02);
            var d2 = 170 * ZoomIn(zoomd2, 1.4, 0.02);
            var s = 170 * ZoomIn(zooms, 1.4, 0.02);
            var a = 170 * ZoomIn(zooma, 1.4, 0.2);
            var b = 170 * ZoomIn(zoomb, 1.4, 0.2);
            var h1 = 170 * ZoomIn(zoomh1, 1, 0.002);
            var h2 = 170 * ZoomIn(zoomh2, 1, 0.002);





            DrawRectangle(dc, SubstrateColor, PenColor, -b / 2, -a / 2, b, a);


            DrawEllipse(dc, WidthColor, new Point(-s / 2 - d1 / 2, a / 2 - h1 - d1 / 2), d1 / 2, d1 / 2);
            DrawEllipse(dc, WidthColor, new Point(s / 2 + d2 / 2, a / 2 - h2 - d2 / 2), d2 / 2, d2 / 2);

            ////Линии внешнего круга
            //DrawLine(dc, new Point(0, 90), new Point(-105, 90));
            //DrawLine(dc, new Point(0, -90), new Point(-105, -90));
            //DrawLine(dc, new Point(-95, 100), new Point(-95, -100));

            ////Подписи ширины и зазоров
            //DrawText(dc, "D", 13, new Point(-110, -5));

            ////Линии внутреннего круга
            //DrawLine(dc, new Point(0, d * 2), new Point(110, d * 2));
            //DrawLine(dc, new Point(0, -d * 2), new Point(110, -d * 2));
            //DrawLine(dc, new Point(100, d * 2 + 10), new Point(100, -d * 2 - 10));

            //Подписи ширины и зазоров
            //DrawText(dc, "d", 13, new Point(110, -10));
        }
    }
}
