namespace debsharp.nugetwalker
{
	using System;
	using System.IO;
	using System.Linq;
	using System.Xml.Linq;
	using NuGet;

	class MainClass
	{
		public static void Main (string[] args)
		{
			var packageList = Path.GetFullPath (args [0]);
			var repoAddress = args.Length > 1 ? args [1] : "https://www.nuget.org/api/v2/";
			var repo = PackageRepositoryFactory.Default.CreateRepository (repoAddress);
			var packages = XDocument.Load (new FileStream (packageList, FileMode.Open)).Root.Elements("package");

			foreach (var packageElement in packages) {
				var package = repo.FindPackage (packageElement.Attribute ("id").Value);
				var depSet = package.DependencySets.Any ()
					? package.DependencySets.Select (x => string.Format (
					             "{0} depends on {1}", 
					             package.Id, 
					             string.Join (
						             ", ",
						             x.Dependencies.Select (y => y.Id + ": " + y.VersionSpec))))
					: new[]{ package.Id + " has no dependencies" };
				foreach (var set in depSet) {
					Console.WriteLine (set);
				}
			}
		}
	}
}
