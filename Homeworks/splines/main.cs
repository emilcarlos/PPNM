using System;
using static System.Console;
using static System.Math;

class main{
	public static class funcs{
		public static int binsearch(double[] x, double z){ 
		if(!(x[0]<=z && z<=x[x.Length-1])) throw new Exception("binsearch: bad z");
		int i=0, j=x.Length-1;
		while(j-i>1){
			int mid=(i+j)/2;
			if(z>x[mid]) i=mid; else j=mid;
			}
		return i;
		}


		public static double linterp(double[] x, double[] y, double z){
        	int i=binsearch(x,z);
        	double dx=x[i+1]-x[i]; if(!(dx>0)) throw new Exception("seems wrong...");
        	double dy=y[i+1]-y[i];
        	return y[i]+dy/dx*(z-x[i]);
        	}

		public static double linterpInteg(double[] x, double[] y, double z){
		double sum = 0;
		int index = binsearch(x, z);
		for(int i=0;i<index;i++){
			//calculate integral of interval
			double dx=x[i+1]-x[i]; if(!(dx>0)) throw new Exception("seems wrong again...");
			double dy=y[i+1]-y[i];
			double a = dy/dx;
			double b = y[i]-a*x[i];
			double integral = (a/2)*(x[i+1]*x[i+1]-x[i]*x[i]) + b*dx;
			sum += integral;
			}
		double last_y = linterp(x, y, z);
		double last_x = z;
		double last_dx = last_x-x[index];
		double last_dy = last_y-y[index];
		double last_a = last_dy/last_dx;
		double last_b = y[index]-last_a*x[index];
		double last_integral = (last_a/2)*(last_x*last_x-x[index]*x[index]) + last_b*last_dx;
		sum += last_integral;
		return sum;
		}
	}

	static void Main(string[] args){
		double[] xs = {0,1,3,4,6,7,9,10,14};
                double[] ys = {0,1,1,2,2,8,8,0.2,0.2};

		foreach(var arg in args){
		if(arg == "points"){
			//pass points to points.data
			int l = xs.Length;
			for(int i=0;i<l;i++){
				WriteLine($"{xs[i]} {ys[i]}");
			}
		}

		if(arg == "liniar"){
			//generate data for plotting the interpolation 
			for(double z=xs[0];z<=xs[xs.Length-1];z+=1.0/10){
				WriteLine($"{z} {funcs.linterp(xs, ys, z)}");
			}
		}

		if(arg == "integral"){
			//generate data for showing the integral at x
			for(double z=xs[0];z<=xs[xs.Length-1];z+=1.0/10){
                                WriteLine($"{z} {funcs.linterpInteg(xs, ys, z)}");
                        }	
		}
		}
	}
}
