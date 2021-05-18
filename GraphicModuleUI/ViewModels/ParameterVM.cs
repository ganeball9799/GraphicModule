using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GraphicModule.Models;

namespace GraphicModuleUI.ViewModels
{
    public class ParameterVM : ViewModelBase
    {
        /// <summary>
        /// Регулярное выражения для числа типа double.
        /// </summary>
        private const string DoubleRegex = "^[0-9]*[.,]?[0-9]+$";

        /// <summary>
        /// Регулярное выражения для числа типа int.
        /// </summary>
        private const string IntRegex = "^[0-9]+$";

        private Parameter _parameter;

        private string _value;

        private RelayCommand<MouseWheelEventArgs> _mouseWheelCommand;

        public RelayCommand<MouseWheelEventArgs> MouseWheelCommand
        {
            get
            {
                return _mouseWheelCommand ??
                       (_mouseWheelCommand = new RelayCommand<MouseWheelEventArgs>(obj =>
                       {
                           var step = ParameterName.Equals(ParameterName.StripsNumber) ? 1 : 0.1;
                           var sign = Math.Sign(obj.Delta);
                           var curValue = double.Parse(Value);

                           obj.Handled = true;
                           curValue += step * sign;
                           Value = curValue.ToString();
                       }));
            }
        }

        public string Sign { get; set; }

        public string Number { get; private set; }

        public delegate void ValueHandler(ParameterVM parameter);

        public event ValueHandler UpdateValue;

        public string Value
        {
            get => _value;
            set
            {
                _value = DotToComma(value);
                RaisePropertyChanged(nameof(Value));
                UpdateValue?.Invoke(this);
            }
        }

        public ParameterName ParameterName;

        public string this[string propertyName]
        {
            get
            {
                string error = null;

                if (propertyName == nameof(Value))
                {
                    if (ParameterName.Equals(ParameterName.StripsNumber))
                    {
                        if (!Regex.IsMatch(Value, IntRegex))
                        {
                            return "Value must be an integer";
                        }
                    }

                    if (!Regex.IsMatch(Value, DoubleRegex))
                    {
                        return "Value must be a fraction";
                    }

                    try
                    {
                        _parameter.Value = double.Parse(Value);
                    }
                    catch (Exception e)
                    {
                        error = e.Message;
                    }
                }

                return error;
            }
        }
        
        public ParameterVM(Parameter parameter)
        {
            _parameter = parameter;
            Value = _parameter.Value.ToString();
            SetSign(_parameter.ParameterName,_parameter.Number);
            ParameterName = _parameter.ParameterName;
            Number = _parameter.Number.ToString();
        }

        public ParameterVM(Parameter parameter, Action<Parameter> action)
        {
            _parameter = parameter;
            Value = _parameter.Value.ToString();
            SetSign(_parameter.ParameterName,_parameter.Number);
            ParameterName = _parameter.ParameterName;
            Number = _parameter.Number.ToString();
            PropertyChanged += (s, e) =>
            {
                if (s is ParameterVM p && e.PropertyName == nameof(p.Value) && p[e.PropertyName] is null)
                {
                    action?.Invoke(new Parameter(ParameterName, _parameter.Max, _parameter.Min , double.Parse(p.Value)));
                }
            };
        }

        public ParameterVM(Parameter parameter, Action<Parameter> action, ValueHandler Render) : this(parameter, action)
        {
            UpdateValue += Render;
        }

        private string DotToComma(string str)
        {
            var replaced = str.Replace('.', ',').Trim();

            var first = replaced[0];

            if (replaced.Length > 1)
            {
                if (first.Equals('0') && !replaced[1].Equals(','))
                {
                    replaced = replaced.Substring(1);
                }
            }


            return replaced;
        }

        private void SetSign(ParameterName parameterName, int num)
        {
            var numberEl = num + 1;
            var numElm = num.ToString();
            switch (parameterName)
            {
                case ParameterName.Slot:
                    Sign = $"S{numberEl}";
                    break;
                case ParameterName.StripsNumber:
                    Sign = $"N  ";
                    break;
                case ParameterName.StripsThickness:
                    Sign = $"t ";
                    break;
                case ParameterName.StripWidth:
                    Sign = $"W{numberEl}";
                    break;
                case ParameterName.SubstrateHeight:
                    Sign = $"h ";
                    break;
            }
        }
    }
}
