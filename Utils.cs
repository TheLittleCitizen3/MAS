using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAS
{
    public static class Utils
    {
        public static void RunEventAsync(Delegate delegates, IAuctionSale auction)
        {
            object obj = auction;
            delegates.GetInvocationList().AsParallel().ForAll(a => a?.DynamicInvoke(obj));
        }


    }
}

