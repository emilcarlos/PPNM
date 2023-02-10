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
	} 
}
