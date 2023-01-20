using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Devices.Shared;
using IotHubDevice.DTO;

using System.Threading.Tasks;

namespace IotHubDevice.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("[controller]")]
    public class IotDevicePropertiesController : Controller
    {
        [Microsoft.AspNetCore.Mvc.HttpPut("UpdateDeviceReportedProperties")]

        public async Task<string> UpdateDeviceReportedProperties(string deviceName,ReportedProperties properties)
        {
            await repository.IotDeviceProperties.AddReportedPropertiesAsync(deviceName,properties);
            return "";
        }
        [Microsoft.AspNetCore.Mvc.HttpPut("UpdateDeviceDesiredProperties")]
        public async Task<string> UpdateDeviceDesiredProperties(string deviceName)
        {
            await repository.IotDeviceProperties.DesiredPropertyUpdate(deviceName);
            return "";
        }
        [Microsoft.AspNetCore.Mvc.HttpPut("UpdateDeviceTagProperties")]
        public async Task<string> UpdateDeviceTagProperties(string deviceName)
        {
            await repository.IotDeviceProperties.UpdateDeviceTagPropertiesAsync(deviceName);
            return "";
        }
        [Microsoft.AspNetCore.Mvc.HttpGet("GetIotDeviceProperties")]
        public async Task<Twin> GetIotDevice(String deviceName)
        {
            var device = await repository.IotDeviceProperties.GetDevicePropertiesAsync(deviceName);
            return device;
        }
    }
}