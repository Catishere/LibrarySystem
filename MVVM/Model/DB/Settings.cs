﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.MVVM.Model.DB
{
    public class Settings
    {
        [Key] 
        public int SettingsId { get; set; }
        public int SuggestionsCount { get; set; }

        public Settings()
        {
        }
    }


}
