
namespace SIM.Client.Commands
{
    using CommandLine;
    using JetBrains.Annotations;
	using SIM.Core.Commands;
	public class ImportCommandFacade : ImportCommand
    {
        [UsedImplicitly]
        public ImportCommandFacade() { }

        [Option('p', "path", Required = true,HelpText ="Path to Exported Solution")]
        public override string PathToExportedInstance { get; set; }

		[Option('s', "siteName", Required = true, HelpText = "Site Name")]
		public override string SiteName { get; set; }


		[Option('t', "tempPath", Required = true, HelpText = "Temporary path to export files")]
		public override string TemporaryPathToUnpack { get; set; }


		[Option('r', "rootPath", Required = false, HelpText = "Instance Root Path")]
		public override string InstanceRoot { get; set; }

		[Option('c', "connectionString", Required = true, HelpText = "SQL Connection String")]
		public override string ConnectionString { get; set; }

		[Option('u', "updateLicense", Required = true, HelpText = "Update License")]
		public override bool UpdateLicense { get; set; }

		[Option('l', "pathToLicenseFile", Required = false, HelpText = "Path to license file")]
		public override string PathToLicenseFile { get; set; }

		[Option('b', "bindings", Required = false, HelpText = "Delimited string of bindings. Ex: 'habitat.dev.local:80,legal.dev.local:80'")]
		public override string Bindings { get; set; }

	}
}