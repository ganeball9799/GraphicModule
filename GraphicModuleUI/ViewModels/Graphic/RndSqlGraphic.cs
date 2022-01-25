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
   public class RndSqlGraphic: StructureImage
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
        public RndSqlGraphic(Geometry geometry)
        {
            _geometry = geometry;

            _diameters.Add(_geometry[ParameterName.DiameterLine].Value);
            _diameters.Add(_geometry[ParameterName.DiameterLine,1].Value);
            _slot = _geometry[ParameterName.Slot].Value;

            Canvas.SetLeft(this, 120);
            Canvas.SetTop(this, 120);

        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);

            var zoomd1 = _diameters[0]/(_slot+_diameters[0]+_diameters[1]);
            var zoomd2 = _diameters[1] / (_slot + _diameters[0] + _diameters[1]);
            var zooms = _slot / (_slot + _diameters[0] + _diameters[1]);

            var d1 = 100 * ZoomIn(zoomd1, 1.4, 0.02);
            var d2 = 100 * ZoomIn(zoomd2, 1.4, 0.02);
            var s = 100 * ZoomIn(zooms, 1.4, 0.02);



            DrawRectangle(dc, SubstrateColor, PenColor, -120, -100, 240, 200);


            DrawEllipse(dc, WidthColor, new Point(-s/2-d1/2, 0), d1/2, d1/2);
            DrawEllipse(dc, WidthColor, new Point(s/2+d2/2, 0), d2/2, d2/2);

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
