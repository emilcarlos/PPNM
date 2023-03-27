using System;
using static System.Console;
using static System.Math;
using static System.Random;
using System.Linq;

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

	public static class ls{
		public static (vector, matrix, vector) lsfit(Func<double,double>[] fs, vector x, vector y, vector dy){
			int l = fs.Length;
			matrix A = new matrix(x.size, l);
			vector b = y.copy();
			matrix R = new matrix(l, l);

			for(int i=0;i<A.size1;i++){
				for(int j=0;j<A.size2;j++){
					A[i,j] = fs[j](x[i])/dy[i];
				}
				b[i] = y[i]/dy[i];
			}
			
			matrix A_prod = A.T*A;
			matrix R2 = new matrix(A_prod.size2, A_prod.size2);

			QRGS.decomp(A, R);
			vector c = QRGS.solve(A, R, b);

			QRGS.decomp(A_prod, R2);
			matrix Co = QRGS.inverse(A_prod, R2);

			vector c_errors = new vector(Co.size2);
			for(int g=0;g<Co.size2;g++){
				c_errors[g] = Math.Sqrt(Co[g,g]);
			}

			return (c, Co, c_errors);
		}
	}


	static void Main(string[] args){
		// TASK A

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

		//Performing fit
		var fs = new Func<double,double>[] {z => 1, z => z};
		vector t = new vector("1,2,3,4,6,9,10,13,15");
		vector y = new vector("117,100,88,72,53,29.5,25.2,15.2,11.1");
		vector dy = new vector("5,5,5,4,4,3,3,2,2");
		vector ln_y = y.copy();
		vector ln_dy = dy.copy();
		for(int i=0;i<y.size;i++){
			ln_y[i] = Log(y[i]);
			ln_dy[i] = dy[i]/y[i];
		}
		var fit_params = ls.lsfit(fs, t, ln_y, ln_dy);
		vector bf = fit_params.Item1;
		vector c_errors = fit_params.Item3;

		double c_a = Math.Exp(bf[0]);
		double c_lamda = -bf[1];

		foreach(var arg in args){
			if(arg == "plot"){
				//generate data for plotting the fit 
				for(double x=0.01+1.0/128;x<=15;x+=1.0/64){
					WriteLine($"{x} {c_a*Math.Exp(-c_lamda*x)}");
				}
			}
			if(arg == "halflife"){
				//check that decomp is working

                		matrix a = random_a.copy();
                		matrix r = new matrix(random_m, random_m);
                		QRGS.decomp(a, r);
                		matrix check1 = a.T*a;
                		matrix check2 = a*r;
                		WriteLine("Decomp check");
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

				//compare half-lifes
				WriteLine("Comparison of half-life");
				double val_theory = 3.631;
				double val_exp = Math.Log(2)/c_lamda;
				double deviation = 100*((val_exp-val_theory)/val_theory);
			       	WriteLine($"Experimental half-life: {val_exp}");
				WriteLine($"Theoretical half-life: {val_theory}");
				WriteLine($"Deviation from theory: {deviation} %");
				WriteLine("");

				//Use error-propagation to find uncertainty of half-life
				WriteLine("Comparision of half-life including uncertainties");
				double error_life = (Math.Log(2.0)/(c_lamda*c_lamda))*c_errors[1];
				WriteLine($"Experimental half-life: {val_exp} +- {error_life}");
				WriteLine($"Theoretical half-life: {val_theory}");
				if(val_exp-error_life<=val_theory && val_theory<=val_exp+error_life){
					WriteLine("Experiemental value agrees with theory.");
				}
				else{WriteLine("Experimental values does not agree with theory.");}
			}	
		}
	}
}
