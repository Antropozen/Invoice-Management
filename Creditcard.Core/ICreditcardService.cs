﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Creditcard.Core
{
    public interface ICreditcardService
    {
        List<Creditcard> GetCreditcards();
        Creditcard AddCreditcard(Creditcard creditcard);

    }
}
