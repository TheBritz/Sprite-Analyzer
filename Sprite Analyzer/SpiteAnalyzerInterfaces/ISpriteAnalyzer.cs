using System.Collections.Generic;

namespace SpriteAnalyzerInterfaces
{
  public interface ISpriteAnalyzer
  {
    /// <summary>
    /// This method collects data for the given points. 
    /// </summary>
    /// <param name="spriteUri">The uri of the sprite to analyze.</param>
    /// <param name="x">The x-position of the pixel to get the data for.</param>
    /// <param name="y">The y-position of the pixel to get the data for.</param>
    /// <param name="auxillaryColors">If this analyzer specified any auxillary
    /// colors, these colors are passed in this parameter. Each color name is a
    /// key in the dictionary with the hex-code color value that was assigned
    /// to it.</param>
    /// <returns></returns>
    Dictionary<string, string> Analyze(string spriteUri,
      int x,int y, Dictionary<string, string> auxillaryColors);

    /// <summary>
    /// The unique identifier for this analyzer, in case more than one analyzer
    /// are defined in the same assembly.
    /// </summary>
    string ID
    {
      get;
    }

    /// <summary>
    /// The name for this analyzer that will be displayed to the user. 
    /// </summary>
    string DisplayName
    {
      get;
    }

    /// <summary>
    /// All analyzers will have one key color but, dependent upon the 
    /// complexity of the analysis, an analyzer may need more colors to gather
    /// the data it needs. This getter will pass back a list of names for
    /// auxillary colors this analyzer may need. These names will be displayed
    /// to the user, so they must be unique and user-friendly.
    /// </summary>
    HashSet<string> AuxilliaryColorNames
    {
      get;
    }
  }
}
