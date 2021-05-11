using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GraphicModule.Models.Enums;

namespace GraphicModule.Models
{
    public class CoupledVerticalInsertLine : Geometry
    {
        public LinesStructure Type = LinesStructure.CoupledVerticalInsert;

        private double _stripThickness;

        /// <summary>
        /// Высота подложки.
        /// </summary>
        private double _substrateHeight;

        /// <summary>
        /// Толщина полосы.
        /// </summary>
        private double _stripWidth;

        /// <summary>
        /// Список расстояний между полосами.
        /// </summary>
        private List<double> _slots;

        public CoupledVerticalInsertLine()
        {
            InitComponent();
        }

        public override void Analyze(List<Parameter> inputParams)
        {
            DistributeParameters(inputParams);
            var slots = _slots.ToArray();
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
            Structure = LinesStructure.CoupledVerticalInsert;
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
