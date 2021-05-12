using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using GalaSoft.MvvmLight;
using GraphicModule.Models.Enums;
using GraphicModuleUI.ViewModels;

namespace GraphicModule.Models
{
    public class LineVM: ViewModelBase
    {
        private Geometry _line;

        public LinesStructure Type;

        public bool IsSelected;

        public string Name { get; set; }

        public List<Geometry> GraphicComponent { get; set; }

        public ObservableCollection<ParameterVM> Parameters { get; set; }

        public LineVM(Geometry line)
        {
            _line = line;
            IsSelected = line is MicrostripLine;
            DefineLine(line);
            InitGraphicComponent();
            InitParameters();
        }

        private void InitParameters()
        {
            Parameters = new ObservableCollection<ParameterVM>();
            foreach (var subCollection in )
            {
                foreach (var item in subCollection.Value.ToString())
                {
                    var param = new ParameterVM(subCollection.ParameterName, item);
                    if (subCollection.ParameterName != ParameterName.StripsNumber &&
                        subCollection.ParameterName != ParameterName.SubstrateHeight
                        && subCollection.ParameterName != ParameterName.StripsThickness)
                    {
                        var counter = 0;
                        foreach (var elem in Parameters)
                        {
                            if (elem.ParameterName == param.ParameterName)
                            {
                                counter++;
                            }  
                        }

                        param.Sign = $"{param.Sign}{counter}";
                    }
                    Parameters.Add(param);
                }
                
            }
        }

        private void DefineLine(Geometry line)
        {
            switch (line.Structure)
            {
                case LinesStructure.SingleCoplanar:
                    Name = "Coplanar";
                    Type = LinesStructure.SingleCoplanar;
                    break;
                case LinesStructure.CoupledVerticalInsert:
                    Name = "Vertical Insert";
                    Type = LinesStructure.CoupledVerticalInsert;
                    break;
                case LinesStructure.Microstrip:
                    Name = "Microstrip";
                    Type = LinesStructure.Microstrip;
                    break;

            }
        }

        private void InitGraphicComponent()
        {
            GraphicComponent = new List<Geometry>();
            switch (_line.Structure)
            {
                case LinesStructure.SingleCoplanar:
                    GraphicComponent.Add(new SingleCoplanarLine());
                    break;
                case LinesStructure.CoupledVerticalInsert:
                    GraphicComponent.Add(new CoupledVerticalInsertLine());
                    break;
                case LinesStructure.Microstrip:
                    GraphicComponent.Add(new MicrostripLine());
                    break;

                default:
                    throw new ArgumentException($"{_line.Structure} is not found");
            }
        }
    }


}
