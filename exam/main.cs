using System;
using static System.Console;
using static System.Math;
using static System.Random;

class main{
	public static class funcs{
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
				B[i] = funcs.solve(Q, R, ei);
			}
			return B;
		}
		
		// Binary search in 1 dimension
		public static int binsearch(double[] x, double z){ 
		if(!(x[0]<=z && z<=x[x.Length-1])) throw new Exception("binsearch: bad z");
		int i=0, j=x.Length-1;
		while(j-i>1){
			int mid=(i+j)/2;
			if(z>x[mid]) i=mid; else j=mid;
			}
		return i;
		}

		// Binary search for matrix index in 2 dimensions
		public static int[] doublesearch(double[] x, double[] y, double px, double py){
			int x_index = funcs.binsearch(x, px);
			int y_index = funcs.binsearch(y, py);
			int[] index = {y_index, x_index};
			return index;
		}

		// Bi-linear interpolation
		public static double bilinear(double[] x, double[] y, matrix F, double px, double py){
			int [] index = doublesearch(x, y, px, py);
			int i = index[0];
			int j = index[1];
			matrix System = new matrix(4, 4);
			vector b = new vector(4);
			int counter = 0;
			for(int I=i;I<i+2;I++){
				for(int J=j;J<j+2;J++){
					System[counter, 0] = 1;
					System[counter, 1] = x[J];
					System[counter, 2] = y[I];
					System[counter, 3] = x[J] * y[I];
					b[counter] = F[I, J];	
					counter = counter + 1;	
				}
			}
			matrix R = new matrix(4, 4);
			funcs.decomp(System, R);
			vector X = funcs.solve(System, R, b);
			double interpolation = X[0] + X[1]*px + X[2]*py + X[3]*px*py;
			return interpolation;
		}
				
	}
	
	static void Main(){
		WriteLine("Start of output file");
		matrix F = new matrix(4,4);
		F[0,0]=1; F[0,1]=1; F[0,2]=1; F[0,3]=1;
		F[1,0]=1; F[1,1]=1; F[1,2]=2; F[1,3]=1;
		F[2,0]=1; F[2,1]=5; F[2,2]=3; F[2,3]=1;
		F[3,0]=1; F[3,1]=1; F[3,2]=1; F[3,3]=1;
		double[] x = {0,1,2,3};
		double[] y = {0,1,2,3};
		double px = 2.3;
		double py = 0.3;
		double check = funcs.bilinear(x, y, F, px, py);
		WriteLine(check);
	}
}
