Out.txt : main.exe
	mono main.exe > Out.txt


main.exe : maeps.cs
	mcs maeps.cs -target:exe -out:main.exe 

clean: 
	rm -rf *.txt *.exe
