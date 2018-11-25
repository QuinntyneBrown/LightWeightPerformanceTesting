using LightWeightPerformanceTesting.Core.Common;
using LightWeightPerformanceTesting.Core.DomainEvents;
using System;

namespace LightWeightPerformanceTesting.Core.Models
{
    public class User: Entity
    {
        public User(Guid userId, string username = null, byte[] salt= null, string password = null) 
            => Apply(new UserCreated()
            {
                UserId = userId,
                Username = username,
                Password = password,
                Salt = salt
            });

        protected override void When(object @event)
        {
            switch (@event)
            {
                case UserCreated data:
                    UserId = data.UserId;
                    Username = data.Username;
                    Salt = data.Salt;
                    Password = data.Password;
                    break;
            }            
        }

        protected override void EnsureValidState()
        {

        }

        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public byte[] Salt { get; private set; }
    }
}
