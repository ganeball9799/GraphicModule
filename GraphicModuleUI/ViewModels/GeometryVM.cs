using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using GraphicModule.Models;
using GraphicModule.Models.Enums;

namespace GraphicModuleUI.ViewModels
{
    //TODO: Удалить лишний класс
    public class GeometryVM:ViewModelBase
    {
        public LinesStructure Type;

        private Geometry _geometry; 

        public string Name { get; set; }

        public bool IsSelected { get; set; }

        public ObservableCollection<ParameterVM> ParametersVM { get; set; }

        public GeometryVM(Geometry geometry)
        {
            _geometry = geometry;
            IsSelected = geometry is MicrostripLine;
            DefineLine(_geometry);
            InitPhysicalParams();

        }

        private void InitPhysicalParams()
        {
            ParametersVM = new ObservableCollection<ParameterVM>();

            foreach (var subCollection in _geometry.Parameters)
            {
                foreach (var item in subCollection.Value.ToString())
                {
                    var param = new ParameterVM(subCollection.ParameterName, item);
                    if (subCollection.ParameterName != ParameterName.StripsNumber && subCollection.ParameterName != ParameterName.SubstrateHeight
                    && subCollection.ParameterName != ParameterName.StripsThickness)
                    {
                        var counter = 0;
                        foreach (var elem in ParametersVM)
                        {
                            if (elem.ParameterName == param.ParameterName)
                            {
                                counter++;
                            }
                        }

                        param.Sign = $"{param.Sign}{counter}";
                    }
                    ParametersVM.Add(param);
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
                case LinesStructure.Microstrip:
                    Name = "Microstrip";
                    Type = LinesStructure.Microstrip;
                    break;
                case LinesStructure.CoupledVerticalInsert:
                    Name = "VerticalInsert";
                    Type = LinesStructure.CoupledVerticalInsert;
                    break;
            }
        }

        private void InitGraphicComponent()
        {

        }



    }
}
