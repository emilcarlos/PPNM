using System;
using static System.Console;
using static System.Math;
using static System.Random;
using System.Linq;
using System.IO;
using static System.IO.TextWriter;

public class main{
	public static vector gradient(Func<vector, double> f, vector x){
		vector grad = new vector(x.size);
		for(int i=0;i<x.size;i++){
			vector stepped = x.copy();
			double step = 1e-7;
			if(x[i] != 0){step = Math.Abs(x[i])*1e-7;}
			stepped[i] = x[i]+step;
			grad[i] = (f(stepped)-f(x))/step;
		}
		return grad;
	}

	public static (vector, int) qnewton(Func<vector,double> f, vector x, double acc, double eps){
		matrix B = new matrix(x.size, x.size);
		B.set_unity();
		vector grad = gradient(f, x);
		int step_counter = 0;
		while(grad.norm() > acc){
			grad = gradient(f, x);
			vector step = -B*grad;
			double lambda = 1.0;
			while(1==1){
				if(f(x+(lambda*step))<f(x)){
					x = x + (lambda * step);
					vector s = lambda * step;
					vector y = gradient(f, x+s) - gradient(f, x);
					vector u = s - (B*y);
					matrix uut = new matrix(u.size, u.size);
					for(int i=0;i<u.size;i++){
					for(int j=0;j<u.size;j++){
						uut[i,j] = u[i]*u[j];
					}
					}
					if(Math.Abs(u.dot(y)) > eps){
						matrix B_step = uut/(u.dot(y));
						B = B + B_step;
					}
					step_counter = step_counter + 1;
					break;
				}
				lambda = lambda*0.5;
				if(lambda < Math.Pow(2,-25)){
					x = x + (lambda *step);
					B.set_unity();
					step_counter = step_counter + 1;
					break;
				}
			}
		}
		return (x, step_counter);
	}

	static double rosen(vector x){
		return Pow(1-x[0], 2) + 100*Pow(x[1]-Pow(x[0],2),2);
	}

	static double himmel(vector x){
		return Pow(Pow(x[0],2)+x[1]-11,2) + Pow(x[0]+Pow(x[1],2)-7,2);
	}

	static void Main(){
		var random = new Random();
		int x = random.Next(-10,10);
		int y = random.Next(-10,10);
		vector start = new vector(x,y);
		vector mini = qnewton(rosen, start, 1e-2, 1e-10).Item1;
		int rosen_step = qnewton(rosen, start, 1e-2, 1e-10).Item2;
		WriteLine($"\nMinimum for Rosenbrock's Valley function found in {rosen_step} steps:");
		WriteLine($"x = {mini[0]}, y = {mini[1]}");
		WriteLine($"(starting point: x = {x}, y = {y})");

		x = random.Next(-100,100);
		y = random.Next(-100,100);
		start = new vector(x,y);
		mini = qnewton(himmel, start, 1e-2, 1e-10).Item1;
		int himmel_step = qnewton(himmel, start, 1e-2, 1e-10).Item2;
		WriteLine($"\nMinimum for Himmelblau's Function found in {himmel_step} steps:");
		WriteLine($"x = {mini[0]}, y = {mini[1]}");
		WriteLine($"(starting point: x = {x}, y = {y})");
	}	
}
