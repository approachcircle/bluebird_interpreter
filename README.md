<!-- markdownlint-disable MD033 -->
# bluebird

this is the repository that contains the source code for the bluebird interpreter. bluebird is a custom command interpreter written in C#, that only exists because i was bored (much like all of my other repositories.)

## github statistics

[![approachcircle's GitHub stats](https://github-readme-stats.vercel.app/api?username=approachcircle)](https://github.com/approachcircle/bluebird_interpreter)

## compiling and OS compatibility

bluebird is compiled using the csc program in mono's API. the project contains a makefile for easy compilation and execution for both linux and windows. to compile bluebird for windows, you would use the following command:

```shell
make wclean wbinary wrun
```

and for compiling to linux using bash, you would use this command:
```shell
make lclean lbinary lrun
```
the wclean or lclean targets delete the compiled binary file, the wbinary or lbinary targets compile the source code into either "bin\bluebird.exe" for windows, or "bin/bluebird" for linux, and the wrun or lrun targets run the compiled binary file either straight from the command line as an executable (windows) or using the mono runtime program (linux)

### NOTICE

the bash shell for linux is required to be located at /bin/bash, as some commands use that path to execute commands through bash.

## where do i get the compiler?

if you download and install mono <a href="https://www.mono-project.com/download/stable/">here</a>, it will provide you with all of the tools you need to compile and run bluebird (including csc.exe etc).

## what's mono?

as quoted by mono on their homepage, "Sponsored by Microsoft, Mono is an open source implementation of Microsoft's .NET Framework based on the ECMA standards for C# and the Common Language Runtime." so yeah, that's what mono is. if you want to know more, you can read more about them on the homepage of their website <a href="https://www.mono-project.com/">here</a>.

## can i use .NET instead?

this project is ready to be used for the .NET framework if you would prefer, you just need to copy the code over into a .NET environment and build it using .NET's build tools instead. code written in C# that is targeted for the mono framework is mostly .NET ready unless it contains using directives referring to mono libraries, and if it does, you should be able to find nuget packages and .dlls containing the library code on the nuget gallery.

## command list

once the program is compiled, and you have executed it, you can run the 'help' or '?' command to view a list of available commands.
