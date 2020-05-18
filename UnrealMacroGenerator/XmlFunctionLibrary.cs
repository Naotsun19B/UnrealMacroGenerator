using System;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Reflection;

namespace UnrealMacroGenerator
{
    enum EditorType
    {
        Unknown,
        MacroEditor,
        LogEditor,
        DelegateEditor
    }

    enum InputType
    {
        Unknown,
        NoInput,
        String,
        Bool,
        Int,
        Float
    }

    struct MetaSpecifiersData
    {
        public string Data;
        public InputType Type;

        public MetaSpecifiersData(string InData, InputType InType) : this()
        {
            Data = InData;
            Type = InType;
        }
    }

    struct MacroSpecifierData
    {
        public string[] MacroSpecifiers;
        public string[] AdvancedSettings;
        public MetaSpecifiersData[] MetaSpecifiers;
    }

    class XmlFunctionLibrary
    {
        private static readonly string ResourceName = "UnrealMacroGenerator.Resources.DefaultConfig.xml";
        private static XElement XmlRoot;

        public static void LoadDefaultConfig()
        {
            var ThisAssembly = Assembly.GetExecutingAssembly();
            var XmlStream = ThisAssembly.GetManifestResourceStream(ResourceName);
            var Xml = XDocument.Load(XmlStream);
            XmlRoot = Xml.Element("Root");
        }

        public static EditorType GetEditorType(string MacroType)
        {
            try
            {
                XElement Table = XmlRoot.Element("EditorType");
                XElement EditorTypeString = Table.Element(MacroType);
                EditorType EditorType;
                Enum.TryParse(EditorTypeString.Value, out EditorType);

                return EditorType;
            }
            catch
            {
                return EditorType.Unknown;
            }
        }

        public static string[] GetMacroTypes(bool bContainsMenuOnly, bool bContainsSearchOnly, bool bContainsSupportedOnly)
        {
            List<string> MacroTypes = new List<string>();
            MacroTypes.AddRange(GetMacroTypesInternal("Menu"));
            if(bContainsMenuOnly)
            {
                MacroTypes.AddRange(GetMacroTypesInternal("MenuOnly"));
            }
            if(bContainsSearchOnly)
            {
                MacroTypes.AddRange(GetMacroTypesInternal("SearchOnly"));
            }
            if(bContainsSupportedOnly)
            {
                MacroTypes.AddRange(GetMacroTypesInternal("SupportedOnly"));
            }

            return MacroTypes.ToArray();
        }

        private static string[] GetMacroTypesInternal(string Category)
        {
            try
            {
                XElement Table = XmlRoot.Element("MacroTypes");
                var Rows = Table.Elements(Category);
                List<string> MacroTypes = new List<string>();
                foreach (XElement Row in Rows)
                {
                    MacroTypes.Add(Row.Value);
                }

                return MacroTypes.ToArray();
            }
            catch
            {
                MessageBox.Show(
                    "Failed to retrieve data from Xml file",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
                return new string[] { "Error" };
            }
        }

        public static string GetDefaultDocumentLink()
        {
            try
            {
                XElement Table = XmlRoot.Element("DocumentLink");
                XElement Link = Table.Element("Default");
                return Link.Value;
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string[] GetLogVerbosity()
        {
            try
            {
                XElement Table = XmlRoot.Element("UE_LOG");
                var Verbositys = Table.Elements("Verbosity");
                List<string> VerbosityList = new List<string>();
                foreach (var Verbosity in Verbositys)
                {
                    VerbosityList.Add(Verbosity.Value);
                }

                return VerbosityList.ToArray();
            }
            catch
            {
                return new string[0];
            }
        }
    }
}
