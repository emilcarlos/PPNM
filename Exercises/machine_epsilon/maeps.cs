using System;

class maeps{
	static void Main(){
		int i = 1;
		while(i+1>i) {i++;}
		Console.WriteLine("my max int = {0}", i);

		int j = -1;
		while(j-1<j) {j--;}
		Console.WriteLine("my min int = {0}", j);

		Console.WriteLine("MinValue = {0}", int.MinValue);

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
	} 
}
