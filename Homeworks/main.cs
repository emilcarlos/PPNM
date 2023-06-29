using System;
using static System.Console;
using static System.Math;

class main{
	public static class funcs{
		public static (vector,vector) rkstep12(
		Func<double,vector,vector> f
		double x,                    
		vector y,                    
		double h                     
		)
		{
		vector k0 = f(x,y);              
		vector k1 = f(x+h/2,y+k0*(h/2)); 
		vector yh = y+k1*h;              
		vector er = (k1-k0)*h;           
		return (yh,er);
		}
	}
}
