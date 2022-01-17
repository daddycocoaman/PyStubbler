# PyStubbler

A .NET Core Implementation for the builder found at https://github.com/mcneel/pythonstubs/tree/master/builder. Creates Python stubs from .NET assemblies. Anything specific to certain DLL packages (like Rhino) have been removed from this package.


## Usage

```
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
```

## Thanks

Thanks to the pythonstubs team for the original code.