DLLS = $(addprefix -reference:,$(filter %.dll,$^))
CODE = $(filter %.cs,$^)

Gamma.svg : gamma.gpi gamma.data factorials.data
	gnuplot $<

gamma.data : main.exe
	mono $< > $@

main.exe : main.cs sfuns.dll
	mcs -target:exe -out:$@ $(CODE) $(DLLS)

sfuns.dll : gamma.cs
	mcs -target:library -out:$@ $(CODE)

factorials.data : Makefile
	>$@
	echo 2.5 0.284 >>$@
	echo 3.5 1.201 >>$@
	echo 0.5 0.572 >>$@
	echo 1.5 -0.121 >>$@
	echo 1.0 0 >>$@
	echo 2.0 0 >>$@
	echo 3.0 0.693 >>$@

main: main.exe sfuns.dll
	mkbundle -o $@ --simple $^

clean:
	$(RM) *.exe *.dll *.svg *.data main
