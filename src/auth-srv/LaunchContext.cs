using System;

namespace EHR.AuthorizationServer
{
    public class LaunchContext
    {
        // Required
        public string Id { get; set; }
        // Required
        public string ClientId { get; set; }
        // Required
        public string Patient { get; set; }
        // Optional
        public string Practitioner { get; set; }
        // Optional
        public string Encounter { get; set; }
    }
}
