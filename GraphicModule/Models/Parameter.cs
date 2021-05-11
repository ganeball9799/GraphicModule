using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using GraphicModule.Models.Enums;

namespace GraphicModule.Models
{
    public class Parameter
    {
        // Зачем это сделано автосвойством?
        // Разве не достаточно просто публичной переменной?
        public ParameterName ParameterName { get; set; }

        public Measure Measure { get; set; }

        public List<double> Values { get; set; }

        public Parameter(ParameterName parameterName, List<double> values, Measure measure)
        {
            ParameterName = parameterName;
            Values = values;
            Measure = measure;
        }

        public Parameter(ParameterName parameterName, List<double> values)
        {
            ParameterName = parameterName;
            Values = values;
        }
    }
}
