using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using GraphicModule.Models.Enums;

namespace GraphicModule.Models
{
    public class Parameter: ICloneable
    {
        public ParameterName ParameterName;

        private double _value;
        
        /// <summary>
        /// Минимальная граница параметра.
        /// </summary>
        private double _min;

        /// <summary>
        /// Свойство минимальной границы параметра.
        /// </summary>
        

        private double _max;

        public double Value
        {
            get => _value;
            set
            {
                var compareResMax = Comparer<double>.Default.Compare(value, _max);
                if (compareResMax > 0)
                {
                    throw new ArgumentException(
                        $"Parameter value cannot be more than {_max}"
                    );
                }

                var compareResMin = Comparer<double>.Default.Compare(value, _min);
                if (compareResMin < 0)
                {
                    throw new ArgumentException(
                        $"Parameter value cannot be less than {_min}"
                    );
                }

                _value = value;
            }
        }

        /// <summary>
        /// Свойство минимальной границы параметра.
        /// </summary>
        public double Min
        {
            get => _min;
            set
            {
                var compareResult = Comparer<double>.Default.Compare(value, _max);
                if (compareResult > 0)
                {
                    throw new ArgumentException(
                        "Min value cannot be more than max"
                    );
                }

                _min = value;
            }
        }

        /// <summary>
        /// Свойство максимальной границы параметра.
        /// </summary>
        public double Max
        {
            get => _max;
            set
            {
                var compareResult = Comparer<double>.Default.Compare(value, _min);
                if (compareResult < 0)
                {
                    throw new ArgumentException(
                        "Max value cannot be less than min"
                    );
                }

                _max = value;
            }
        }
        public int Number { get;private set; }

        public Parameter(ParameterName parameterName, double max, double min, double value, int number = 0)
        {
            ParameterName = parameterName;
            Value = value;
            Max = max;
            Min = min;
            
            Number = number;
        }

        public object Clone() => new Parameter(ParameterName, Max, Min,  Value, Number);
    }
}
