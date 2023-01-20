using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;

namespace IotHubDevice.repository
{
    public class IotDevice
    {
        static RegistryManager? registryManager;
        static string connectionString = "HostName=joshiIotDevice.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=4jnu7vdM7hr5J2wproAe8fTESWbOIK/H4Z0feFNYetM=";
        //static Device device;
        public static async Task AddDeviceAsync(string deviceId)
        {
            Device device;
            if (string.IsNullOrEmpty(deviceId))
            {
                throw new ArgumentNullException("correct device id please");
            }
            registryManager = RegistryManager.CreateFromConnectionString(connectionString);
            device = await registryManager.AddDeviceAsync(new Device(deviceId));
            return;
        }
        public static async Task<Device> GetDeviceAsync(string deviceId)
        {
            Device device;
            registryManager = RegistryManager.CreateFromConnectionString(connectionString);
            device = await registryManager.GetDeviceAsync(deviceId);
            return device;
        }
        public static async Task RemoveDeviceAsync(string deviceId)
        {
            registryManager = RegistryManager.CreateFromConnectionString(connectionString);
            await registryManager.RemoveDeviceAsync(deviceId);

        }
        public static async Task<Device> UpdateDeviceAsync(string deviceId)
        {
            if (string.IsNullOrEmpty(deviceId))
            {
                throw new ArgumentNullException("device id please");
            }
            Device device = new Device(deviceId);
            registryManager = RegistryManager.CreateFromConnectionString(connectionString);
            device = await registryManager.GetDeviceAsync(deviceId);
            device.StatusReason = "Updated";
            device = await registryManager.UpdateDeviceAsync(device);
            return device;

        }

    }
}
