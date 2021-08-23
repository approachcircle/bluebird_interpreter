# windows targets:
wclean:
	@echo deleting binary...
	@if not exist bin\ (echo binary folder does not exist, creating... && mkdir bin\)
	@if exist bin\bluebird.exe (del bin\bluebird.exe) else (echo binary file does not exist anyway)
wbinary:
	@if not exist bin\ (echo binary folder does not exist, creating... && mkdir bin\)
	@echo running compiler...
	@csc -recurse:src/*.cs -out:bin/bluebird.exe -d:TRACE -nologo
wrun:
	@echo executing...
	@bin\bluebird.exe
wrunmono:
	@echo executing with mono runtime...
	@mono bin\bluebird.exe

# linux targets:
lclean:
	@echo "deleting binary..."
	@/bin/bash -c "[[ ! -d bin/ ]] && echo \"binary folder does not exist, creating...\" && mkdir bin/ || true"
	@/bin/bash -c "[[ ! -f bin/bluebird ]] && echo \"binary file does not exist anyway\" || true"
	@/bin/bash -c "[[ -f bin/bluebird ]] && rm bin/bluebird || true"

lbinary:
	@/bin/bash -c "[[ ! -d bin/ ]] && echo \"binary folder does not exist, creating...\" && mkdir bin/ || true"
	@echo "running compiler..."
	@csc -recurse:src/*.cs -out:bin/bluebird -nologo
lrun:
	@echo executing...
	@mono bin/bluebird