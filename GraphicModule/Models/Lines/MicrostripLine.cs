using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GraphicModule.Models.Enums;

namespace GraphicModule.Models
{
    // А этот класс решил не наследовать от geometry?
    public class MicrostripLine: Geometry
    {
        private int _stripsNumber;

        private double _stripThickness;

        private double _substrateHeight;


        private List<double> _stripWidth;

        private List<double> _slots;

        public MicrostripLine()
        {
            InitComponent();
            Structure = LinesStructure.Microstrip;
        }

        public override void Analyze(List<Parameter> inputParams)
        {
            DistributeParameters(inputParams);
            FillCollections();
        }
        private void DistributeParameters(List<Parameter> parameters)
        {
            foreach (var item in parameters)
            {
                switch (item.ParameterName)
                {
                    case ParameterName.StripsNumber:
                        var value = (int)item.Values.First();
                        if (value < 1)
                        {
                            throw new ArgumentException("There must be at least 1 strip on the microstrip line");
                        }
                        if (value > 6)
                        {
                            throw new ArgumentException("There must be at least 1 strip on the microstrip line");
                        }
                        _stripsNumber = value;
                        break;
                    case ParameterName.StripsThickness:
                        _stripThickness = item.Values.First();
                        break;
                    case ParameterName.SubstrateHeight:
                        _substrateHeight = item.Values.First();
                        break;
                    case ParameterName.StripsWidth:
                        _stripWidth = item.Values;
                        break;
                    case ParameterName.Slot:
                        _slots = item.Values;
                        break;
                }
            }
        }

        private void InitComponent()
        {
            _stripsNumber = 2;
            _stripThickness = 0.059;
            _substrateHeight = 15;
            _stripWidth = new List<double> {2,4};
            _slots = new List<double> { 1 };
            FillCollections();
        }

        private void FillCollections()
        {
            ParametersLine = new List<Parameter>
            {
                new Parameter(ParameterName.StripsNumber,new List<double>{_stripsNumber}),
                new Parameter(ParameterName.StripsThickness, new List<double>{_stripThickness}, Measure.Millimeter),
                new Parameter(ParameterName.SubstrateHeight, new List<double>{_substrateHeight}, Measure.Millimeter),
                new Parameter(ParameterName.StripsWidth, _stripWidth, Measure.Millimeter),
                new Parameter(ParameterName.Slot, _slots, Measure.Millimeter)
            };
        }
    }
}
