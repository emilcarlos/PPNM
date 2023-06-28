using System;
using static System.Console;
using static System.Math;
using static System.Random;
using System.Linq;
using System.IO;
using static System.IO.TextWriter;

public class main{
	// gradient function from minimization homework
	public static vector gradient(Func<int, vector, vector, vector, double> f, int n, vector x, vector y, vector p){
		vector grad = new vector(p.size);
		for(int i=0;i<p.size;i++){
			vector stepped = p.copy();
			double step = 1e-7;
			if(p[i] != 0){step = Math.Abs(p[i])*1e-7;}
			stepped[i] = p[i]+step;
			grad[i] = (f(n, x, y, stepped)-f(n, x, y, p))/step;
		}
		return grad;
	}

	// function that performs Quasi-Newton minimization 
	public static (vector, int) qnewton(Func<int, vector, vector, vector, double> f, int n, vector x, vector y, vector p, double acc, double eps){
		matrix B = new matrix(p.size, p.size);
		B.set_unity();
		vector grad = gradient(f, n, x, y, p);
		int step_counter = 0;
		while(grad.norm() > acc){
			grad = gradient(f, n, x, y, p);
			vector step = -B*grad;
			double lambda = 1.0;
			while(1==1){
				if(f(n, x, y, p+(lambda*step))<f(n, x, y, p)){
					p = p + (lambda * step);
					vector s = lambda * step;
					vector Y = gradient(f, n, x, y, p+s) - gradient(f, n, x, y, p);
					vector u = s - (B*Y);
					matrix uut = new matrix(u.size, u.size);
					for(int i=0;i<u.size;i++){
					for(int j=0;j<u.size;j++){
						uut[i,j] = u[i]*u[j];
					}
					}
					if(Math.Abs(u.dot(Y)) > eps){
						matrix B_step = uut/(u.dot(Y));
						B = B + B_step;
					}
					step_counter = step_counter + 1;
					break;
				}
				lambda = lambda*0.5;
				if(lambda < Math.Pow(2,-25)){
					p = p + (lambda *step);
					B.set_unity();
					step_counter = step_counter + 1;
					break;
				}
			}
		}
		return (p, step_counter);
	}
	
	// Gaussian wavelet used as activation function
	static double activation(double x){
		return x * Math.Exp(-x*x);
	}
	
	// Response function for the network
	static double response(double x, int n, vector p){
		double Sum = 0.0;
		for(int i=0;i<n;i++){
			Sum = Sum + activation((x-p[i*3])/p[i*3+1])*p[i*3+2];
		}
		return Sum;
	}
	
	// Cost function
	static double cost(int n, vector x, vector y, vector p){
		double Sum = 0;
		for(int k=0;k<x.size;k++){
			Sum = Sum + Math.Pow(response(x[k], n, p)-y[k],2);
		}
		return Sum;
	}
	
	// Training function
	static vector train(int n, vector p, vector x, vector y){
		return qnewton(cost, n, x, y, p, 1e-2,1e-10).Item1;
	}
	
	// Function to be approximated
	static double function(double x){
		return Math.Cos(5*x-1)*Math.Exp(-x*x);
	}

	static void Main(string[] args){
		// set number of neurons
		int n = 20;

		// create random start values for parameters
		var random = new Random();
		vector p = new vector(3*n);
		for(int i=0;i<p.size;i++){
			p[i] = random.NextDouble();
		}

		// create data to tabulate function on [-1,1]
		vector xs = new vector("-1.0, -0.8, -0.6, -0.4, -0.2, -0.0, 0.2, 0.4, 0.6, 0.8, 1.0");
		vector ys = new vector(xs.size);
		for(int i=0;i<xs.size;i++){
			ys[i] = function(xs[i]);
		}

		// train network to minimize cost function
		vector trained = train(n, p, xs, ys);
		
		// pass data needed to create plot
		foreach(var arg in args){
		if(arg == "points"){
			//pass points to points.data
			for(int i=0;i<xs.size;i++){
				WriteLine($"{xs[i]} {ys[i]}");
			}
		}

		if(arg == "fit"){
			//pass fitted points to fit.data
			for(int i=-110;i<110;i++){
				WriteLine($"{i*0.01}	{response(i*0.01,n,trained)}");
			}
		}

		if(arg == "function"){
			//pass function points to function.data
			for(int i=-110;i<110;i++){
				WriteLine($"{i*0.01}	{function(i*0.01)}");
			}
		}
		}
	}	
}
