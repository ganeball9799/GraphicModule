using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using GraphicModule.Models.Enums;

namespace GraphicModule.Models
{
    public class MicrostripLine: Geometry
    {

        public override Parameter this[ParameterName paramName, int number = 0]
        {
            get => GetParam(paramName, number);
            set
            {
                var param = GetParam(paramName, number);
                if (paramName == ParameterName.StripsNumber)
                {
                    if (param.Value > value.Value)
                    {
                        var diff = param.Value - value.Value;

                        for (var i = 0; i <= diff; i++)
                        {
                            var lastWidth = _parameters.FindLast((elem) => elem.ParameterName.Equals(ParameterName.StripWidth));
                            _parameters.Remove(lastWidth);
                        }

                        for (var i = 0; i <= diff - 1; i++)
                        {
                            var lastSlot = _parameters.FindLast((elem) => elem.ParameterName.Equals(ParameterName.Slot));
                            _parameters.Remove(lastSlot);
                        }
                    }
                    else if (param.Value < value.Value)
                    {
                        var diff = value.Value - param.Value;

                        for (var i = 0; i <= diff; i++)
                        {
                            var lastWidth = _parameters.FindLast((elem) => elem.ParameterName.Equals(ParameterName.StripWidth));
                            var newWidth = new Parameter(ParameterName.StripWidth, 100, 0, lastWidth.Number + 1);
                            var index = _parameters.IndexOf(lastWidth);
                            _parameters.Insert(index + 1, newWidth);
                        }

                        for (var i = 0; i <= diff - 1; i++)
                        {
                            var lastSlot = _parameters.FindLast((elem) => elem.ParameterName.Equals(ParameterName.Slot));
                            var newSlot = new Parameter(ParameterName.Slot, 100, 0, lastSlot.Number + 1);
                            var index = _parameters.IndexOf(lastSlot);
                            _parameters.Insert(index + 1, newSlot);
                        }
                    }
                }
                param.Value = value.Value;
            }
        }


        public MicrostripLine()
        {
            InitComponent();
        }
        

        private void InitComponent()
        {
            Structure = LinesStructure.Microstrip;
            _parameters = new List<Parameter>
            {
                new Parameter(ParameterName.StripWidth, 30,70),
                new Parameter(ParameterName.StripWidth, 30,70),
                new Parameter(ParameterName.Slot,20,40),
                new Parameter(ParameterName.StripsNumber,2,6),
                new Parameter(ParameterName.StripsThickness,10,70),
                new Parameter(ParameterName.SubstrateHeight,10,70)
            };
            
        }

        public override void Doit()
        {
            var stripsNumber = (int)Math.Round(GetParam(ParameterName.StripsNumber).Value);
            var stripsThickness = GetParam(ParameterName.StripsThickness).Value;
            var substrateHeight = GetParam(ParameterName.SubstrateHeight).Value;
            var stripsWidths = ParamsToArray(ParameterName.StripWidth);
            var slots = ParamsToArray(ParameterName.Slot);
            ResetParams();
        }

        public override List<Parameter> ParametersLine()
        {
            var parameters = new List<Parameter>
            {
                GetParam(ParameterName.StripsNumber),
                GetParam(ParameterName.StripsThickness),
                GetParam(ParameterName.SubstrateHeight),
            };

            var stripsNumber = GetParam(ParameterName.StripsNumber).Value;

            var widths = _parameters.FindAll((item) => item.Number.Equals(ParameterName.StripWidth));

            for (var i = 0; i < stripsNumber; i++)
            {
                var param = widths.Find((item) => item.ParameterName.Equals(i));
                parameters.Add(param);
            }

            var slots = _parameters.FindAll((item) => item.ParameterName.Equals(ParameterName.Slot));

            for (var i = 0; i < stripsNumber; i++)
            {
                var param = widths.Find((item) => item.Number.Equals(i));
                parameters.Add(param);
            }

            return parameters;
        }

        private void ResetParams()
        {
            var parameters = ParametersLine();
            _parameters.Add(GetParam(ParameterName.StripsThickness));
            _parameters = parameters;
        }
        private double[] ParamsToArray(ParameterName paramName)
        {
            var linesNumber = (int)Math.Round(GetParam(ParameterName.StripsNumber).Value);
            if (paramName.Equals(ParameterName.Slot))
            {
                linesNumber--;
            }
            var array = new double[linesNumber];
            var list = _parameters.FindAll((item) => item.ParameterName.Equals(paramName));
            for (var i = 0; i < linesNumber; i++)
            {
                array[i] = list.Find((item) => item.Number.Equals(i)).Value;
            }

            return array;
        }
    }
}
