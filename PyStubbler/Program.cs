using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.IO;

using DocoptNet;

namespace PyStubbler
{
    class Program
    {
        private const string UsagePatterns = @"
Usage:
    PyStubbler (-h | --help)
    PyStubbler (-V | --version)
    PyStubbler [--dest=<dest_path>] [--search=<search_path>...]  <target_dll> ...

Options:
    -h --help                   Show this help
    -V --version                Show version
    --dest=<dest_path>          Path to save the subs to
    --search=<search_path>      Path to search for referenced assemblies
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
                    string? destPath = null;
                    if (arguments["--dest"] != null && arguments["--dest"].IsString)
                        destPath = (string)arguments["--dest"].Value;

                    // grab search paths if provided
                    string[]? searchPaths = null;
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

                    Console.WriteLine($"[*] Building stubs for {assmPath}");
                    try
                    {
                        StubBuilder.BuildAssemblyStubs(
                            assmPath,
                            destPath: destPath,
                            searchPaths: searchPaths
                            );
                        Console.WriteLine($"[*] Stubs saved to {destPath}");
                    }
                    catch (Exception sgEx)
                    {

                        Console.WriteLine($"[*] Error: Failed generating stubs | {sgEx.ToString()}");
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
