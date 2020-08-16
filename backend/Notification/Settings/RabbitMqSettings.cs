using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notification.Settings
{
    public class RabbitMqSettings : IRabbitMqSettings
    {
        public string Url { get; set; }
    }

    public interface IRabbitMqSettings
    {
        string Url { get; set; }
    }
}
