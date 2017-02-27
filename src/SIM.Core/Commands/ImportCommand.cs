

namespace SIM.Core.Commands
{
	
	using System;
	using System.Data.SqlClient;
	using System.IO;
	using System.Linq;
	using Sitecore.Diagnostics.Base;
	using JetBrains.Annotations;
	using SIM.Adapters.SqlServer;
	using SIM.Core.Common;
	using SIM.Pipelines;
	using SIM.Pipelines.Import;
	using SIM.Pipelines.Install;

	public class ImportCommand : AbstractCommand<string[]>
	{
		
		public virtual string PathToExportedInstance { get; [UsedImplicitly]set; }

		public virtual string SiteName { get; [UsedImplicitly]set; }

		public virtual string TemporaryPathToUnpack { get; [UsedImplicitly]set; }

		public virtual string InstanceRoot { get; [UsedImplicitly]set; }

		public virtual string ConnectionString { get; [UsedImplicitly]set; }

		public virtual bool UpdateLicense { get; [UsedImplicitly]set; }

		[CanBeNull]
		public virtual string PathToLicenseFile { get; [UsedImplicitly]set; }
		
		public virtual string Bindings { get; [UsedImplicitly]set; }


		protected override void DoExecute(CommandResult<string[]> result)
		{
			Assert.ArgumentNotNull(result, nameof(result));

			var siteName = SiteName;
			Assert.ArgumentNotNullOrEmpty(SiteName, nameof(SiteName));

			var profile = Profile.Read();
			var repository = profile.LocalRepository;
			Ensure.IsNotNullOrEmpty(repository, "Profile.LocalRepository is not specified");
			Ensure.IsTrue(Directory.Exists(repository), "Profile.LocalRepository points to missing folder");

			var license = profile.License;
			Ensure.IsNotNullOrEmpty(license, "Profile.License is not specified");
			Ensure.IsTrue(File.Exists(license), "Profile.License points to missing file");

			var builder = profile.GetValidConnectionString();

			var instancesFolder = "";
			if (String.IsNullOrEmpty(InstanceRoot))
			{
				instancesFolder = profile.InstancesFolder;
				Ensure.IsNotNullOrEmpty(instancesFolder, "Profile.InstancesFolder is not specified");
				Ensure.IsTrue(Directory.Exists(instancesFolder), "Profile.InstancesFolder points to missing folder");
			}
			else
			{
				instancesFolder = InstanceRoot;
			}
			var rootPath = Path.Combine(instancesFolder, siteName);

			Ensure.IsTrue(!Directory.Exists(rootPath), "Folder already exists: {0}", rootPath);


			PipelineManager.Initialize( XmlDocumentEx.LoadXml(PipelinesConfig.Contents).DocumentElement);

			var sqlServerAccountName = SqlServerManager.Instance.GetSqlServerAccountName(builder);
			var webServerIdentity = Settings.CoreInstallWebServerIdentity.Value;

			var bindings = Bindings.Split(',').Select(part => part.Split(':')).ToDictionary(split => split[0].ToString(), split=> Int32.Parse(split[1].ToString()));
			
			var importArgs = new ImportArgs(PathToExportedInstance, siteName,TemporaryPathToUnpack,rootPath,new SqlConnectionStringBuilder(ConnectionString), UpdateLicense,PathToLicenseFile,bindings);

			var controller = new AggregatePipelineController();
			PipelineManager.StartPipeline("import", importArgs, controller, false);

			result.Success = !string.IsNullOrEmpty(controller.Message);
			result.Message = controller.Message;
			result.Data = controller.GetMessages().ToArray();
		}
	}
}
