using DigichList.Core.Entities.Base;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DigichList.Core.Entities
{
    public class Admin : Entity
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public AccessLevels AccessLevel { get; set; }

        public enum AccessLevels
        {
            Admin = 1,
            SuperAdmin
        }
    }
}
