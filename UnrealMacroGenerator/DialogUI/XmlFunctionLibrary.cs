﻿using System;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

namespace UnrealMacroGenerator.DialogUI
{
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

    struct MacroTableData
    {
        public string[] MacroSpecifiers;
        public string[] AdvancedSettings;
        public MetaSpecifiersData[] MetaSpecifiers;
    }

    class XmlFunctionLibrary
    {
        private const string XmlPath = "../../DialogUI/MacroData.xml";
        private const string XmlRoot = "MacroData";

        static public string[] GetMacroTypes()
        {
            try
            {
                XDocument Xml = XDocument.Load(XmlPath);
                XElement Root = Xml.Element(XmlRoot);
                XElement Table = Root.Element("MacroTypes");
                var Rows = Table.Elements("Data");
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

        static public MacroTableData GetMacroTableData(string MacroType)
        {
            try
            {
                XDocument Xml = XDocument.Load(XmlPath);
                XElement Root = Xml.Element(XmlRoot);
                XElement Table = Root.Element(MacroType);

                List<string> MacroSpecifiersList = new List<string>();
                XElement MacroSpecifiers = Table.Element("MacroSpecifiers");
                var MacroSpecifiersRows = MacroSpecifiers.Elements("Data");
                foreach (XElement Row in MacroSpecifiersRows)
                {
                    MacroSpecifiersList.Add(Row.Value);
                }

                List<string> AdvancedSettingsList = new List<string>();
                XElement AdvancedSettings = Table.Element("AdvancedSettings");
                var AdvancedSettingsRows = AdvancedSettings.Elements("Data");
                foreach (XElement Row in AdvancedSettingsRows)
                {
                    AdvancedSettingsList.Add(Row.Value);
                }

                List<MetaSpecifiersData> MetaSpecifiersList = new List<MetaSpecifiersData>();
                XElement MetaSpecifiers = Table.Element("MetaSpecifiers");
                var MetaSpecifiersRows = MetaSpecifiers.Elements("Data");
                foreach (XElement Row in MetaSpecifiersRows)
                {
                    InputType RowInputType;
                    string InputTypeString = Row.Attribute("Input").ToString();
                    InputTypeString = InputTypeString.Split('\"')[1];
                    Enum.TryParse(InputTypeString, out RowInputType);
                    MetaSpecifiersList.Add(new MetaSpecifiersData(Row.Value, RowInputType));
                }

                MacroSpecifiersList.Sort();
                AdvancedSettingsList.Sort();
                MetaSpecifiersList.Sort((Lhp, Rhp) => string.Compare(Lhp.Data, Rhp.Data));

                MacroTableData Result;
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
                
                MacroTableData ErrorResult;
                ErrorResult.MacroSpecifiers = new string[] { "Error" };
                ErrorResult.AdvancedSettings = new string[] { "Error" };
                ErrorResult.MetaSpecifiers = new MetaSpecifiersData[] { new MetaSpecifiersData("Error", InputType.Specifier) };
                return ErrorResult;
            }
        }
    }
}