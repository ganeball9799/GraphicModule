using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using GraphicModule.Models;
using Geometry = GraphicModule.Models.Geometry;

namespace GraphicModuleUI.ViewModels.Graphic
{
    public class SingleCoplanarGraphic : StructureImage
    {
        private Geometry _geometry;

        /// <summary>
        /// Лист с зазорами линий
        /// </summary>
        private List<double> _slots = new List<double>();

        /// <summary>
        /// Ширина линии
        /// </summary>
        private double _stripWidth;

        /// <summary>
        /// Толщина линии
        /// </summary>
        private double _stripsThicknees;

        /// <summary>
        /// Толщина подложки
        /// </summary>
        private double _substrateHeight;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public SingleCoplanarGraphic(Geometry geometry)
        {
            _geometry = geometry;

            _slots.Add(_geometry[ParameterName.Slot].Value);
            _slots.Add(_geometry[ParameterName.Slot, 1].Value);
            _stripWidth = _geometry[ParameterName.StripWidth].Value;
            _stripsThicknees = _geometry[ParameterName.StripsThickness].Value;
            _substrateHeight = _geometry[ParameterName.SubstrateHeight].Value;

            Canvas.SetLeft(this, 120);
            Canvas.SetTop(this, 120);
        }
        
        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            var screenWidth = 8;
            var zoomt = _stripsThicknees / ((_substrateHeight + _stripWidth + _slots[0] + _slots[1]) / 4);
            var zoomh = _substrateHeight / ((_stripWidth + _stripsThicknees) / 2);
            var zooms1 = _slots[0] / ((_slots[1] + _substrateHeight + _stripWidth + _stripsThicknees) / 4);
            var zooms2 = _slots[1] / ((_slots[0] + _substrateHeight + _stripWidth + _stripsThicknees) / 4);
            var zoomw = _stripWidth / ((_substrateHeight + _stripsThicknees + _slots[0] + _slots[1]) / 4);

            var S1 = 40 * Zoomin(zooms1);
            var S2 = 40 * Zoomin(zooms2);
            var W1 = 60 * Zoomin(zoomw);
            var t = 40 * Zoomin(zoomt);
            var h = 40 * Zoomin(zoomh);
            
            DrawRectangle(dc, ColorWidths, ColorPen, -W1 / 2, -t, W1, t);
            DrawRectangle(dc, ColorSubstrate, ColorPen, -(screenWidth + S1 + W1 / 2), 0, W1 + S1 + S2 + screenWidth * 2, h);
            DrawRectangle(dc, ColorGround, ColorPen, -(screenWidth + S1 + W1 / 2), -t, screenWidth, t);
            DrawRectangle(dc, ColorGround, ColorPen, W1 / 2 + S2, -t, screenWidth, t);
            
            //Линии разделения для слотов и ширины
            dc.DrawLine(ColorLine, new Point(-(S1 + W1 / 2), -t), new Point(-(S1 + W1 / 2), -(t + 15)));
            dc.DrawLine(ColorLine, new Point(-W1 / 2, -t), new Point(-W1 / 2, -(t + 15)));
            dc.DrawLine(ColorLine, new Point(W1 / 2, -t), new Point(W1 / 2, -(t + 15)));
            dc.DrawLine(ColorLine, new Point(W1 / 2 + S2, -t), new Point(W1 / 2 + S2, -(t + 15)));
            dc.DrawLine(ColorLine, new Point(-(S1 + W1 / 2 + 5), -(t + 10)), new Point(W1 / 2 + S2 + 5, -(t + 10)));

            //Подписи ширины и зазоров
            dc.DrawText(GetDrawingText("W"), new Point(-5, -(t + 30)));
            dc.DrawText(GetDrawingText("S1"), new Point(-(S1 / 2 + W1 / 2 + 5), -(t + 30)));
            dc.DrawText(GetDrawingText("S2"), new Point(W1 / 2 + S2 / 2 - 5, -(t + 30)));

            //Линии для разделения толщины линии и подложки
            dc.DrawLine(ColorLine, new Point(-(screenWidth + S1 + W1 / 2), -t), new Point(-(10 + screenWidth + S1 + W1 / 2), -t));
            dc.DrawLine(ColorLine, new Point(-(screenWidth + S1 + W1 / 2), 0), new Point(-(10 + screenWidth + S1 + W1 / 2), 0));
            dc.DrawLine(ColorLine, new Point(-(screenWidth + S1 + W1 / 2), h), new Point(-(10 + screenWidth + S1 + W1 / 2), h));
            dc.DrawLine(ColorLine, new Point(-(5 + screenWidth + S1 + W1 / 2), -5 - t), new Point(-(5 + screenWidth + S1 + W1 / 2), h + 5));

            //Подписи линий разделения толщины линии и подложки
            dc.DrawText(GetDrawingText("h"), new Point(-(20 + screenWidth + S1 + W1 / 2), h / 2 - 10));
            dc.DrawText(GetDrawingText("t"), new Point(-(20 + screenWidth + S1 + W1 / 2), -t / 2 - 10));
        }
    }
}
