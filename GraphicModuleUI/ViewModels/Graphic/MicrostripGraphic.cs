using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using GraphicModule.Models;
using Geometry = GraphicModule.Models.Geometry;

namespace GraphicModuleUI.ViewModels.Graphic
{
    public class MicrostripGraphic : StructureImage
    {
        private Geometry _geometry;
        public MicrostripGraphic(Geometry geometry)
        {
            _geometry = geometry;
            Canvas.SetLeft(this, 100);
            Canvas.SetTop(this, 200);
        }

        protected override void OnRender(DrawingContext dc)
        {
            double n = _geometry[ParameterName.StripsNumber].Value;
            double h = _geometry[ParameterName.SubstrateHeight].Value;
            double t = _geometry[ParameterName.StripsThickness].Value;
            var gap = 10;
            var groung = 5;

            base.OnRender(dc);

            var myPen = new Pen(Brushes.Black, 0.1);

            var widthBrush = new SolidColorBrush(Colors.Blue);
            var substrateHeightBrush = new SolidColorBrush(Colors.LimeGreen);
            var groundBrush = new SolidColorBrush(Colors.Black);
            //var text = new FormattedText(S, CultureInfo.GetCultureInfo("en-Us"), FlowDirection.LeftToRight,
            //    new Typeface("verdana"), 10, Brushes.Red);
            if (n == 1)
            {
                double W1 = _geometry[ParameterName.StripWidth, 0].Value;
                var substrateRect = new Rect(70, 0, W1 + gap * 2, h);
                var groundRect = new Rect(70, h, W1 + gap * 2, groung);

                var widthRect = new Rect(gap+50, -t, W1, t);
                dc.DrawRectangle(widthBrush, myPen, widthRect);
                dc.DrawRectangle(substrateHeightBrush, myPen, substrateRect);
                dc.DrawRectangle(groundBrush, myPen, groundRect);
            }
            else if (n == 2)
            {
                double W1 = _geometry[ParameterName.StripWidth, 0].Value;
                double W2 = _geometry[ParameterName.StripWidth, 1].Value;
                double S1 = _geometry[ParameterName.Slot, 0].Value;
                var substrateRect = new Rect(40, 0, W1 + S1 + W2 + gap * 2, h);
                var groundRect = new Rect(40, h, W1 + S1 + W2 + gap * 2, groung);

                var widthRect = new Rect(gap + 40, -t, W1, t);
                var widthRect2 = new Rect(gap + 40 + W1 + S1, -t, W2, t);
                dc.DrawRectangle(widthBrush, myPen, widthRect);
                dc.DrawRectangle(widthBrush, myPen, widthRect2);
                dc.DrawRectangle(substrateHeightBrush, myPen, substrateRect);
                dc.DrawRectangle(groundBrush, myPen, groundRect);
            }
            else if (n == 3)
            {
                double W1 = _geometry[ParameterName.StripWidth, 0].Value*0.9;
                double W2 = _geometry[ParameterName.StripWidth, 1].Value * 0.9;
                double W3 = _geometry[ParameterName.StripWidth, 2].Value * 0.9;
                double S1 = _geometry[ParameterName.Slot, 0].Value * 0.9;
                double S2 = _geometry[ParameterName.Slot, 1].Value * 0.9;
                var substrateRect = new Rect(10, 0, W1 + S1 + W2 + W3 + S2 + gap * 2, h);
                var groundRect = new Rect(10, h, W1 + S1 + W2 + W3 + S2 + gap * 2, groung);

                var widthRect = new Rect(gap + 10, -t, W1, t);
                var widthRect2 = new Rect(gap + 10 + W1 + S1, -t, W2, t);
                var widthRect3 = new Rect(gap + 10 + W1 + S1 + W2 + S2, -t, W3, t);
                dc.DrawRectangle(widthBrush, myPen, widthRect);
                dc.DrawRectangle(widthBrush, myPen, widthRect2);
                dc.DrawRectangle(widthBrush, myPen, widthRect3);
                dc.DrawRectangle(substrateHeightBrush, myPen, substrateRect);
                dc.DrawRectangle(groundBrush, myPen, groundRect);

            }
            else if (n == 4)
            {
                double W1 = _geometry[ParameterName.StripWidth, 0].Value * 0.8;
                double W2 = _geometry[ParameterName.StripWidth, 1].Value * 0.8;
                double W3 = _geometry[ParameterName.StripWidth, 2].Value * 0.8;
                double W4 = _geometry[ParameterName.StripWidth, 3].Value * 0.8;
                double S1 = _geometry[ParameterName.Slot, 0].Value * 0.8;
                double S2 = _geometry[ParameterName.Slot, 1].Value * 0.8;
                double S3 = _geometry[ParameterName.Slot, 2].Value * 0.8;
                var substrateRect = new Rect(-10, 0, W1 + S1 + W2 + W3 + W4 + S2 + S3 + gap * 2, h);
                var groundRect = new Rect(-10, h, W1 + S1 + W2 + W3 + W4 + S2 + S3 + gap * 2, groung);

                var widthRect = new Rect(gap - 10, -t, W1, t);
                var widthRect2 = new Rect(gap - 10 + W1 + S1, -t, W2, t);
                var widthRect3 = new Rect(gap - 10 + W1 + S1 + W2 + S2, -t, W3, t);
                var widthRect4 = new Rect(gap - 10 + W1 + S1 + W2 + W3 + S2 + S3, -t, W4, t);

                dc.DrawRectangle(widthBrush, myPen, widthRect);
                dc.DrawRectangle(widthBrush, myPen, widthRect2);
                dc.DrawRectangle(widthBrush, myPen, widthRect3);
                dc.DrawRectangle(widthBrush, myPen, widthRect4);
                dc.DrawRectangle(substrateHeightBrush, myPen, substrateRect);
                dc.DrawRectangle(groundBrush, myPen, groundRect);

            }
            else if (n == 5)
            {
                double W1 = _geometry[ParameterName.StripWidth, 0].Value * 0.8;
                double W2 = _geometry[ParameterName.StripWidth, 1].Value * 0.8;
                double W3 = _geometry[ParameterName.StripWidth, 2].Value * 0.8;
                double W4 = _geometry[ParameterName.StripWidth, 3].Value * 0.8;
                double W5 = _geometry[ParameterName.StripWidth, 4].Value * 0.8;
                double S1 = _geometry[ParameterName.Slot, 0].Value * 0.8;
                double S2 = _geometry[ParameterName.Slot, 1].Value * 0.8;
                double S3 = _geometry[ParameterName.Slot, 2].Value * 0.8;
                double S4 = _geometry[ParameterName.Slot, 3].Value * 0.8;

                var substrateRect = new Rect(-40, 0, W1 + S1 + W2 + W3 + W4 + W5 + S2 + S3 + S4 + gap * 2, h);
                var groundRect = new Rect(-40, h, W1 + S1 + W2 + W3 + W4 + W5 + S2 + S3 + S4 + gap * 2, groung);

                var widthRect = new Rect(gap - 40, -t, W1, t);
                var widthRect2 = new Rect(gap - 40 + W1 + S1, -t, W2, t);
                var widthRect3 = new Rect(gap - 40 + W1 + S1 + W2 + S2, -t, W3, t);
                var widthRect4 = new Rect(gap - 40 + W1 + S1 + W2 + W3 + S2 + S3, -t, W4, t);
                var widthRect5 = new Rect(gap - 40 + W1 + S1 + W2 + W3 + W4 + S2 + S3 + S4, -t, W5, t);

                dc.DrawRectangle(widthBrush, myPen, widthRect);
                dc.DrawRectangle(widthBrush, myPen, widthRect2);
                dc.DrawRectangle(widthBrush, myPen, widthRect3);
                dc.DrawRectangle(widthBrush, myPen, widthRect4);
                dc.DrawRectangle(widthBrush, myPen, widthRect5);
                dc.DrawRectangle(substrateHeightBrush, myPen, substrateRect);
                dc.DrawRectangle(groundBrush, myPen, groundRect);

            }
            else if (n == 6)
            {
                double W1 = _geometry[ParameterName.StripWidth, 0].Value * 0.6;
                double W2 = _geometry[ParameterName.StripWidth, 1].Value * 0.6;
                double W3 = _geometry[ParameterName.StripWidth, 2].Value * 0.6;
                double W4 = _geometry[ParameterName.StripWidth, 3].Value * 0.6;
                double W5 = _geometry[ParameterName.StripWidth, 4].Value * 0.6;
                double W6 = _geometry[ParameterName.StripWidth, 5].Value * 0.6;
                double S1 = _geometry[ParameterName.Slot, 0].Value * 0.6;
                double S2 = _geometry[ParameterName.Slot, 1].Value * 0.6;
                double S3 = _geometry[ParameterName.Slot, 2].Value * 0.6;
                double S4 = _geometry[ParameterName.Slot, 3].Value * 0.6;
                double S5 = _geometry[ParameterName.Slot, 4].Value * 0.6;

                var substrateRect = new Rect(-70, 0, W1 + S1 + W2 + W3 + W4 + W5 + W6 + S2 + S3 + S4 + S5 + gap * 2, h);
                var groundRect = new Rect(-70, h, W1 + S1 + W2 + W3 + W4 + W5 + W6 + S2 + S3 + S4 + S5 + gap * 2, groung);
                var widthRect = new Rect(gap - 70, -t, W1, t);
                var widthRect2 = new Rect(gap - 70 + W1 + S1, -t, W2, t);
                var widthRect3 = new Rect(gap - 70 + W1 + S1 + W2 + S2, -t, W3, t);
                var widthRect4 = new Rect(gap - 70 + W1 + S1 + W2 + W3 + S2 + S3, -t, W4, t);
                var widthRect5 = new Rect(gap - 70 + W1 + S1 + W2 + W3 + W4 + S2 + S3 + S4, -t, W5, t);
                var widthRect6 = new Rect(gap - 70 + W1 + S1 + W2 + W3 + W4 + W5 + S2 + S3 + S4 + S5, -t, W6, t);

                dc.DrawRectangle(widthBrush, myPen, widthRect);
                dc.DrawRectangle(widthBrush, myPen, widthRect2);
                dc.DrawRectangle(widthBrush, myPen, widthRect3);
                dc.DrawRectangle(widthBrush, myPen, widthRect4);
                dc.DrawRectangle(widthBrush, myPen, widthRect5);
                dc.DrawRectangle(widthBrush, myPen, widthRect6);

                dc.DrawRectangle(substrateHeightBrush, myPen, substrateRect);
                dc.DrawRectangle(groundBrush, myPen, groundRect);

            }


            //dc.DrawText(text, new Point(50, 50));
        }
    }
}
