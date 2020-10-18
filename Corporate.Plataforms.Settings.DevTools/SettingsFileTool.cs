using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Corporate.Plataforms.Settings.DevTools
{
    public class SettingsFileTool
    {

        private string _applicationName;
        private string _folderPath;
        Dictionary<string, XmlDocument> _configFiles = new Dictionary<string, XmlDocument>();
        List<PropertyData> lstPropertyData = new List<PropertyData>();

        public SettingsFileTool(string applicationName, string folderPath, string extension)
        {
            _applicationName = applicationName;
            ReadDocumentFile(folderPath, extension);
        }

        private void ReadDocumentFile(string folderPath, string extension)
        {
            _folderPath = folderPath;
            foreach (var filePath in Directory.GetFiles(folderPath, extension, SearchOption.TopDirectoryOnly))
            {
                FileInfo fileInfo = new FileInfo(filePath);
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(fileInfo.FullName);
                _configFiles.Add(fileInfo.Name.Replace(fileInfo.Extension, string.Empty), xmlDocument);
            }
        }

        public List<PropertyData> GetProperties()
        {
            foreach (var file in _configFiles)
            {
                lstPropertyData.AddRange(ReadSections(file.Key, file.Value));
                lstPropertyData.AddRange(ReadAppSeetings(file.Key, file.Value));
            }

            return lstPropertyData;
        }

        private List<PropertyData> ReadSections(string instance, XmlDocument xmlDocument)
        {
            List<PropertyData> lstPropertyData = new List<PropertyData>();

            XmlNode configSectionsNode = FindNode(xmlDocument.DocumentElement.ChildNodes, "configSections");

            foreach (XmlNode childNode in configSectionsNode.ChildNodes)
            {
                if (childNode.Name.Equals("section"))
                {
                    lstPropertyData.AddRange(GetSettingNameAndValueFromSettings(instance, childNode.Attributes.GetNamedItem("name").Value, xmlDocument.DocumentElement.ChildNodes));
                }
                else if (childNode.Name.Equals("sectionGroup"))
                {
                    XmlNode sessionGroup = FindNode(xmlDocument.DocumentElement.ChildNodes, childNode.Attributes.GetNamedItem("name").Value);

                    foreach (XmlNode childGrupNode in childNode.ChildNodes)
                    {
                        if (childGrupNode.Name.Equals("section"))
                        {
                            lstPropertyData.AddRange(GetSettingNameAndValueFromSettings(instance, childGrupNode.Attributes.GetNamedItem("name").Value, sessionGroup.ChildNodes));
                        }
                    }
                }
            }

            return lstPropertyData;
        }

        private List<PropertyData> ReadAppSeetings(string instance, XmlDocument xmlDocument)
        {
            List<PropertyData> lstPropertyData = new List<PropertyData>();

            XmlNode AppSeetingsNode = FindNode(xmlDocument.DocumentElement.ChildNodes, "AppSeetings");

            if (AppSeetingsNode != null)
                lstPropertyData.AddRange(GetSettingNameAndValueFromSettings(instance, "AppSeetings", AppSeetingsNode.ChildNodes));
            
            return lstPropertyData;
        }

        public void CreateClassesFiles()
        {
            string folderClasses = Path.Combine(_folderPath, "Classes");

            if (!Directory.Exists(folderClasses))
                Directory.CreateDirectory(folderClasses);

            foreach (var propertiesReference in lstPropertyData.GroupBy(x => x.Reference))
            {
                StringBuilder classCommand = new StringBuilder();

                classCommand.AppendLine($"namespace {_applicationName}");
                classCommand.AppendLine(@"{");
                classCommand.AppendLine($"  public class {propertiesReference.Key}");
                classCommand.AppendLine(@"  {");

                foreach (var propertyData in propertiesReference)
                {
                    classCommand.AppendLine($@"     public {propertyData.Type} {propertyData.Property} {{ get; set; }}");
                }

                classCommand.AppendLine(@"  }");
                classCommand.AppendLine(@"}");

                File.WriteAllText(Path.Combine(folderClasses, $"{propertiesReference.Key}.cs"), classCommand.ToString());
            }
        }

        public void CreateInsertCommands()
        {
            string folderInserts = Path.Combine(_folderPath, "Inserts");

            if (!Directory.Exists(folderInserts))
                Directory.CreateDirectory(folderInserts);
            StringBuilder InsetCommand = new StringBuilder();

            foreach (var instances in lstPropertyData.GroupBy(x => x.Instance))
            {
                InsetCommand.AppendLine($@"IF NOT EXISTS(SELECT 1 FROM TB_APPLICATION WHERE APPLICATION_NAME = ""{_applicationName}"")");
                InsetCommand.AppendLine($"BEGIN");
                InsetCommand.AppendLine($"	INSERT INTO APPLICATION_NAME (APPLICATION_NAME, USER_CREATED)");
                InsetCommand.AppendLine($@"	VALUES (""{_applicationName}"", ""#{{USER_CREATED}}#"")");
                InsetCommand.AppendLine($"END");
                InsetCommand.AppendLine($"GO\n");


                InsetCommand.AppendLine($@"DECLARE @APPLICATION_ID INT = (SELECT APPLICATION_ID FROM TB_APPLICATION WHERE APPLICATION_NAME = ""{_applicationName}"")");
                InsetCommand.AppendLine($"GO\n");

                InsetCommand.AppendLine($@"IF NOT EXISTS(SELECT 1 FROM TB_INSTANCE INSTANCE_NAME NAME = ""{instances.Key}"" AND APPLICATION_ID = @APPLICATION_ID )");
                InsetCommand.AppendLine($"BEGIN");
                InsetCommand.AppendLine($"	INSERT INTO TB_INSTANCES (INSTANCE_NAME, APPLICATION_ID, USER_CREATED)");
                InsetCommand.AppendLine($@"	VALUES (""{instances.Key})"", @APPLICATION_ID, ""#{{USER_CREATED}}#"")");
                InsetCommand.AppendLine($"END");
                InsetCommand.AppendLine($"GO\n");

                InsetCommand.AppendLine($@"DECLARE @INSTANCE_ID = (SELECT INSTANCE_ID FROM TB_INSTANCES WHERE NAME = ""{instances.Key}"" AND APPLICATION_ID = @APPLICATION_ID )\n");
                InsetCommand.AppendLine($"GO\n");

                foreach (var propertiesReference in instances.Where(x => x.ReferenceType == ReferenceType.Setting).GroupBy(x => x.Reference))
                {
                    foreach (var propertyData in propertiesReference)
                    {
                        InsetCommand.AppendLine($@"IF NOT EXISTS(SELECT ID FROM TB_PROPERTY WHERE SETTING_REFERENCE = ""{propertiesReference.Key}""");
                        InsetCommand.AppendLine($@"                                             AND PROPERTY_NAME = ""{propertyData.Property}""");
                        InsetCommand.AppendLine($@"                                             AND APPLICATION_ID = @APPLICATION_ID )");
                        InsetCommand.AppendLine($"BEGIN");
                        InsetCommand.AppendLine($"	INSERT INTO TB_PROPERTY (PROPERTY_NAME, PROPERTY_TYPE, SETTING_REFERENCE, APPLICATION_ID, USER_CREATED)");
                        InsetCommand.AppendLine($@"	VALUES (""{propertyData.Property}"", ""{propertyData.Type}"", ""{propertiesReference.Key}"", @APPLICATION_ID, ""#{{USER_CREATED}}#"")");
                        InsetCommand.AppendLine($"END");
                        InsetCommand.AppendLine($"GO\n\n");

                        InsetCommand.AppendLine($@"DECLARE @PROPERTY_ID = (SELECT PROPERTY_ID FROM TB_PROPERTY WHERE SETTING_REFERENCE = ""{propertiesReference.Key}""");
                        InsetCommand.AppendLine($@"                                             AND PROPERTY_NAME = ""{propertyData.Property}""");
                        InsetCommand.AppendLine($@"                                             AND APPLICATION_ID = @APPLICATION_ID )");

                        InsetCommand.AppendLine($@"IF NOT EXISTS(SELECT ID FROM TB_INSTANCE_PROPERTY WHERE INSTANCE_ID = @INSTANCE_ID AND PROPERTY_ID = @PROPERTY_ID)");
                        InsetCommand.AppendLine($"BEGIN");
                        InsetCommand.AppendLine($"	INSERT INTO TB_INSTANCE_PROPERTY (INSTANCE_ID, PROPERTY_ID, PROPERTY_VALUE, USER_CREATED)");
                        InsetCommand.AppendLine($@"	VALUES (@INSTANCE_ID, @PROPERTY_ID, ""{propertyData.Value}"", ""#{{USER_CREATED}}#"")");
                        InsetCommand.AppendLine($"END");
                        InsetCommand.AppendLine($"GO\n\n");
                    }
                }

                foreach (var propertiesReference in instances.Where(x => x.ReferenceType == ReferenceType.Library).GroupBy(x => x.Reference))
                {
                    InsetCommand.AppendLine($@"IF NOT EXISTS(SELECT 1 FROM TB_LIBRARY WHERE LIBRARY_NAME = ""{propertiesReference.Key}"")");
                    InsetCommand.AppendLine($"BEGIN");
                    InsetCommand.AppendLine($"	INSERT INTO TB_LIBRARY (LIBRARY_NAME, USER_CREATED)");
                    InsetCommand.AppendLine($@"	VALUES (""{propertiesReference.Key}"", ""#{{USER_CREATED}}#"")");
                    InsetCommand.AppendLine($"END");
                    InsetCommand.AppendLine($"GO\n");

                    InsetCommand.AppendLine($@"DECLARE @LIBRARY_ID INT = (SELECT LIBRARY_ID FROM TB_LIBRARY WHERE LIBRARY_NAME = ""{propertiesReference.Key}"")");
                    InsetCommand.AppendLine($"GO\n");

                    foreach (var propertyData in propertiesReference)
                    {
                        InsetCommand.AppendLine($@"IF NOT EXISTS(SELECT ID FROM TB_LIBRARY_PROPERTY WHERE LIBRARY_PROPERTY_NAME = ""{propertyData.Property}""");
                        InsetCommand.AppendLine($@"                                                   AND LIBRARY_ID = @LIBRARY_ID )");
                        InsetCommand.AppendLine($"BEGIN");
                        InsetCommand.AppendLine($"	INSERT INTO TB_LIBRARY_PROPERTY (LIBRARY_PROPERTY_NAME, LIBRARY_PROPERTY_TYPE, LIBRARY_ID, USER_CREATED)");
                        InsetCommand.AppendLine($@"	VALUES (""{propertyData.Property}"", ""{propertyData.Type}"", @LIBRARY_ID, ""#{{USER_CREATED}}#"")");
                        InsetCommand.AppendLine($"END");
                        InsetCommand.AppendLine($"GO\n\n");

                        InsetCommand.AppendLine($@"DECLARE @LIBRARY_PROPERTY_ID = (SELECT LIBRARY_PROPERTY_ID FROM TB_LIBRARY_PROPERTY WHERE LIBRARY_PROPERTY_NAME = ""{propertyData.Property}""");
                        InsetCommand.AppendLine($@"                                                                                     AND LIBRARY_ID = @LIBRARY_ID )");

                        InsetCommand.AppendLine($@"IF NOT EXISTS(SELECT ID FROM TB_INSTANCE_LABRARY_REFERENCE WHERE INSTANCE_ID = @INSTANCE_ID AND LIBRARY_PROPERTY_ID = @LIBRARY_PROPERTY_ID)");
                        InsetCommand.AppendLine($"BEGIN");
                        InsetCommand.AppendLine($"	INSERT INTO TB_INSTANCE_LABRARY_REFERENCE (INSTANCE_ID, LIBRARY_PROPERTY_ID, PROPERTY_VALUE, USER_CREATED)");
                        InsetCommand.AppendLine($@"	VALUES (@INSTANCE_ID, @LIBRARY_PROPERTY_ID, ""{propertyData.Value}"", ""#{{USER_CREATED}}#"")");
                        InsetCommand.AppendLine($"END");
                        InsetCommand.AppendLine($"GO\n\n");
                    }
                }

                //foreach (var propertiesReference in instances.Where(x => x.ReferenceType == ReferenceType.Application).GroupBy(x => x.Reference))
                //{
                //    InsetCommand.AppendLine($@"IF NOT EXISTS(SELECT 1 FROM TB_LIBRARY WHERE LIBRARY_NAME = ""{propertiesReference.Key}"")");
                //    InsetCommand.AppendLine($"BEGIN");
                //    InsetCommand.AppendLine($"	INSERT INTO TB_LIBRARY (LIBRARY_NAME, USER_CREATED)");
                //    InsetCommand.AppendLine($@"	VALUES (""{propertiesReference.Key}"", ""#{{USER_CREATED}}#"")");
                //    InsetCommand.AppendLine($"END");
                //    InsetCommand.AppendLine($"GO\n");

                //    InsetCommand.AppendLine($@"DECLARE @LIBRARY_ID INT = (SELECT LIBRARY_ID FROM TB_LIBRARY WHERE LIBRARY_NAME = ""{propertiesReference.Key}"")");
                //    InsetCommand.AppendLine($"GO\n");

                //    foreach (var propertyData in propertiesReference)
                //    {
                //        InsetCommand.AppendLine($@"IF NOT EXISTS(SELECT ID FROM TB_LIBRARY_PROPERTY WHERE LIBRARY_PROPERTY_NAME = ""{propertyData.Property}""");
                //        InsetCommand.AppendLine($@"                                                   AND LIBRARY_ID = @LIBRARY_ID )");
                //        InsetCommand.AppendLine($"BEGIN");
                //        InsetCommand.AppendLine($"	INSERT INTO TB_LIBRARY_PROPERTY (LIBRARY_PROPERTY_NAME, LIBRARY_PROPERTY_TYPE, LIBRARY_ID, USER_CREATED)");
                //        InsetCommand.AppendLine($@"	VALUES (""{propertyData.Property}"", ""{propertyData.Type}"", @LIBRARY_ID, ""#{{USER_CREATED}}#"")");
                //        InsetCommand.AppendLine($"END");
                //        InsetCommand.AppendLine($"GO\n\n");

                //        InsetCommand.AppendLine($@"DECLARE @LIBRARY_PROPERTY_ID = (SELECT LIBRARY_PROPERTY_ID FROM TB_LIBRARY_PROPERTY WHERE LIBRARY_PROPERTY_NAME = ""{propertyData.Property}""");
                //        InsetCommand.AppendLine($@"                                                                                     AND LIBRARY_ID = @LIBRARY_ID )");

                //        InsetCommand.AppendLine($@"IF NOT EXISTS(SELECT ID FROM TB_INSTANCE_LABRARY_REFERENCE WHERE INSTANCE_ID = @INSTANCE_ID AND LIBRARY_PROPERTY_ID = @LIBRARY_PROPERTY_ID)");
                //        InsetCommand.AppendLine($"BEGIN");
                //        InsetCommand.AppendLine($"	INSERT INTO TB_INSTANCE_LABRARY_REFERENCE (INSTANCE_ID, LIBRARY_PROPERTY_ID, PROPERTY_VALUE, USER_CREATED)");
                //        InsetCommand.AppendLine($@"	VALUES (@INSTANCE_ID, @LIBRARY_PROPERTY_ID, ""{propertyData.Value}"", ""#{{USER_CREATED}}#"")");
                //        InsetCommand.AppendLine($"END");
                //        InsetCommand.AppendLine($"GO\n\n");
                //    }
                //}
            }

            File.WriteAllText(Path.Combine(folderInserts, $"PropertyValues_{_applicationName}.cs"), InsetCommand.ToString());
        }

        private XmlNode FindNode(XmlNodeList list, string nodeName)
        {
            if (list.Count > 0)
            {
                foreach (XmlNode node in list)
                {
                    if (node.Name.Equals(nodeName)) return node;
                    if (node.HasChildNodes) FindNode(node.ChildNodes, nodeName);
                }
            }
            return null;
        }

        private IEnumerable<PropertyData> GetSettingNameAndValueFromSettings(string instance, string sectionName, XmlNodeList list)
        {
            XmlNode listProperties = FindNode(list, sectionName);

            foreach (XmlNode node in listProperties.ChildNodes)
            {
                if (node.Attributes.Count > 0)
                {
                    yield return new PropertyData
                    {
                        Instance = instance,
                        Reference = sectionName,
                        Property = node.Attributes.GetNamedItem("name").Value,
                        Type = node.Attributes.GetNamedItem("serializeAs").Value,
                        Value = node.InnerText
                    };
                }
            }
        }
    }

}
