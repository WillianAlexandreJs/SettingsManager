using Corporate.Plataforms.Settings.Model;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Corporate.Plataforms.Settings.Client
{
    public class ApplicationConfigHub<T> : IDisposable
    {
        #region Properties

        private HubConnection _hubConnection;
        protected LoggerManager loggerManager { get; private set; }

        protected T Configuration { get; set; }
        protected string ApplicationId { get; private set; }
        protected string ConnectionId { get; private set; }

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

        public string GetConnectionState()
        {
            return _hubConnection.State.ToString();
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
                _hubConnection = new HubConnectionBuilder()
                  .WithUrl(new Uri(new Uri(hubConnectionUrl), hubProxyName), options =>
                  {
                      options.AccessTokenProvider = () => Task.FromResult(GenerateToken(applicationName, applicationId));
                  })
                  .WithAutomaticReconnect(new TimeSpan[] { TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(30), TimeSpan.FromMinutes(1), TimeSpan.FromSeconds(5) })
                  .Build();

                _hubConnection.StartAsync()
                    .ContinueWith(task =>
                    {
                        if (task.IsFaulted)
                        {
                            GetLocalSettings(task.Exception.GetBaseException());
                        }
                        else
                        {
                            _hubConnection.KeepAliveInterval = keepAliveInterval;
                            _hubConnection.Closed += HubConnection_Closed;
                            _hubConnection.Reconnecting += HubConnection_Reconnecting;
                            _hubConnection.Reconnected += HubConnection_Reconnected;
                            _hubConnection.On<PropertyData>("UpdateInstanceSettings", UpdateSettingApplication);
                            _hubConnection.InvokeAsync<List<PropertyData>>("GetInstanceSettings", applicationId)
                            .ContinueWith(InitSettings =>
                            {
                                if (task.IsFaulted)
                                {
                                    GetLocalSettings(task.Exception.GetBaseException());
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

        protected virtual async Task HubConnection_Closed(Exception exception)
        {
            Console.WriteLine($"_hubConnection_Closed New State: {_hubConnection.State} {_hubConnection.ConnectionId} {exception.Message}");
        }

        protected virtual async Task HubConnection_Reconnecting(Exception exception)
        {
            Console.WriteLine($"_hubConnection_Reconnecting New State: {_hubConnection.State} {_hubConnection.ConnectionId}");

        }

        protected virtual async Task HubConnection_Reconnected(string connectionId)
        {
            ConnectionId = connectionId;
            InitSettingApplication(await _hubConnection.InvokeAsync<List<PropertyData>>("GetInstanceSettings", ApplicationId));
        }

        #endregion

        #region "Protected virtual Methods"

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

            if (propertyInfo.GetValue(obj)?.ToString() != itemConfig.PropertyValue)
                propertyInfo.SetValue(obj, Convert.ChangeType(itemConfig.PropertyValue, propertyInfo.PropertyType), null);

        }

        #endregion

        #region "private Methods"

        private void GetLocalSettings(Exception ex)
        {
            if (!File.Exists(GetFileName()))
                return;

            Configuration = JsonConvert.DeserializeObject<T>(File.ReadAllText(GetFileName()));
            Console.WriteLine("There was an error opening the connection:{0}", ex);
        }

        private void CreateSettingBackup()
        {
            if (File.Exists(GetFileName()))
                File.Delete(GetFileName());

            File.WriteAllText(GetFileName(), JsonConvert.SerializeObject(Configuration));
        }

        private string GetFileName()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"settings.json");
        }

        public void Dispose()
        {
            CreateSettingBackup();
        }

        #endregion

    }
}
