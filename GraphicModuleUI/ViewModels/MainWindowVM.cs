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
using GraphicModuleUI.Enums;
using GraphicModuleUI.ViewModels;
using GraphicModuleUI.ViewModels.GraphicComponent;
using Geometry = GraphicModule.Models.Geometry;

namespace GraphicModuleUI.ViewModel
{
    public class MainWindowVM : ViewModelBase
    {
        public MainWindowVM()
        {
            FillTreeView();
            TreeViewSelectionCommand = new RelayCommand(SelectStructure);

            //Lines = new ObservableCollection<Geometry>();
            //Lines.Add(new MicrostripLine());
            //Lines.Add(new CoupledVerticalInsertLine());
            //Lines.Add(new SingleCoplanarLine());
            //IncrementNCommand = new RelayCommand(IncreaseByOne);
            //DecrementNCommand = new RelayCommand(DecreaseByOne);
            //ApplyChangesCommand = new RelayCommand(ButtonDraw);

            //Widths.Add(new ListItemView("W", 30, "mm"));
            //SubstrateHeight = new ParameterVM(ParameterName.SubstrateHeight,10);
            //StripsThickness = new ParameterVM(ParameterName.StripsThickness, 5);
        }

        /// <summary>
        /// явл€етс€ ли лини€ одиночной.
        /// </summary>
        private bool _isSingle;

        /// <summary>
        /// явл€етс€ ли лини€ св€занной.
        /// </summary>
        private bool _isCoupled;

        /// <summary>
        /// явл€етс€ ли лини€ многрпроводной.
        /// </summary>
        private bool _isMultiple;

        private LineVM _selectedLine;


        private int _stripsNumber = 1;

        private ParameterVM _stripsThickness;

        private ParameterVM _substrateHeight;

        private RelayCommand _applyChangesCommand;

        public List<TreeNode<LineVM>> LinesTreeView { get; set; }

        public bool IsSingle
        {
            get => _isSingle;
            set
            {
                _isSingle = value;
                RaisePropertyChanged(nameof(IsSingle));
            }
        }

        public bool IsCoupled
        {
            get => _isCoupled;
            set
            {
                _isCoupled = value;
                RaisePropertyChanged(nameof(IsCoupled));
            }
        }

        public bool IsMultiple
        {
            get => _isMultiple;
            set
            {
                _isMultiple = value;
                RaisePropertyChanged(nameof(IsMultiple));
            }
        }


        public ObservableCollection<ListItemView> Widths { get; set; } = new ObservableCollection<ListItemView>();

        public ObservableCollection<ListItemView> Slots { get; set; } = new ObservableCollection<ListItemView>();
        public ObservableCollection<Geometry> Lines { get; set; }

        public LineVM SelectedLine
        {
            get => _selectedLine;
            set
            {
                _selectedLine = value;
                RaisePropertyChanged(nameof(SelectedLine));
            }
        }

        public RelayCommand TreeViewSelectionCommand { get; private set; }

        private void SelectStructure()
        {
            switch (SelectedLine.Type)
            {
                case StructureType.Single:
                    IsSingle = true;
                    IsCoupled = false;
                    IsMultiple = false;
                    break;
                case StructureType.Coupled:
                    IsSingle = false;
                    IsCoupled = true;
                    IsMultiple = false;
                    break;
                case StructureType.Multiple:
                    IsSingle = false;
                    IsCoupled = false;
                    IsMultiple = true;
                    break;
            }
        }

        public int StripsNumber
        {
            get => _stripsNumber;
            set
            {
                if (_stripsNumber > value)
                {
                    Widths.Remove(Widths.Last());
                    Slots.Remove(Slots.Last());
                    ListItemView.Decrement();
                }
                else
                {
                    Widths.Add(new ListItemView("W", 30, "mm"));
                    Slots.Add(new ListItemView("S", 10, "mm"));
                }


                _stripsNumber = value;
                RaisePropertyChanged(nameof(StripsNumber));
            }
        }

        /// <summary>
        /// —войство высоты подложки.
        /// </summary>
        public ParameterVM SubstrateHeight
        {
            get => _substrateHeight;
            set
            {
                _substrateHeight = value;
                RaisePropertyChanged(nameof(SubstrateHeight));
            }
        }

        /// <summary>
        /// —войство толщины полос.
        /// </summary>
        public ParameterVM StripsThickness
        {
            get => _stripsThickness;
            set
            {
                _stripsThickness = value;
                RaisePropertyChanged(nameof(StripsThickness));
            }
        }

        /// <summary>
        /// —войство команды увеличени€ N на единицу
        /// </summary>
        public RelayCommand IncrementNCommand { get; private set; }

        private void IncreaseByOne()
        {
            if (StripsNumber<6)
            {
              StripsNumber++;  
            }
        }

        /// <summary>
        /// —войство команды уменьшени€ N на единицу
        /// </summary>
        public RelayCommand DecrementNCommand { get; private set; }

        private void DecreaseByOne()
        {
            if (StripsNumber>1)
            {
                StripsNumber--;
            }
        }

        private void FillTreeView()
        {
            SelectedLine = new LineVM(new MicrostripLine());
            LinesTreeView = new List<TreeNode<LineVM>> {
                new TreeNode<LineVM>
                {
                    Name = "Single",
                    Lines = new List<LineVM>
                    {
                        new LineVM(new SingleCoplanarLine())
                    }
                },
                new TreeNode<LineVM>
                {
                    Name = "Coupled",
                    Lines = new List<LineVM>
                    {
                        new LineVM(new CoupledVerticalInsertLine())
                    }
                },
                new TreeNode<LineVM>
                {
                    Name = "Multi-condition",
                    Lines = new List<LineVM>
                    {
                        SelectedLine
                    }
                }
            };
        }

        public RelayCommand ApplyChangesCommand { get; private set; }

        private void ButtonDraw()
        {

        }
    }



    
}