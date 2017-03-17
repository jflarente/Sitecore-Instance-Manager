namespace SIM.Client.Commands
{
  using CommandLine;
  using JetBrains.Annotations;
  using SIM.Core.Commands;

  public class ImportCommandFacade : ImportCommand
  {
    [UsedImplicitly]
    public ImportCommandFacade()
    {
    }

    [Option('p', "path", Required = true, HelpText = "Path to Exported Solution")]
    public override string PathToExportedInstance { get; set; }

    [Option('s', "siteName", Required = true, HelpText = "Site Name")]
    public override string SiteName { get; set; }

    [Option('c', "connectionString", Required = false, HelpText = "SQL Connection String")]
    public override string ConnectionString { get; set; }

    [Option('t', "tempPath", Required = false, HelpText = "Temporary path to export files")]
    public override string TemporaryPathToUnpack { get; set; }

    [Option('r', "rootPath", Required = false, HelpText = "Instance Root Path")]
    public override string InstanceRoot { get; set; }

    [Option('u', "updateLicense", Required = false, HelpText = "Update License", DefaultValue = false)]
    public override bool UpdateLicense { get; set; }

    [Option('l', "pathToLicenseFile", Required = false, HelpText = "Path to license file")]
    public override string PathToLicenseFile { get; set; }

    [Option('b', "bindings", Required = false,
    HelpText = "Delimited string of bindings. Ex: 'habitat.dev.local:80,legal.dev.local:80'")]
    public override string Bindings { get; set; }

    [Option('y', "identityType", Required = false,
    HelpText = "AppPool Identity Type Ex: NetworkServer or SpecificUser")]
    public override string AppPoolIdentityType { get; set; }

    [Option('i', "userName", Required = false,
    HelpText = "AppPool User Name")]
    public override string AppPoolUserName { get; set; }

    [Option('w', "password", Required = false,
    HelpText = "AppPool user's Password")]
    public override string AppPoolPassword { get; set; }

    [Option('d', "databaseNamePrefix", Required = false,
    HelpText = "Database Name prefix - if not specified existing database names will have a numbered suffix added automatically")]
    public override bool UseInstanceNameForDatabasePrefix { get; set; }
  }
}