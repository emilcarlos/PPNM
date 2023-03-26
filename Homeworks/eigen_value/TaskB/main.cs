using System;
using static System.Console;
using static System.Random;
using static System.Math;

public class main{
	public static class jacobi{
		public static void timesJ(matrix A, int p, int q, double theta){
			double c = Cos(theta), s = Sin(theta);
			for(int i=0;i<A.size1;i++){
				double aip = A[i,p], aiq = A[i,q];
				A[i,p] = c*aip-s*aiq;
				A[i,q] = s*aip+c*aiq;
			}
		}

		public static void Jtimes(matrix A, int p, int q, double theta){
			double c = Cos(theta), s = Sin(theta);
			for(int j=0;j<A.size1;j++){
				double apj = A[p,j], aqj = A[q,j];
				A[p,j] = c*apj+s*aqj;
				A[q,j] = -s*apj+c*aqj;
			}
		}

		public static (matrix, matrix, vector) cyclic(matrix M){
			matrix A = M.copy();
			matrix V = matrix.id(M.size1);
			vector w = new vector(M.size1);
			int n = M.size1;
			bool changed;
			do{
				changed = false;
				for(int p=0;p<n-1;p++){
				for(int q=p+1;q<n;q++){
					double apq=A[p,q], app=A[p,p], aqq=A[q,q];
					double theta=0.5*Atan2(2*apq,aqq-app);
					double c=Cos(theta),s=Sin(theta);
					double new_app=c*c*app-2*s*c*apq+s*s*aqq;
					double new_aqq=s*s*app+2*s*c*apq+c*c*aqq;
					if(new_app != app || new_aqq != aqq){
						changed = true;
						timesJ(A,p,q,theta);
						Jtimes(A,p,q,-theta);
						timesJ(V,p,q,theta);
					}
				} 
				}
			}while(changed);
			matrix D = A.copy();
			for(int i=0;i<M.size1;i++){
				w[i] = D[i,i];
			}
			return (D, V, w);
		}
	}

	public static void Main(string[] args){
		//Task B

		//read input
		double rmax = 0;
		double dr = 0;
		foreach(var arg in args){
			var words = arg.Split(":");
			if(words[0]=="-rmax"){
				rmax = double.Parse(words[1]);
			}
			if(words[0]=="-dr"){
				dr = double.Parse(words[1]);
			}
		}
		//WriteLine($"{rmax} {dr}");
		
		//create H
		int npoints = (int)(rmax/dr)-1;

		vector r = new vector(npoints);
		for(int i=0;i<npoints;i++)r[i]=dr*(i+1);

		matrix H = new matrix(npoints,npoints);
		for(int i=0;i<npoints-1;i++){
   		H[i,i]  =-2;
  		H[i,i+1]= 1;
   		H[i+1,i]= 1;
  		}

		H[npoints-1,npoints-1]=-2;
		matrix.scale(H,-0.5/dr/dr);
		for(int i=0;i<npoints;i++)H[i,i]+=-1/r[i];
		
		//eigen
		vector values = jacobi.cyclic(H).Item3;
		matrix vectors = jacobi.cyclic(H).Item2;
		
		double min_val = values[0];
		vector min_vector = vectors[0];
		
		
	}
}
