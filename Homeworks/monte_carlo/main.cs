using System;
using static System.Console;
using static System.Math;
using static System.Random;
using System.Linq;
using System.IO;
using static System.IO.TextWriter;

public class main{
	static (double,double) plainmc(Func<vector,double> f,vector a,vector b,int N){
        int dim = a.size; 
	double V = 1; 
	for(int i=0;i<dim;i++) V *= b[i] - a[i];
        double sum = 0, sum2 = 0;
	var x = new vector(dim);
	var rnd = new Random();
        for(int i=0;i<N;i++){
                for(int k=0;k<dim;k++) x[k] = a[k] + rnd.NextDouble() * (b[k]-a[k]);
                double fx = f(x); sum += fx; sum2 += fx*fx;
	}
        double mean = sum/N, sigma = Sqrt(sum2/N-mean*mean);
        var result = (mean*V,sigma*V/Sqrt(N));
        return result;
	}

	//function describing a half unit sphere (unit dome)
	static double unitsphere(vector cordi){
		double x = cordi[0];
		double y = cordi[1];
		if(x*x+y*y > 1){return 0.0;}
		else{return Math.Sqrt(1-x*x-y*y);}
	}
	
	//difficult function from task description
	static double difficult(vector cordi){
		double x = cordi[0];
		double y = cordi[1];
		double z = cordi[2];
		return 1/(1-Cos(x)*Cos(y)*Cos(z))/(PI*PI*PI);
	}


	static void Main(){
		//calculates volume of half unit sphere
		vector a = new vector("-1, -1");
		vector b = new vector("1, 1");
		
		WriteLine("--------------------------------------");
		WriteLine("Half unit sphere");
		WriteLine($"Actual value:	{(4.0/3.0)*PI*0.5}");
		int number = 100000;
		var result = plainmc(unitsphere, a, b, number);
		WriteLine($"Calculated value with {number} points:	{result.Item1} ± {result.Item2}");


		int N = 3000;
		double real = (4.0/3.0)*PI*0.5;
		var mc = new StreamWriter("mc.data");
		var actual = new StreamWriter("actual.data");
		var nth = new StreamWriter("nth.data");

		for(int i=1;i<=N;i += 10){
		var answer = plainmc(unitsphere, a, b, i);
		mc.WriteLine($"{i} {answer.Item2}");
		actual.WriteLine($"{i} {Math.Abs(answer.Item1-real)}");
		nth.WriteLine($"{i} {1/Math.Sqrt(i)}");
		}
		mc.Close();
		actual.Close();
		nth.Close();

		//calculates difficult integral from task description
		WriteLine("---------------------------------------");
		WriteLine("Difficult integral from task description");
		WriteLine("Actual value:	1.3932039296856768591842462603255");

		vector a_hard = new vector("0, 0, 0");
		vector b_hard = new vector(PI, PI, PI);
		int N_hard = 1000000;
		var answer_hard = plainmc(difficult, a_hard, b_hard, N_hard);
		WriteLine($"Calculated value with {N_hard} points:	{answer_hard.Item1} ± {answer_hard.Item2}");
	}
}
