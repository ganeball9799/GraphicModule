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
        /// <summary>
        /// Экземпляр линии.
        /// </summary>
        private readonly Geometry _line;

        /// <summary>
        /// Список параметров.
        /// </summary>
        private ObservableCollection<ParameterVM> _parameters;

        /// <summary>
        /// Тип линии.
        /// </summary>
        public LinesStructure Type;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public LineVM(Geometry line)
        {
            _line = line;
            DefineLine(line);
            InitGraphicComponent();
            InitParameters();
        }

        /// <summary>
        /// Список графических компонентов
        /// </summary>
        public ObservableCollection<StructureImage> GraphicComponent { get; set; }

        /// <summary>
        /// Название линии
        /// </summary>
        public string Name { get;private set; }

        /// <summary>
        /// Свойство списков параметров структуры.
        /// </summary>
        public ObservableCollection<ParameterVM> Parameters
        {
            get => _parameters;
            set
            {
                _parameters = value;
                RaisePropertyChanged(nameof(Parameters));
            }
        }

        /// <summary>
        /// Метод, определяющий название и тип линии.
        /// </summary>
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
                case LinesStructure.Coaxial:
                    Name = "Coaxial";
                    Type = LinesStructure.Coaxial;
                    break;
            }
        }

        /// <summary>
        /// Метод инициализации графического компонента
        /// </summary>
        private void InitGraphicComponent()
        {
            GraphicComponent = new ObservableCollection<StructureImage>();

            switch (_line.Structure)
            {
                case LinesStructure.SingleCoplanar:
                    GraphicComponent.Add(new SingleCoplanarGraphic(_line));
                    break;
                case LinesStructure.CoupledVerticalInsert:
                    GraphicComponent.Add(new CoupledVerticalInsertGraphic(_line));
                    break;
                case LinesStructure.Microstrip:
                    GraphicComponent.Add(new MicrostripGraphic(_line));
                    break;
                case LinesStructure.Coaxial:
                    GraphicComponent.Add(new CoaxialGraphic(_line));
                    break;
                default:
                    throw new ArgumentException($"{_line.Structure} is not found");
            }
        }

        /// <summary>
        /// Метод инициализации списка параметров линии.
        /// </summary>
        private void InitParameters()
        {
            var physParams = _line.ParametersLine();
            var physParamsVM = ParamsToParamsVM(physParams);

            Parameters = new ObservableCollection<ParameterVM>(physParamsVM);
        }

        /// <summary>
        /// Метод, отследывающий изменение значений параметров.
        /// </summary>
        private void OnParameterChanged(Parameter parameter)
        {
            _line[parameter.ParameterName, parameter.Number] = parameter;

            if (parameter.ParameterName.Equals(ParameterName.StripsNumber))
            {
                InitParameters();
            }
        }

        /// <summary>
        /// Конвертация списка параметров в список его вью-моделей.
        /// </summary>
        private List<ParameterVM> ParamsToParamsVM(List<Parameter> paramsList)
        {
            var _parametersVM = new List<ParameterVM>();
            foreach (var param in paramsList)
            {
                var parametrVm = new ParameterVM((Parameter)param.Clone(), OnParameterChanged, Render);

                _parametersVM.Add(parametrVm);
            }
            return _parametersVM;
        }

        /// <summary>
        /// Метод инициализации измений в структуре
        /// </summary>
        /// <param name="parameter"></param>
        private void Render(ParameterVM parameter)
        {
            GraphicComponent.Clear();
            switch (_line.Structure)
            {
                case LinesStructure.SingleCoplanar:
                    GraphicComponent.Add(new SingleCoplanarGraphic(_line));
                    break;
                case LinesStructure.CoupledVerticalInsert:
                    GraphicComponent.Add(new CoupledVerticalInsertGraphic(_line));
                    break;
                case LinesStructure.Microstrip:
                    GraphicComponent.Add(new MicrostripGraphic(_line));
                    break;
                case LinesStructure.Coaxial:
                    GraphicComponent.Add(new CoaxialGraphic(_line));
                    break;

                default:
                    throw new ArgumentException($"{_line.Structure} is not found");
            }
        }
    }
}