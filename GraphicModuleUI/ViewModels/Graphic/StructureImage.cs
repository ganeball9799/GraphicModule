using System.Globalization;
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
        /// Задает цвет обводки фигуры
        /// </summary>
        protected Pen PenColor = new Pen(Brushes.Black, 0.1);

        /// <summary>
        /// Задает цвет линии подписи
        /// </summary>
        private Pen LineColor = new Pen(Brushes.Red, 0.5);

        /// <summary>
        /// Задает цвет линии
        /// </summary>
        protected SolidColorBrush WidthColor = new SolidColorBrush(Color.FromRgb(80, 80, 230));

        /// <summary>
        /// Задает цвет подложки
        /// </summary>
        protected SolidColorBrush SubstrateColor = new SolidColorBrush(Color.FromRgb(140, 137, 126));

        /// <summary>
        /// Задает цвет подложки для линии с вертикальной вставкой
        /// </summary>
        protected SolidColorBrush SubstrateColorSecond = new SolidColorBrush(Color.FromRgb(204, 173, 96));

        /// <summary>
        /// Задает цвет экрана
        /// </summary>
        protected SolidColorBrush GroundColor = new SolidColorBrush(Colors.Black);

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
            if (zoom > 1.4)
            {
                zoom = 1.4;
            }
            else if (zoom < 0.5)
            {
                zoom = 0.5;
            }

            return zoom;
        }
        
        /// <summary>
        /// Метод для отрисовки текста
        /// </summary>
        protected void DrawText(DrawingContext dc, string measure,double textSize, Point point) =>
            dc.DrawText(GetDrawingText(measure,textSize), point);

        /// <summary>
        /// Метод для отрисовки линии
        /// </summary>
        protected void DrawLine(DrawingContext dc, Point p1, Point p2) => dc.DrawLine(LineColor, p1, p2);

        /// <summary>
        /// Метод для задания подписи
        /// </summary>
        private FormattedText GetDrawingText(string measure, double textSize) =>
            new FormattedText(measure, CultureInfo.GetCultureInfo("en-Us"),
                FlowDirection.LeftToRight, new Typeface("verdana"), textSize, Brushes.DarkBlue);
    }
}