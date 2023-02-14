using System;

class maeps{
	public static bool approx(double a, double b, double acc=1e-9, double eps=1e-9){
		if (Math.Abs(b-a) < acc) return true;
		else if (Math.Abs(b-a) < Math.Max(Math.Abs(a),Math.Abs(b))*eps) return true;
		else return false;	

	}


	static void Main(){
		int i = 1;
		while(i+1>i) {i++;}
		Console.WriteLine("my max int = {0}", i);

		int j = -1;
		while(j-1<j) {j--;}
		Console.WriteLine("my min int = {0}", j);

		Console.WriteLine("MinValue = {0}", int.MinValue);
		
		// Opgave 2
		double x = 1;
		while(1+x != 1){x/=2;}
		x*=2;
		float y = 1F;
		while(1F+y != 1F){y/=2;}
		y*=2;
		double check_double = Math.Pow(2, -52);
		double check_float = Math.Pow(2, -23);
		Console.WriteLine("machine epsilon for double = " + x);
		Console.WriteLine("machine epsilon for float " + y);
		Console.WriteLine("check for double = " + check_double);
		Console.WriteLine("check for float = " + check_float);

		// Opgave 3
		Console.WriteLine("Opgave 3");
		
		int n = (int)1e6;
		double epsilon = Math.Pow(2, -52);
		double tiny = epsilon/2;
		double sumA = 0, sumB = 0;

		sumA += 1;
		for(int g=0; g<n; g++){sumA+=tiny;}
		for(int h=0; h<n; h++){sumB+=tiny;}
		sumB+=1;

		Console.WriteLine($"sumA-1 = {sumA-1:e} should be {n*tiny:e}");
		Console.WriteLine($"sumB-1 = {sumB-1:e} should be {n*tiny:e}");
		
		Console.WriteLine("The two numbers are different, because tiny is less than the machine epsilon. 1 + tiny will still be 1. Therefore sumA will end up as 1. SumB on the other hand will end up as n * tiny plus 1 since n * tiny is larger than the machine epsilon.");  
		
		// Opgave 4
		Console.WriteLine("Opgave 4");

		double d1 = 0.1+0.1+0.1+0.1+0.1+0.1+0.1+0.1;
		double d2 = 8*0.1;

		Console.WriteLine($"d1={d1:e15}");
		Console.WriteLine($"d2={d2:e15}");
		Console.WriteLine($"d1==d2 ? => {d1==d2}");
		
		Console.WriteLine($"approx(d1,d2)? => {approx(d1, d2)}");
	} 
}
