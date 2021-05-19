using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GraphicModule.Models;
using GraphicModule.Models.Enums;
using GraphicModuleUI.ViewModels;

namespace GraphicModuleUI.ViewModel
{
    public class MainWindowVM : ViewModelBase
    {
        public MainWindowVM()
        {
            SelectedLine = new LineVM(new SingleCoplanarLine());

            Lines = new ObservableCollection<LineVM>
            {
                SelectedLine,
                new LineVM(new CoupledVerticalInsertLine()),
                new LineVM(new MicrostripLine())
            };

        }
        public ObservableCollection<LineVM> Lines { get; set; }

        private LineVM _selectedLine;

        //private ObservableCollection<ParameterVM> _parameters;
        //public ObservableCollection<ParameterVM> Parameters
        //{
        //    get => _parameters;
        //    set
        //    {
        //        _parameters = value;
        //        RaisePropertyChanged(nameof(Parameters));
        //    }
        //}

        public LineVM SelectedLine
        {
            get => _selectedLine;
            set
            {
                _selectedLine = value;
                //Parameters = SelectedLine.Parameters;
                RaisePropertyChanged(nameof(SelectedLine));
            }
        }
    }



}