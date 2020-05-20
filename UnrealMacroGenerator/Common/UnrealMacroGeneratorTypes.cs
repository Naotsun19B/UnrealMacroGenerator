using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}
