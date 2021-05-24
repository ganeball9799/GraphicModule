using System;
using System.Collections.Generic;
using System.Globalization;
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
            Canvas.SetLeft(this, 200);
            Canvas.SetTop(this, 200);
        }

        protected override void OnRender(DrawingContext dc)
        {
            var z = 0.5;
            double n = _geometry[ParameterName.StripsNumber].Value;
            double h = _geometry[ParameterName.SubstrateHeight].Value * 2;
            double t = _geometry[ParameterName.StripsThickness].Value * 2;
            var gap = 10;
            var groung = 5;

            base.OnRender(dc);

            var myPen = new Pen(Brushes.Black, 0.1);

            var widthBrush = new SolidColorBrush(Colors.RoyalBlue);
            var substrateHeightBrush = new SolidColorBrush(Colors.SaddleBrown);
            var groundBrush = new SolidColorBrush(Colors.Black);
            var penLine = new Pen(Brushes.Red, 0.5);


            var textWidth = new FormattedText("W", CultureInfo.GetCultureInfo("en-Us"), FlowDirection.LeftToRight,
                new Typeface("verdana"), 8, Brushes.Red);
            var textWidth2 = new FormattedText("W2", CultureInfo.GetCultureInfo("en-Us"), FlowDirection.LeftToRight,
                new Typeface("verdana"), 8, Brushes.Red);
            var textWidth3 = new FormattedText("W3", CultureInfo.GetCultureInfo("en-Us"), FlowDirection.LeftToRight,
                new Typeface("verdana"), 8, Brushes.Red);
            var textWidth4 = new FormattedText("W4", CultureInfo.GetCultureInfo("en-Us"), FlowDirection.LeftToRight,
                new Typeface("verdana"), 8, Brushes.Red);
            var textWidth5 = new FormattedText("W5", CultureInfo.GetCultureInfo("en-Us"), FlowDirection.LeftToRight,
                new Typeface("verdana"), 8, Brushes.Red);
            var textWidth6 = new FormattedText("W6", CultureInfo.GetCultureInfo("en-Us"), FlowDirection.LeftToRight,
                new Typeface("verdana"), 8, Brushes.Red);
            var textS1 = new FormattedText("S1", CultureInfo.GetCultureInfo("en-Us"), FlowDirection.LeftToRight,
                new Typeface("verdana"), 8, Brushes.Red);
            var textS2 = new FormattedText("S2", CultureInfo.GetCultureInfo("en-Us"), FlowDirection.LeftToRight,
                new Typeface("verdana"), 8, Brushes.Red);
            var textS3 = new FormattedText("S3", CultureInfo.GetCultureInfo("en-Us"), FlowDirection.LeftToRight,
                new Typeface("verdana"), 8, Brushes.Red);
            var textS4 = new FormattedText("S4", CultureInfo.GetCultureInfo("en-Us"), FlowDirection.LeftToRight,
                new Typeface("verdana"), 8, Brushes.Red);
            var textS5 = new FormattedText("S5", CultureInfo.GetCultureInfo("en-Us"), FlowDirection.LeftToRight,
                new Typeface("verdana"), 8, Brushes.Red);
            var textSubstrateHeight = new FormattedText("h", CultureInfo.GetCultureInfo("en-Us"), FlowDirection.LeftToRight,
                new Typeface("verdana"), 8, Brushes.Red);
            var textThickness = new FormattedText("t", CultureInfo.GetCultureInfo("en-Us"), FlowDirection.LeftToRight,
                new Typeface("verdana"), 8, Brushes.Red);


            if (n == 1)
            {
                double W1 = _geometry[ParameterName.StripWidth, 0].Value;
                var substrateRect = new Rect(-(W1 + gap * 2) / 2, 0, W1 + gap * 2, h);
                var groundRect = new Rect(-(W1 + gap * 2) / 2, h, W1 + gap * 2, groung);
                var widthRect = new Rect(-W1 / 2, -t, W1, t);


                dc.DrawRectangle(widthBrush, myPen, widthRect);
                dc.DrawRectangle(substrateHeightBrush, myPen, substrateRect);
                dc.DrawRectangle(groundBrush, myPen, groundRect);

                //Линии для разделения ширин и зазоров
                dc.DrawLine(penLine, new Point(-W1 / 2, -t), new Point(-W1 / 2, -(t + 20)));
                dc.DrawLine(penLine, new Point(W1 / 2, -t), new Point(W1 / 2, -(t + 20)));
                dc.DrawLine(penLine, new Point(-10 - W1 / 2, -(t + 15)), new Point(W1 / 2 + 10, -(t + 15)));



                //Линии для разделения толщин линии и подложки
                dc.DrawLine(penLine, new Point(-10 - (W1 + gap * 2) / 2, 0), new Point(-(W1 + gap * 2) / 2, 0));
                dc.DrawLine(penLine, new Point(-10 - (W1 + gap * 2) / 2, h), new Point(-(W1 + gap * 2) / 2, h));
                dc.DrawLine(penLine, new Point(-10 - (W1 + gap * 2) / 2, -t), new Point(-W1 / 2, -t));
                dc.DrawLine(penLine, new Point(-5 - (W1 + gap * 2) / 2, -t - 5), new Point(-5 - (W1 + gap * 2) / 2, h + 5));





                //Подписи зазоров и ширин линий
                dc.DrawText(textWidth, new Point(-3, -(t + 25)));

                //Подписи толщин линии и подложки
                dc.DrawText(textSubstrateHeight, new Point(-15 - (W1 + gap * 2) / 2, h / 2 - 4));
                dc.DrawText(textThickness, new Point(-15 - (W1 + gap * 2) / 2, -(t / 2) - 4));

            }
            else if (n == 2)
            {

                double W1 = _geometry[ParameterName.StripWidth, 0].Value;
                double W2 = _geometry[ParameterName.StripWidth, 1].Value;
                double S1 = _geometry[ParameterName.Slot, 0].Value;
                var substrateRect = new Rect(-(gap + W1), 0, W1 + S1 + W2 + gap * 2, h);
                var groundRect = new Rect(-(gap + W1), h, W1 + S1 + W2 + gap * 2, groung);

                var widthRect = new Rect(-W1, -t, W1, t);
                var widthRect2 = new Rect(S1, -t, W2, t);
                dc.DrawRectangle(widthBrush, myPen, widthRect);
                dc.DrawRectangle(widthBrush, myPen, widthRect2);
                dc.DrawRectangle(substrateHeightBrush, myPen, substrateRect);
                dc.DrawRectangle(groundBrush, myPen, groundRect);

                //Линии для разделения ширин и зазоров
                dc.DrawLine(penLine, new Point(-W1, -t), new Point(-W1, -(t + 20)));
                dc.DrawLine(penLine, new Point(0, -t), new Point(0, -(t + 20)));
                dc.DrawLine(penLine, new Point(S1, -t), new Point(S1, -(t + 20)));
                dc.DrawLine(penLine, new Point(S1 + W2, -t), new Point(S1 + W2, -(t + 20)));

                dc.DrawLine(penLine, new Point(-(gap + W1), -(t + 15)), new Point(S1 + W2 + 10, -(t + 15)));



                //Линии для разделения толщин линии и подложки
                dc.DrawLine(penLine, new Point(-(W1 + 25), 0), new Point(-(gap + W1), 0));
                dc.DrawLine(penLine, new Point(-(W1 + 25), h), new Point(-(gap + W1), h));
                dc.DrawLine(penLine, new Point(-(W1 + 25), -t), new Point(-W1, -t));
                dc.DrawLine(penLine, new Point(-(W1 + 20), -t - 5), new Point(-(W1 + 20), h + 5));


                //Подписи зазоров и ширин линий
                dc.DrawText(textWidth, new Point(-(W1 / 2 + 2), -(t + 25)));
                dc.DrawText(textWidth2, new Point(S1 + W1 / 2 - 2, -(t + 25)));
                dc.DrawText(textS1, new Point(S1 / 2 - 2, -(t + 25)));



                //Подписи толщин линии и подложки
                dc.DrawText(textSubstrateHeight, new Point(-(W1 + 35), h / 2 - 3));
                dc.DrawText(textThickness, new Point(-(W1 + 35), -(t / 2) - 3));
            }
            else if (n == 3)
            {
                double W1 = _geometry[ParameterName.StripWidth, 0].Value * 0.9;
                double W2 = _geometry[ParameterName.StripWidth, 1].Value * 0.9;
                double W3 = _geometry[ParameterName.StripWidth, 2].Value * 0.9;
                double S1 = _geometry[ParameterName.Slot, 0].Value * 0.9;
                double S2 = _geometry[ParameterName.Slot, 1].Value * 0.9;
                var substrateRect = new Rect(-(W1 + S1 + gap), 0, W1 + S1 + W2 + W3 + S2 + gap * 2, h);
                var groundRect = new Rect(-(W1 + S1 + gap), h, W1 + S1 + W2 + W3 + S2 + gap * 2, groung);

                var widthRect = new Rect(-(W1 + S1), -t, W1, t);
                var widthRect2 = new Rect(0, -t, W2, t);
                var widthRect3 = new Rect(W2 + S2, -t, W3, t);
                dc.DrawRectangle(widthBrush, myPen, widthRect);
                dc.DrawRectangle(widthBrush, myPen, widthRect2);
                dc.DrawRectangle(widthBrush, myPen, widthRect3);
                dc.DrawRectangle(substrateHeightBrush, myPen, substrateRect);
                dc.DrawRectangle(groundBrush, myPen, groundRect);


                //Линии для разделения ширин и зазоров
                dc.DrawLine(penLine, new Point(-(W1 + S1 - z), -t), new Point(-(W1 + S1 - z), -(t + 20)));
                dc.DrawLine(penLine, new Point(-S1 - z, -t), new Point(-S1 - z, -(t + 20)));
                dc.DrawLine(penLine, new Point(z, -t), new Point(z, -(t + 20)));
                dc.DrawLine(penLine, new Point(W2 - z, -t), new Point(W2 - z, -(t + 20)));
                dc.DrawLine(penLine, new Point(S2 + W2 + z, -t), new Point(S2 + W2 + z, -(t + 20)));
                dc.DrawLine(penLine, new Point(S2 + W2 + W3 - z, -t), new Point(S2 + W2 + W3 - z, -(t + 20)));

                dc.DrawLine(penLine, new Point(-(gap + W1 + S1), -(t + 15)), new Point(S2 + W2 + W3 + gap, -(t + 15)));


                //Линии для разделения толщин линии и подложки
                dc.DrawLine(penLine, new Point(-(W1 + S1 + gap + 25), 0), new Point(-(W1 + S1 + gap), 0));
                dc.DrawLine(penLine, new Point(-(W1 + S1 + gap + 25), h), new Point(-(W1 + S1 + gap), h));
                dc.DrawLine(penLine, new Point(-(W1 + S1 + gap + 25), -t), new Point(-(W1 + S1), -t));
                dc.DrawLine(penLine, new Point(-(W1 + S1 + gap + 20), -t - 5), new Point(-(W1 + S1 + gap + 20), h + 5));


                //Подписи зазоров и ширин линий
                dc.DrawText(textWidth, new Point(-(W1 / 2 + S1 + 3), -(t + 25)));
                dc.DrawText(textWidth2, new Point(W2 / 2 - 3, -(t + 25)));
                dc.DrawText(textWidth3, new Point(W2 + S2 + W3 / 2 - 3, -(t + 25)));
                dc.DrawText(textS1, new Point(-(S1 / 2 + 3), -(t + 25)));
                dc.DrawText(textS2, new Point(W2 + S2 / 2 - 3, -(t + 25)));



                //Подписи толщин линии и подложки
                dc.DrawText(textSubstrateHeight, new Point(-(W1+S1+gap + 35), h / 2 - 3));
                dc.DrawText(textThickness, new Point(-(W1 + S1 + gap + 35), -(t / 2) - 3));

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
