using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using GraphicModule.Models;

namespace GraphicModuleUI.ViewModels.Graphic
{
    public class SingleCoplanarGraphic : StructureImage
    {
        private List<Parameter> _parameters;

        private double h;

        private double t;

        private double S1;

        private double S2;

        private double W1;

        public SingleCoplanarGraphic(List<Parameter> parameters)
        {
            _parameters = parameters;
            Canvas.SetLeft(this, 100);
            Canvas.SetTop(this, 200);
        }

        protected override void OnRender(DrawingContext dc)
        {
            S1 = _parameters[0].Value*3;
            S2 = _parameters[1].Value * 3;
            t = _parameters[2].Value * 3;
            W1 = _parameters[3].Value * 3;
            h = _parameters[4].Value * 3;


            base.OnRender(dc);

            var wSolidBrush = new SolidColorBrush(Colors.Blue);
            var hSolidBrsh = new SolidColorBrush(Colors.LimeGreen);



            Pen myPen = new Pen(Brushes.Black, 0);
            var hRect = new Rect(0, h, W1+S1+S2, h);
            var wRect = new Rect(S1,0,W1,t);


            dc.DrawRectangle(wSolidBrush, myPen, wRect);
            dc.DrawRectangle(hSolidBrsh, myPen, hRect);

        }
    }
}
