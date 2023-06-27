using System;
using static System.Console;
using static System.Math;
using static System.Random;
using System.Linq;
using System.IO;
using static System.IO.TextWriter;

public class main{
	//decomp function from earlier homework
	public static void decomp(matrix a, matrix r){
		int m = r.size1;
		for(int i=0;i<m;i++){
			double ai_norm = a[i].norm();
			matrix.set(r,i,i,ai_norm);
			a[i] = a[i]/r[i,i];
			for(int j=i+1;j<m;j++){
				r[i,j] = a[i].dot(a[j]);
				a[j] = a[j] - a[i]*r[i,j];
			}
		}	
	}
	
	//solve function from earlier homework
	public static vector solve(matrix Q, matrix R, vector b){
		vector x = Q.T*b;
		for(int i=x.size-1;i>=0;i--){
			double sum=0;
			for(int k=i+1;k<x.size;k++) sum = sum + R[i,k]*x[k];
			x[i] = (x[i]-sum)/R[i,i];
		}
		return x;
	}
	
	//function to calculate the Jacobian of a vector function f and coordinate vector x
	public static matrix jacobi(Func<vector,vector>f, vector x){
		vector current = f(x);
                matrix jacobian = new matrix(current.size, x.size);
                for(int i=0;i<x.size;i++){
                        vector alter_x = x.copy();
                        double step = Math.Abs(alter_x[i])*Pow(2, -26);
                        alter_x[i] += step;
                        vector altered = f(alter_x);
                        vector new_col = (altered-current)/step;
                        for(int j=0;j<new_col.size;j++){
                                jacobian[j,i] = new_col[j];
                        }
                }
		return jacobian;	
	}
	
	//implementation of Newton's method
	static vector newton(Func<vector,vector>f, vector x, double eps=1e-2){
	while(f(x).norm() >= eps){
		//calcultes Jacobian
		matrix jacobian = jacobi(f, x);
		vector current = f(x);

		//solve liniar set of equations
		matrix R = new matrix(jacobian.size2, jacobian.size2);
		decomp(jacobian, R);
		vector x_step = solve(jacobian, R, -current);

		//backtracking line search
		double lambda = 1.0;
		while(f(x+(x_step*lambda)).norm() > (1-lambda*0.5)*f(x).norm() & lambda > 1.0/1024.0){
			lambda = lambda*0.5;
		}

		//updating x
		x = x + (lambda*x_step);
	}
		return x; 
	}

	//neural net class
	public class ann{
   		private int N;
   		Func<double,double> f = x => x*Exp(-x*x);
   		private vector P;
   		public ann(int n, vector p){
			N = n;
			P = p;
		}
   		double response(double x){
      			double Sum = 0.0;
			for(int i;i<N;i++){
				double input = (x-p[i*3])/p[i*3+1];
				double addition = f(input);
				Sum = Sum + addition*p[i*3+2];
			}
			return Sum;
     		}
   		void train(vector x,vector y){
			for(int i;i<N;i++){
				double Sum_a = 0.0;
			}	
   		}
	}

	

	// Rosen function
	static vector rosen(vector x){
		vector function = new vector(0,0);
		//derivative with regards to x
		function[0] = -2*(1-x[0])-400*x[0]*x[0]*(x[1]-x[0]*x[0]);
		//derivative with regards to y
		function[1] = 200*(x[1]-x[0]*x[0]);
		return function;
	}

	static double function(double x){
		return Math.Cos(5*x-1)*Math.Exp(-x*x);
	}

	static void Main(){
			
	}
}
