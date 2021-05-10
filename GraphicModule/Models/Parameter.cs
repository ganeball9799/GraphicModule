using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace GraphicModule.Models
{
    public class Parameter
    {
        // Зачем это сделано автосвойством?
        // Разве не достаточно просто публичной переменной?
        public ParameterName ParameterName;


        private double _values;

        public double Values
        {
            get => _values;
            set
            {
                if (value < 0 || value>50)
                {
                    throw new ArgumentException($"Значение не может быть отрицательным");
                }

                _values = value;
            }
        }

        public Parameter(ParameterName parameterName, double values)
        {
            ParameterName = parameterName;
            Values = values;
        }
    }
}
