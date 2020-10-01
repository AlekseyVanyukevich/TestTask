using System;
using System.IO;
using System.Threading.Tasks;
using AutoMapper.Configuration;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Options;
using TestTask.Domain.Options;

namespace TestTask.Domain.Services
{
    public class FirebaseService : IFirebaseService
    {
        private readonly FirebaseOptions _options;

        public FirebaseService(IOptions<FirebaseOptions> options)
        {
            _options = options.Value;
        }
        public async Task SendNotifications(string title, string body, string topic)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GoogleFirebase.json");
            var defaultApp = FirebaseApp.Create(new AppOptions
            {
                Credential = GoogleCredential.FromFile(path)
            });

            var fcm = FirebaseAdmin.Messaging.FirebaseMessaging.GetMessaging(defaultApp);
            Message message = new Message
            {
                Notification = new Notification
                {
                    Title = title,
                    Body = body
                },
                Topic = topic
            };
            var result = await fcm.SendAsync(message);
        }
    }
}