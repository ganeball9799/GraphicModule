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
        public CoupledVerticalInsertGraphic(Geometry geometry)
        {
            _geometry = geometry;
            Canvas.SetLeft(this, 200);
            Canvas.SetTop(this, 200);
        }

        protected override void OnRender(DrawingContext dc)
        {
            double h1 = _geometry[ParameterName.SubstrateHeight].Value ;
            double h2 = _geometry[ParameterName.SubstrateHeight,1].Value;
            double t = _geometry[ParameterName.StripsThickness].Value;
            double W = _geometry[ParameterName.StripWidth].Value;

            var gap = 10;
            base.OnRender(dc);

            var myPen = new Pen(Brushes.Black, 0.1);

            var widthBrush = new SolidColorBrush(Colors.RoyalBlue);
            var substrateHeightBrush1 = new SolidColorBrush(Colors.SaddleBrown);
            var substrateHeightBrush2 = new SolidColorBrush(Colors.Gray);
            var penLine = new Pen(Brushes.Red, 0.5);

            var textWidth = new FormattedText("W", CultureInfo.GetCultureInfo("en-Us"), FlowDirection.LeftToRight,
                new Typeface("verdana"), 8, Brushes.Red);

            
            var substrateRect1 = new Rect(-(gap+t+h2/2), 0, t*2+h2+gap * 2, h1);
            var substrateRect2 = new Rect(-h2/2, -W, h2, W);
            var widthRect1 = new Rect(-(h2/2+t), -W, t, W);
            var widthRect2 = new Rect(h2 / 2 , -W, t, W);
            
            dc.DrawRectangle(substrateHeightBrush1, myPen, substrateRect1);
            dc.DrawRectangle(substrateHeightBrush2, myPen, substrateRect2);
            dc.DrawRectangle(widthBrush, myPen, widthRect1);
            dc.DrawRectangle(widthBrush, myPen, widthRect2);

        }
    }
}