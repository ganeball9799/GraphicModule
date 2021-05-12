using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GraphicModule.Models.Enums;

namespace GraphicModule.Models
{
    public class SingleCoplanarLine : Geometry
    {
        private double _stripThickness;

        private double _substrateHeight;

        
        private double _stripWidth;

        private List<double> _slots;

        public SingleCoplanarLine()
        {
            InitComponent();
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
                    case ParameterName.StripsThickness:
                        _stripThickness = item.Values.First();
                        break;
                    case ParameterName.SubstrateHeight:
                        _substrateHeight = item.Values.First();
                        break;
                    case ParameterName.StripsWidth:
                        _stripWidth = item.Values.First();
                        break;
                    case ParameterName.Slot:
                        _slots = item.Values;
                        break;
                }
            }
        }

        private void InitComponent()
        {
            Structure = LinesStructure.SingleCoplanar;
            _stripThickness = 0.059;
            _substrateHeight = 15;
            _stripWidth = 0.6;
            _slots = new List<double> { 1, 3 };
            FillCollections();
        }

        private void FillCollections()
        {
            ParametersLine = new List<Parameter>
            {
                new Parameter(ParameterName.StripsThickness, new List<double>{_stripThickness}, Measure.Millimeter),
                new Parameter(ParameterName.SubstrateHeight, new List<double>{_substrateHeight}, Measure.Millimeter),
                new Parameter(ParameterName.StripsWidth, new List<double>{_stripWidth}, Measure.Millimeter),
                new Parameter(ParameterName.Slot, _slots, Measure.Millimeter)
            };
        }

    }
}
