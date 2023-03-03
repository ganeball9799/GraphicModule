using System.Globalization;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace GraphicModuleUI.ViewModels
{
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Media;
    using GraphicModule.Models;

    public class StructureImage : FrameworkElement
    {
        /// <summary>
        /// Цвет линии подписи
        /// </summary>
        private readonly Pen _lineColor;

        /// <summary>
        /// Цвет обводки фигуры
        /// </summary>
        protected Pen PenColor;

        /// <summary>
        /// Цвет линии
        /// </summary>
        protected SolidColorBrush WidthColor;

        /// <summary>
        /// Цвет подложки
        /// </summary>
        protected SolidColorBrush SubstrateColor;

        /// <summary>
        /// Цвет второй подложки
        /// </summary>
        protected SolidColorBrush SubstrateSecondColor;

        /// <summary>
        /// Цвет экрана
        /// </summary>
        protected SolidColorBrush GroundColor;

        public StructureImage()
        {
            Canvas.SetLeft(this, 120);
            Canvas.SetTop(this, 120);
            _lineColor = new Pen(Brushes.Red, 0.5);
            PenColor = new Pen(Brushes.Black, 0.1);
            WidthColor = new SolidColorBrush(Color.FromRgb(184, 115, 51));
            SubstrateColor = new SolidColorBrush(Color.FromRgb(249, 250, 222));
            SubstrateSecondColor = new SolidColorBrush(Color.FromRgb(149, 194, 234));
            GroundColor = new SolidColorBrush(Colors.Black);
        }
        /// <summary>
        /// Метод для отрисовки прямоугольника
        /// </summary>
        protected void DrawRectangle(DrawingContext dc, SolidColorBrush color,
                                        Pen pen, double x, double y, double width, double height)
        {
            var figure = new Rect(x, y, width, height);
            dc.DrawRectangle(color, pen, figure);
        }

        /// <summary>
        /// Метод для отрисовки элипса
        /// </summary>
        protected void DrawEllipse(DrawingContext dc, SolidColorBrush color, Point point, double r1, double r2) =>
            dc.DrawEllipse(color, PenColor, point, r1, r2);

        /// <summary>
        /// Метод для коэффициентов масштабирования
        /// </summary>
        protected double ZoomIn(double zoom)
        {
            if (zoom > 1.7)
            {
                zoom = 1.7;
            }
            else if (zoom < 0.05)
            {
                zoom = 0.05;
            }

            return zoom;
        }

        /// <summary>
        /// Метод для коэффициентов масштабирования
        /// </summary>
        protected double ZoomIn(double zoom, double max, double min)
        {
            if (zoom > max)
            {
                zoom = max;
            }
            else if (zoom < min)
            {
                zoom = min;
            }

            return zoom;
        }

        /// <summary>
        /// Метод для отрисовки текста
        /// </summary>
        [System.Obsolete]
        protected void DrawText(DrawingContext dc, string measure, double textSize, Point point) =>
            dc.DrawText(GetDrawingText(measure, textSize), point);

        /// <summary>
        /// Метод для отрисовки линии
        /// </summary>
        protected void DrawLine(DrawingContext dc, Point p1, Point p2) => dc.DrawLine(_lineColor, p1, p2);

        /// <summary>
        /// Метод для задания подписи
        /// </summary>
        [System.Obsolete]
        private FormattedText GetDrawingText(string measure, double textSize) =>
            new FormattedText(measure, CultureInfo.GetCultureInfo("en-Us"),
                FlowDirection.LeftToRight, new Typeface("verdana"), textSize, Brushes.DarkBlue);
    }
}
