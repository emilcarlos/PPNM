using System;
using static System.Console;
using static System.Math;
using static System.Random;
using System.Linq;
using System.IO;

public class genlist<T>{
	public T[] data;
	public int size => data.Length;
	public T this[int i] => data[i];
	public genlist(){ data = new T[0]; }
	public void add(T item){
		T[] newdata = new T[size+1];
		System.Array.Copy(data,newdata,size);
		newdata[size]=item;
		data=newdata;
	}
}

public class main{
	static double integrate(Func<double,double> f, double a, double b,
				double delta=0.001, double eps=0.001, double f2=Double.NaN, double f3=Double.NaN){
		double h=b-a;
		if(Double.IsNaN(f2)){ f2=f(a+2*h/6); f3=f(a+4*h/6);} // first call, no points to reuse
		double f1=f(a+h/6), f4=f(a+5*h/6);
		double Q = (2*f1+f2+f3+2*f4)/6*(b-a); // higher order rule
		double q = (  f1+f2+f3+  f4)/4*(b-a); // lower order rule
		double err = Math.Abs(Q-q);
		if (err <= delta+eps*Math.Abs(Q)) return Q;
		else return integrate(f,a,(a+b)/2,delta/Math.Sqrt(2),eps,f1,f2)+integrate(f,(a+b)/2,b,delta/Math.Sqrt(2),eps,f3,f4);
	}

	//Functions to be integrated
	static double func1(double x){return Math.Sqrt(x);}
	static double func2(double x){return 1/Math.Sqrt(x);}
	static double func3(double x){return 4*Math.Sqrt(1-x*x);}
	static double func4(double x){return Math.Log(x)/Math.Sqrt(x);}
		
	//error function
	static double helper1(double x){return Math.Exp(-x*x);}
	public static Func<double,double> helper2(double z){
		return (x) => {return Math.Exp(-(z+(1-x)/x)*(z+(1-x)/x))/x/x;};
	}
	static double erf(double z){
		if(z<0) return -erf(-z);
		else if(0<=z & z<=1) return (2/Sqrt(PI))*integrate(helper1, 0, z);
		else if(1<z) return 1-(2/Sqrt(PI))*integrate(main.helper2(z), 0, 1);
		else return 0;
	}

	static void Main(){
		//Integrating four functions from task desription and checking answers
		double answer1 = integrate(func1, 0, 1);
		double answer2 = integrate(func2, 0, 1);
		double answer3 = integrate(func3, 0, 1);
		double answer4 = integrate(func4, 0, 1);
		WriteLine("Checking integration method");
		WriteLine($"Correct answers: {"2/3"}	{2}	{Math.PI}	{-4}");
		WriteLine($"Calculated results: {answer1}	{answer2}	{answer3}	{answer4}");
		var answers = new genlist<double>();
		var correct = new genlist<double>();
		answers.add(answer1); answers.add(answer2); answers.add(answer3); answers.add(answer4);
		correct.add(0.6666666666); correct.add(2.0); correct.add(Math.PI); correct.add(-4.0);
		double delta = 0.001;
		double eps = 0.001;
		int check = 0;	
		for(int i=0;i<answers.size;i++){
			if(Math.Abs(answers[i]-correct[i]) < delta+eps*Math.Abs(answers[i])){check += 1;}
		}
		if(check == answers.size){WriteLine("All results are within uncertainties.");}
		else{WriteLine("Not all results are within uncertainties.");}

		//generating data for plotting the errorfunction
		var error_data = new StreamWriter("error.data");

		for(double x=-5+1.0/128;x<=5;x+=1.0/64){
			error_data.WriteLine($"{x} {erf(x)}");
		}
	}
}
