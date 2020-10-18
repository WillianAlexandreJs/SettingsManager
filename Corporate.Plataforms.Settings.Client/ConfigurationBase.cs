using ND.Util.Communication.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Corporate.Plataforms.Settings.Client
{
    public abstract class ConfigurationBase : INotifyPropertyChanged
    {
        protected CommunicationManager _communication;
        protected IDbConnection _dbConnection;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void SetPropertyField<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, newValue))
            {
                field = newValue;
                OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
            }
        }

        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        protected void DataBaseConnectionStringChanged(string value)
        {
            if (_dbConnection == null)
                return;

            while (_dbConnection.State == ConnectionState.Open)
            {
                _dbConnection.Close();
                _dbConnection.ConnectionString = value;
                _dbConnection.Open();
                return;
            }

        }

        protected void NidiuConnectionQueueChanged(CommunicationManager communication)
        {
            if (communication.IsOpen)
                communication.Close();

            communication.Open();
        }

        protected virtual void RaizeActionOnPropertyChange(PropertyChangedEventArgs e)
        {

            //if(e.GetType().GetCustomAttributes(true) != null)
            //    DataBaseConnectionStringChanged(e)

        }
    }
}
