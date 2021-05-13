using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using GraphicModule.Models;

namespace GraphicModuleUI.ViewModels
{
    public class ParameterVM : ViewModelBase
    {
        private Parameter _parameter;

        private string _value;

        

        public string Sign { get; set; }

        public string Number { get; private set; }

        public string Value
        {
            get => _value;
            set
            {
                _value = value;
                RaisePropertyChanged(nameof(Value));
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

        public Parameter GetValid() => _parameter;

        public ParameterVM(Parameter parameter)
        {
            _parameter = parameter;
            Value = _parameter.Value.ToString();
            SetSign(_parameter.ParameterName);
            ParameterName = _parameter.ParameterName;
            Number = _parameter.Number.ToString();
        }

        public ParameterVM(Parameter parameter, Action<Parameter> action)
        {
            _parameter = parameter;
            Value = _parameter.Value.ToString();
            SetSign(_parameter.ParameterName);
            ParameterName = _parameter.ParameterName;
            Number = _parameter.Number.ToString();
            PropertyChanged += (s, e) =>
            {
                action?.Invoke(_parameter);
            };
        }

        private void SetSign(ParameterName parameterName)
        {
            switch (parameterName)
            {
                case ParameterName.SubstrateHeight:
                    Sign = "h";
                    break;
                    ;
                case ParameterName.Slot:
                    Sign = "S";
                    break;
                case ParameterName.StripsNumber:
                    Sign = "N";
                    break;
                case ParameterName.StripsThickness:
                    Sign = "t";
                    break;
                case ParameterName.StripWidth:
                    Sign = "W";
                    break;
            }
        }
    }
}
