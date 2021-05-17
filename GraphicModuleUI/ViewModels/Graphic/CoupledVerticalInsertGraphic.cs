namespace GraphicModuleUI.ViewModels.Graphic
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class CoupledVerticalInsertGraphic : StructureImage
    {
        public CoupledVerticalInsertGraphic()
        {
            Canvas.SetLeft(this, 80);
            Canvas.SetTop(this, 40);
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            var mySolidColorBrush = new SolidColorBrush();
            mySolidColorBrush.Color = Colors.Pink;
            var myPen = new Pen(Brushes.Black, 10);
            var myRect = new Rect(10, 10, 50, 50);
            dc.DrawRectangle(mySolidColorBrush, myPen, myRect);
        }
    }
}