using Corporate.Plataforms.Settings.Model;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Corporate.Plataforms.Settings.Client
{
    public class ApplicationConfigHub<T>
    {
        #region Properties

        protected T Configuration { get; set; }
        protected string ApplicationId { get; private set; }
        protected string ConnectionId { get; private set; }
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

        public void StartHubConnection()
        {
            StartHub(HubConnectionConfig.Settings.HubConnectionUrl,
                     HubConnectionConfig.Settings.HubProxyName,
                     HubConnectionConfig.Settings.ApplicationName,
                     HubConnectionConfig.Settings.ApplicationId,
                     HubConnectionConfig.Settings.KeepAliveInterval);
        }

        public void StartHubConnection(string hubConnectionUrl, string hubProxyName, string applicationName, string applicationId, TimeSpan keepAliveInterval)
        {
            StartHub(hubConnectionUrl, hubProxyName, applicationName, applicationId, keepAliveInterval);
        }

        private void StartHub(string hubConnectionUrl, string hubProxyName, string applicationName, string applicationId, TimeSpan keepAliveInterval)
        {
            try
            {
                Thread.Sleep(TimeSpan.FromSeconds(100));

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
                            _hubConnection.KeepAliveInterval = keepAliveInterval;
                            _hubConnection.On("Closed", HubConnection_Closed);
                            _hubConnection.On("Reconnecting", HubConnection_Reconnecting);
                            _hubConnection.On("Reconnected", HubConnection_ReconnectedAsync);
                            _hubConnection.On<PropertyData>("UpdateInstanceSettings", UpdateSettingApplication);
                            _hubConnection.InvokeAsync<List<PropertyData>>("GetInstanceSettings", applicationId)
                            .ContinueWith(InitSettings =>
                            {
                                if (task.IsFaulted)
                                {
                                    Console.WriteLine("There was an error opening the connection:{0}", task.Exception.GetBaseException());
                                }
                                else
                                {
                                    InitSettingApplication(InitSettings.Result);
                                }

                            }).Wait();
                        }

                    }).Wait();

                ApplicationId = applicationId;
                ConnectionId = _hubConnection.ConnectionId;
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

        protected virtual async void HubConnection_Closed()
        {
            Console.WriteLine($"_hubConnection_Closed New State: {_hubConnection.State} {_hubConnection.ConnectionId}");
        }

        protected virtual async void HubConnection_Reconnecting()
        {
            Console.WriteLine($"_hubConnection_Reconnecting New State: {_hubConnection.State} {_hubConnection.ConnectionId}");
            
        }

        protected virtual async void HubConnection_ReconnectedAsync()
        {
            ConnectionId = _hubConnection.ConnectionId;
            InitSettingApplication(await _hubConnection.InvokeAsync<List<PropertyData>>("GetInstanceSettings", ApplicationId));
        }

        #endregion

        #region "Privete Methods"

        protected virtual void InitSettingApplication(List<PropertyData> settings)
        {
            try
            {
                foreach (var itemConfig in settings)
                {
                    UpdateSettingApplication(itemConfig);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        protected virtual void UpdateSettingApplication(PropertyData itemConfig)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(itemConfig.SettingReference))
                {
                    object nestedProperty = Configuration.GetType().GetProperty(itemConfig.SettingReference).GetValue(Configuration);

                    if (nestedProperty == null)
                    {
                        Console.WriteLine($"Referencia {itemConfig.PropertyName} não encontrada");
                        return;
                    }

                    UpdatePropertyValue(nestedProperty, itemConfig);
                }
                else
                {
                    UpdatePropertyValue(Configuration, itemConfig);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        protected virtual void UpdatePropertyValue(object obj, PropertyData itemConfig)
        {
            PropertyInfo propertyInfo;
            propertyInfo = obj.GetType().GetProperty(itemConfig.PropertyName);

            if (propertyInfo == null)
            {
                Console.WriteLine($"Propriedade {itemConfig.PropertyName} não encontrada");
                return;
            }
            
            if(propertyInfo.GetValue(obj)?.ToString() != itemConfig.PropertyValue)
                propertyInfo.SetValue(obj, Convert.ChangeType(itemConfig.PropertyValue, propertyInfo.PropertyType), null);
                
        }

        #endregion







    }
}
