using SpriteAnalyzerInterfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpriteCollector.GameMakerProject
{
  public class SpriteCollector : ISpriteCollector
  {
    #region Constants
    private const string Name = "GameMaker-Studio Project";
    private const string Description = "A GameMaker-Studio Project folder; " + 
      "sprites resources contained within this project will be analyzed";
    private static List<string> GameMakerDirectories = new List<string>()
    {
      "Configs",
      "fonts",
      "objects",
      "rooms",
      "scripts",
      "sprites"
    };
    #endregion

    #region Private Methods
    /// <summary>
    /// Private method that determines whether or not the directory at the
    /// given path is a GameMaker project.
    /// </summary>
    /// <param name="directoryUri">The uri of the directory to evaluate</param>
    /// <returns>A bool, true means it is a GM project</returns>
    private bool IsDirectoryGameMakerProject(string directoryUri)
    {
      foreach(string folder in GameMakerDirectories)
      {
        if(!Directory.Exists(directoryUri + @"\" + folder))
        {
          return false;
        }
      }
      return true;
    }
    #endregion

    #region ISpriteCollector Members
    /// <summary>
    /// The description of this Sprite Collector's sprite collection format.
    /// </summary>
    public string SpriteCollectionFormatDescription
    {
      get
      {
        return Description;
      }
    }

    /// <summary>
    /// The icon that represents the sprite collection format.
    /// </summary>
    public string SpriteCollectionFormatIcon
    {
      get
      {
        return null;
      }
    }

    /// <summary>
    /// The name of this Sprite Collector's sprite collection format.
    /// </summary>
    public string SpriteCollectionFormatName
    {
      get
      {
        return Name;
      }
    }

    /// <summary>
    /// This method collects the list of sprite uri's to be analyzed.
    /// </summary>
    /// <param name="spriteCollectionUri">The sprite collection uri.</param>
    /// <returns>A list of sprite uris</returns>
    public List<string> CollectSprites(string spriteCollectionUri)
    {
      return null;
    }

    /// <summary>
    /// This method identifies any sprite collections that exist in the given
    /// uri.
    /// </summary>
    /// <param name="searchUri">The uri to search for sprite collections
    /// </param>
    /// <returns>A list of sprite collection uris</returns>
    public List<string> IdentifySpriteCollections(string searchUri)
    {
      List<string> retList = new List<string>();
      string[] folders = Directory.GetDirectories(searchUri);

      foreach(string folder in folders)
      {
        if(IsDirectoryGameMakerProject(folder))
        {
          retList.Add(folder);
        }
      }

      return retList;
    }
    #endregion
  }
}
