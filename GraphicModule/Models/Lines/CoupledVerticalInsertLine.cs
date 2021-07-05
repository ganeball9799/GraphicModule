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
        /// <summary>
        /// Индексатор параметров линии.
        /// </summary>
        public override Parameter this[ParameterName paramName,int number = 0]
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
        public CoupledVerticalInsertLine()
        {
            Structure = LinesStructure.CoupledVerticalInsert;
            _parameters = new List<Parameter>
            {
                new Parameter(ParameterName.StripWidth, 70,1,20),
                new Parameter(ParameterName.StripsThickness,70,1,10),
                new Parameter(ParameterName.SubstrateHeight,70,1,20),
                new Parameter(ParameterName.SubstrateHeight,70,1,20,1)
            };
        }

        /// <summary>
        /// Получение параметров.
        /// </summary>
        /// <returns>Список физических параметров структуры.</returns>
        public override List<Parameter> ParametersLine()
        {
            var parameters = new List<Parameter>
            {
                GetParam(ParameterName.StripsThickness),
                GetParam(ParameterName.StripWidth),
                GetParam(ParameterName.SubstrateHeight),
                GetParam(ParameterName.SubstrateHeight,1),
            };
            return parameters;
        }
    }
}
