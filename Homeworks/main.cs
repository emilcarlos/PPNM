using System;
using static System.Console;
using static System.Math;

class main{
	public static class funcs{
		public static (vector,vector) rkstep12(
		Func<double,vector,vector> f /* the f from dy/dx=f(x,y) */
		double x,                    /* the current value of the variable */
		vector y,                    /* the current value y(x) of the sought function */
		double h                     /* the step to be taken */
		)
		{
		vector k0 = f(x,y);              /* embedded lower order formula (Euler) */
		vector k1 = f(x+h/2,y+k0*(h/2)); /* higher order formula (midpoint) */
		vector yh = y+k1*h;              /* y(x+h) estimate */
		vector er = (k1-k0)*h;           /* error estimate */
		return (yh,er);
		}
	}
}
