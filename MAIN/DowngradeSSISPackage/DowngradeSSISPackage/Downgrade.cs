using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace DowngradeSSISPackage
{
    class Downgrade
    {


        // Methods
        private static string AdjustADONETSource(string xml)
        {
            for (int i = xml.IndexOf("componentClassID=\"{BF01D463-7089-41EE-8F05-0A6DC17CE633}\" description=\"Extracts data from a relational database by using a .NET provider.\""); i >= 0; i = xml.IndexOf("componentClassID=\"{BF01D463-7089-41EE-8F05-0A6DC17CE633}\" description=\"Extracts data from a relational database by using a .NET provider.\"", (int)(i + 1)))
            {
                int index = xml.IndexOf("</component>", (int)(i + 1));
                int num3 = xml.IndexOf("name=\"TableOrViewName\"", (int)(i + 1));
                int startIndex = xml.IndexOf("</property>", (int)(num3 + 1));
                int num5 = startIndex - 1;
                while ((num5 >= 0) && (xml[num5] != '>'))
                {
                    num5--;
                }
                string str = xml.Substring(num5 + 1, (startIndex - num5) - 1);
                xml = RemoveProperty(xml, i, "TableOrViewName");
                xml = RemoveProperty(xml, i, "AccessMode");
                if (!string.IsNullOrEmpty(str))
                {
                    num3 = xml.IndexOf("name=\"SqlCommand\"", (int)(i + 1));
                    startIndex = xml.IndexOf("</property>", (int)(num3 + 1));
                    num5 = startIndex - 1;
                    while ((num5 >= 0) && (xml[num5] != '>'))
                    {
                        num5--;
                    }
                    if (string.IsNullOrEmpty(xml.Substring(num5 + 1, (startIndex - num5) - 1).Trim()))
                    {
                        str = str.Replace("\".\"", "].[");
                        if (str[0] == '"')
                        {
                            str = "[" + str.Substring(1, str.Length - 2) + "]";
                        }
                        string str2 = "select * from " + str;
                        xml = xml.Substring(0, num5 + 1) + str2 + xml.Substring(startIndex);
                    }
                }
            }
            return xml;
        }

        private static string AdjustFlatFileConnectionManagers(string xml)
        {
            for (int i = xml.IndexOf("<DTS:FlatFileColumn>"); i >= 0; i = xml.IndexOf("<DTS:FlatFileColumn>", (int)(i + 1)))
            {
                int index = xml.IndexOf("<DTS:Property DTS:Name=\"DataType\">", (int)(i + 1));
                string str = "<DTS:Property DTS:Name=\"DataType\">146";
                if (index == xml.IndexOf(str, (int)(i + 1)))
                {
                    xml = xml.Substring(0, index) + "<DTS:Property DTS:Name=\"DataType\">129" + xml.Substring(index + str.Length);
                    str = "<DTS:Property DTS:Name=\"MaximumWidth\">0</DTS:Property>";
                    index = xml.IndexOf(str, (int)(i + 1));
                    if (index < 0)
                    {
                        return xml;
                    }
                    xml = xml.Substring(0, index) + "<DTS:Property DTS:Name=\"MaximumWidth\">27</DTS:Property>" + xml.Substring(index + str.Length);
                }
            }
            return xml;
        }

        private static string AdjustLookupMatchOutputErrorDisposition(string xml)
        {
            int num2;
            for (int i = xml.IndexOf("name=\"Lookup Match Output\""); i >= 0; i = xml.IndexOf("name=\"Lookup Match Output\"", (int)(num2 + 1)))
            {
                num2 = i;
                while ((i > 0) && (xml[i] != '<'))
                {
                    i--;
                }
                if (xml[i] != '<')
                {
                    return xml;
                }
                if (xml[i + 1] == 'o')
                {
                    int num3 = xml.IndexOf(">", (int)(i + 1)) + 1;
                    if (num3 < i)
                    {
                        return xml;
                    }
                    string str = "errorRowDisposition=\"NotUsed\"";
                    int index = xml.IndexOf(str, (int)(i + 1));
                    if ((index >= 0) && (index < num3))
                    {
                        xml = xml.Substring(0, index) + "errorRowDisposition=\"RedirectRow\"" + xml.Substring(index + str.Length);
                    }
                }
            }
            return xml;
        }

       public static void ProcessPackage(string SourceFilename,string DestinationFilename )
       {

                Assembly executingAssembly = Assembly.GetExecutingAssembly();
                string[] strArray = new StreamReader(executingAssembly.GetManifestResourceStream(executingAssembly.GetManifestResourceNames()[2])).ReadToEnd().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                string contents = RemoveDesignerVariables(File.ReadAllText(SourceFilename));
                foreach (string str2 in strArray)
                {
                    string[] strArray2 = str2.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    contents = contents.Replace(strArray2[1], strArray2[0]);
                }
                contents = RemoveScripts(AdjustLookupMatchOutputErrorDisposition(RemoveLookupNoMatchOutputs(AdjustFlatFileConnectionManagers(AdjustADONETSource(contents.Replace("Foodmart 2000", "SSIS_Foodmart2000").Replace("<DTS:Property DTS:Name=\"PackageFormatVersion\">3</DTS:Property>", "<DTS:Property DTS:Name=\"PackageFormatVersion\">2</DTS:Property>").Replace("version=\"5\" pipelineVersion=\"0\" contactInfo=\"Lookup", "version=\"4\" pipelineVersion=\"0\" contactInfo=\"Lookup").Replace("version=\"4\" pipelineVersion=\"0\" contactInfo=\"Extracts data from a relational database by using a .NET provider.", "version=\"2\" pipelineVersion=\"0\" contactInfo=\"Extracts data from a relational database by using a .NET provider.").Replace("version=\"3\" pipelineVersion=\"0\" contactInfo=\"Extracts data from a relational database by using a .NET provider.", "version=\"2\" pipelineVersion=\"0\" contactInfo=\"Extracts data from a relational database by using a .NET provider.")))))).Replace("Version=10.0.0.0", "Version=9.0.242.0").Replace("length=\"0\" dataType=\"dbTimeStampOffset\" codePage=\"0\"", "length=\"27\" dataType=\"str\" codePage=\"1252\"");
                File.WriteAllText(DestinationFilename , contents);
            
       }

        private static void PrintUsage()
        {
            Console.WriteLine("Usage: downgrade KatmaiPackageName.dtsx");
        }

        private static string RemoveDesignerVariables(string xml)
        {
            for (int i = xml.IndexOf("DTS:Name=\"Namespace\">dts-designer-1.0</DTS:Property>"); i >= 0; i = xml.IndexOf("DTS:Name=\"Namespace\">dts-designer-1.0</DTS:Property>"))
            {
                while (xml.Substring(i, 0x15) != "<DTS:PackageVariable>")
                {
                    i--;
                }
                int num2 = xml.IndexOf("</DTS:PackageVariable>", i) + 0x16;
                if (xml[num2] == '\r')
                {
                    num2++;
                }
                if (xml[num2] == '\n')
                {
                    num2++;
                }
                xml = xml.Remove(i, num2 - i);
            }
            return xml;
        }

        private static string RemoveLookupNoMatchOutputs(string xml)
        {
            for (int i = xml.IndexOf("name=\"Lookup No Match Output\""); i >= 0; i = xml.IndexOf("name=\"Lookup No Match Output\""))
            {
                int index;
                while ((i > 0) && (xml[i] != '<'))
                {
                    i--;
                }
                if (xml[i] != '<')
                {
                    return xml;
                }
                if (xml[i + 1] == 'p')
                {
                    int startIndex = xml.IndexOf("startId=\"", (int)(i + 1)) + 9;
                    index = xml.IndexOf('"', startIndex);
                    int num4 = int.Parse(xml.Substring(startIndex, index - startIndex)) + 1;
                    xml = xml.Substring(0, startIndex) + num4 + xml.Substring(index);
                    startIndex = xml.IndexOf("name=\"", (int)(i + 1)) + 6;
                    index = xml.IndexOf('"', startIndex);
                    xml = xml.Substring(0, startIndex) + "Lookup Error Output" + xml.Substring(index);
                }
                else
                {
                    index = xml.IndexOf("</output>", (int)(i + 1)) + 9;
                    if (index < i)
                    {
                        return xml;
                    }
                    xml = xml.Replace(xml.Substring(i, index - i), "");
                }
            }
            return xml;
        }

        private static string RemoveProperty(string xml, int s, string prop)
        {
            int index = xml.IndexOf("</component>", (int)(s + 1));
            int startIndex = xml.IndexOf(string.Format("name=\"{0}\"", prop), (int)(s + 1));
            if ((startIndex >= 0) && (startIndex <= index))
            {
                while (xml[startIndex] != '<')
                {
                    startIndex--;
                }
                int num3 = xml.IndexOf("</property>", (int)(startIndex + 1)) + 11;
                xml = xml.Remove(startIndex, num3 - startIndex);
            }
            return xml;
        }

        private static string RemoveScripts(string xml)
        {
            xml = xml.Replace("version=\"4\" pipelineVersion=\"0\" contactInfo=\"Executes a custom script", "version=\"0\" pipelineVersion=\"0\" contactInfo=\"Executes a custom script");
            xml = xml.Replace("version=\"3\" pipelineVersion=\"0\" contactInfo=\"Executes a custom script", "version=\"0\" pipelineVersion=\"0\" contactInfo=\"Executes a custom script");
            xml = xml.Replace("Microsoft.SqlServer.Dts.Pipeline.ScriptUIVariablePickerDlg, Microsoft.SqlServer.TxScript, Version=10.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91", "");
            xml = xml.Replace("Microsoft.SqlServer.Dts.Pipeline.ScriptComponentHost, Microsoft.SqlServer.TxScript, Version=10.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91", "Microsoft.SqlServer.Dts.Pipeline.ScriptComponentHost, Microsoft.SqlServer.TxScript, Version=9.0.242.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91");
            xml = xml.Replace("name=\"VSTAProjectName\"", "name=\"VsaProjectName\"");
            xml = ReplaceProperty(xml, "name=\"SourceCode\" dataType=\"System.String\" state=\"cdata\" isArray=\"true\"", "name=\"SourceCode\" dataType=\"System.String\" state=\"cdata\" isArray=\"true\" description=\"Stores the source code of the component\" typeConverter=\"\" UITypeEditor=\"\" containsID=\"false\" expressionType=\"None\"><arrayElements arrayElementCount=\"0\"/></property>");
            xml = ReplaceProperty(xml, "name=\"BinaryCode\" dataType=\"System.String\" state=\"cdata\" isArray=\"true\"", "name=\"BinaryCode\" dataType=\"System.String\" state=\"cdata\" isArray=\"true\" description=\"Stores the binary representation of the component\" typeConverter=\"\" UITypeEditor=\"\" containsID=\"false\" expressionType=\"None\"><arrayElements arrayElementCount=\"0\"/></property>");
            xml = ReplaceProperty(xml, "name=\"BreakpointCollection\" dataType=\"System.String\" state=\"default\" isArray=\"true\"", "name=\"PreCompile\" dataType=\"System.Boolean\" state=\"default\" isArray=\"false\" description=\"Indicates whether to store pre-compiled binary representation of the script. This makes the package larger, but offers faster start-up time. Required to execute in native 64-bit mode.\" typeConverter=\"\" UITypeEditor=\"\" containsID=\"false\" expressionType=\"None\">true</property>");
            xml = xml.Replace("expressionType=\"None\">CSharp</property>", "expressionType=\"None\">VisualBasic</property>");
            return xml;
        }

        private static string ReplaceProperty(string xml, string src, string dest)
        {
            for (int i = xml.IndexOf(src); i >= 0; i = xml.IndexOf(src, (int)(i + 1)))
            {
                int num2 = xml.IndexOf("</property>", (int)(i + 1)) + 11;
                if (num2 < 0)
                {
                    return xml;
                }
                xml = xml.Replace(xml.Substring(i, num2 - i), dest);
            }
            return xml;
        }
    }
}

 
