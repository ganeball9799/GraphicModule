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
        /// <summary>
        /// Свойство имения параметра
        /// </summary>
        public ParameterName ParameterName;

        /// <summary>
        /// значение параметра
        /// </summary>
        private double _value;
        
        /// <summary>
        /// Минимальная граница параметра.
        /// </summary>
        private double _min;

        /// <summary>
        /// Свойство максимальной границы параметра.
        /// </summary>
        private double _max;

        /// <summary>
        /// Свойство для значения параметра
        /// </summary>
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

        /// <summary>
        /// Поле для номера параметра
        /// </summary>
        public int Number { get;private set; }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public Parameter(ParameterName parameterName, double max, double min, double value, int number = 0)
        {
            ParameterName = parameterName;
            Max = max;
            Min = min;
            Value = value;
            
            Number = number;
        }

        /// <summary>
        /// Метод Clone
        /// </summary>
        public object Clone() => new Parameter(ParameterName, Max, Min,  Value, Number);
    }
}
