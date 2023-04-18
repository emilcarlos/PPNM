
DIR = $(HOME)/PPNM/matlib/matrix

All: out.txt error.svg

out.txt: main.exe
	mono $< > $@

matrix.dll : $(DIR)/vector.cs $(DIR)/matrix.cs
	mcs -target:library -out:./matrix.dll $^

main.exe: main.cs matrix.dll
	mcs -reference:matrix.dll main.cs

error.svg : error.gpi error.data table.hardcode
	gnuplot $<

error.data : main.exe
	mono $< > $@

clean: 
	rm -rf *.txt *.exe *.svg *.data
