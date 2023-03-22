DLLS = $(addprefix -reference:,$(filter %.dll,$^))
CODE = $(filter %.cs,$^)

erf.svg : erf.gpi erf.data table.data
	gnuplot $<

erf.data : main.exe
	mono $< > $@

main.exe : main.cs sfuns.dll
	mcs -target:exe -out:$@ $(CODE) $(DLLS)

sfuns.dll : erf.cs
	mcs -target:library -out:$@ $(CODE)

table.data : Makefile
	>$@
	echo 0.0 0.0 >>$@
	echo 0.5 0.520499878 >>$@
	echo 1.0 0.842700793 >>$@
	echo 0.1 0.112462916 >>$@
	echo 0.2 0.222702589 >>$@
	echo 2.0 0.995322265 >>$@
	echo -0.5 -0.520499878 >>$@
	echo -1.0 -0.842700793 >>$@
	echo -0.1 -0.112462916 >>$@
	echo -0.2 -0.222702589 >>$@
	echo -2.0 -0.995322265 >>$@

main: main.exe sfuns.dll
	mkbundle -o $@ --simple $^

clean:
	$(RM) *.exe *.dll *.svg *.data main
