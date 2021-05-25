using System;

namespace ShowMeTheMoney.Events
{
    public class Navigate : EventArgs
    {
        public string Destination;

        public Navigate(string destination)
        {
            Destination = destination;
        }
    }
}