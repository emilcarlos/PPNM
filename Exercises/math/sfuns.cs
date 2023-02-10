using System;

class prime {
	
	static double gamma(double x){
	if(x<0)return Math.PI/Math.Sin(Math.PI*x)/gamma(1-x);
	if(x<9)return gamma(x+1)/x;
	double lngamma=x*Math.Log(x+1/(12*x-1/x/10))-x+Math.Log(2*Math.PI/x)/2;
	return Math.Exp(lngamma);
	}

	static void print(){
		double gam_one = gamma(1.0);
		Console.WriteLine("gamma(1.0) = " + gam_one);
	}
}
