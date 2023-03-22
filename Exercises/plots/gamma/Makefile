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
	echo -1.5 2.363 >>$@
	echo -0.5 -3.545 >>$@
	echo 0.5 1.772 >>$@
	echo 1.5 0.886 >>$@
	echo 1.0 1.0 >>$@
	echo 2.0 1.0 >>$@
	echo 3.0 2.0 >>$@

main: main.exe sfuns.dll
	mkbundle -o $@ --simple $^

clean:
	$(RM) *.exe *.dll *.svg *.data main
