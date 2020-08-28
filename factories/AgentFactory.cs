using MAS.agent;
using System;

namespace MAS.factories
{
    class AgentFactory
    {
        public IAgent GetAgent()
        {
            Random r = new Random();
            decimal moneyBalance = (decimal)r.Next(1, 100);
            return new Agent(moneyBalance);
        }
    }
}
