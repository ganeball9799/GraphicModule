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
    public class SingleCoplanarLine : Geometry
    {
        public override Parameter this[ParameterName paramName, int number = 0]
        {
            get => GetParam(paramName, number);
            set
            {
                var param = GetParam(paramName, number);
                param.Value = value.Value;
            }
        }
        public SingleCoplanarLine()
        {
            Structure = LinesStructure.SingleCoplanar;
            _parameters = new List<Parameter>
            {
                new Parameter(ParameterName.StripWidth,70,1,30),
                new Parameter(ParameterName.Slot,70,1,20),
                new Parameter(ParameterName.Slot,70,10,20,1),
                new Parameter(ParameterName.StripsThickness,70,1,20),
                new Parameter(ParameterName.SubstrateHeight,70,1,20),
            };
        }

        public override List<Parameter> ParametersLine()
        {
            var Params = new List<Parameter>
            {
                GetParam(ParameterName.Slot),
                GetParam(ParameterName.Slot, 1),
                GetParam(ParameterName.StripsThickness),
                GetParam(ParameterName.StripWidth),
                GetParam(ParameterName.SubstrateHeight),
            };
            return Params;
        }
    }
}
