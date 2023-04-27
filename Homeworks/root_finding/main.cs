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

	//simpel linear system with 2 equations and variables
	//correct answer: x[0]=-4, x[1]=1
	static vector simpel2(vector x){
		vector function = new vector(0,0);
		function[0] = x[0] - 7*x[1] + 11;
		function[1] = 5*x[0] + 2*x[1] + 18;
		return function;
	}
	
	//simpel linear system with 1 equation and variable
	static vector simpel1(vector x){
		vector function = new vector("0");
		function[0] = 5*x[0] - 11;
		return function;
	}	

	//simpel linear system with 3 equations and variables
	//correct answer: x[0]=3, x[1]=-8, x[2]=-2
	static vector simpel3(vector x){
		vector function = new vector(0,0,0);
		function[0] = 2*x[0] + 5*x[1] + 2*x[2] + 38;
		function[1] = 3*x[0] - 2*x[1] + 4*x[2] -17;
		function[2] = -6*x[0] + x[1] - 7*x[2] + 12;
		return function;
	}
	
	static vector rosen(vector x){
		vector function = new vector(0,0);
		//derivative with regards to x
		function[0] = -2*(1-x[0])-400*x[0]*x[0]*(x[1]-x[0]*x[0]);
		//derivative with regards to y
		function[1] = 200*(x[1]-x[0]*x[0]);
		return function;
	}

	static void Main(){
		var random = new Random();
		int a = random.Next(1,10);
		int b = random.Next(1,10);
		int c = random.Next(1,10);

		vector tester3 = new vector(a,b,c);
		vector tester2 = new vector(a,b);
		vector tester1 = new vector($"{a}");

		vector answer1 = newton(simpel1, tester1);
		vector answer2 = newton(simpel2, tester2);
		vector answer3 = newton(simpel3, tester3);
		vector answerR = newton(rosen, tester2);
		
		WriteLine("Linear equation: 5x = 11");
		WriteLine("Real solution: x=2.2");
		WriteLine($"Calculated solution: x={answer1[0]}");
		WriteLine("-------------------------------------------------------");
		WriteLine("System of linear equations:");
		WriteLine("x - 7y = -11");
		WriteLine("5x + 2y = -18");
		WriteLine("Real solution: x=-4, y=1");
		WriteLine($"Calculated solution: x={answer2[0]}, y={answer2[1]}");
		WriteLine("-------------------------------------------------------");
		WriteLine("System of linear equations:");
		WriteLine("2x + 5y + 2z = -38");
		WriteLine("3x -2y + 4z = 17");
		WriteLine("-6x + y - 7z = -12");
		WriteLine("Real solution: x=3, y=-8, z=-2");
		WriteLine($"Calculated solution: x={answer3[0]}, y={answer3[1]}, z={answer3[2]}");
		WriteLine("-------------------------------------------------------");
		WriteLine("Extremum of Rosenbrockfunction:");
		WriteLine("Theoretical global minimum: x=1, y=1");
		WriteLine($"Calculated extremum: x={answerR[0]}, y={answerR[1]}");
	}
}
