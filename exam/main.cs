using System;
using static System.Console;
using static System.Math;
using static System.Random;
using System.IO;
using static System.IO.TextWriter;

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
		int random_n = random.Next(4,12);
		int random_m = random.Next(4,12);
		matrix random_a = new matrix(random_n, random_m);
		double upper_value = 5.0;
		double lower_value = -5.0;
		for(int i=0;i<random_n;i++){
			for(int j=0;j<random_m;j++){
				random_a[i,j] = random.NextDouble()*(upper_value-lower_value) + lower_value;
			}
		}

		//coordinate vectors
		vector vtx = new vector(random_m);
		vector vty = new vector(random_n);
		int low_x = -6;
		int high_x = 6;
		int low_y = -6;
		int high_y = 6;
		vtx[0] = low_x;
		vtx[vtx.size-1] = high_x;
		vty[0] = low_y;
		vty[vty.size-1] = high_y;
		for(int i=1;i<random_m-1;i++){vtx[i] = random.NextDouble()*(high_x-low_x) + low_x;}
		for(int i=1;i<random_n-1;i++){vty[i] = random.NextDouble()*(high_y-low_y) + low_y;}
		double[] dtx = vtx;
		double[] dty = vty;
		Array.Sort(dtx);
		Array.Sort(dty);
		vector tx = new vector(dtx);
		vector ty = new vector(dty);

		//generate plots and test to check interpolating function
		foreach(var arg in args){

		// generates interpolation and plots it as a surface-plot
		if(arg == "interpolate"){
			var interpolate = new StreamWriter("interpolate.data");
			for(int xi=low_x*25;xi<high_x*25;xi++){
				double XI = xi*0.04;
				for(int yi=low_y*25;yi<high_y*25;yi++){
					double YI = yi*0.04;
					interpolate.WriteLine($"{XI}\t{YI}\t{funcs.bilinear(tx, ty, random_a, XI, YI)}");
				}
				interpolate.WriteLine("");
			}
			interpolate.Close();
			var points = new StreamWriter("points.data");
			for(int xi=0;xi<random_m;xi++){
                                for(int yi=0;yi<random_n;yi++){
                                        points.WriteLine($"{tx[xi]}\t{ty[yi]}\t{random_a[yi,xi]}");
                                }
                                points.WriteLine("");
			}
			points.Close();
		}

		// tests interpolation on an example with a known solution
		if(arg == "test"){
			matrix test_matrix = new matrix(8, 8);
                	for(int i=0;i<8;i++){
                        	for(int j=0;j<8;j++){
                                	test_matrix[i,j] = i*j;
                        	}
                	}
			test_matrix.print("test_matrix");
			vector test_x = new vector("0, 0.5, 1.5, 3, 3.4, 5, 6, 7");
                	vector test_y = new vector("0.5, 2, 3, 4.5, 5, 6, 6.7, 7");
			double test_px = 3.3;
			double test_py = 3.3;
			WriteLine($"px = {test_px}, py = {test_py}");
			double test = funcs.bilinear(test_x, test_y, test_matrix, test_px, test_py);
			WriteLine($"x1 = 3, x2 = 3.4, y1 = 3, y2 = 4.5");
			test_x.print("x: ");
			test_y.print("y: ");
                	WriteLine($"Q11 = {test_matrix[2,3]}, Q12 = {test_matrix[3,3]}, Q21 = {test_matrix[2,4]}, Q22 = {test_matrix[3,4]}");
			WriteLine("Expected value: 8.25");
			WriteLine($"Interpolated value: {test}");
		}

		// tests interpolation along one of the gridlines specified by a constant y-value
		if(arg == "line_interpolation"){
			var line_inter = new StreamWriter("line_inter.data");
			var line_points = new StreamWriter("line_points.data");
			double YI = ty[random_n-3];
			for(int xi=0;xi<random_m;xi++){
				line_inter.WriteLine($"{tx[xi]}\t{funcs.bilinear(tx, ty, random_a, tx[xi], YI)}");
			}
			line_inter.Close();

			for(int xi=0;xi<random_m;xi++){
                                line_points.WriteLine($"{tx[xi]}\t{random_a[random_n-3,xi]}");
                        }
			line_points.Close();
		}
		
		// recreates the plot found in https://en.wikipedia.org/wiki/Bilinear_interpolation
		if(arg == "wiki"){
			var wiki = new StreamWriter("wiki.data");
			matrix F = new matrix(2,2);
			F[0,0] = 0.0;
			F[0,1] = 1.0;
			F[1,0] = 1.0;
			F[1,1] = 0.5;
			vector x = new vector("0,1");
			vector y = new vector("0,1");
			for(int xi=0*100;xi<1*100;xi++){
				double XI = xi*0.01;
                                for(int yi=0*100;yi<1*100;yi++){
                                        double YI = yi*0.01;
                                        wiki.WriteLine($"{XI}\t{YI}\t{funcs.bilinear(x, y, F, XI, YI)}");
                                }
                                wiki.WriteLine("");
			}
			wiki.Close();
		}
	}
}
}
