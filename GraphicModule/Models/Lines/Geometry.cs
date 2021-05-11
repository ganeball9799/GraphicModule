﻿using System.Collections.Generic;
using GraphicModule.Models.Enums;


namespace GraphicModule.Models
{
    //Этот класс нужно внести в папку Lines
    public abstract class Geometry
    {
        public LinesStructure Structure;

        public List<Parameter> ParametersLine;

        //public double this[ParameterName parameterName]
        //{
        //    get => GetParam(parameterName).Values;
        //    set
        //    {
        //        GetParam(parameterName).Values = value;
        //    }
        //}

        //private Parameter GetParam(ParameterName parameterName) =>
        //    Parameters.Find((_parameters) => _parameters.ParameterName.Equals(parameterName));

        public abstract void Analyze(List<Parameter> inputParameters);

    }
}
