
out.txt: main.exe
	mono main.exe > out.txt
	
main.exe: main.cs
	mcs main.cs -target:exe -out:$@

clean:
	$(RM) *.exe *.dll [Oo]ut* log*
