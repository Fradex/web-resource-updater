﻿using System;

namespace WebPackUpdater.Model
{
    public class User
    {
        public Guid Id { get; set; }

        public string Login { get; set; }

        public string PasswordHash { get; set; }
    }
}