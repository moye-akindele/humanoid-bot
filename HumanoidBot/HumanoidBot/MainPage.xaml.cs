using Windows.UI.Xaml.Controls;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Pca9685Test.ConfigData;

namespace Pca9685Test
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void PWMMotionViewControl_Loaded(object sender, RoutedEventArgs e)
        {
            Pca9685Test.ViewModel.PWMMotionViewModel PWMMotionViewModelObject 
                = new Pca9685Test.ViewModel.PWMMotionViewModel();
            
            Task.WaitAll(PWMMotionViewModelObject.GetJson(AppBox.motionDataPath));
            PWMMotionViewControl.DataContext = PWMMotionViewModelObject;
        }
        
        //public async Task<bool> FindI2CAddressesAsync(Func<Message, Task> onMessageReceived)
        //    {
        //        string aqs = I2cDevice.GetDeviceSelector("I2C1");

        //        var i2CDevices = await DeviceInformation.FindAllAsync(aqs).AsTask().ConfigureAwait(false);
        //        if (i2CDevices.Count == 0)
        //        {
        //            //await onMessageReceived(new Message("bus not found")).ConfigureAwait(false);
        //            return false;
        //        }

        //        for (byte slaveAddress = 0x00; slaveAddress <= 0x7F; slaveAddress++)
        //        {
        //            var settings = new I2cConnectionSettings(slaveAddress)
        //            {
        //                BusSpeed = I2cBusSpeed.FastMode,
        //                SharingMode = I2cSharingMode.Shared
        //            };

        //            foreach (var i2CDevice in i2CDevices)
        //            {
        //                var i2CDeviceDetails = await I2cDevice.FromIdAsync(i2CDevice.Id, settings).AsTask().ConfigureAwait(false);

        //                try
        //                {
        //                    var testCommand = new byte[] { 0x00, 0x0 };
        //                    i2CDeviceDetails.Read(testCommand);

        //                    //await onMessageReceived(new Message($"SlaveAddress: {settings.SlaveAddress:X} SUCCESS \n"));
        //                }
        //                catch (Exception)
        //                {
        //                     //await onMessageReceived(new Message($"SlaveAddress: {settings.SlaveAddress:X} FAILED \n"));
        //                }
        //            }
        //        }

        //        return false;
        //    }


    }
}
