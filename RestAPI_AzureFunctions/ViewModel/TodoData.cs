﻿using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;

namespace RestAPI_AzureFunctions.ViewModel
{
    public class TodoData
    {
        public string TaskName { get; set; } = null!;
        public string Username { get; set; } = null!;
        public bool IsDone { get; set; } = false;
    }
}
