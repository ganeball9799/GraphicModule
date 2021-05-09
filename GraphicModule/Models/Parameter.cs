using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace GraphicModule.Models
{
    public class Parameter
    {
        public ParameterName ParameterName { get; set; }

        private double _values;

        public double Values
        {
            get => _values;
            set
            {
                if (value<0)
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
