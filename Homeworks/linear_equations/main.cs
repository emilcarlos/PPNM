using System;
using static System.Console;
using static System.Math;
using static System.Random;

class main{
	public static class QRGS{
		// Decomp function
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
		// Solve function
		public static vector solve(matrix Q, matrix R, vector b){
			vector x = Q.T*b;
			for(int i=x.size-1;i>=0;i--){
				double sum=0;
				for(int k=i+1;k<x.size;k++) sum = sum + R[i,k]*x[k];
				x[i] = (x[i]-sum)/R[i,i];
			}
			return x;
		}
		
		// Determinant function
		public static double det(matrix R){
			double prod = 1.0;
			for(int i=0;i<R.size1;i++){
				prod = prod * R[i,i];
			}
			return prod;
		}

		// Inverse function
		public static matrix inverse(matrix Q, matrix R){
			matrix B = new matrix(Q.size1, Q.size2);
			for(int i=0;i<Q.size2;i++){
				vector ei = new vector(Q.size2);
				ei[i] = 1;
				B[i] = QRGS.solve(Q, R, ei);
			}
			return B;
		}
				
	}


	static void Main(){
		// TASK A
		WriteLine("TASK A");

		//random tall
		var random = new Random();
		int random_n = random.Next(2,7);
		int random_m = random.Next(2,random_n);
		matrix random_a = new matrix(random_n, random_m);
		for(int i=0;i<random_n;i++){
			for(int j=0;j<random_m;j++){
				random_a[i,j] = random.NextDouble();
			}
		}
		
		//random quadratic
		int random_s = random.Next(2,7);
		matrix random_quad = new matrix(random_s, random_s);
		for(int i=0;i<random_s;i++){
			for(int j=0;j<random_s;j++){
				random_quad[i,j] = random.NextDouble();
			}
		}

		//random vector
		vector random_v = new vector(random_s);
		for(int i=0;i<random_s;i++){
			random_v[i] = random.NextDouble();
		}
		
		//check that decomp is working
		matrix a = random_a.copy();
		matrix r = new matrix(random_m, random_m);

		a.print("This is A before decomposition");
		r.print("This is R before decomposition");

		QRGS.decomp(a, r);

		a.print("This is Q after decomposition");
		r.print("This is R after decomposition");

		matrix check1 = a.T*a;
		matrix check2 = a*r;

                WriteLine("Check status:");

                r.set_unity();
                if(check1.approx(r)){
                        WriteLine("Q_transpose * Q equals identity matrix.");
                }
                else{
                        WriteLine("Q_transpose * Q does not equal identity matrix");
                }
                if(random_a.approx(check2)){
                        WriteLine("QR is identical with original A.");
                }
                else{
                        WriteLine("QR is not equal to original A.");
		}
		WriteLine("");

		//check that solve is working
		WriteLine("The following is the check for the solve function:");
		matrix A = random_quad;
		matrix R = new matrix(random_s, random_s);
		vector c = random_v;

		A.print("A:");
		c.print("b:");

		QRGS.decomp(A, R);
		vector x = QRGS.solve(A, R, c);
		x.print("x:");

		vector check3 = (A*R)*x;

		if(c.approx(check3)){
			WriteLine("Ax is equal to b.");
		}
		else{
			WriteLine("Ax is not equal to b.");
		}

		// TASK B
		WriteLine("TASK B");

		//random sqaure matrix
                int rand_s = random.Next(2,7);
                matrix A_b = new matrix(rand_s, rand_s);
                for(int i=0;i<rand_s;i++){
                        for(int j=0;j<rand_s;j++){
                                A_b[i,j] = random.NextDouble();
                        }
                }
		A_b.print("A:");

		// check inverse function
		matrix Q_b = A_b.copy();
		matrix R_b = new matrix(Q_b.size2, Q_b.size2);
		QRGS.decomp(Q_b, R_b);
		matrix B = QRGS.inverse(Q_b, R_b);
		B.print("B:");

		matrix check4 = A_b*B;
		R_b.set_unity();
		if(R_b.approx(check4)){
			WriteLine("B is the inverse of A.");
		}
		else{
			WriteLine("B is not the inverse of A.");
		}
		

	}
}	
