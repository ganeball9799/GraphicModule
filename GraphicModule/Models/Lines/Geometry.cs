using System.Collections.Generic;
using System.Windows;
using GraphicModule.Models;
using GraphicModule.Models.Enums;


namespace GraphicModule.Models
{
    public abstract class Geometry
    {
        /// <summary>
        /// Имя структуры
        /// </summary>
        public LinesStructure Structure;

        /// <summary>
        /// Список параметров
        /// </summary>
        protected List<Parameter> _parameters;

        /// <summary>
        /// Индексатор параметров по типу и номеру.
        /// </summary>
        public abstract Parameter this[ParameterName parameterName,int number = 0] { get; set; }

        /// <summary>
        /// Получение параметров
        /// </summary>
        public abstract List<Parameter> ParametersLine();

        /// <summary>
        /// Получение параметра по имени
        /// </summary>
        protected Parameter GetParam(ParameterName paramName, int paramNumber = 0) =>
            _parameters.Find((parameter) => parameter.ParameterName.Equals(paramName) && parameter.Number.Equals(paramNumber));
    }
}
