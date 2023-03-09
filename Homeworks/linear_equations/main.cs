using System;
using static System.Console;
using static System.Math;

class main{
	public static class QRGS{
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
		
		public static vector solve(matrix Q, matrix R, vector b){
			vector x = Q.T*b;
			for(int i=x.size-1;i>=0;i--){
				double sum=0;
				for(int k=i+1;k<x.size;k++) sum = sum + R[i,k]*x[k];
				x[i] = (x[i]-sum)/R[i,i];
			}
			return x;
		}
		
		public static double det(matrix R){
			double prod = 1.0;
			for(int i=0;i<R.size1;i++){
				prod = prod * R[i,i];
			}
			return prod;
		}
	}


	static void Main(){
		//check that decomp is working
		matrix a = new matrix("2,1,1;1,1,3;0,0,1;4,4,4;1,2,3");
		matrix r = new matrix("0,0,0;0,0,0;0,0,0");
		a.print("This is A before decomposition");
		r.print("This is R before decomposition");
		QRGS.decomp(a, r);
		a.print("This is Q after decomposition");
		r.print("This is R after decomposition");

		matrix check1 = a.T*a;
		matrix check2 = a*r;

		check1.print("Q_transpose * Q is");
		check2.print("QR is");

		//check that solve is working
		WriteLine("The following is the check for the solve function:");
		matrix A = new matrix("8,3,6,8;2,8,7,9;1,2,1,1;9,1,9,1");
		matrix R = new matrix("0,0,0,0;0,0,0,0;0,0,0,0;0,0,0,0");
		vector c = new vector("4,3,1,5");

		A.print("A");
		c.print("b");
		QRGS.decomp(A, R);
		vector x = QRGS.solve(A, R, c);
		vector check3 = (A*R)*x;
		x.print("x");
		check3.print("Ax");
		//QRGS.det(r);
	}
}	
