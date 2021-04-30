<!-- markdownlint-disable MD033 -->
# bluebird

this is the repository that contains the source code for the bluebird interpreter.

## what is it?

bluebird is an interpreter written in C#, that only exists because i was bored (much like all of my other repositories.)

## where do i get the compiler?

if you download and install mono <a href="https://www.mono-project.com/download/stable/">here</a>, it will provide you with all of the tools you need to compile and run bluebird (including csc.exe etc).

## compiling

bluebird is natively compiled using csc.exe. the command to compile the code into a working binary is as follows:

```shell
csc main.cs /out:bluebird.exe
```

this will produce an executable binary with the filename 'bluebird.exe'.

## command list

once the program is compiled, and you have executed it, you can run the 'help' or '?' command to view a list of available commands.
