using System.Collections.Generic;
using System.ComponentModel;

namespace Pca9685Test.Model
{
    public class PWMMotionModel
    {

    }

    public class ActionData : INotifyPropertyChanged
    {
        private List<MotionAction> action;                
        public List<MotionAction> Action
        {
            get { return action; }

            set
            {
                if (action != value)
                {
                    action = value;
                    RaisePropertyChanged("MotionActions");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }

    public class MotionAction : INotifyPropertyChanged
    {
        private string actionId;
        public string ActionId
        {
            get
            {
                return actionId;
            }

            set
            {
                if (actionId != value)
                {
                    actionId = value;
                    RaisePropertyChanged("ActionId");
                }
            }
        }

        private string actionName;
        public string ActionName
        {
            get { return actionName; }

            set
            {
                if (actionName != value)
                {
                    actionName = value;
                    RaisePropertyChanged("ActionName");
                }
            }
        }

        private bool isRepeat;
        public bool IsRepeat
        {
            get { return isRepeat; }

            set
            {
                if (isRepeat != value)
                {
                    isRepeat = value;
                    RaisePropertyChanged("IsRepeat");
                }
            }
        }

        private List<PWMMotion> pWMMotion;
        public List<PWMMotion> PWMMotion
        {
            get { return pWMMotion; }

            set
            {
                if (pWMMotion != value)
                {
                    pWMMotion = value;
                    RaisePropertyChanged("PWMMotion");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }

    public class PWMMotion : INotifyPropertyChanged
    {
        private ushort pinNumber;
        private ushort pWM;
        private bool invertPWM;

        public ushort PinID
        {
            get
            {
                return pinNumber;
            }

            set
            {
                if (pinNumber != value)
                {
                    pinNumber = value;
                    RaisePropertyChanged("PinNumber");
                }
            }
        }

        public ushort PWM
        {
            get { return pWM; }

            set
            {
                if (pWM != value)
                {
                    pWM = value;
                    RaisePropertyChanged("PWM");
                }
            }
        }

        public bool InvertPWM
        {
            get { return invertPWM; }

            set
            {
                if (invertPWM != value)
                {
                    invertPWM = value;
                    RaisePropertyChanged("InvertPWM");
                }
            }
        }
                
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
