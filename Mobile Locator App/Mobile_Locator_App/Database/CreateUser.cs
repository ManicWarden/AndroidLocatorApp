﻿using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;
using StackExchange.Redis;

namespace Mobile_Locator_App.Database
{
    // use redis HMSET to create a user object with the unique ID
    // username
    class CreateUser : UntypedActor
    {
        public const string StartCommand = "start";
        public const string ExitCommand = "exit";
        private readonly IActorRef _createUserActor;
        private readonly string _username;
        private readonly string _password;

        public CreateUser(IActorRef createUserActor, string username, string password)
        {
            _createUserActor = createUserActor;
            _username = username;
            _password = password;
            Console.Write("**************************Create User triggered");
        }


        protected override void OnReceive(object message)
        {
            if(message.Equals(StartCommand))
            {
                // method call
            }

        }
    }
}
