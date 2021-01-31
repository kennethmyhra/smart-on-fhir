namespace SmartDemo.AuthServer
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
        // Stores the generated code so that we can associate it with 
        // this Launch Context when the token endpoint is called
        public string Code { get; set; }
    }
}
