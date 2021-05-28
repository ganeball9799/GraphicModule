using System.Globalization;
using GraphicModule.Models;

namespace GraphicModuleUI.ViewModels.Graphic
{
    using Geometry = GraphicModule.Models.Geometry;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class CoupledVerticalInsertGraphic : StructureImage
    {
        private Geometry _geometry;

        private double stripsWidth;
        private double stripsThicknees;
        private double substrateHeight1;
        private double substrateHeight2;


        private const double gap = 10;
        private const double ground = 5;
        private const double textSize = 5;

        private double zoomt;
        private double zoomh1;
        private double zoomh2;
        private double zoomw;

        public CoupledVerticalInsertGraphic(Geometry geometry)
        {
            _geometry = geometry;

            stripsWidth = _geometry[ParameterName.StripWidth].Value;
            stripsThicknees = _geometry[ParameterName.StripsThickness].Value;
            substrateHeight1 = _geometry[ParameterName.SubstrateHeight, 0].Value;
            substrateHeight2 = _geometry[ParameterName.SubstrateHeight, 1].Value;

            Canvas.SetLeft(this, 120);
            Canvas.SetTop(this, 120);
        }

        protected override void OnRender(DrawingContext dc)
        {
            zoomw = stripsWidth / substrateHeight1;
            zoomh1 = substrateHeight1 / stripsWidth;
            zoomh2 = (substrateHeight2 / stripsThicknees)/1.5;
            zoomt = (stripsThicknees / substrateHeight2)/1.5;

             

            double h1 =20 * zoomh1+substrateHeight1/ 5;
            double h2 = 20 * zoomh2+substrateHeight2/ 5;
            double t = 20 * zoomt+stripsThicknees/ 5;
            double W = 20 * zoomw+stripsWidth/ 5;
            var z = 0.5;

            base.OnRender(dc);

            var myPen = new Pen(Brushes.Black, 0.1);

            var widthBrush = new SolidColorBrush(Color.FromRgb(80, 80, 230));
            var substrateHeightBrush1 = new SolidColorBrush(Color.FromRgb(140, 137, 126));
            var substrateHeightBrush2 = new SolidColorBrush(Color.FromRgb(204, 173, 96));
            var penLine = new Pen(Brushes.Red, 0.5);
            var groundBrush = new SolidColorBrush(Colors.Black);

            var textWidth = new FormattedText("W", CultureInfo.GetCultureInfo("en-Us"), FlowDirection.LeftToRight,
                new Typeface("verdana"), textSize, Brushes.Red);
            var textSubstrateHeight = new FormattedText("h1", CultureInfo.GetCultureInfo("en-Us"), FlowDirection.LeftToRight,
                new Typeface("verdana"), textSize, Brushes.Red);
            var textSubstrateHeight2 = new FormattedText("h2", CultureInfo.GetCultureInfo("en-Us"), FlowDirection.LeftToRight,
                new Typeface("verdana"), textSize, Brushes.Red);
            var textThickness = new FormattedText("t", CultureInfo.GetCultureInfo("en-Us"), FlowDirection.LeftToRight,
                new Typeface("verdana"), textSize, Brushes.Red);


            var substrateRect1 = new Rect(-(gap + t + h2 / 2), 0, t * 2 + h2 + gap * 2, h1);
            var substrateRect2 = new Rect(-h2 / 2, -W, h2, W);
            var widthRect1 = new Rect(-(h2 / 2 + t), -W, t, W);
            var widthRect2 = new Rect(h2 / 2, -W, t, W);
            var groundRect = new Rect(-(gap + t + h2 / 2), h1, t * 2 + h2 + gap * 2, ground);

            dc.DrawRectangle(substrateHeightBrush1, myPen, substrateRect1);
            dc.DrawRectangle(substrateHeightBrush2, myPen, substrateRect2);
            dc.DrawRectangle(groundBrush, myPen, groundRect);
            dc.DrawRectangle(widthBrush, myPen, widthRect1);
            dc.DrawRectangle(widthBrush, myPen, widthRect2);

            //Линии разделения для толщины линии и диэлектрика
            dc.DrawLine(penLine, new Point(-(h2 / 2 + t - z), -W), new Point(-(h2 / 2 + t - z), -(W + 20)));
            dc.DrawLine(penLine, new Point(-(h2 / 2), -W), new Point(-(h2 / 2), -(W + 20)));
            dc.DrawLine(penLine, new Point((h2 / 2), -W), new Point((h2 / 2), -(W + 20)));
            dc.DrawLine(penLine, new Point((h2 / 2 + t - z), -W), new Point((h2 / 2 + t - z), -(W + 20)));

            dc.DrawLine(penLine, new Point(-(h2 / 2 + t + 5), -(W + 15)), new Point((h2 / 2 + t + 5), -(W + 15)));

            //Подписи для толщины линии и диэлектрика
            dc.DrawText(textSubstrateHeight2, new Point(-3, -(W + 25)));
            dc.DrawText(textThickness, new Point(-(h2 / 2 + t / 2 + 2), -(W + 25)));
            dc.DrawText(textThickness, new Point((h2 / 2 + t / 2 - 2), -(W + 25)));

            //Линии для разделения толщины подложки и ширины линии
            dc.DrawLine(penLine, new Point(-(gap + t + h2 / 2 + 25), -W), new Point(-(t + h2 / 2), -W));
            dc.DrawLine(penLine, new Point(-(gap + t + h2 / 2 + 25), 0), new Point(-(gap + t + h2 / 2), 0));
            dc.DrawLine(penLine, new Point(-(gap + t + h2 / 2 + 25), h1), new Point(-(gap + t + h2 / 2), h1));

            dc.DrawLine(penLine, new Point(-(gap + t + h2 / 2 + 20), -W - 5), new Point(-(gap + t + h2 / 2 + 20), h1 + 5));


            //Подписи для ширины линии и толщины подложки
            dc.DrawText(textWidth, new Point(-(gap + t + h2 / 2 + 35), -W / 2 - 3));
            dc.DrawText(textSubstrateHeight, new Point(-(gap + t + h2 / 2 + 35), h1 / 2 - 3));
        }
    }
}