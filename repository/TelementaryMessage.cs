using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Azure.Devices.Shared;
using Microsoft.Azure.Devices.Client;
using System.Text;
using Microsoft.Azure.Devices;
using Newtonsoft.Json;
using System.Threading.Tasks;
using IotHubDevice.DTO;


namespace IotHubDevice.repository
{
    public class TelementaryMessage
    {
        private static string connectionString = "HostName=ashuiothubnew.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=7M9PIPold7W7pfqyz4IboT/vzLvzGDEGvmVkJO76iyM=";
        public static RegistryManager? registryManager;
        public static DeviceClient? client = null;
        public static string myDeviceConnection = "HostName=iotdevicehubltimind.azure-devices.net;DeviceId=iotdevice1;SharedAccessKey=ERusWEqTQeU5TPzsjPQ4Tsr1+wYoo0mWG0j6Oz/iDrE=";
        
        public static async Task<string> SendMessage(string deviceName)
        {
            try
            {
                registryManager = RegistryManager.CreateFromConnectionString(connectionString);
                var device = await registryManager.GetTwinAsync(deviceName);
                ReportedProperties properties = new ReportedProperties();
                TwinCollection reportedprop;
                reportedprop = device.Properties.Reported;
                client = DeviceClient.CreateFromConnectionString(myDeviceConnection,Microsoft.Azure.Devices.Client.TransportType.Mqtt);
                
                double minTemperature = 20;
                double minHumidity = 60;
                Random rand = new Random();

                while (true)
                {
                    double currentTemperature = minTemperature + rand.NextDouble() * 15;
                    double currentHumidity = minHumidity + rand.NextDouble() * 20;

                    // Create JSON message  

                    var telemetryDataPoint = new
                    {

                        temperature = currentTemperature,
                        humidity = currentHumidity
                    };

                    string messageString = "";



                    messageString = JsonConvert.SerializeObject(telemetryDataPoint);

                    var message = new Microsoft.Azure.Devices.Client.Message(Encoding.ASCII.GetBytes(messageString));

                     await client.SendEventAsync(message);
                    Console.WriteLine("{0} > Sending message: {1}", DateTime.Now, messageString);
                    var teleresponse=messageString;
                    await Task.Delay(1000 * 10);
                    return teleresponse;

                }

                // commented old code
                // while(true)
                // {
                //     var telementry = new
                //     {
                //         temprature = reportedprop["temprature"],
                //         pressure = reportedprop["pressure"],
                //         accuracy = reportedprop["accuracy"],

                //     };
                //     var telementryString = JsonConvert.SerializeObject(telementry);
                //     var message = new Microsoft.Azure.Devices.Client.Message(Encoding.ASCII.GetBytes(telementryString));
                //     await client.SendEventAsync(message);
                //     Console.WriteLine("{0}>sending message:{1}", DateTime.Now, telementryString);
                //     await Task.Delay(1000);
                // }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}