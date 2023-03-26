DIR = $(HOME)/PPNM/matlib/matrix

aut.txt: main.exe
	mono main.exe > Out.txt

matrix.dll : $(DIR)/vector.cs $(DIR)/matrix.cs
	mcs -target:library -out:./matrix.dll $^

main.exe: main.cs matrix.dll
	mcs -reference:matrix.dll main.cs

clean: 
	rm -rf *.txt *.exe
