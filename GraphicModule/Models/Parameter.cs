using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace GraphicModule.Models
{
    public class Parameter
    {
        public ParameterName ParameterName;

        private double _value;

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

        private double _max;

        public double Max
        {
            get => _max;
            set
            {
                var Result = Comparer<double>.Default.Compare(value, 0);
                if (Result <0)
                {
                    throw new ArgumentException("Минимальное значение не может быть больше максимального");
                }

                _max = value;
            }
        }
        public int Number { get;private set; }

        public Parameter(ParameterName parameterName, double value,double max ,int number = 0)
        {
            ParameterName = parameterName;
            Value = value;
            Max = max;
            Number = number;
        }
    }
}
