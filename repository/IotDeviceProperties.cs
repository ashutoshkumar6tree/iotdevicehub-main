using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Shared;
using Microsoft.Azure.Devices;
using IotHubDevice.DTO;
using Microsoft.Azure.Devices.Client;

namespace IotHubDevice.repository
{
    public class IotDeviceProperties
    {
        private static string connectionString = "HostName=joshiIotDevice.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=4jnu7vdM7hr5J2wproAe8fTESWbOIK/H4Z0feFNYetM=";
        public static RegistryManager registryManager = RegistryManager.CreateFromConnectionString(connectionString);
        public static DeviceClient? client = null;
        public static string myDeviceConnection = "HostName=joshiIotDevice.azure-devices.net;DeviceId=iotdevice1;SharedAccessKey=7cbM5rmIueUN37CLMAlo+sYVcbFu1mfqwMtI3Vuscsc=";
        public static async Task AddReportedPropertiesAsync(string deviceName, ReportedProperties properties)
        {
            if (string.IsNullOrEmpty(deviceName))
            {
                throw new ArgumentNullException("valid device name please");
            }
            else
            {
                client = DeviceClient.CreateFromConnectionString(myDeviceConnection, Microsoft.Azure.Devices.Client.TransportType.Mqtt);
                TwinCollection reportedProperties, connectivity;
                reportedProperties = new TwinCollection();
                connectivity = new TwinCollection();
                reportedProperties["type"] = "cellular";
                reportedProperties["connectivity"] = "connectivity";
                reportedProperties["temprature"] = properties.temprature;
                reportedProperties["pressure"] = properties.pressure;
                reportedProperties["accuracy"] = properties.accuracy;
                reportedProperties["dateTimeLastAppLaunch"] = properties.dateTimeLastAppLaunch;
                await client.UpdateReportedPropertiesAsync(reportedProperties);
                return;

            }
        }
        public static async Task DesiredPropertyUpdate(string deviceName)
        {
            client = DeviceClient.CreateFromConnectionString(myDeviceConnection, Microsoft.Azure.Devices.Client.TransportType.Mqtt);
            var device = await registryManager.GetTwinAsync(deviceName);
            TwinCollection desiredProperties, telemantryconfig;
            desiredProperties = new TwinCollection();
            telemantryconfig = new TwinCollection();
            telemantryconfig["frequency"] = "5Hz";
            desiredProperties["telementaryconfig"] = telemantryconfig;
            device.Properties.Desired["telemantryconfig"] = telemantryconfig;
            await registryManager.UpdateTwinAsync(device.DeviceId, device, device.ETag);
            return;



        }
        public static async Task<Twin> GetDevicePropertiesAsync(string deviceName)
        {
            var device = await registryManager.GetTwinAsync(deviceName);
            return device;
        }
        public static async Task UpdateDeviceTagPropertiesAsync(string deviceName)
        {
            if (string.IsNullOrEmpty(deviceName))
            {
                throw new ArgumentNullException("valid device name please");
            }
            else
            {
                var twin = await registryManager.GetTwinAsync(deviceName);
                var patchData =
                 @"{
                   tags:{
                        location:{
                            region:'Canada',
                            plant:'IOTPro'
                            }
                       }
                  }";
                client = DeviceClient.CreateFromConnectionString(myDeviceConnection, Microsoft.Azure.Devices.Client.TransportType.Mqtt);
                TwinCollection connectivity;
                connectivity = new TwinCollection();
                connectivity["type"] = "cellular";
                await registryManager.UpdateTwinAsync(twin.DeviceId, patchData, twin.ETag);
                return;
            }

        }
    }
}