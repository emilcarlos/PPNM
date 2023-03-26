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

	public static void Main(){
		// random symmetric matrix A
		var random = new Random();
		int random_n = random.Next(2,200);
		matrix random_A = new matrix(random_n, random_n);
		for(int i=0;i<random_n;i++){
		for(int j=i;j<random_n;j++){
			random_A[i,j] = random.Next(1,1000);
			random_A[j,i] = random_A[i,j];
		}
		}

		//Identity matrix
		matrix ID = new matrix(random_n, random_n);
		ID.set_unity();
		
		// Eigenvalue decomposition
		matrix V = jacobi.cyclic(random_A).Item2;
		matrix D = jacobi.cyclic(random_A).Item1;

		// Checks
		//random_A.print("Random symmetric matrix A:");
		//D.print("D:");
		//V.print("V:");
		WriteLine($"A is a {random_n}x{random_n} symmetric matrix.");

		matrix VAV = V.T*random_A*V;
		matrix VDV = V*D*V.T;
		matrix VTV = V.T*V;
		matrix VVT = V*V.T;
		
		if(VAV.approx(D)){WriteLine("V^T*A*V equals D.");}
		else{WriteLine("V^T*A*V does not equal D.");}

		if(VDV.approx(random_A)){WriteLine("V*D*V^T equals A.");}
		else{WriteLine("V*D*V^T does not equal A.");}

		if(VTV.approx(ID)){WriteLine("V^T*V equals identity matrix.");}
		else{WriteLine("V^T*V does not equal identity matrix.");}

		if(VVT.approx(ID)){WriteLine("V*V^T equals identity matrix.");}
		else{WriteLine("V*V^T does not equal identity matrix.");}
	}
}
