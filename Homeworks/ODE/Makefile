DIR = $(HOME)/PPNM/matlib/matrix

All: simple.svg test.svg pendul.svg

simple.svg: simple.gpi out.txt
	gnuplot $<

test.svg: test.gpi out.txt
	gnuplot $<

pendul.svg: pendul.gpi out.txt
	gnuplot $<

out.txt: main.exe
	mono $< > $@

matrix.dll : $(DIR)/vector.cs $(DIR)/matrix.cs
	mcs -target:library -out:./matrix.dll $^

main.exe: main.cs matrix.dll
	mcs -reference:matrix.dll main.cs

clean: 
	rm -rf *.txt *.exe *.svg
