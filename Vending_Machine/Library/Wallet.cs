﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public abstract class Wallet
    {
        public int Id { get; set;}
        public int CoinValue { get; set; }

        public int Quantity { get; set; }

    }
}
