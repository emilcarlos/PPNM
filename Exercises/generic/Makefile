
out.txt: main.exe Makefile input.txt
	cat input.txt | mono main.exe 2> out.txt

main.exe: main.cs
	mcs main.cs -target:exe -out:main.exe

clean:
	rm --force out.txt main.exe
