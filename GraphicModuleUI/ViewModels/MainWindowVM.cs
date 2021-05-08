using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GraphicModule.Models;
using GraphicModule.Models.Enums;

namespace GraphicModuleUI.ViewModel
{
    public class MainWindowVM : ViewModelBase
    {
        public MainWindowVM()
        {
            Lines.Add(new LineStructure());
            Lines.Add(new LineStructure());
            Lines.Add(new LineStructure());
        }

        private bool ChangeMicrostripLine;

        private bool ChangeSingleLine;

        private bool ChangeCoupledLine;

        private int _stripsNumber = 1;

        private ListItemView _stripsThickness;

        private ListItemView _substrateHeight;

        private RelayCommand _applyChangesCommand;

        private RelayCommand _incrementNCommand;

        private RelayCommand _decrementNCommand;

        public ObservableCollection<ListItemView> Widths { get; set; } = new ObservableCollection<ListItemView>();

        public ObservableCollection<ListItemView> Slots { get; set; } = new ObservableCollection<ListItemView>();
        public ObservableCollection<LineStructure> Lines { get; set; } = new ObservableCollection<LineStructure>();

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
        public ListItemView SubstrateHeight
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
        public ListItemView StripsThickness
        {
            get => _stripsThickness;
            set
            {
                _stripsThickness = value;
                RaisePropertyChanged(nameof(StripsThickness));
            }
        }

        
    }
}