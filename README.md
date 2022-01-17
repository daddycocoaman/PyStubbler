# PyStubbler

A .NET Core Implementation for the builder found at https://github.com/mcneel/pythonstubs/tree/master/builder. Creates Python stubs from .NET assemblies. Anything specific to certain DLL packages (like Rhino) have been removed from this package.


## Usage

```
Usage:
    PyStubbler (-h | --help)
    PyStubbler (-V | --version)
    PyStubbler [--dest=<dest_path>] [--search=<search_path>...] <target_dll> ...

Options:
    -h --help                   Show this help
    -V --version                Show version
    --dest=<dest_path>          Path to save the subs to
    --search=<search_path>      Path to search for referenced assemblies
```

## Thanks

Thanks to the pythonstubs team for the original code.