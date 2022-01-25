﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GraphicModule.Models.Enums;

namespace GraphicModule.Models.Lines
{
    public class RndSqr : Geometry
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

        public RndSqr()
        {
            Structure = LinesStructure.RndSql;
            _parameters = new List<Parameter>
            {
                new Parameter(ParameterName.DiameterLine,70,0.1,2),
                new Parameter(ParameterName.DiameterLine,70,0.1,2,1),
                new Parameter(ParameterName.Slot,70,1,2),

            };
        }
        public override List<Parameter> ParametersLine()
        {
            var Params = new List<Parameter>
            {
                GetParam(ParameterName.DiameterLine),
                GetParam(ParameterName.DiameterLine,1),
                GetParam(ParameterName.Slot),
            };
            return Params;
        }

    }
}
