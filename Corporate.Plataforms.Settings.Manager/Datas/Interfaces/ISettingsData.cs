using Corporate.Plataforms.Settings.Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Corporate.Plataforms.Settings.Manager.Datas.Interfaces
{
    public interface ISettingsData
    {
        List<Instance> GetInstances();
        List<Property> GetProperties();
        List<Integration> GetIntegrations();
    }
}
