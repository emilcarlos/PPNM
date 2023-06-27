DIR = $(HOME)/PPNM/matlib/matrix
DLLS = $(addprefix -reference:,$(filter %.dll,$^))
CODE = $(filter %.cs,$^)

interpolate.svg : interpolate.gpi interpolate.data points.data
	gnuplot $<

Out.txt: main.exe
	mono main.exe > Out.txt

interpolate.data : main.exe
	mono $< interpolate > $@

points.data : main.exe
	mono $< points > $@


matrix.dll : $(DIR)/vector.cs $(DIR)/matrix.cs
	mcs -target:library -out:./matrix.dll $^

main.exe: main.cs matrix.dll
	mcs -reference:matrix.dll main.cs

main: main.exe
	mkbundle -o $@ --simple $^

clean: 
	rm -rf *.txt *.exe
