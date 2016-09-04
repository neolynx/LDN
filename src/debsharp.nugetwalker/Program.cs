namespace debsharp.nugetwalker
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;
    using NuGet;

    class MainClass
    {
        public static void Main(string[] args)
        {
            string packageList;
            try
            {
                packageList = Path.GetFullPath(args[0]);
            }
            catch
            {
                Console.WriteLine(@"
usage: debsharp.nugetwalker [path to packages.config file] [(options) URL of nuget source]
");
                return;
            }
            var repoAddress = args.Length > 1 ? args[1] : "https://www.nuget.org/api/v2/";
            var repo = PackageRepositoryFactory.Default.CreateRepository(repoAddress);
            var packages = XDocument.Load(new FileStream(packageList, FileMode.Open)).Root.Elements("package");

            foreach (var packageElement in packages)
            {
                var package = repo.FindPackage(packageElement.Attribute("id").Value);
                var depSet = package.DependencySets.Select(x =>
                {
                    var deps = string.Join(" ", x.Dependencies.Select(y => $"{y.Id}-{y.VersionSpec}"));
                    return $"{package.Id}-{package.Version.ToNormalizedString()}: {deps}";
                });
                foreach (var set in depSet)
                {
                    Console.WriteLine(set);
                }
            }
        }
    }
}
