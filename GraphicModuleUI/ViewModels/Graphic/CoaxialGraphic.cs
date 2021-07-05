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

            var d = 50 * ZoomIn(zoomd);
            
            DrawEllipse(dc, SubstrateColor, new Point(0, 0), 100, 100);
            DrawEllipse(dc, WidthColor, new Point(0, 0), d * 2, d * 2);

            ////Линии разделения для слотов и ширины
            //DrawLine(dc, new Point(-(S1 + W1 / 2), -t), new Point(-(S1 + W1 / 2), -(t + 15)));
            //DrawLine(dc, new Point(-W1 / 2, -t), new Point(-W1 / 2, -(t + 15)));
            //DrawLine(dc, new Point(W1 / 2, -t), new Point(W1 / 2, -(t + 15)));
            //DrawLine(dc, new Point(W1 / 2 + S2, -t), new Point(W1 / 2 + S2, -(t + 15)));
            //DrawLine(dc, new Point(-(S1 + W1 / 2 + 5), -(t + 10)), new Point(W1 / 2 + S2 + 5, -(t + 10)));

            ////Подписи ширины и зазоров
            //DrawText(dc, "W", 13, new Point(-5, -(t + 30)));
            //DrawText(dc, "S1", 13, new Point(-(S1 / 2 + W1 / 2 + 5), -(t + 30)));
            //DrawText(dc, "S2", 13, new Point(W1 / 2 + S2 / 2 - 5, -(t + 30)));

            ////Линии для разделения толщины линии и подложки
            //DrawLine(dc, new Point(-(screenWidth + S1 + W1 / 2), -t),
            //            new Point(-(10 + screenWidth + S1 + W1 / 2), -t));
            //DrawLine(dc, new Point(-(screenWidth + S1 + W1 / 2), 0),
            //            new Point(-(10 + screenWidth + S1 + W1 / 2), 0));
            //DrawLine(dc, new Point(-(screenWidth + S1 + W1 / 2), h),
            //            new Point(-(10 + screenWidth + S1 + W1 / 2), h));
            //DrawLine(dc, new Point(-(5 + screenWidth + S1 + W1 / 2), -5 - t),
            //            new Point(-(5 + screenWidth + S1 + W1 / 2), h + 5));

            ////Подписи линий разделения толщины линии и подложки
            //DrawText(dc, "h", 13, new Point(-(20 + screenWidth + S1 + W1 / 2), h / 2 - 10));
            //DrawText(dc, "t", 13, new Point(-(20 + screenWidth + S1 + W1 / 2), -t / 2 - 10));
        }
    }
}
