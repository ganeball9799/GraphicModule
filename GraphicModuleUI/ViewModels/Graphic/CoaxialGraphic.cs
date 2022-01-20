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
    public class CoaxialGraphic : StructureImage
    {
        /// <summary>
        /// Экземпляр линии
        /// </summary>
        private Geometry _geometry;

        /// <summary>
        /// Лист с зазорами линий
        /// </summary>
        private List<double> _diameters = new List<double>();


        /// <summary>
        /// Конструктор класса
        /// </summary>
        public CoaxialGraphic(Geometry geometry)
        {
            _geometry = geometry;

            _diameters.Add(_geometry[ParameterName.DiameterLine].Value);
            _diameters.Add(_geometry[ParameterName.DiameterDielectric].Value);

            Canvas.SetLeft(this, 120);
            Canvas.SetTop(this, 120);
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            
            var zoomd = _diameters[0] / _diameters[1];

            var d = 45 * ZoomIn(zoomd,1,0.02);
            
            DrawEllipse(dc, SubstrateColor, new Point(0, 0), 90, 90);
            DrawEllipse(dc, WidthColor, new Point(0, 0), d * 2, d * 2);

            //Линии внешнего круга
            DrawLine(dc, new Point(0, 90), new Point(-105, 90));
            DrawLine(dc, new Point(0, -90), new Point(-105, -90));
            DrawLine(dc, new Point(-95, 100), new Point(-95, -100));
            
            //Подписи ширины и зазоров
            DrawText(dc, "D", 13, new Point(-110, -5));

            //Линии внутреннего круга
            DrawLine(dc, new Point(0, d*2), new Point(110, d*2));
            DrawLine(dc, new Point(0, -d*2), new Point(110, -d*2));
            DrawLine(dc, new Point(100, d*2+10), new Point(100, -d*2-10));

            //Подписи ширины и зазоров
            DrawText(dc, "d", 13, new Point(110, -10));
        }
    }
}
