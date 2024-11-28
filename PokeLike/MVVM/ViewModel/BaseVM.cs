﻿using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeLike.MVVM.ViewModel
{
    public class BaseVM : ObservableObject
    {

        public virtual void OnShowVM() { }
        public virtual void OnHideVM() { }


    }
}