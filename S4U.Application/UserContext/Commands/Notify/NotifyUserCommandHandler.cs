using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using S4U.Domain.Entities;
using S4U.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace S4U.Application.UserContext.Commands.Notify
{
    public class NotifyUserCommandHandler : IRequestHandler<NotifyUserCommand, bool>
    {
        private readonly SqlContext _context;
        private readonly IHostingEnvironment _env;
        private FirebaseApp _app;

        public NotifyUserCommandHandler(SqlContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;

            try
            {
                _app = FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile(_env.ContentRootPath + "\\firebase-sdk.json"),
                    ProjectId = "stock4u-f97f2"
                }, "Stock4U_API");
            }
            catch(Exception ex)
            {
                var _erro = ex.Message;
                _app = FirebaseApp.GetInstance("Stock4U_API");
            }
        }

        public async Task<bool> Handle(NotifyUserCommand request, CancellationToken cancellationToken)
        {
            var _messaging = FirebaseMessaging.GetMessaging(_app);

            var _user = await _context.Set<User>()
                                      .Where(e => e.Id == request.UserID)
                                      .FirstOrDefaultAsync();

            if (!string.IsNullOrEmpty(_user.PushToken))
            {
                var _data = new Dictionary<string, string>();
                _data.Add("click_app", "FLUTTER_NOTIFICATION_CLICK");
                if (request.RedirectID.HasValue) _data.Add("redirectionID", request.RedirectID.Value.ToString());

                var _push = new Message()
                {
                    Notification = new FirebaseAdmin.Messaging.Notification
                    {
                        Title = request.Title,
                        Body = request.Body
                    },
                    Token = _user.PushToken,
                    Data = _data
                };

                var _result = await _messaging.SendAsync(_push);

                var _id = Guid.NewGuid();

                await _context.Notifications.AddAsync(new Domain.Entities.Notification
                {
                    Id = Guid.NewGuid(),
                    Title = request.Title,
                    Body = request.Body,
                    RedirectID = request.RedirectID,
                    UserID = request.UserID
                }, cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);
            }

            return true;
        }
    }
}