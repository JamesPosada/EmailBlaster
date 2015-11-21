using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailBlaster
{
    public enum FrxContactType
    {

        Retail = 1,
        Preferred = 2,
        Distributor = 3,
        BusinessBuilder = 4,
        Prospect = 10
    }

    public enum MarketingListType
    {
        AllLists = 1,
        TurnerOnly = 2,
        FREZZOROnly = 3,
        DoNotSend = 4,
        All = 100000000
    }


}