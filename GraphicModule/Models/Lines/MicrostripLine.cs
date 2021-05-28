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
    public class MicrostripLine : Geometry
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

                        for (var i = 0; i < diff; i++)
                        {
                            var lastWidth = _parameters.FindLast((elem) => elem.ParameterName.Equals(ParameterName.StripWidth));
                            _parameters.Remove(lastWidth);
                        }

                        for (var i = 0; i < diff - 1; i++)
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
                            var newWidth = new Parameter(ParameterName.StripWidth, 70, 1, 20, lastWidth.Number + 1);
                            var index = _parameters.IndexOf(lastWidth);
                            _parameters.Insert(index + 1, newWidth);
                        }

                        for (var i = 0; i <= diff - 1; i++)
                        {
                            var lastSlot = _parameters.FindLast((elem) => elem.ParameterName.Equals(ParameterName.Slot));


                            var newNumber = lastSlot != null ? lastSlot.Number + 1 : 0;
                            var newSlot = new Parameter(ParameterName.Slot, 70, 1, 10, newNumber);
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
            Structure = LinesStructure.Microstrip;
            _parameters = new List<Parameter>
            {
                new Parameter(ParameterName.StripsNumber,6,1,2),
                new Parameter(ParameterName.StripsThickness,70,1,10),
                new Parameter(ParameterName.SubstrateHeight,70,1,10),
                new Parameter(ParameterName.StripWidth, 70,1,20),
                new Parameter(ParameterName.StripWidth, 70,1,20,1),
                new Parameter(ParameterName.Slot,70,1,10)
            };
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

            var widths = _parameters.FindAll((item) => item.ParameterName.Equals(ParameterName.StripWidth));

            for (var i = 0; i < stripsNumber; i++)
            {
                var param = widths.Find((item) => item.Number.Equals(i));
                parameters.Add(param);
            }

            var slots = _parameters.FindAll((item) => item.ParameterName.Equals(ParameterName.Slot));

            for (var i = 0; i < stripsNumber - 1; i++)
            {
                var param = slots.Find((item) => item.Number.Equals(i));
                parameters.Add(param);
            }
            return parameters;

        }
    }
}
