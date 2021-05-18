namespace GraphicModuleUI.ViewModels
{
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Media;
    using GraphicModule.Models;

    public class StructureImage : UIElement
    {

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            var mySolidColorBrush = new SolidColorBrush();
            mySolidColorBrush.Color = Colors.Pink;
            var myPen = new Pen(Brushes.Black, 10);
            var myRect = new Rect(3, 10, 50, 50);
        }
    }
}