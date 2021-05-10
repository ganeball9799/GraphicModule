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
            Lines = new ObservableCollection<Geometry>();
            Lines.Add(new MicrostripLine());
            Lines.Add(new CoupledVerticalInsertLine());
            Lines.Add(new SingleCoplanarLine());
            IncrementNCommand = new RelayCommand(IncreaseByOne);
            DecrementNCommand = new RelayCommand(DecreaseByOne);
            ApplyChangesCommand = new RelayCommand(ButtonDraw);
            
            Widths.Add(new ListItemView("W", 30, "mm"));
            SubstrateHeight = new ParameterVM(ParameterName.SubstrateHeight,10);
            StripsThickness = new ParameterVM(ParameterName.StripsThickness, 5);
        }

        private int _stripsNumber = 1;

        private ParameterVM _stripsThickness;

        private ParameterVM _substrateHeight;

        private RelayCommand _applyChangesCommand;

        private GeometryVM _selectedLine;

        public ObservableCollection<ListItemView> Widths { get; set; } = new ObservableCollection<ListItemView>();

        public ObservableCollection<ListItemView> Slots { get; set; } = new ObservableCollection<ListItemView>();
        public ObservableCollection<Geometry> Lines { get; set; }

        public GeometryVM SelectedLine
        {
            get => _selectedLine;
            set
            {
                _selectedLine = value;
                RaisePropertyChanged(nameof(SelectedLine));
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
        /// Свойство высоты подложки.
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
        /// Свойство толщины полос.
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
        /// Свойство команды увеличения N на единицу
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
        /// Свойство команды уменьшения N на единицу
        /// </summary>
        public RelayCommand DecrementNCommand { get; private set; }

        private void DecreaseByOne()
        {
            if (StripsNumber>1)
            {
                StripsNumber--;
            }
        }

        public RelayCommand ApplyChangesCommand { get; private set; }

        private void ButtonDraw()
        {

        }
    }



    
}