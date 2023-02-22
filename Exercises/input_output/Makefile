out.txt: main.exe main2.exe Makefile
	mono main.exe -numbers:1,2,3,4,5 > out.txt
	echo 1 2 3 4 5 | mono main2.exe > out.txt

main.exe: main.cs
	mcs main.cs -target:exe -out:main.exe

main2.exe: main2.cs
	mcs main2.cs -target:exe -out:main2.exe

clean:
	rm --force out.txt main.exe

