DLLS = $(addprefix -reference:,$(filter %.dll,$^))
CODE = $(filter %.cs,$^)

liniar.svg : liniar.gpi liniar.data points.data integral.data
	gnuplot $<

liniar.data : main.exe
	mono $< liniar > $@

points.data : main.exe
	mono $< points > $@

integral.data : main.exe
	mono $< integral > $@

main.exe : main.cs
	mcs -target:exe -out:$@ $(CODE) $(DLLS)

main: main.exe
	mkbundle -o $@ --simple $^

clean:
	$(RM) *.exe *.dll *.svg *.data main
