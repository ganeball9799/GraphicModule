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

        public override Parameter this[ParameterName paramName,int number = 0]
        {
            get => GetParam(paramName, number);
            set
            {
                var param = GetParam(paramName, number);
                param.Value = value.Value;
            }
        }

        public CoupledVerticalInsertLine()
        {
            InitComponent();
        }
        private void InitComponent()
        {
            Structure = LinesStructure.CoupledVerticalInsert;
            _parameters = new List<Parameter>
            {
                new Parameter(ParameterName.StripWidth, 30,70),
                new Parameter(ParameterName.Slot,20,40),
                new Parameter(ParameterName.Slot,20,40),
                new Parameter(ParameterName.StripsNumber,1,6),
                new Parameter(ParameterName.StripsThickness,10,70),
                new Parameter(ParameterName.SubstrateHeight,20,70)
            };
            
        }

        public override List<Parameter> ParametersLine()
        {
            var parameters = new List<Parameter>
            {
                GetParam(ParameterName.StripsNumber),
                GetParam(ParameterName.SubstrateHeight),
                GetParam(ParameterName.StripsThickness),
                GetParam(ParameterName.StripWidth),
                GetParam(ParameterName.Slot),
                GetParam(ParameterName.Slot,1)
            };
            return parameters;
        }

        private void ResetParams()
        {
            var parameters = new List<Parameter>
            {
                GetParam(ParameterName.StripsThickness),
                GetParam(ParameterName.StripsThickness),
                GetParam(ParameterName.SubstrateHeight),
                GetParam(ParameterName.StripWidth),
                GetParam(ParameterName.Slot),
                GetParam(ParameterName.Slot, 1)
                
            };
            _parameters = parameters;
        }
        public override void Doit()
        {
            var stripsThickness = GetParam(ParameterName.StripsThickness).Value;
            var substrateHeight = GetParam(ParameterName.SubstrateHeight).Value;
            var stripWidth = GetParam(ParameterName.StripWidth).Value;
            var slots = new double[] { GetParam(ParameterName.Slot).Value, GetParam(ParameterName.Slot, 1).Value };
            ResetParams();
        }
    }
}
