using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Windows.Forms;
using UnrealMacroGenerator.Properties;

namespace UnrealMacroGenerator
{
    class SettingsFunctionLibrary
    {
        public static MacroSpecifierData GetMacroSpecifierData(string MacroType)
        {
            try
            {
                MacroSpecifierData Result;
                Result.MacroSpecifiers = new string[0];
                Result.AdvancedSettings = new string[0];
                Result.MetaSpecifiers = new MetaSpecifiersData[0];
                string[] RawMetaSpecifiers = new string[0];

                var MacroSpecifiersList = Settings.Default[MacroType + "_MacroSpecifiers"];
                if (MacroSpecifiersList != null)
                {
                    if (MacroSpecifiersList is StringCollection Value)
                    {
                        Result.MacroSpecifiers = StringCollectionToStringArray(Value);
                    }
                }

                var AdvancedSettingsList = Settings.Default[MacroType + "_AdvancedSettings"];
                if (AdvancedSettingsList != null)
                {
                    if (AdvancedSettingsList is StringCollection Value)
                    {
                        Result.AdvancedSettings = StringCollectionToStringArray(Value);
                    }
                }

                var MetaSpecifiersList = Settings.Default[MacroType + "_MetaSpecifiers"];
                if (MetaSpecifiersList != null)
                {
                    if (MetaSpecifiersList is StringCollection Value)
                    {
                        RawMetaSpecifiers = StringCollectionToStringArray(Value);
                    }
                }

                List<MetaSpecifiersData> MetaSpecifiers = new List<MetaSpecifiersData>();
                foreach (var RawMetaSpecifier in RawMetaSpecifiers)
                {
                    string[] Splited = RawMetaSpecifier.Split(';');
                    if (Splited.Length == 2)
                    {
                        InputType RowInputType;
                        System.Enum.TryParse(Splited[1], out RowInputType);
                        MetaSpecifiers.Add(new MetaSpecifiersData(Splited[0], RowInputType));
                    }
                }
                Result.MetaSpecifiers = MetaSpecifiers.ToArray();

                Array.Sort(Result.MacroSpecifiers);
                Array.Sort(Result.AdvancedSettings);
                Array.Sort(Result.MetaSpecifiers, (Lhp, Rhp) => string.Compare(Lhp.Data, Rhp.Data));

                return Result;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(
                    "Failed to read data from the setting file\n" + Ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
                return new MacroSpecifierData();
            }
        }

        public static string GetDocumentLink(string MacroType)
        {
            try
            {
                string DocumentLink = string.Empty;
                if (Settings.Default[MacroType + "_Document"] is string Value) 
                {
                    DocumentLink = Value;
                }

                if (string.IsNullOrEmpty(DocumentLink) || string.IsNullOrWhiteSpace(DocumentLink))
                {
                    DocumentLink = XmlFunctionLibrary.GetDefaultDocumentLink();
                }

                return DocumentLink;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(
                    "Failed to read data from the setting file\n" + Ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
                return string.Empty;
            }
        }

        public static string GetTemplateString(string MacroType)
        {
            try
            {
                string TemplateString = string.Empty;

                if (Settings.Default[MacroType + "_Template"] is string Value) 
                {
                    TemplateString = Value;
                }

                return TemplateString;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(
                    "Failed to read data from the setting file\n" + Ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
                return string.Empty;
            }
        }

        public static string[] GetLogCategory()
        {
            try
            {
                string[] LogCategory = StringCollectionToStringArray(Settings.Default.UE_LOG_LogCategory);
                Array.Sort(LogCategory);
                return LogCategory;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(
                    "Failed to read data from the setting file\n" + Ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
                return new string[0];
            }
        }

        public static Color GetMainFrameColor()
        {
            return Settings.Default.MainColor;
        }

        public static Color GetBackgroundColor()
        {
            return Settings.Default.BackgroundColor;
        }

        public static Color GetTextColor()
        {
            return Settings.Default.TextColor;
        }

        public static Color GetLinkColor()
        {
            return Settings.Default.LinkColor;
        }

        private static string[] StringCollectionToStringArray(StringCollection Collection)
        {
            return new List<string>(new StringCollectionEnumerable(Collection)).ToArray();
        }
    }

    public class StringCollectionEnumerable : IEnumerable<string>
    {
        private StringCollection UnderlyingCollection;

        public StringCollectionEnumerable(StringCollection UnderlyingCollection)
        {
            this.UnderlyingCollection = UnderlyingCollection;
        }

        public IEnumerator<string> GetEnumerator()
        {
            return new StringEnumeratorWrapper(UnderlyingCollection.GetEnumerator());
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    public class StringEnumeratorWrapper : IEnumerator<string>
    {
        private StringEnumerator UnderlyingEnumerator;

        public StringEnumeratorWrapper(StringEnumerator UnderlyingEnumerator)
        {
            this.UnderlyingEnumerator = UnderlyingEnumerator;
        }

        public string Current
        {
            get
            {
                return this.UnderlyingEnumerator.Current;
            }
        }

        public void Dispose()
        {
            // No-op 
        }

        object System.Collections.IEnumerator.Current
        {
            get
            {
                return this.UnderlyingEnumerator.Current;
            }
        }

        public bool MoveNext()
        {
            return this.UnderlyingEnumerator.MoveNext();
        }

        public void Reset()
        {
            this.UnderlyingEnumerator.Reset();
        }
    }
}
