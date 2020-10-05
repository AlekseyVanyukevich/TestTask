using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using AutoMapper.Configuration;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TestTask.Domain.Options;

namespace TestTask.Domain.Services
{
    public class FirebaseService : IFirebaseService
    {
        private readonly FirebaseMessaging _messaging;
        private readonly List<string> _tokens;
        private readonly ILogger<FirebaseService> _logger;

        public FirebaseService(IOptions<FirebaseOptions> options, ILogger<FirebaseService> logger)
        {
            var app = FirebaseApp.Create(new AppOptions
            {
                Credential = GoogleCredential.FromFile(options.Value.JsonPath)
            });
            _messaging = FirebaseMessaging.GetMessaging(app);
            _tokens = new List<string>();
            _logger = logger;
        }
        public async Task SendNotifications(string title, string body)
        {
            //
            // var r = await _messaging
            //     .SendAllAsync(
            //         _tokens.Select(t => CreateMessage(title, body, t)
            //         ));
            // _logger.LogInformation(r.SuccessCount.ToString());
        }

        public async Task SendNotification(string title, string body, string token)
        {
            var message = CreateMessage(title, body, token);
            _logger.LogInformation(message.Token);
            Thread.Sleep(1000);
            var res = await _messaging.SendAsync(message).ConfigureAwait(false);
            _logger.LogInformation(res);
        }

        public void AddToken(string token)
        {
            _tokens.Add(token);
        }

        private Message CreateMessage(string title, string body, string token)
        {
            return new Message
            {
                Notification = new Notification
                {
                    Body = body,
                    Title = title
                },
                Token = token,
            };
        }
    }
}