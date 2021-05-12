using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GraphicModule.Models;
using GraphicModule.Models.Enums;
using GraphicModuleUI.ViewModels;
using Geometry = GraphicModule.Models.Geometry;

namespace GraphicModuleUI.ViewModel
{
    public class MainWindowVM : ViewModelBase
    {
        public MainWindowVM()
        {
            
            Lines = new ObservableCollection<LineVM>
            {
                Lines.Add(new LineVM(new SingleCoplanarLine()),
                Lines.Add(MicrostripLine),
                new LineVM(new MicrostripLine())
            };
            
            
            
            TreeViewSelectionCommand = new RelayCommand(SelectStructure);
        }
        public ObservableCollection<LineVM> Lines { get; set; }

        private void FillTreeView()
        {
            Geometry g = (Geometry)Lines.GetEnumerator();
            MessageBox.Show(g.Name);
        }


        /// <summary>
        /// �������� �� ����� ���������.
        /// </summary>
        private bool _isSingle;

        /// <summary>
        /// �������� �� ����� ���������.
        /// </summary>
        private bool _isCoupled;

        /// <summary>
        /// �������� �� ����� ��������������.
        /// </summary>
        private bool _isMultiple;


        private LineVM _selectedLine;

        public List<LineVM> LinesTreeView { get; set; }

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
        /// �������� ������� ������ ����� � ������.
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

        private void SelectStructure()
        {
            switch (SelectedLine.Type)
            {
                case LinesStructure.SingleCoplanar:
                    IsSingle = true;
                    IsCoupled = false;
                    IsMultiple = false;
                    break;
                case LinesStructure.CoupledVerticalInsert:
                    IsSingle = false;
                    IsCoupled = true;
                    IsMultiple = false;
                    break;
                case LinesStructure.Microstrip:
                    IsSingle = false;
                    IsCoupled = false;
                    IsMultiple = true;
                    break;
            }
        }

        





        private void ButtonDraw()
        {

        }
    }



    
}