using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using GraphicModule.Models;

namespace GraphicModuleUI.ViewModels
{
    public class ParameterVM:ViewModelBase
    {
        private double _value;

        //TODO: private set
        public string Sign { get; set; }

        public double Value
        {
            get => _value;
            set
            {
                _value = value;
                RaisePropertyChanged(nameof(Value));
            }
        }

        public ParameterName ParameterName { get; set; }

        public int Number { get; set; } = 0;

        public ParameterVM(ParameterName parameterName, double value, int number)
        {
            Value = value;
            SetSign(parameterName);
            ParameterName = parameterName;
            Number = number;
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
                case ParameterName.StripsWidth:
                    Sign = "W";
                    break;
            }
        }
    }
}
