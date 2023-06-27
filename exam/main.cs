using System;
using static System.Console;
using static System.Math;
using static System.Random;

class main{
	public class genlist<T>{
	public T[] data;
	public int size => data.Length; // property
	public T this[int i] => data[i]; // indexer
	public genlist(){ data = new T[0]; }
	public void add(T item){ /* add item to the list */
		T[] newdata = new T[size+1];
		System.Array.Copy(data,newdata,size);
		newdata[size]=item;
		data=newdata;
	}
	}

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
		public static int binsearch(vector x, double z){ 
		if(!(x[0]<=z && z<=x[x.size-1])) throw new Exception("binsearch: bad z");
		int i=0, j=x.size-1;
		while(j-i>1){
			int mid=(i+j)/2;
			if(z>x[mid]) i=mid; else j=mid;
			}
		return i;
		}

		// Binary search for matrix index in 2 dimensions
		public static int[] doublesearch(vector x, vector y, double px, double py){
			int x_index = funcs.binsearch(x, px);
			int y_index = funcs.binsearch(y, py);
			int[] index = {y_index, x_index};
			return index;
		}

		// Bi-linear interpolation
		public static double bilinear(vector x, vector y, matrix F, double px, double py){
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
	
	static void Main(string[] args){
		//random matrix as grid
		var random = new Random();
		//int random_n = random.Next(2,20);
		//int random_m = random.Next(2,20);
		int random_n = 9;
		int random_m = 9;
		matrix random_a = new matrix(random_n, random_m);
		for(int i=0;i<random_n;i++){
			for(int j=0;j<random_m;j++){
				random_a[i,j] = random.NextDouble();
			}
		}
		//random_a.print("Test matrix");

		//coordinate vectors
		vector tx = new vector(random_m);
		vector ty = new vector(random_n);
		for(int i=0;i<random_m;i++){tx[i] = i;}
		for(int i=0;i<random_n;i++){ty[i] = i;}

		//random point in grid
		double tpx = random.Next(2,random_m-1) + 0.01*random.Next(2,100);
		double tpy = random.Next(2,random_n-1) + 0.01*random.Next(2,100);
		//WriteLine($"px = {tpx}, py = {tpy}");

		//calculates interpolated value for point
		double check2 = funcs.bilinear(tx, ty, random_a, tpx, tpy);
		//WriteLine($"interpolated value = {check2}");
		int x1 = (int)Math.Floor(tpx);
		int x2 = (int)Math.Ceiling(tpx);
		int y1 = (int)Math.Floor(tpy);
		int y2 = (int)Math.Ceiling(tpy);
		//WriteLine($"x1 = {x1}, x2 = {x2}, y1 = {y1}, y2 = {y2}");
		//WriteLine($"Q11 = {random_a[y1,x1]}, Q12 = {random_a[y2,x1]}, Q21 = {random_a[y1,x2]}, Q22 = {random_a[y2,x2]}");

		//generate plot to check interpolating function
		foreach(var arg in args){
		if(arg == "interpolate"){
			//pass points to interpolate.data
			for(int xi=1;xi<(random_m-1.02)*50;xi++){
				double XI = xi*0.02;
				for(int yi=1;yi<(random_n-1.02)*50;yi++){
					double YI = yi*0.02;
					WriteLine($"{XI}\t{YI}\t{funcs.bilinear(tx, ty, random_a, XI, YI)}");
				}
				WriteLine("");
			}
		}
		if(arg == "points"){
			//pass grid points to points.data
			for(int xi=0;xi<random_m;xi++){
				for(int yi=0;yi<random_n;yi++){
					WriteLine($"{tx[xi]}\t{ty[yi]}\t{random_a[yi,xi]}");
				}
			}
		}
		}
	}
}
