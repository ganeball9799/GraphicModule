using System.Collections.Generic;
using System.Windows;
using GraphicModule.Models.Enums;


namespace GraphicModule.Models
{
    //TODO: Убрать наследование от FrameworkElement
    public abstract class Geometry
    {
        public LinesStructure Structure { get; set; }

        //TODO: Установить модификатор доступа protected
        protected List<Parameter> Parameters;

        public double this[ParameterName parameterName]
        {
            get => GetParam(parameterName).Value;
            set=> GetParam(parameterName).Value = value;
        }

        public Parameter GetParam(ParameterName parameterName) =>
            Parameters.Find((_parameters) => _parameters.ParameterName.Equals(parameterName));

    }
}
