<!-- markdownlint-disable MD033 -->
# bluebird

this is the repository that contains the source code for the bluebird interpreter.

## what is it?

bluebird is an interpreter written in C#, that only exists because i was bored (much like all of my other repositories.)

## compiling

bluebird is natively compiled using csc.exe. this project has a makefile which you can execute with the correct targets though. the 'clean' target will delete the bluebird binary, the 'binary' target will produce an executable binary with the filename 'bluebird.exe' in the 'bin' directory, and the 'run' target executes the program. so the ideal make command would be:

```make
make clean binary run
```

## where do i get the compiler?

if you download and install mono <a href="https://www.mono-project.com/download/stable/">here</a>, it will provide you with all of the tools you need to compile and run bluebird (including csc.exe etc).

## what's mono?

as quoted by mono on their homepage, "Sponsored by Microsoft, Mono is an open source implementation of Microsoft's .NET Framework based on the ECMA standards for C# and the Common Language Runtime." so yeah, that's what mono is. if you want to know more, you can read more about them on the homepage of their website <a href="https://www.mono-project.com/">here</a>.

## command list

once the program is compiled, and you have executed it, you can run the 'help' or '?' command to view a list of available commands.
