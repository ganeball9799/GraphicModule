using System.Collections.Generic;
using System.Windows;
using GraphicModule.Models;
using GraphicModule.Models.Enums;


namespace GraphicModule.Models
{
    public abstract class Geometry
    {
        public LinesStructure Structure;

        protected List<Parameter> _parameters;

        public abstract Parameter this[ParameterName parameterName,int number = 0] { get; set; }

        public abstract List<Parameter> ParametersLine();

        protected Parameter GetParam(ParameterName paramName, int paramNumber = 0) =>
            _parameters.Find((parameter) => parameter.ParameterName.Equals(paramName) && parameter.Number.Equals(paramNumber));
    }
}
