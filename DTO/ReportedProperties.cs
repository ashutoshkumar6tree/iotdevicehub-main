using System;
namespace IotHubDevice.DTO
{
    public class ReportedProperties
    {
        public string? temprature { get; set; }
        public string? pressure { get; set; }
        public string? accuracy { get; set; }
        public string? dateTimeLastAppLaunch { get; set; }

    }
}