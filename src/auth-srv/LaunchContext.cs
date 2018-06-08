using System;

namespace EHR.AuthorizationServer
{
    public class LaunchContext
    {
        public string Id { get; set; }
        public string ClientId { get; set; }
        public string Patient { get; set; }
        public string Practitioner { get; set; }
        public string Encounter { get; set; }
    }
}
