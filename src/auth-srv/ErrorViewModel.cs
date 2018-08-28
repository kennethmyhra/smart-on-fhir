namespace EHR.AuthorizationServer
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public string Error { get; set; }

        public string ErrorDescription { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
