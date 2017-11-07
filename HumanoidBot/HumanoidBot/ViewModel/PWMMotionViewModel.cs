using AdafruitClassLibrary;
using Newtonsoft.Json;
using Pca9685Test.ConfigData;
using Pca9685Test.Model;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;
using static AdafruitClassLibrary.I2CBase;

namespace Pca9685Test.ViewModel
{
    public class PWMMotionViewModel
    {
        Pca9685 servoDriver = new Pca9685();
        string SelectedMotionName = AppBox.MotionStand;

        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<PWMMotion> Movements
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public MotionAction SelectedMotion
        {
            get;
            set;
        }

        private string _JsonData;
        public String JsonData
        {
            get
            {
                return _JsonData;
            }

            set
            {
                if (_JsonData != value)
                {
                    _JsonData = value;
                    //RaisePropertyChanged("JsonData");
                }
            }
        }

        public PWMMotionViewModel()
        {
            Task.Run(() => InitializePca());
        }

        /// <summary>
        /// Initializes drivers, retrieves JSON and activates the infinite loop.
        /// </summary>
        public async void InitializePca()
        {
            await servoDriver.InitPCA9685Async(I2CSpeed.I2C_100kHz);
            servoDriver.SetPWMFrequency(50);
            
            Task.WaitAll(GetJson(AppBox.motionDataPath));
            //Task.WaitAll(ExecutePcaCommand());
            ExecutePcaCommand();
        }

        public void LoadMotionList()
        {
            //Task.WaitAll(GetJson(AppBox.motionDataPath));
        }

        /// <summary>
        /// Reads in JSON data from a text or JSON file.
        /// </summary>
        /// <param name="relativePath">The relative file path.</param>
        /// <returns></returns>
        public async Task GetJson(string relativePath)
        {
            // Load the text block with text from a file
            var filename = System.IO.Path.Combine(Package.Current.InstalledLocation.Path, relativePath);
            var file = await StorageFile.GetFileFromPathAsync(filename);
            var inputStream = await file.OpenSequentialReadAsync();

            string fileContents;
            using (var streamReader = new StreamReader(inputStream.AsStreamForRead()))
            {
                fileContents = await streamReader.ReadToEndAsync();
            }
            
            var jSONData = JsonConvert.DeserializeObject<Model.ActionData>(fileContents);
            SelectedMotion = jSONData.Action.Where(m => m.ActionName.Equals("stand")).First();

            ObservableCollection<PWMMotion> movements = new ObservableCollection<PWMMotion>();            
            foreach (PWMMotion pWMMotion in SelectedMotion.PWMMotion)
            {
                movements.Add(new PWMMotion { PinID = pWMMotion.PinID, PWM = pWMMotion.PWM, InvertPWM = pWMMotion.InvertPWM });
            }            
            Movements = movements;
        }
        
        /// <summary>
        /// Runs teh infinite while loop that keeps the robot functioning.
        /// </summary>
        /// <returns></returns>
        public void ExecutePcaCommand()
        {
            //PWM value should be from 0 to 4095 inclusive.
            while (true)
            {                
                if (SelectedMotionName != AppBox.MotionStop)
                {
                    DoMotionHold(Movements);
                }             
            }
        }

        /// <summary>
        /// Moves the servo to a specific position without returning.
        /// </summary>
        /// <param name="Movements">Collection of parameters for each servo.</param>
        private void DoMotionHold(ObservableCollection<PWMMotion> Movements)
        {
            foreach (PWMMotion Movement in Movements)
            {
                servoDriver.SetPin(Movement.PinID, Movement.PWM, false);
            }
            Task.Delay(1000).Wait();
            foreach (PWMMotion Movement in Movements)
            {
                servoDriver.SetPin(Movement.PinID, 0, false);
            }
        }

        /// <summary>
        /// Uses ocsilating motion to move servos to a specific point and return them.
        /// </summary>
        /// <param name="Movements">Collection of parameters for each servo.</param>
        private void DoMotion(ObservableCollection<PWMMotion> Movements)
        {
            //Move to
            foreach (PWMMotion Movement in Movements)
            {
                servoDriver.SetPin(Movement.PinID, Movement.PWM, false);
            }            
            Task.Delay(1000).Wait();
            foreach (PWMMotion Movement in Movements)
            {
                servoDriver.SetPin(Movement.PinID, 0, false);
            }
            
            //Return to
            foreach (PWMMotion Movement in Movements)
            {
                servoDriver.SetPin(Movement.PinID, 1, false);
            }
            Task.Delay(1000).Wait();
            foreach (PWMMotion Movement in Movements)
            {
                servoDriver.SetPin(Movement.PinID, 0, false);
            }
        }
         
    }
}
