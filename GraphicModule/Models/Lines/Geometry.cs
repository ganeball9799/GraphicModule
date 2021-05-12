using System.Collections.Generic;
using System.Windows;
using GraphicModule.Models.Enums;


namespace GraphicModule.Models
{
    //TODO: Убрать наследование от FrameworkElement
    //Этот класс нужно внести в папку Lines
    public abstract class Geometry:FrameworkElement
    {
        public LinesStructure Structure { get; set; }

        //TODO: Установить модификатор доступа protected
        public List<Parameter> Parameters;

        public double this[ParameterName parameterName]
        {
            get => GetParam(parameterName).Values;
            set=> GetParam(parameterName).Values = value;
        }

        private Parameter GetParam(ParameterName parameterName) =>
            Parameters.Find((_parameters) => _parameters.ParameterName.Equals(parameterName));

    }
}
