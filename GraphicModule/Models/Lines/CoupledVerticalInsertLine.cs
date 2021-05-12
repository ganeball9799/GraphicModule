using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using GraphicModule.Models.Enums;

namespace GraphicModule.Models
{
    public class CoupledVerticalInsertLine : Geometry
    {
        public LinesStructure Type = LinesStructure.CoupledVerticalInsert;

        //TODO: Убрать объявление - оно есть в базовом классе
        

        public CoupledVerticalInsertLine()
        {
            InitComponent();
        }
        private void InitComponent()
        {
            Structure = LinesStructure.CoupledVerticalInsert;
            Parameters = new List<Parameter>
            {
                new Parameter(ParameterName.StripsWidth, 30),
                new Parameter(ParameterName.Slot,20),
                new Parameter(ParameterName.Slot,20),
                new Parameter(ParameterName.StripsNumber,1),
                new Parameter(ParameterName.StripsThickness,10),
                new Parameter(ParameterName.SubstrateHeight,20)
            };
        }

    }
}
