using System;
using static System.Console;
using static System.Math;

public static class com {
	public static void Main(){
		//numbers
		complex minus_one = new complex(-1,0);
		complex I = new complex(0,1);
		complex e = new complex(Math.E,0);
		complex pi = new complex(Math.PI,0);
		complex a = new complex(cmath.pow(2.0, -0.5),cmath.pow(2.0,-0.5));
		complex b = new complex(0.540302306, 0.841470985);
		complex c = cmath.pow(e, -pi*0.5);
		complex d = new complex(0, 11.548739357257748);

		//calculations
		complex square = cmath.sqrt(minus_one);
		complex square_i = cmath.sqrt(I);
		complex ei = cmath.pow(e, I);
		complex eipi = cmath.pow(ei, Math.PI);
		complex ii = cmath.pow(I, I);
		complex ln_i = cmath.log(I);
		complex sin_ipi = cmath.sin(I*Math.PI);
		
		//checks
		bool check1 = square.approx(I);
		bool check2 = square_i.approx(a);
		bool check3 = ei.approx(b);
		bool check4 = eipi.approx(minus_one);
		bool check5 = ii.approx(c);
		bool check6 = ln_i.approx(I*pi*0.5);
		bool check7 = sin_ipi.approx(d);

		
		//output
		WriteLine($"Sqrt of -1 is {square} which is {check1}.");
		WriteLine($"Sqrt of i is {square_i} which is {check2}.");
		WriteLine($"e^i is {ei} which is {check3}.");
		WriteLine($"e^(i*pi) is {eipi} which is {check4}.");
		WriteLine($"i^i is {ii} which is {check5}.");
		WriteLine($"ln(i) is {ln_i} which is {check6}.");
		WriteLine($"sin(i*pi) is {sin_ipi} which is {check7}.");
	}
}
