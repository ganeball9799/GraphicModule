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

        public ParameterName ParameterName;
        public string Measure { get; set; }

        public ParameterVM(ParameterName parameterName, double value)
        {
            Value = value;
            SetSign(parameterName);
            ParameterName = parameterName;
            
        }

        private void SetSign(ParameterName parameterName)
        {
            switch (parameterName)
            {
                case ParameterName.SubstrateHeight:
                    Sign = "h";
                    Measure = " (mm)";
                    break;
                    ;
                case ParameterName.Slot:
                    Sign = "S";
                    Measure = " (mm):";
                    break;
                case ParameterName.StripsNumber:
                    Sign = "N";
                    Measure = " :";
                    break;
                case ParameterName.StripsThickness:
                    Sign = "t";
                    Measure = " (mm):";
                    break;
                case ParameterName.StripsWidth:
                    Sign = "W";
                    Measure = " (mm):";
                    break;
            }
        }
    }
}
