using System.Globalization;
using GraphicModule.Models;
using Geometry = GraphicModule.Models.Geometry;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GraphicModuleUI.ViewModels.Graphic
{
    public class CoupledVerticalInsertGraphic : StructureImage
    {
        /// <summary>
        /// Экземпляр линии
        /// </summary>
        private Geometry _geometry;

        /// <summary>
        /// Ширина линии
        /// </summary>
        private double _stripsWidth;

        /// <summary>
        /// Толщина линии
        /// </summary>
        private double _stripsThicknees;

        /// <summary>
        /// Толщина подложки
        /// </summary>
        private double _substrateHeight1;

        /// <summary>
        /// Толщина подложки 
        /// </summary>
        private double _substrateHeight2;

        /// <summary>
        /// Конструктор класса линии
        /// </summary>
        public CoupledVerticalInsertGraphic(Geometry geometry)
        {
            _geometry = geometry;

            _stripsWidth = _geometry[ParameterName.StripWidth].Value;
            _stripsThicknees = _geometry[ParameterName.StripsThickness].Value;
            _substrateHeight1 = _geometry[ParameterName.SubstrateHeight, 0].Value;
            _substrateHeight2 = _geometry[ParameterName.SubstrateHeight, 1].Value;

            Canvas.SetLeft(this, 120);
            Canvas.SetTop(this, 120);
        }

        /// <summary>
        /// Метод отрисовки линии
        /// </summary>
        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            var gapLine = 30;
            var ground = 5;
            var textSize = 12;
            var zoomw = _stripsWidth / ((_substrateHeight1 + _substrateHeight2 + _stripsWidth) / 3);
            var zoomh1 = _substrateHeight1 / ((_substrateHeight2 + _stripsThicknees + _stripsWidth) / 3);
            var zoomh2 = _substrateHeight2 / ((_substrateHeight1 + _stripsWidth + _stripsThicknees) / 3);
            var zoomt = _stripsThicknees*2 / ((_substrateHeight1 + _stripsWidth + _substrateHeight2) / 3);

            double h1 = 40 * ZoomIn(zoomh1);
            double h2 = 40 * ZoomIn(zoomh2);
            double t = 20 * ZoomIn(zoomt);
            double W = 40 * ZoomIn(zoomw);
            var z = 0.5;

            DrawRectangle(dc, WidthColor, PenColor, -(h2 / 2 + t), -W, t, W);
            DrawRectangle(dc, WidthColor, PenColor, h2 / 2, -W, t, W);
            DrawRectangle(dc, SubstrateColor, PenColor, -(gapLine + t + h2 / 2), 0, t * 2 + h2 + gapLine * 2, h1);
            DrawRectangle(dc, SubstrateColorSecond, PenColor, -h2 / 2, -W, h2, W);
            DrawRectangle(dc, GroundColor, PenColor, -(gapLine + t + h2 / 2), h1, t * 2 + h2 + gapLine * 2, ground);

            //Линии разделения для толщины линии и диэлектрика
            DrawLine(dc, new Point(-(h2 / 2 + t - z), -W), new Point(-(h2 / 2 + t - z), -(W + 15)));
            DrawLine(dc, new Point(-(h2 / 2), -W), new Point(-(h2 / 2), -(W + 15)));
            DrawLine(dc, new Point((h2 / 2), -W), new Point((h2 / 2), -(W + 15)));
            DrawLine(dc, new Point((h2 / 2 + t - z), -W), new Point((h2 / 2 + t - z), -(W + 15)));
            DrawLine(dc, new Point(-(h2 / 2 + t + 5), -(W + 10)), new Point((h2 / 2 + t + 5), -(W + 10)));

            //Подписи для толщины линии и диэлектрика
            DrawText(dc, "h2", textSize, new Point(-5, -(W + 25)));
            DrawText(dc, "t", textSize, new Point(-(h2 / 2 + t / 2 + 2), -(W + 25)));
            DrawText(dc, "t", textSize, new Point((h2 / 2 + t / 2 - 2), -(W + 25)));

            //Линии для разделения толщины подложки и ширины линии
            DrawLine(dc, new Point(-(gapLine + t + h2 / 2 + 15), -W), new Point(-(t + h2 / 2), -W));
            DrawLine(dc, new Point(-(gapLine + t + h2 / 2 + 15), 0), new Point(-(gapLine + t + h2 / 2), 0));
            DrawLine(dc, new Point(-(gapLine + t + h2 / 2 + 15), h1), new Point(-(gapLine + t + h2 / 2), h1));
            DrawLine(dc, new Point(-(gapLine + t + h2 / 2 + 10), -W - 5), new Point(-(gapLine + t + h2 / 2 + 10), h1 + 5));

            //Подписи для ширины линии и толщины подложки
            DrawText(dc, "W", textSize, new Point(-(gapLine + t + h2 / 2 + 25), -W / 2 - 5));
            DrawText(dc, "h1", textSize, new Point(-(gapLine + t + h2 / 2 + 25), h1 / 2 - 5));
        }
    }
}