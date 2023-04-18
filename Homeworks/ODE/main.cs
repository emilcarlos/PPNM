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

public static class main{
	 public static (vector,vector) rkstep12(Func<double,vector,vector> f,double x,vector y,double h){
                vector k0 = f(x,y);              /* embedded lower order formula (Euler) */
                vector k1 = f(x+h/2,y+k0*(h/2)); /* higher order formula (midpoint) */
                vector yh = y+k1*h;              /* y(x+h) estimate */
                vector er = (k1-k0)*h;           /* error estimate */
                return (yh,er);
	 	}

                public static (genlist<double>,genlist<vector>) driver(
                Func<double,vector,vector> f, /* the f from dy/dx=f(x,y) */
                double a,                    /* the start-point a */
                vector ya,                   /* y(a) */
                double b,                    /* the end-point of the integration */
                double h=0.01,               /* initial step-size */
                double acc=0.01,             /* absolute accuracy goal */
                double eps=0.01              /* relative accuracy goal */
                ){
                if(a>b) throw new ArgumentException("driver: a>b");
                double x=a; vector y=ya.copy();
                var xlist=new genlist<double>(); xlist.add(x);
                var ylist=new genlist<vector>(); ylist.add(y);
                do      {
                        if(x>=b) return (xlist,ylist); /* job done */
                        if(x+h>b) h=b-x;               /* last step should end at b */
                        var (yh,erv) = rkstep12(f,x,y,h);
                        double tol = (acc+eps*yh.norm()) * Sqrt(h/(b-a));
                        double err = erv.norm();
                        if(err<=tol){ // accept step
                                x+=h; y=yh;
                                xlist.add(x);
                                ylist.add(y);
                                }
                        h *= Min( Pow(tol/err,0.25)*0.95 , 2); // reajust stepsize
                        }while(true);
                }//driver

	// functions to be tested
	static vector simple(double x, vector y){
		return new vector(y[1], -y[0]);
	}

	static vector test(double x, vector y){
		return new vector(y[1], y[0]*y[1]);
	}

	static vector pendul(double x, vector y){
		return new vector(y[1], -0.25*y[1]-5*Math.Sin(y[0]));
	}

	static void Main(){
		//code that does the testing
		vector start_y = new vector(0, 1);
		(var xlist, var ylist) = driver(simple, 0, start_y, 10);
		var simple_record = new StreamWriter("simple.data");
		for(int i=0; i<xlist.size;i++){
			simple_record.WriteLine($"{xlist[i]} {ylist[i][0]} {ylist[i][1]}");
		}
		
		vector test_y = new vector(1, 2);
		(var test_xlist, var test_ylist) = driver(test, 0, test_y, 1);
		var test_record = new StreamWriter("test.data");
		for(int i=0;i<test_xlist.size;i++){
			test_record.WriteLine($"{test_xlist[i]} {test_ylist[i][0]} {test_ylist[i][1]}");
		}

		vector pendul_y = new vector (Math.PI*0.95, 0);
		(var pxlist, var pylist) = driver(pendul, 0, pendul_y, 10);
		var pendul_record = new StreamWriter("pendul.data");
		for(int i=0; i<pxlist.size;i++){
			pendul_record.WriteLine($"{pxlist[i]} {pylist[i][0]} {pylist[i][1]}");
		}

	}


}
