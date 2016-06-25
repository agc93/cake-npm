using System;
using System.Collections.Generic;
using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.Npm
{
    /// <summary>
    /// A wrapper around the Node Npm package manager
    /// </summary>
    public class NpmRunner : Tool<NpmRunnerSettings>
    {
        private DirectoryPath WorkingDirectory { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="NpmRunner" /> class.
        /// </summary>
        /// <param name="fileSystem">The file system</param>
        /// <param name="environment">The environment</param>
        /// <param name="processRunner">The process runner</param>
        /// <param name="tools">The tools</param>
        internal NpmRunner(IFileSystem fileSystem, ICakeEnvironment environment, IProcessRunner processRunner, IToolLocator tools) : base(fileSystem, environment, processRunner, tools)
        {
            WorkingDirectory = environment.WorkingDirectory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NpmRunner" /> class
        /// </summary>
        /// <param name="packagePath">Path to the package file</param>
        /// <param name="fileSystem">The file system</param>
        /// <param name="environment">The environment</param>
        /// <param name="processRunner">The process runner</param>
        /// <param name="tools">The tools</param>
        public NpmRunner(DirectoryPath packagePath, IFileSystem fileSystem, ICakeEnvironment environment, IProcessRunner processRunner, IToolLocator tools)
            : this(fileSystem, environment, processRunner, tools)
        {
            WorkingDirectory = packagePath;
        }

        /// <summary>
        /// execute 'npm install' with options
        /// </summary>
        /// <param name="configure">options when running 'npm install'</param>
        public void Install(Action<NpmInstallSettings> configure = null)
        {
            var settings = new NpmInstallSettings();
            configure?.Invoke(settings);

            var args = GetNpmInstallArguments(settings);

            Run(settings, args);
        }

        private ProcessArgumentBuilder GetNpmInstallArguments(NpmInstallSettings settings)
        {
            var args = new ProcessArgumentBuilder();
            settings.Evaluate(args);
            return args;
        }

        /// <summary>
        /// execute 'npm run'/'npm run-script' with arguments
        /// </summary>
        /// <param name="scriptName">name of the </param>
        /// <param name="configure"></param>
        public void RunScript(string scriptName, Action<NpmRunScriptSettings> configure = null)
        {
            var settings = new NpmRunScriptSettings(scriptName);
            configure?.Invoke(settings);
            var args = GetNpmRunArguments(settings);

            Run(settings, args);
        }

        private ProcessArgumentBuilder GetNpmRunArguments(NpmRunScriptSettings settings)
        {
            var args = new ProcessArgumentBuilder();
            settings.Evaluate(args);
            return args;
        }

        /// <summary>
        /// Gets the name of the tool
        /// </summary>
        /// <returns>the name of the tool</returns>
        protected override string GetToolName()
        {
            return "Npm Runner";
        }

        /// <summary>
        /// Gets the name of the tool executable
        /// </summary>
        /// <returns>The tool executable name</returns>
        protected override IEnumerable<string> GetToolExecutableNames()
        {
            yield return "npm.cmd";
            yield return "npm";
        }

        /// <summary>
        /// Gets the working directory.
        /// Defaults to the currently set working directory.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns>The working directory for the tool.</returns>
        protected override DirectoryPath GetWorkingDirectory(NpmRunnerSettings settings)
        {
            return WorkingDirectory;
        }
    }
}
