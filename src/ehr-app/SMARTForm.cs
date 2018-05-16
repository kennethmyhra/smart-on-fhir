using CefSharp;
using CefSharp.WinForms;
using System;
using System.Windows.Forms;

namespace EHRApp
{
    public partial class SMARTForm : Form
    {
        private ChromiumWebBrowser _browser;

        public SMARTForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            _browser = new ChromiumWebBrowser("about:blank")
            {
                Dock = DockStyle.Fill
            };
            Controls.Add(_browser);
            
            _browser.ConsoleMessage += OnBrowserConsoleMessage;
            _browser.StatusMessage += OnBrowserStatusMessage;
            _browser.TitleChanged += OnBrowserTitleChanged;
            _browser.AddressChanged += OnBrowserAddressChanged;
            _browser.LoadingStateChanged += OnBrowserLoadingStateChanged;

            string bitness = Environment.Is64BitProcess ? "x64" : "x86";
            string version = $"Chromium: {Cef.ChromiumVersion}, CEF: {Cef.CefVersion}, Environment: {Cef.CefSharpVersion}";
            GetMdiParent().DisplayOutput(version);
        }

        private void OnBrowserLoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {
            GetMdiParent().DisplayOutput(e.IsLoading ? "Loading" : "Ready");
        }

        public MDIParent GetMdiParent()
        {
            return (MDIParent)MdiParent;
        }

        public void LoadSmartApp(SmartApplication application, string fhirBaseUrl, string patientId)
        {
            _browser.Load($"{application.Url}?fhirServiceUrl={fhirBaseUrl}&patientId={patientId}");
        }
        
        private void OnBrowserAddressChanged(object sender, AddressChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Address)) return;

            GetMdiParent().DisplayOutput(e.Address);
        }

        private void OnBrowserTitleChanged(object sender, TitleChangedEventArgs e)
        {
            this.InvokeOnUiThreadIfRequired(() => Text = e.Title);
        }

        private void OnBrowserStatusMessage(object sender, StatusMessageEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Value)) return;

            GetMdiParent().DisplayOutput(e.Value);
        }

        private void OnBrowserConsoleMessage(object sender, ConsoleMessageEventArgs e)
        {
            GetMdiParent().DisplayOutput($"Line: {e.Line}, Source: {e.Source}, Message: {e.Message}");
        }
    }
}
