using System;
using static System.Console;
using static System.Math;
using System.Threading;
using System.Threading.Tasks;

public static class start{
	public class data { public int a,b; public double sum;}
	public static void harmonic(object obj){
        	var local = (data)obj;
        	local.sum=0;
		for(int i=local.a;i<local.b;i++)local.sum+=1.0/i;
        }

	public static void Main(string[] args){
		int nthreads = 1, nterms = (int)1e8;
		foreach(var arg in args){
			var words = arg.Split(':');
			if(words[0]=="-threads") nthreads=int.Parse(words[1]);
			if(words[0]=="-terms") nterms=int.Parse(words[1]);
		}

		data[] x = new data[nthreads];
		for(int i=0;i<nthreads;i++){
			x[i] = new data();
			x[i].a = 1 + nterms/nthreads*i;
			x[i].b = 1 + nterms/nthreads*(i+1);
			WriteLine($"i = {i}, a = {x[i].a}, s = {x[i].b}");
		}
		x[x.Length-1].b=nterms+1;

		var threads = new Thread[nthreads];
		for(int j=0;j<nthreads;j++){
			threads[j] = new Thread(harmonic);
			threads[j].Start(x[j]);

		}
		
		for(int h=0;h<nthreads;h++) threads[h].Join();

		double sum = 0.0;
		for(int g=0;g<nthreads;g++) sum+=x[g].sum;
		WriteLine(sum);

		double Sum=0; Parallel.For(1, nterms+1, delegate(int a){Sum+=1.0/a;});
		WriteLine($"{nthreads}, {nterms}");
		WriteLine(Sum);	
	}
}
