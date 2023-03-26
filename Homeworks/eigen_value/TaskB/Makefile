DIR = $(HOME)/PPNM/matlib/matrix

Out.txt: main.exe
	mono main.exe -rmax:10 -dr:0.3 > Out.txt

matrix.dll : $(DIR)/vector.cs $(DIR)/matrix.cs
	mcs -target:library -out:./matrix.dll $^

main.exe: main.cs matrix.dll
	mcs -reference:matrix.dll main.cs

clean: 
	rm -rf *.txt *.exe
