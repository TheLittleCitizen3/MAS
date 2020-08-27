using MAS.agent;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAS.factories
{
    class AgentFactory
    {
        public IAgent GetAgent()
        {
            Random r = new Random();
            decimal moneyBalance = (decimal)r.Next(500000, 5000000);
            return new Agent(moneyBalance);
        }
    }
}
