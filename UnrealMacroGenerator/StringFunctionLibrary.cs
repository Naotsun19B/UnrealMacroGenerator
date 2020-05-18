using System.Collections.Generic;

namespace UnrealMacroGenerator
{
    class StringFunctionLibrary
    {
        public static List<string> SplitParameterByComma(string Parameter)
        {
            // 文字列中のカンマを無視してカンマで分割する
            List<string> Splited = new List<string>();

            bool bIsInString = false;
            int PrevCommaIndex = -1;
            for (int Index = 0; Index < Parameter.Length; Index++)
            {
                if (Parameter[Index] == '\"')
                {
                    bIsInString = !bIsInString;
                }

                if (!bIsInString && Parameter[Index] == ',')
                {
                    Splited.Add(Parameter.Substring(PrevCommaIndex + 1, Index - PrevCommaIndex - 1));
                    PrevCommaIndex = Index;
                }
            }

            if (PrevCommaIndex > -1)
            {
                for (int Index = Parameter.Length - 1; Index > 0; Index--)
                {
                    if (Parameter[Index] == '\"')
                    {
                        bIsInString = !bIsInString;
                    }

                    if (!bIsInString && Parameter[Index] == ',')
                    {
                        Splited.Add(Parameter.Substring(Index + 1, Parameter.Length - Index - 1));
                        break;
                    }
                }
            }
            // 項目が1つの場合
            else
            {
                Splited.Add(Parameter);
            }

            return Splited;
        }

        public static int CountOfChar(string SearchString, char TargetChar)
        {
            return SearchString.Length - SearchString.Replace(TargetChar.ToString(), "").Length;
        }

        public static int CountOfString(string SearchString, string[] TargetStrings)
        {
            int Count = 0;
            foreach (var TargetString in TargetStrings)
            {
                int Index = SearchString.IndexOf(TargetString, 0);
                while (Index != -1)
                {
                    Count++;
                    Index = SearchString.IndexOf(TargetString, Index + TargetString.Length);
                }
            }

            return Count;
        }

        static public string RemoveChars(string EditString, char[] TargetChars)
        {
            string TrimmedString = string.Empty;
            bool bIsInString = false;
            for (int Index = 0; Index < EditString.Length; Index++)
            {
                if (EditString[Index] == '\"')
                {
                    bIsInString = !bIsInString;
                }

                bool bIsFind = false;
                foreach(var TargetChar in TargetChars)
                {
                    if(EditString[Index] == TargetChar)
                    {
                        bIsFind = true;
                    }
                }

                if (bIsInString || !bIsFind)
                {
                    TrimmedString += EditString[Index];
                }
            }

            return TrimmedString;
        }
    }
}
