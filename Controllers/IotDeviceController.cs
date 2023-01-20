using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Devices;
using IotHubDevice.DTO;
using IotHubDevice.repository;

namespace IotHubDevice.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("[controller]")]
    public class IotDeviceController : Controller
    {
        
        [Microsoft.AspNetCore.Mvc.HttpPost("AddDevice")]
        public async Task<string> AddDevice(string deviceName)
        {
            await IotHubDevice.repository.IotDevice.AddDeviceAsync(deviceName);
            return "";
        }

        [Microsoft.AspNetCore.Mvc.HttpGet("GetDevice")]
       
        public async Task<Device> GetDevice(string deviceId) 
        {
            Device device;
            device = await repository.IotDevice.GetDeviceAsync(deviceId);
            return device;
        }
        [Microsoft.AspNetCore.Mvc.HttpDelete("RemoveDevice")]
       
        public async Task<string> RemoveDevice(string deviceId)
        {
            await repository.IotDevice.RemoveDeviceAsync(deviceId);
            return "";
        }
        [Microsoft.AspNetCore.Mvc.HttpPut("UpdateDevice")]
    
        public async Task<Device> UpdateDevice(string deviceId)
        {
            Device device;
            device = await repository.IotDevice.UpdateDeviceAsync(deviceId);
            return device;
        }


    }
}