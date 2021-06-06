using System.Globalization;

namespace GraphicModuleUI.ViewModels
{
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Media;
    using GraphicModule.Models;

    public class StructureImage : FrameworkElement
    {
        /// <summary>
        /// Метод для коэффициентов масштабирования
        /// </summary>
        public double Zoomin(double zoom)
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
        /// Метод для задания подписи
        /// </summary>
        public FormattedText GetDrawingText(string measure) =>
            new FormattedText(measure, CultureInfo.GetCultureInfo("en-Us"),
                FlowDirection.LeftToRight, new Typeface("verdana"), 13, Brushes.DarkBlue);

        /// <summary>
        /// Задает цвет обводки фигуры
        /// </summary>
        public Pen ColorPen = new Pen(Brushes.Black, 0.1);

        /// <summary>
        /// Задает цвет линии подписи
        /// </summary>
        public Pen ColorLine = new Pen(Brushes.Red, 0.5);

        /// <summary>
        /// Задает цвет линии
        /// </summary>
        public SolidColorBrush ColorWidths = new SolidColorBrush(Color.FromRgb(80, 80, 230));

        /// <summary>
        /// Задает цвет подложки
        /// </summary>
        public SolidColorBrush ColorSubstrate = new SolidColorBrush(Color.FromRgb(140, 137, 126));

        /// <summary>
        /// Задает цвет экрана
        /// </summary>
        public SolidColorBrush ColorGround = new SolidColorBrush(Colors.Black);

        public Rect DrawRectangle(DrawingContext dc,SolidColorBrush color,Pen pen, double x, double y, double width, double height)
        {
            var figure = new Rect(x, y, width, height);
            dc.DrawRectangle(color,pen,figure);

            return figure;
        }
    }
}