using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FiftyEightBits.PreCode
{
    public class SpecialSettings :  INotifyPropertyChanged
    {
        private double _fontSize = 12;
        
        static SpecialSettings()
        {
                
        }               

        public double FontSize
        {
            get 
            { 
                return _fontSize; 
            }
            set 
            { 
                _fontSize = value;
                Notify("FontSize");
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void Notify(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        #endregion
    }
}
