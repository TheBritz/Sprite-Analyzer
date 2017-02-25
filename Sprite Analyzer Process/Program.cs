using DRAW = System.Drawing;
using JSON = Newtonsoft.Json;
using SpriteAnalyzerInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Sprite_Analyzer_Process
{
  class Program
  {
    [ImportMany(typeof(ISpriteAnalyzer))]
    private IEnumerable<Lazy<ISpriteAnalyzer>> m_analyzers;

    //Create the color dictionary
    private static Dictionary<string, DRAW.Color> m_colorDict =
      new Dictionary<string, DRAW.Color>();
    //Create the analyzer dictionary
    private static Dictionary<DRAW.Color, ISpriteAnalyzer> m_analyzerDict =
      new Dictionary<DRAW.Color, ISpriteAnalyzer>();
    //Create the assembly dictiionary
    private static Dictionary<string, Assembly> m_assebmlyDict =
      new Dictionary<string, Assembly>();

    static void Main(string[] args)
    {
      //Command line args are as follows (color args will be hex value):
      //<Analyzation directory> <Output directory> <color 1> ... <color n>
      List<string> argList = new List<string>(args);

      //Check for keyword parameter 
      string keyword = 
        GetSpecificParameter(Constants.Parameters.Keyword, ref argList);

      //Check for regex parameter
      string regex = 
        GetSpecificParameter(Constants.Parameters.Regex, ref argList);

      if (argList.Count > 2)
      {
        string analyzeDirectory = argList[0];
        string outputDirectory = argList[1];
        //Check to make sure the analysis directory exists
        if (!Directory.Exists(analyzeDirectory))
        {
          //Exit the program since the folder given to analyze does not exist
        }

        //Iterate through args 3 to n to get all the colors to look for
        DRAW.ColorConverter colConverter = new DRAW.ColorConverter();
        for (int i = 2; i < argList.Count; i++)
        {
          string name, analyzerDLL, analyzerID;
          DRAW.Color color = new DRAW.Color();
          GetColorArgParts
            (argList[i], out name, out color, out analyzerDLL, out analyzerID);

          if(string.IsNullOrEmpty(name))
          {
            name = i.ToString();
          }
          m_colorDict.Add(name, color);

          if (!string.IsNullOrEmpty(analyzerDLL) && !string.IsNullOrEmpty(analyzerID))
          {
            AddToAnalyzerDict(color, analyzerDLL, analyzerID);
          }
        }

        AnalyzeDirectory(analyzeDirectory, outputDirectory, regex, keyword);
      }
      else
      {
        //Check if the user is asking for help
        if (argList.Contains(Constants.Parameters.Help) ||
          argList.Contains(Constants.Parameters.HelpAlternate))
        {
          PrintHelpInfo();
        }

        //Do some analysis of the args passed-in in order to provide good user
        //feedback
      }
    }

    private void ImportAnalyzers()
    {
      //An aggregate catalog that combines multiple catalogs
      var catalog = new AggregateCatalog();

      string analyzerDir = Constants.Path.InstallDir + @"bin\Analyzers";

      //Get the directory catalog
      var catalogDir = new DirectoryCatalog(analyzerDir);

      //Create the CompositionContainer with the parts in the catalog.
      CompositionContainer container = new CompositionContainer(catalog);

      //Fill the imports of this object
      container.ComposeParts(this);
    }

    /// <summary>
    /// Analyzes sprites in the given directory, outputting JSON data files to
    /// the given out directory.
    /// </summary>
    /// <param name="analyzeDir">The directory to perform the analysis in
    /// </param>
    /// <param name="outDir">The directory to place all resulting data</param>
    /// <param name="fileRegex">The optional regex pattern to filter the files.
    /// </param>
    private static void AnalyzeDirectory(string analyzeDir,
      string outDir, string fileRegex = "", string keyword = "")
    {
      string[] files;
      if (!string.IsNullOrEmpty(fileRegex))
      {
        files = Directory.GetFiles(analyzeDir, fileRegex);
      }
      else
      {
        files = Directory.GetFiles(analyzeDir);
      }

      //Iterate through the files and analyze
      foreach (string file in files)
      {
        AnalyzeFile(file, keyword);
      }
    }

    /// <summary>
    /// Analyze the given file
    /// </summary>
    private static void AnalyzeFile(string file, string keyword = "")
    {
      DRAW.Bitmap img = (DRAW.Bitmap)DRAW.Image.FromFile(file);
      for (int h = 0; h < img.Width; h++)
      {
        for (int v = 0; v < img.Height; v++)
        {
          if(h == 15 && v == 10)
          {

          }
          DRAW.Color color = img.GetPixel(h, v);
          foreach (var kvp in m_colorDict)
          {
            DRAW.Color colorEval = kvp.Value;
            if (color.ToArgb() == colorEval.ToArgb())
            {
              //A key pixel has been found
              string colorName = kvp.Key;

              string fname = Path.GetFileNameWithoutExtension(file);

              //Get the core sprite name Game Maker png file names follow this 
              //format: <sprite name>_<image index>.png
              //The keyword allows for specific data image files to apply to
              //another image. Here's an example, for a file name of 
              //"weaponRudimentaryLaserHandholds_spr_0" - if a keyword of 
              //'Handholds' is used the data in this file will apply towards
              //the 0 image index of the weaponRudimentaryLaser_spr

              //Get the core sprite name
              int index = fname.LastIndexOf('_');
              int imageIndex = Convert.ToInt32(fname.Substring(index + 1));
              string spriteName = fname.Substring(0, index);
              //Remove the keyword to get the core sprite name
              string spriteNameCore = fname.Replace(keyword, "");
            }
          }
        }
      }
    }

    /// <summary>
    /// Returns the specified parameter
    /// </summary>
    /// <param name="parameterName">The name of the parameter</param>
    /// <param name="argList"></param>
    /// <returns></returns>
    private static string GetSpecificParameter
      (string parameterName, ref List<string> argList)
    {
      //Get the parameter pattern
      string pattern =
        Constants.Parameters.ParamSpecifier + parameterName + "=";
      foreach (string arg in argList)
      {
        if (arg.Substring(0, pattern.Length) == pattern)
        {
          //We've found the parameter, capture the value and clean it out from 
          //the argList
          string[] argElements = arg.Split('=');
          string value;
          if (argElements.Length > 1)
          {
            value = argElements[1];

          }
          else
          {
            value = "";
          }

          //Delete this list entry
          argList.Remove(arg);
          return value;
        }
      }
      return "";
    }
    
    /// <summary>
    /// Extracts the individual parts of a color argument
    /// </summary>
    /// <param name="arg">The argument</param>
    /// <param name="name">The name of the analysis data</param>
    /// <param name="color">The color source of this analysis</param>
    /// <param name="analyzerDLL">The name of the analyzer component</param>
    private static void GetColorArgParts(string arg, out string name, 
      out DRAW.Color color, out string analyzerDLL, out string analyzerID)
    {
      string colorStr = string.Empty;
      name = string.Empty;
      analyzerDLL = string.Empty;
      analyzerID = string.Empty;
      if (arg.ElementAt(0) == '-' && arg.Contains("=") && arg.Contains("|"))
      {
        //This argument contains a name for this color, get both
        string[] thisArgElements = arg.Split
          ("-=|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        name = thisArgElements[0];
        colorStr = thisArgElements[1];
        analyzerDLL = thisArgElements[2];
        analyzerID = thisArgElements[3];
      }
      else
      {
        colorStr = arg;
      }

      //Try to instantiate the color from the raw color string in case 
      //it's a name
      color = DRAW.Color.FromName(colorStr);

      //Instantiate a color converter
      DRAW.ColorConverter colConverter = new DRAW.ColorConverter();

      //Check if this is a valid color
      if (color.Equals(DRAW.Color.FromName("Invalid")))
      {
        //Sanitize color string
        if (colorStr.ElementAt(0) != '#')
        {
          colorStr = "#" + colorStr;
        }

        color = (DRAW.Color)colConverter.ConvertFromString(colorStr);
      }
    }

    /// <summary>
    /// Instantiates the specified analyzer and places it in the analyzer dict.
    /// </summary>
    /// <param name="color">The color that this analyzer has been assigned.</param>
    /// <param name="dll">The assembly where this analyzer exists.</param>
    /// <param name="id">The unique id of this analyzer.</param>
    private static void AddToAnalyzerDict(DRAW.Color color, string dll, string id)
    {
      ISpriteAnalyzer analyzer = LoadAnalyzer(dll, id);
      if(analyzer != null)
      {
        m_analyzerDict[color] = analyzer;
      }
    }

    /// <summary>
    /// Instantiates the specified analyzer and returns it
    /// </summary>
    /// <param name="dll">The dll where this analyzer resides.</param>
    /// <param name="id">The id of this analyzer (in case there are more than
    /// one analyzer in the same assembly).</param>
    /// <returns>A reference to the new analyzer.</returns>
    private static ISpriteAnalyzer LoadAnalyzer(string dll, string id)
    {
      string installDir = string.Empty;
      installDir = Constants.Path.InstallDir;

      string assemblyPath = Constants.Path.AnalyzerDir + dll;
      Assembly assembly = null;
      //Check to see if this assembly has already been loaded and put in the
      //assembly dictionary.
      if (!m_assebmlyDict.TryGetValue(assemblyPath, out assembly))
      {
        //Assembly was not in the assembly dictionary - load it from the 
        //analyzer directory
        assembly = Assembly.LoadFile(assemblyPath);
      }
      
      foreach(Type type in assembly.GetExportedTypes())
      {
        ISpriteAnalyzer analyzer = 
          Activator.CreateInstance(type) as ISpriteAnalyzer;
        //Perform a null check - only types that implement ISpriteAnalyzer will
        //not be null
        if (analyzer != null)
        {
          //Check if this is the specific analyzer we want by comparing its id
          if(analyzer.ID == id)
          {
            //We found it, return it
            return analyzer;
          }
        }
      }

      return null;
    }

    /// <summary>
    /// Prints out info that will assist the user in properly formatting a 
    /// command-line call of this program.
    /// </summary>
    private static void PrintHelpInfo()
    {
      //
    }
  }
}
