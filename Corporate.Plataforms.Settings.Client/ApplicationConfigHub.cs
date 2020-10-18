using Corporate.Plataforms.Settings.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Corporate.Plataforms.Settings.Client
{
    public class ApplicationConfigHub<T>
    {
        #region Properties

        private readonly T _configuration;
        protected HubConnection _hubConnection;
        protected LoggerManager loggerManager;

        #endregion

        #region Methods

        public HubConnectionState State
        {
            get { return _hubConnection.State; }
        }

        public string GetConnectionId()
        {
            return _hubConnection.ConnectionId;
        }

        #endregion

        public ApplicationConfigHub(T configuration)
        {
            _configuration = configuration;
        }

        public void StartHubConnection()
        {
            StartHub(HubConnectionConfig.Settings.HubConnectionUrl,
                     HubConnectionConfig.Settings.HubProxyName,
                     HubConnectionConfig.Settings.ApplicationName,
                     HubConnectionConfig.Settings.ApplicationId,
                     HubConnectionConfig.Settings.KeepAliveInterval,
                     UpdateSettingApplication);
        }

        public void StartHubConnection(string hubConnectionUrl, string hubProxyName, string applicationName, string applicationId, TimeSpan keepAliveInterval)
        {
            StartHub(hubConnectionUrl, hubProxyName, applicationName, applicationId, keepAliveInterval, UpdateSettingApplication);
        }

        public void StartHubConnection(string hubConnectionUrl, string hubProxyName, string applicationName, string applicationId, TimeSpan keepAliveInterval, Action<ItemConfiguracao> updateSettingApplication)
        {
            StartHub(hubConnectionUrl, hubProxyName, applicationName, applicationId, keepAliveInterval, updateSettingApplication);
        }

        private void StartHub(string hubConnectionUrl, string hubProxyName, string applicationName, string applicationId, TimeSpan keepAliveInterval, Action<ItemConfiguracao> actionUpdateSettingApplication)
        {
            try
            {
                _hubConnection = new HubConnectionBuilder()
                  .WithUrl(new Uri(new Uri(hubConnectionUrl), hubProxyName), options =>
                  {
                      options.AccessTokenProvider = () => Task.FromResult(GenerateToken(applicationName, applicationId));
                  })
                  .WithAutomaticReconnect()
                  .Build();

                _hubConnection.StartAsync()
                    .ContinueWith(task =>
                    {
                        if (task.IsFaulted)
                        {
                            Console.WriteLine("There was an error opening the connection:{0}", task.Exception.GetBaseException());
                        }
                        else
                        {
                            _hubConnection.On("Closed", HubConnection_Closed);
                            _hubConnection.On("Reconnecting", HubConnection_Reconnecting);
                            _hubConnection.On("Reconnected", HubConnection_Reconnected);
                            _hubConnection.KeepAliveInterval = keepAliveInterval;
                            _hubConnection.On<ItemConfiguracao>("UpdateApplicationConfiguration", actionUpdateSettingApplication);

                        }
                    })
                    .Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }


        private string GenerateToken(string applicationName, string applicationId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = "HubConfigManager",
                Audience = "HubConfigManager",
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, applicationName),
                    new Claim(ClaimTypes.NameIdentifier, applicationId),
                }),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes("Y2F0Y2hlciUyMHdvbmclMjBsb3ZlJTIwLm5ldA==")), SecurityAlgorithms.HmacSha256Signature)
            };


            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(stoken);

            return token;
        }

        public void CloseHub()
        {
            _hubConnection.StopAsync();
            _hubConnection.DisposeAsync();
        }

        #region Events

        protected virtual void HubConnection_Closed()
        {
            Console.WriteLine("_hubConnection_Closed New State:" + _hubConnection.State + " " + _hubConnection.ConnectionId);
        }

        protected virtual void HubConnection_Reconnecting()
        {
            Console.WriteLine("_hubConnection_Reconnecting New State:" + _hubConnection.State + " " + _hubConnection.ConnectionId);
        }

        protected virtual void HubConnection_Reconnected()
        {
            Console.WriteLine("_hubConnection_Reconnected New State:" + _hubConnection.State + " " + _hubConnection.ConnectionId);
        }

        public void UpdateSettingApplication(ItemConfiguracao itemConfig)
        {
            try
            {
                PropertyInfo propertyInfo = _configuration.GetType().GetProperty(itemConfig.Nome);
                if (propertyInfo != null)
                    propertyInfo.SetValue(_configuration, Convert.ChangeType(itemConfig.Valor, propertyInfo.PropertyType), null);
                else
                    Console.WriteLine($"Propriedade {itemConfig.Nome} não encontrada");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #endregion







    }
}
