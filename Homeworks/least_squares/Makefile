DLLS = $(addprefix -reference:,$(filter %.dll,$^))
CODE = $(filter %.cs,$^)

DIR = $(HOME)/PPNM/matlib/matrix

All : fit.svg Out.txt
	mv -t outputs $^

fit.svg : fit.gpi fit.data exp.data
	gnuplot $<

fit.data : main.exe
	mono $< plot > $@

Out.txt : main.exe
	mono $< halflife > $@

matrix.dll : $(DIR)/vector.cs $(DIR)/matrix.cs
	mcs -target:library -out:./matrix.dll $^

main.exe: main.cs matrix.dll
	mcs -target:exe -out:$@ $(CODE) $(DLLS)

exp.data : Makefile
	>$@
	echo 1 117 5 >>$@
	echo 2 100 5 >>$@
	echo 3 88 5 >>$@
	echo 4 72 4 >>$@
	echo 6 53 4 >>$@
	echo 9 29.5 3 >>$@
	echo 10 25.2 3 >>$@
	echo 13 15.2 2 >>$@
	echo 15 11.1 2 >>$@

main : main.exe matrix.dll
	mkbundle -o $@ --simple $^

clean: 
	$(RM) *.exe *.dll *.svg *.data *.txt main
