using System.Collections.Generic;

namespace SpriteAnalyzerInterfaces
{
  /// <summary>
  /// The ISpriteCollector interface defines the component that will identify
  /// sprite files to be analyzed. A class that implements this interface must
  /// be able to handle the specific assett structure it is designed to collect
  /// from. Additionally, it must be able to identify its corresponding assett.
  /// </summary>
  public interface ISpriteCollector
  {
    /// <summary>
    /// The name of this Sprite Collector's format. Example: "Game Maker Project"
    /// </summary>
    string SpriteCollectionFormatName
    {
      get;
    }

    /// <summary>
    /// The description of this Sprite Collector's format.
    /// </summary>
    string SpriteCollectionFormatDescription
    {
      get;
    }

    /// <summary>
    /// The uri of the icon that represents this Sprite Collector's format
    /// </summary>
    string SpriteCollectionFormatIcon
    {
      get;
    }

    /// <summary>
    /// This method is called when a user browses to a folder, this method
    /// implementation should identify any sprite collections that match the
    /// format this Sprite collector applies to.
    /// 
    /// A practical example of a sprite collection is a game maker project. The
    /// sprite image files are contained in the folder: 
    /// [GM Project Folder]\Sprites\images
    /// A Game Maker project could be identified by the presence of a 
    /// .project.gmx file in the root project folder, the presence of certain
    /// other folders, and the presence of the \Sprites\images folder.
    /// </summary>
    /// <param name="uri">The uri to search for sprite collections</param>
    /// <returns></returns>
    List<string> IdentifySpriteCollections(string searchUri);

    /// <summary>
    /// This method is called when sprites need to be gathered from a sprite 
    /// collection for analysis.
    /// </summary>
    /// <param name="spriteCollectionUri"></param>
    /// <returns></returns>
    List<string> CollectSprites
      (string spriteCollectionUri);
  }
}
