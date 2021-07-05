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
    public class CoaxialLine : Geometry
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
        public CoaxialLine()
        {
            Structure = LinesStructure.Coaxial;
            _parameters = new List<Parameter>
            {
                new Parameter(ParameterName.DiameterDielectric,70,1,10),
                new Parameter(ParameterName.DiameterLine,70,1,8)
                
            };
        }

        /// <summary>
        /// Получение физических параметров.
        /// </summary>
        public override List<Parameter> ParametersLine()
        {
            var Params = new List<Parameter>
            {
                GetParam(ParameterName.DiameterLine),
                GetParam(ParameterName.DiameterDielectric)
            };
            return Params;
        }
    }
}