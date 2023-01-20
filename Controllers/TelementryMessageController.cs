using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using IotHubDevice.repository;
using System.Threading.Tasks;

namespace IotHubDevice.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("[controller]")]
    public class TelementryMessageController : Controller
    {
        [Microsoft.AspNetCore.Mvc.HttpPost("SendTelementaryMessage")]
        public async Task<string> SendMessage(string deviceName)
        {
            await IotHubDevice.repository.TelementaryMessage.SendMessage(deviceName);
            return "";
        }
    }
}