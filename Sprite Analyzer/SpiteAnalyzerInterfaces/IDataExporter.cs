namespace SpriteAnalyzerInterfaces
{
  interface ISpriteDataExporter
  {
    /// <summary>
    /// The name of this export format. This text will be displayed in the 
    /// export format drop-down
    /// </summary>
    string ExportFormatName
    {
      get;
    }

    /// <summary>
    /// The description of this export format. This text will be displayed to
    /// the user in order to give the user a description of the data format.
    /// </summary>
    string ExportFormatDescription
    {
      get;
    }

    /// <summary>
    /// The file extension for this export format. If this data exporter is
    /// designed to handle the data-persistence part, this property can be set
    /// to return null.
    /// </summary>
    string ExportFormatFileExtension
    {
      get;
    }

    /// <summary>
    /// This method is a version of the main method of an IDataExporter 
    /// implementation. This method will be called when no valid file extension
    /// is provided via the ExportFormatFileExtension property.
    /// </summary>
    /// <param name="spriteData">The data captured during sprite analysis.
    /// </param>
    /// <param name="exportUri">Where to export the data.</param>
    void Export(ISpriteData spriteData, string exportUri);

    /// <summary>
    /// This method is a version of the main method of an IDataExporter 
    /// implementation. This method will be called when a valid file extension
    /// is provided via the ExportFormatFileExtension property. The string
    /// format of the data is returned and the SpriteAnalyzer framework will
    /// handle the persistence of the data file.
    /// </summary>
    /// <param name="spriteData">The data captured during sprite analysis.
    /// </param>
    /// <returns>The string format of the data.</returns>
    string Export(ISpriteData spriteData);
  }
}
