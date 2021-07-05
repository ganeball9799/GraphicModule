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
        /// <summary>
        /// Индексатор параметров линии.
        /// </summary>
        public override Parameter this[ParameterName paramName, int number = 0]
        {
            get => GetParam(paramName, number);
            set
            {
                var param = GetParam(paramName, number);
                param.Value = value.Value;
            }
        }

        /// <summary>
        /// Конструктор класса.
        /// </summary>
        public SingleCoplanarLine()
        {
            Structure = LinesStructure.SingleCoplanar;
            _parameters = new List<Parameter>
            {
                new Parameter(ParameterName.Slot,70,1,20),
                new Parameter(ParameterName.Slot,70,1,20,1),
                new Parameter(ParameterName.StripWidth,70,1,30),
                new Parameter(ParameterName.StripsThickness,70,1,30),
                new Parameter(ParameterName.SubstrateHeight,70,1,30),
            };
        }

        /// <summary>
        /// Получение  параметров.
        /// </summary>
        public override List<Parameter> ParametersLine()
        {
            var Params = new List<Parameter>
            {
                GetParam(ParameterName.Slot),
                GetParam(ParameterName.Slot,1),
                GetParam(ParameterName.StripWidth),
                GetParam(ParameterName.StripsThickness),
                GetParam(ParameterName.SubstrateHeight),
            };
            return Params;
        }
    }
}
