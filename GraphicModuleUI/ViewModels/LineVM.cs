namespace GraphicModule.Models
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using GalaSoft.MvvmLight;
    using GraphicModule.Models.Enums;
    using GraphicModuleUI.ViewModels;
    using GraphicModuleUI.ViewModels.Graphic;

    public class LineVM : ViewModelBase
    {
        private readonly Geometry _line;

        private ObservableCollection<ParameterVM> parameters;

        public LinesStructure Type;

        public LineVM(Geometry line)
        {
            _line = line;
            DefineLine(line);
            InitGraphicComponent();
            InitParameters();
        }

        public ObservableCollection<StructureImage> GraphicComponent { get; set; }


        public string Name { get;private set; }

        public ObservableCollection<ParameterVM> Parameters
        {
            get => parameters;
            set
            {
                parameters = value;
                RaisePropertyChanged(nameof(Parameters));
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
            GraphicComponent = new ObservableCollection<StructureImage>();
            switch (_line.Structure)
            {
                case LinesStructure.SingleCoplanar:
                    GraphicComponent.Add(new SingleCoplanarGraphic(_line.ParametersLine(),_line));
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


        private void InitParameters()
        {
            var physParams = _line.ParametersLine();
            var physParamsVM = ParamsToParamsVM(physParams);

            Parameters = new ObservableCollection<ParameterVM>(physParamsVM);
        }

        private void OnParameterChanged(Parameter parameter)
        {
            _line[parameter.ParameterName, parameter.Number] = parameter;

            if (parameter.ParameterName.Equals(ParameterName.StripsNumber))
            {
                InitParameters();
            }
        }

        private List<ParameterVM> ParamsToParamsVM(List<Parameter> paramsList)
        {
            var parametersVM = new List<ParameterVM>();
            foreach (var param in paramsList)
            {
                var parametrVm = new ParameterVM((Parameter)param.Clone(), OnParameterChanged, Render);

                parametersVM.Add(parametrVm);
            }

            return parametersVM;
        }

        private void Render(ParameterVM parameter)
        {
            GraphicComponent.Clear();
            switch (_line.Structure)
            {
                case LinesStructure.SingleCoplanar:
                    GraphicComponent.Add(new SingleCoplanarGraphic(_line.ParametersLine(),_line));
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