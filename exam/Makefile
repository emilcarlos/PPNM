DIR = $(HOME)/PPNM/matlib/matrix
DLLS = $(addprefix -reference:,$(filter %.dll,$^))
CODE = $(filter %.cs,$^)

All: wiki.svg line.svg interpolate.svg test.txt Read_Me.txt 

wiki.svg : wiki.gpi wiki.data
	gnuplot $<

line.svg : line.gpi line_inter.data
	gnuplot $<

interpolate.svg : interpolate.gpi interpolate.data
	gnuplot $<

Read_Me.txt: main.exe
	mono main.exe > Read_Me.txt

wiki.data : main.exe
	mono $< wiki > $@

line_inter.data : main.exe
	mono $< line_interpolation > $@


interpolate.data : main.exe
	mono $< interpolate > $@

test.txt : main.exe
	mono $< test > $@

matrix.dll : $(DIR)/vector.cs $(DIR)/matrix.cs
	mcs -target:library -out:./matrix.dll $^

main.exe: main.cs matrix.dll
	mcs -reference:matrix.dll main.cs

main: main.exe
	mkbundle -o $@ --simple $^

clean: 
	rm -rf *.txt *.exe
