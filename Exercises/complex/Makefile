DIR = $(HOME)/PPNM/matlib/complex

Out.txt: main.exe
	mono main.exe > Out.txt

cmath.dll : $(DIR)/cmath.cs $(DIR)/complex.cs
	mcs -target:library -out:./cmath.dll $^

main.exe : main.cs cmath.dll
	mcs -reference:cmath.dll main.cs
