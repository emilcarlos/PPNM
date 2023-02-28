out.txt: main.exe Makefile
	mono main.exe > out.txt

main.exe: main.cs
	mcs main.cs -target:exe -out:main.exe

clean:
	rm --force out.txt main.exe

