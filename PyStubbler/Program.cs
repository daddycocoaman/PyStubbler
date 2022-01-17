using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.IO;

using DocoptNet;
using PyStubblerLib;

namespace PyStubbler
{
    class Program
    {
        private const string UsagePatterns = @"
Usage:
    PyStubbler (-h | --help)
    PyStubbler (-V | --version)
    PyStubbler [--dest=<dest_path>] [--search=<search_path>...] [--prefix=<prefix>] [--postfix=<postfix>] [--dest-is-root] <target_dll> ...

Options:
    -h --help                   Show this help
    -V --version                Show version
    --dest=<dest_path>          Path to save the subs to
    --search=<search_path>      Path to search for referenced assemblies
    --prefix=<prefix>           Root namespace directory prefix
    --postfix=<postfix>         Root namespace directory postfix
    --dest-is-root              Use destination path for root namespace
";

        static void Main(string[] args)
        {
            var arguments = new Docopt().Apply(UsagePatterns, args, version: Assembly.GetExecutingAssembly().GetName().Version, exit: true);

            foreach (string targetDll in (ArrayList)arguments["<target_dll>"].Value)
            {
                string assmPath = targetDll;
                if (File.Exists(assmPath))
                {
                    // grab dest path if provided
                    string destPath = null;
                    if (arguments["--dest"] != null && arguments["--dest"].IsString)
                        destPath = (string)arguments["--dest"].Value;

                    // grab search paths if provided
                    string[] searchPaths = null;
                    if (arguments["--search"] != null && arguments["--search"].IsList)
                    {
                        List<string> lookupPaths = new List<string>();
                        foreach (string searchPath in (ArrayList)arguments["--search"].Value)
                        {
                            Console.WriteLine($"[*] Search path {searchPath}");
                            lookupPaths.Add(searchPath);
                        }
                        searchPaths = lookupPaths.ToArray();
                    }

                    // prepare generator configs
                    // grab pre and postfixes for root namespace dir names
                    var genCfg = new BuildConfig
                    {
                        Prefix = arguments["--prefix"] != null ? (string)arguments["--prefix"].Value : string.Empty,
                        Postfix = arguments["--postfix"] != null ? (string)arguments["--postfix"].Value : string.Empty,
                        DestPathIsRoot = arguments["--dest-is-root"] != null ? (bool)arguments["--dest-is-root"].Value : false,
                    };

                    Console.WriteLine($"[*] Building stubs for {assmPath}");
                    try
                    {
                        var dest = StubBuilder.BuildAssemblyStubs(
                            assmPath,
                            config: genCfg,
                            destPath: destPath,
                            searchPaths: searchPaths
                            );
                        Console.WriteLine($"[*] Stubs saved to {dest}");
                    }
                    catch (Exception sgEx)
                    {
                        Console.WriteLine($"[*] Error: Failed generating stubs | {sgEx.Message}");
                    }
                }
                else
                {
                    Console.WriteLine($"[*] Error: can not find {assmPath}");
                }
            }
        }
    }
}
