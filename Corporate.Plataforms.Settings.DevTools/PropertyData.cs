namespace Corporate.Plataforms.Settings.DevTools
{
    public class PropertyDetails
    {
        public PropertyDetails()
        {
            ReferenceType = EReferenceType.Setting;
        }

        public string Instance { get; set; }
        public string Property { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public string Reference { get; set; }
        public EReferenceType ReferenceType { get; set; }
    }

    public enum EReferenceType
    {
        Setting = 1,
        Library,
        Application

    }
}
