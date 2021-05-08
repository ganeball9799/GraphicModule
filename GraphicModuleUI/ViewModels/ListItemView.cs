using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;

namespace GraphicModule.Models
{
    public class ListItemView: ViewModelBase
    {
        /// <summary>
        /// Номер элемента списка ширин
        /// </summary>
        private static int _numberW = 1;

        /// <summary>
        /// Номер элемента списка слотов
        /// </summary>
        private static int _numberS = 1;

        private double _itemValue;

        /// <summary>
        /// Имя элемента списка
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// Значение элемента списка
        /// </summary>
        public double ItemValue
        {
            get => _itemValue;
            set
            {
                _itemValue = value;
                RaisePropertyChanged(nameof(ItemValue));
            }
        }

        /// <summary>
        /// Еденица измерения элемента списка
        /// </summary>
        public string ItemMeasure { get; set; }

        public ListItemView(string name, double value, string measure)
        {
            ItemValue = value;
            ItemMeasure = measure;
            DefineItem(name);
        }

        /// <summary>
        /// Метод определения элемента для отображения
        /// </summary>
        /// <param name="name">Имя элемента для отображения</param>
        private void DefineItem(string name)
        {
            switch (name)
            {
                case "W":
                    ItemName = name + _numberW.ToString() + " = ";
                    _numberW++;
                    break;
                case "S":
                    ItemName = name + _numberS.ToString() + " = ";
                    _numberS++;
                    break;
                default:
                    ItemName = name + " = ";
                    break;
            }
        }

        /// <summary>
        /// Уменьшить кол-во линий и слотов
        /// </summary>
        public static void Decrement()
        {
            _numberW--;
            _numberS--;
        }
    }


}
