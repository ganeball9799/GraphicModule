using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace GraphicModule.Models
{
    public class Parameter
    {
        //TODO: set -> private set
        public ParameterName ParameterName { get; set; }

        //TODO: _values -> _value
        private double _value;

        //TODO: Values -> Value
        public double Value
        {
            get => _value;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException($"Значение не может быть отрицательным");
                }

                _value = value;
            }
        }

        public int Number { get; set; } = 0;

        public Parameter(ParameterName parameterName, double value, int number)
        {
            ParameterName = parameterName;
            Value = value;
            Number = number;
        }
    }
}
