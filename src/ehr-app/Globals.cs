namespace EHRApp
{
    public class Globals
    {
        public static ApplicationSettings ApplicationSettings { get; internal set; }
        public static SmartAppSettings SmartAppSettings { get; internal set; }
        
        public static SmartApplication GetSmartApplicationSettings(string key)
        {
            foreach(SmartApplication settings in SmartAppSettings.SmartApplications)
            {
                if(settings.Key == key)
                {
                    return settings;
                }
            }

            return null;
        }
    }
}
