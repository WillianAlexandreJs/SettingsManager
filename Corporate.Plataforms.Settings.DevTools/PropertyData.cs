namespace Corporate.Plataforms.Settings.DevTools
{
    public class PropertyData
    {
        public string Instance { get; set; }
        public string Property { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public string Reference { get; set; }
        public ReferenceType ReferenceType { get; set; }
    }

    public enum ReferenceType
    {
        Setting,
        Library,
        Application

    }
}
