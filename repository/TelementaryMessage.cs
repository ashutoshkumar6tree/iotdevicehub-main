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
        private static string connectionString = "HostName=joshiIotDevice.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=4jnu7vdM7hr5J2wproAe8fTESWbOIK/H4Z0feFNYetM=";
        public static RegistryManager? registryManager;
        public static DeviceClient? client = null;
        public static string myDeviceConnection = "HostName=joshiIotDevice.azure-devices.net;DeviceId=iotdevice1;SharedAccessKey=7cbM5rmIueUN37CLMAlo+sYVcbFu1mfqwMtI3Vuscsc=";
        
        public static async Task SendMessage(string deviceName)
        {
            try
            {
                registryManager = RegistryManager.CreateFromConnectionString(connectionString);
                var device = await registryManager.GetTwinAsync(deviceName);
                ReportedProperties properties = new ReportedProperties();
                TwinCollection reportedprop;
                reportedprop = device.Properties.Reported;
                client = DeviceClient.CreateFromConnectionString(myDeviceConnection,Microsoft.Azure.Devices.Client.TransportType.Mqtt);
                while(true)
                {
                    var telementry = new
                    {
                        temprature = reportedprop["temprature"],
                        pressure = reportedprop["pressure"],
                        accuracy = reportedprop["accuracy"],

                    };
                    var telementryString = JsonConvert.SerializeObject(telementry);
                    var message = new Microsoft.Azure.Devices.Client.Message(Encoding.ASCII.GetBytes(telementryString));
                    await client.SendEventAsync(message);
                    Console.WriteLine("{0}>sending message:{1}", DateTime.Now, telementryString);
                    await Task.Delay(1000);
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}