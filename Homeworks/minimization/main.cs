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
			double step = Math.Pow(2, -26);
			if(x[i] != 0){step = Math.Abs(x[i])*Math.Pow(2,-26);}
			stepped[i] = x[i]+step;
			grad[i] = (f(stepped)-f(x))/step;
		}
		return grad;
	}

	public static vector qnewton(Func<vector,double> f, vector x, double acc, double eps){
		matrix B = new matrix(x.size, x.size);
		B.set_unity();
		vector grad = gradient(f, x);
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
					break;
				}
				lambda = lambda*0.5;
				if(lambda < 1.0/1024){
					x = x + (lambda *step);
					B.set_unity();
					break;
				}
			}
		}
		return x;
	}

	static double rosen(vector x){
		return Pow(1-x[0], 2) + 100*Pow(x[1]-Pow(x[0],2),2);
	}

	static double himmel(vector x){
		return Pow(Pow(x[0],2)+x[1]-11,2) + Pow(x[0]+Pow(x[1],2)-7,2);
	}

	static void Main(){
		vector start = new vector(3,3);
		vector mini = qnewton(rosen, start, 1e-2, 1e-10);
		mini.print();
		start = new vector(25,25);
		mini = qnewton(himmel, start, 1e-2, 1e-10);
		mini.print();
	}	
}
