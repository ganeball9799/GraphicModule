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
        /// <summary>
        /// ����������� �������
        /// </summary>
        public MainWindowVM()
        {
            SelectedLine = new LineVM(new SingleCoplanarLine());
            Lines = new ObservableCollection<LineVM>
            {
                SelectedLine,
                new LineVM(new CoupledVerticalInsertLine()),
                new LineVM(new MicrostripLine()),
                new LineVM(new CoaxialLine())
            };
        }

        /// <summary>
        /// ��������� �����
        /// </summary>
        private LineVM _selectedLine;

        /// <summary>
        /// �������� ��������� �����
        /// </summary>
        public LineVM SelectedLine
        {
            get => _selectedLine;
            set
            {
                _selectedLine = value;
                RaisePropertyChanged(nameof(SelectedLine));
            }
        }

        /// <summary>
        /// ��������� �����
        /// </summary>
        public ObservableCollection<LineVM> Lines { get; set; }
    }



}