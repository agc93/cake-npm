using System;
using Cake.Core;
using Cake.Core.Annotations;
using Cake.Core.IO;

namespace Cake.Npm
{
    /// <summary>
    /// Provides a wrapper around Npm functionality within a Cake build script
    /// </summary>
    [CakeAliasCategory("Npm")]
    public static class NpmRunnerAliases
    {
        /// <summary>
        /// Get an Npm runner
        /// </summary>
        /// <param name="context">The context</param>
        /// <returns></returns>
        /// <example>
        /// <para>Run 'npm install'</para>
        /// <para>Cake task:</para>
        /// <code>
        /// <![CDATA[
        /// Task("Npm-Install")
        ///     .Does(() =>
        /// {
        ///     Npm.Install();
        /// });
        /// ]]>
        /// </code>
        /// <para>Run 'npm install gulp'</para>
        /// <para>Cake task:</para>
        /// <code>
        /// <![CDATA[
        /// Task("Npm-Install-Gulp")
        ///     .Does(() =>
        /// {
        ///     Npm.Install(settings => settings.Package("gulp"));
        /// });
        /// ]]>
        /// </code>
        /// <para>Run 'npm install gulp -g'</para>
        /// <para>Cake task:</para>
        /// <code>
        /// <![CDATA[
        /// Task("Npm-Install-Gulp")
        ///     .Does(() =>
        /// {
        ///     Npm.Install(settings => settings.Package("gulp").Globally());
        /// });
        /// ]]>
        /// </code>
        /// <para>Run 'npm install --production'</para>
        /// <para>Cake task:</para>
        /// <code>
        /// <![CDATA[
        /// Task("Npm-Install-Production")
        ///     .Does(() =>
        /// {
        ///     Npm.Install(settings => settings.ForProduction());
        /// });
        /// ]]>
        /// </code>
        /// <para>Run 'npm install --force'</para>
        /// <para>Cake task:</para>
        /// <code>
        /// <![CDATA[
        /// Task("Npm-Install-Force")
        ///     .Does(() =>
        /// {
        ///     Npm.Install(settings => settings.Force());
        /// });
        /// ]]>
        /// </code>
        /// <para>Run 'npm run hello'</para>
        /// <para>Cake task:</para>
        /// <code>
        /// <![CDATA[
        /// Task("Npm-RunScript")
        ///     .Does(() =>
        /// {
        ///     Npm.RunScript("hello");
        /// });
        /// ]]>
        /// </code>
        /// <para>Run 'npm run arguments -- -debug "arg-value.file"'</para>
        /// <para>Cake task:</para>
        /// <code>
        /// <![CDATA[
        /// Task("Npm-RunScript-WithArgs")
        ///     .Does(() =>
        /// {
        ///     Npm.RunScript("arguments", settings => settings.WithArgument("-debug").WithArgument("arg-value.file"));
        /// });
        /// ]]>
        /// </code>
        /// </example>
        [CakePropertyAlias]
        public static NpmRunner Npm(this ICakeContext context)
        {
            if(context == null) throw new ArgumentNullException(nameof(context));

            return new NpmRunner(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
        }

        /// <summary>
        /// Gets an Npm runner in the context of the given package directory
        /// </summary>
        /// <param name="context">The context</param>
        /// <param name="packagePath">A path to the package.json (or a package directory)</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        [CakeMethodAlias]
        public static NpmRunner Npm(this ICakeContext context, Path packagePath)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (packagePath is FilePath)
            {
                packagePath = ((FilePath) packagePath).MakeAbsolute(context.Environment).GetDirectory();
            }
            return new NpmRunner((DirectoryPath)packagePath, context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
        }
    }
}