using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Windows;
using System.Windows.Media;
using GalaSoft.MvvmLight;
using GraphicModule.Models.Enums;
using GraphicModuleUI.ViewModels;
using GraphicModuleUI.ViewModels.Graphic;

namespace GraphicModule.Models
{
    public class LineVM: ViewModelBase
    {
        private Geometry _line;

        public LinesStructure Type;

        public bool IsSelected { get; set; }

        public string Name { get; set; }

        public List<StructureImage> GraphicComponent { get; set; }

        private ObservableCollection<ParameterVM> parameters;

        public ObservableCollection<ParameterVM> Parameters
        {
            get => parameters;
            set
            {
                parameters = value;
                RaisePropertyChanged(nameof(Parameters));
            }
        }

        public LineVM(Geometry line)
        {
            _line = line;
            IsSelected = line is MicrostripLine;
            DefineLine(line);
            InitGraphicComponent();
            InitParameters();
        }

        private List<ParameterVM> ParamsToParamsVM(List<Parameter> paramsList)
        {
            var parametersVM = new List<ParameterVM>();
            foreach (var param in paramsList)
            {
                parametersVM.Add(new ParameterVM((Parameter)param.Clone(), OnParameterChanged));
            }

            return parametersVM;
        }

        private void OnParameterChanged(Parameter parameter)
        {
            _line[parameter.ParameterName] = parameter;

            if (parameter.ParameterName.Equals(ParameterName.StripsNumber))
            {
                InitParameters();
            }
        }



        private void InitParameters()
        {
            var physParams = _line.ParametersLine();
            var physParamsVM = ParamsToParamsVM(physParams);

            Parameters = new ObservableCollection<ParameterVM>(physParamsVM);
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
            GraphicComponent = new List<StructureImage>();
            switch (_line.Structure)
            {
                case LinesStructure.SingleCoplanar:
                    GraphicComponent.Add(new SingleCoplanarGraphic(_line.ParametersLine()));
                    break;
                case LinesStructure.CoupledVerticalInsert:
                    GraphicComponent.Add(new CoupledVerticalInsertGraphic());
                    break;
                case LinesStructure.Microstrip:
                    GraphicComponent.Add(new MicrostripGraphic());
                    break;

                default:
                    throw new ArgumentException($"{_line.Structure} is not found");
            }
        }
    }


}
