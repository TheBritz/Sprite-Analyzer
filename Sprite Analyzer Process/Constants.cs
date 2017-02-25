using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprite_Analyzer_Process
{
  public static class Constants
  {
    public struct Parameters
    {
      public const string Help = "help";
      public const string HelpAlternate = "h";
      public const string Regex = "regex";
      public const string RegexAlternate = "r";
      public const string Keyword = "keyword";
      public const string KeywordAlternate = "k";
      public const string ParamSpecifier = "/";
    }

    public struct Path
    {
      public static string InstallDir
      {
        get
        {
          #if DEBUG
          return @"C:\Program Files\Sprite Analyzer\";
          #else
          //Get install dir from registry
          #endif
        }
      }

      public static string AnalyzerDir = InstallDir + @"bin\Analyzers\";
    }

  }
}
