DIR = $(HOME)/PPNM/matlib/matrix

All: out.txt points.data fit.data function.data fit.svg
	cat out.txt

fit.svg : fit.gpi fit.data points.data function.data
	gnuplot $<

points.data : main.exe
	mono $< points > $@

fit.data : main.exe
	mono $< fit > $@

function.data : main.exe
	mono $< function > $@

out.txt: main.exe
	mono $< > $@


matrix.dll : $(DIR)/vector.cs $(DIR)/matrix.cs
	mcs -target:library -out:./matrix.dll $^

main.exe: main.cs matrix.dll
	mcs -reference:matrix.dll main.cs

clean: 
	rm -rf *.txt *.exe *.svg *.data
