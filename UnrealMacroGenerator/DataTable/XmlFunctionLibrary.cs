using System;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

namespace UnrealMacroGenerator.DialogUI
{
    enum EditorType
    {
        Unknown,
        MacroEditor,
        LogEditor
    }

    enum InputType
    {
        Unknown,
        Specifier,
        TextBox,
        CheckBox,
        NumericUpDown,
        NumericUpDownFloat
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
        private static readonly string ConfigXmlPath = "../../DataTable/ConfigDataTable.xml";
        private static readonly string EditorXmlPath = "../../DataTable/EditorDataTable.xml";
        private static readonly string XmlRoot = "Root";

        public static string[] GetMacroTypes(bool bContainsMenuOnly, bool bContainsSupportedOnly)
        {
            List<string> MacroTypes = new List<string>();
            MacroTypes.AddRange(GetMacroTypesInternal("Menu"));
            if(bContainsMenuOnly)
            {
                MacroTypes.AddRange(GetMacroTypesInternal("MenuOnly"));
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
                XDocument Xml = XDocument.Load(EditorXmlPath);
                XElement Root = Xml.Element(XmlRoot);
                XElement Table = Root.Element("MacroTypes");
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

        public static MacroSpecifierData GetMacroSpecifierData(string MacroType)
        {
            try
            {
                XDocument Xml = XDocument.Load(ConfigXmlPath);
                XElement Root = Xml.Element(XmlRoot);
                XElement Table = Root.Element(MacroType);

                List<string> MacroSpecifiersList = new List<string>();
                var MacroSpecifiersRows = Table.Elements("MacroSpecifiers");
                foreach (XElement Row in MacroSpecifiersRows)
                {
                    MacroSpecifiersList.Add(Row.Value);
                }

                List<string> AdvancedSettingsList = new List<string>();
                var AdvancedSettingsRows = Table.Elements("AdvancedSettings");
                foreach (XElement Row in AdvancedSettingsRows)
                {
                    AdvancedSettingsList.Add(Row.Value);
                }

                List<MetaSpecifiersData> MetaSpecifiersList = new List<MetaSpecifiersData>();
                var MetaSpecifiersRows = Table.Elements("MetaSpecifiers");
                foreach (XElement Row in MetaSpecifiersRows)
                {
                    InputType RowInputType;
                    Enum.TryParse(Row.Attribute("Input").Value, out RowInputType);
                    MetaSpecifiersList.Add(new MetaSpecifiersData(Row.Value, RowInputType));
                }

                MacroSpecifiersList.Sort();
                AdvancedSettingsList.Sort();
                MetaSpecifiersList.Sort((Lhp, Rhp) => string.Compare(Lhp.Data, Rhp.Data));

                MacroSpecifierData Result;
                Result.MacroSpecifiers = MacroSpecifiersList.ToArray();
                Result.AdvancedSettings = AdvancedSettingsList.ToArray();
                Result.MetaSpecifiers = MetaSpecifiersList.ToArray();

                return Result;
            }
            catch
            {
                MessageBox.Show(
                    "Failed to retrieve data from Xml file",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
                
                MacroSpecifierData ErrorResult;
                ErrorResult.MacroSpecifiers = new string[] { "Error" };
                ErrorResult.AdvancedSettings = new string[] { "Error" };
                ErrorResult.MetaSpecifiers = new MetaSpecifiersData[] { new MetaSpecifiersData("Error", InputType.Specifier) };
                return ErrorResult;
            }
        }

        public static string GetDocumentationLink(string MacroType)
        {
            try
            {
            XDocument Xml = XDocument.Load(ConfigXmlPath);
            XElement Root = Xml.Element(XmlRoot);
            XElement Table = Root.Element("DocumentationLink");
            XElement Link = Table.Element(MacroType);

            return Link.Value;
            }
            catch
            {
                XDocument Xml = XDocument.Load(EditorXmlPath);
                XElement Root = Xml.Element(XmlRoot);
                XElement Table = Root.Element("DocumentationLink");
                XElement Link = Table.Element("Default");
                return Link.Value;
            }
        }

        public static string GetTemplateString(string MacroType)
        {
            try
            {
                XDocument Xml = XDocument.Load(ConfigXmlPath);
                XElement Root = Xml.Element(XmlRoot);
                XElement Table = Root.Element("Template");
                XElement TemplateString = Table.Element(MacroType);

                return TemplateString.Value;
            }
            catch
            {
                return string.Empty;
            }
        }

        public static EditorType GetEditorType(string MacroType)
        {
            try
            {
                XDocument Xml = XDocument.Load(EditorXmlPath);
                XElement Root = Xml.Element(XmlRoot);
                XElement Table = Root.Element("EditorType");
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

        public static string[] GetLogCategory()
        {
            try
            {
                XDocument Xml = XDocument.Load(ConfigXmlPath);
                XElement Root = Xml.Element(XmlRoot);
                XElement Table = Root.Element("UE_LOG");
                var CategoryNames = Table.Elements("CategoryName");
                List<string> CategoryNameList = new List<string>();
                foreach(var CategoryName in CategoryNames)
                {
                    CategoryNameList.Add(CategoryName.Value);
                }

                return CategoryNameList.ToArray();
            }
            catch
            {
                return new string[0];
            }
        }

        public static string[] GetLogVerbosity()
        { 
            try
            {
                XDocument Xml = XDocument.Load(EditorXmlPath);
                XElement Root = Xml.Element(XmlRoot);
                XElement Table = Root.Element("UE_LOG");
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
