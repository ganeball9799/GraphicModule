using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
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
            Lines = new ObservableCollection<LineVM>
            {
                new LineVM(new SingleCoplanarLine()),
                new LineVM(new CoupledVerticalInsertLine()),
                new LineVM(new MicrostripLine())
            };


            TreeViewSelectionCommand = new RelayCommand(SelectStructure);
        }
        public ObservableCollection<LineVM> Lines { get; set; }



        /// <summary>
        /// �������� �� ����� ���������.
        /// </summary>
        /// //TODO: ��� �����?
        private bool _isSingle;

        /// <summary>
        /// �������� �� ����� ���������.
        /// </summary>
        /// //TODO: ��� �����?
        private bool _isCoupled;

        /// <summary>
        /// �������� �� ����� ��������������.
        /// </summary>
        /// //TODO: ��� �����?
        private bool _isMultiple;


        private LineVM _selectedLine;

        public List<LineVM> LinesTreeView { get; set; }

        //TODO: ��� �����?
        public bool IsSingle
        {
            get => _isSingle;
            set
            {
                _isSingle = value;
                RaisePropertyChanged(nameof(IsSingle));
            }
        }

        /// <summary>
        /// �������� ����������� �����.
        /// </summary>
        /// //TODO: ��� �����?
        public bool IsCoupled
        {
            get => _isCoupled;
            set
            {
                _isCoupled = value;
                RaisePropertyChanged(nameof(IsCoupled));
            }
        }

        /// <summary>
        /// �������� ���������������� �����.
        /// </summary>
        /// //TODO: ��� �����?
        public bool IsMultiple
        {
            get => _isMultiple;
            set
            {
                _isMultiple = value;
                RaisePropertyChanged(nameof(IsMultiple));
            }
        }

        /// <summary>
        /// �������� ������� ��� ������ �� �������.
        /// </summary>
        public RelayCommand TreeViewSelectionCommand { get; private set; }
        

        public LineVM SelectedLine
        {
            get => _selectedLine;
            set
            {
                _selectedLine = value;
                RaisePropertyChanged(nameof(SelectedLine));
            }
        }

        //TODO: ��� �����?
        private void SelectStructure()
        {
            switch (SelectedLine.Type)
            {
                case LinesStructure.SingleCoplanar:
                    IsSingle = true;
                    IsCoupled = false;
                    IsMultiple = false;
                    _selectedLine.IsSelected = true;
                    break;
                case LinesStructure.CoupledVerticalInsert:
                    IsSingle = false;
                    IsCoupled = true;
                    IsMultiple = false;
                    _selectedLine.IsSelected = true;

                    break;
                case LinesStructure.Microstrip:
                    IsSingle = false;
                    IsCoupled = false;
                    IsMultiple = true;
                    _selectedLine.IsSelected = true;

                    break;
            }

        }
    }



}