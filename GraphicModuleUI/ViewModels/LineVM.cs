using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using GraphicModule.Models;
using GraphicModule.Models.Enums;
using GraphicModuleUI.Enums;
using GraphicModuleUI.ViewModels.GraphicComponent;

namespace GraphicModuleUI.ViewModels
{
    public class LineVM:ViewModelBase
    {
        private Geometry _line;

        public Geometry Line
        {
            get => _line;
            set
            {
                _line = value;
                RaisePropertyChanged(nameof(Line));
            }
        }

        public StructureType Type;

        public bool IsSelected { get; set; }

        public string Name { get; set; }

        public List<GeometryVM> GraphicComponent { get; set; }

        public ObservableCollection<ParameterVM> ParametersLine { get; set; }

        public LineVM(Geometry line)
        {
            Line = line;
            IsSelected = line is MicrostripLine;
            DefineLine(line);
            InitGraphicComponent();
            InitPhysicalParams();


        }

        private void DefineLine(Geometry line)
        {
            switch (line.Structure)
            {
                case LinesStructure.SingleCoplanar:
                    Name = "Coplanar";
                    Type = StructureType.Single;
                    break;

                case LinesStructure.CoupledVerticalInsert:
                    Name = "Vertical Insert";
                    Type = StructureType.Coupled;
                    break;

                case LinesStructure.Microstrip:
                    Name = "Microstrip";
                    Type = StructureType.Multiple;
                    break;
            };
        }

        private void InitGraphicComponent()
        {
            GraphicComponent = new List<GeometryVM>();

            switch (Line.Structure)
            {
                case LinesStructure.SingleCoplanar:
                    GraphicComponent.Add(new SingleCoplanarGC());
                    break;

                case LinesStructure.CoupledVerticalInsert:
                    GraphicComponent.Add(new CoupledVerticalInsertGC());
                    break;

                case LinesStructure.Microstrip:
                    GraphicComponent.Add(new MicrostripGC());
                    break;

                default:
                    throw new ArgumentException($"Graphic component for {Line.Structure} is not found");
            };
        }

        private void InitPhysicalParams()
        {
            ParametersLine = new ObservableCollection<ParameterVM>();

            foreach (var subCollection in Line.ParametersLine)
            {
                foreach (var item in subCollection.Values)
                {
                    var param = new ParameterVM(subCollection.ParameterName, item);
                    if (subCollection.ParameterName != ParameterName.StripsNumber && subCollection.ParameterName != ParameterName.SubstrateHeight
                                                                && subCollection.ParameterName != ParameterName.StripsThickness)
                    {
                        var counter = 0;
                        foreach (var elem in ParametersLine)
                        {
                            if (elem.ParameterName == param.ParameterName)
                            {
                                counter++;
                            }
                        }

                        param.Sign = $"{param.Sign}{counter}";
                    }
                    ParametersLine.Add(param);
                }
            }
        }
    }
}
