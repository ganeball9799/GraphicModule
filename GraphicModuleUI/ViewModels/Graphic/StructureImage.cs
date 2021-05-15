using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using GalaSoft.MvvmLight;
using GraphicModule.Models;
using GraphicModule.Models.Enums;

namespace GraphicModuleUI.ViewModels
{

    public class StructureImage: FrameworkElement
    {

        private List<Parameter> _parameters;


        public StructureImage(/*List<Parameter> parameter*/)
        {
            //_parameters = parameter;
        }
        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            mySolidColorBrush.Color = Colors.Pink;
            Pen myPen = new Pen(Brushes.Black, 10);
            Rect myRect = new Rect(3, 10, 50, 50);
            dc.DrawRectangle(mySolidColorBrush, myPen, myRect);
        }
    }
}
