﻿using System.Windows;

namespace Braces.Core.DTOs
{
    public class FireEventDTO : IDTO
    {
        public string eventName { get; set; }

        public string fileTypeName { get; set; }

        public object args { get; set; }
    }
}