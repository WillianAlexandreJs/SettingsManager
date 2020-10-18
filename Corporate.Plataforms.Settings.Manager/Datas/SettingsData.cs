using Corporate.Plataforms.Settings.Manager.Datas.Interfaces;
using Corporate.Plataforms.Settings.Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Corporate.Plataforms.Settings.Manager.Datas
{
    public class SettingsData : ISettingsData
    {
        public List<Instance> GetInstances()
        {
            throw new NotImplementedException();
        }

        public List<Integration> GetIntegrations()
        {
            throw new NotImplementedException();
        }

        public List<Property> GetProperties()
        {
            throw new NotImplementedException();
        }
    }
}
