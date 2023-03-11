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
    public class WireAboveMSL : Geometry
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
        public WireAboveMSL()
        {
            Structure = LinesStructure.WireAboveMSL;
            _parameters = new List<Parameter>
            {
                new Parameter(ParameterName.Height,10,0,2),
                new Parameter(ParameterName.StripWidth,70,1,30),
                new Parameter(ParameterName.StripsThickness,70,1,30),
                new Parameter(ParameterName.SubstrateHeight,70,1,30),
                new Parameter(ParameterName.DiameterLine,70,10,30)
            };
        }

        public override List<Parameter> ParametersLine()
        {
            var Params = new List<Parameter>
            {
                GetParam(ParameterName.Height),
                GetParam(ParameterName.StripWidth),
                GetParam(ParameterName.StripsThickness),
                GetParam(ParameterName.SubstrateHeight),
                GetParam(ParameterName.DiameterLine)
            };
            return Params;
        }
    }
}
