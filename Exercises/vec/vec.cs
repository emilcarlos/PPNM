using System;

namespace vec {
	public class vec {
		public double x, y, z;

		//making a vector with given coordinates. Default values is set to zero.
		public vec(){x=y=z=0;}
		public vec(double x, double y, double z){this.x=x; this.y=y; this.z=z;}

		
		//operators
		public static vec operator*(vec v, double c){
			return new vec(c*v.x, c*v.y, c*v.z);
		}
		public static vec operator*(double c, vec v){return v*c;}
		public static vec operator+(vec u, vec v){
			return new vec(u.x+v.x,u.y+v.y,u.z+v.z);
		}
		public static vec operator-(vec u){
			return new vec(-u.x, -u.y, -u.z);
		}
		public static vec operator-(vec u, vec v){return u+(-v);}
		
		//print method
		public void print(string s){
			Console.Write(s);
			Console.WriteLine($"{x} {y} {z}");
		}
		public void print(){this.print("");}
	
	
		//vector calculations
		public static double dot(vec v, vec w){
			return v.x*w.x + v.y*w.y + v.z*w.z;
		}
		public static vec cross(vec v, vec w){
			return new vec(v.y*w.z-v.z*w.y,v.z*w.x-v.x*w.z,v.x*w.y-v.y*w.x);
		}
		public static double norm(vec v){
			return Math.Sqrt(v.x*v.x + v.y*v.y + v.z*v.z); 
		}

		//comparison
		static bool approx(double a,double b,double acc=1e-9,double eps=1e-9){
			if(Math.Abs(a-b)<acc){return true;}
			if(Math.Abs(a-b)<(Math.Abs(a)+Math.Abs(b))*eps){return true;}
			return false;
		}
		public static bool approx(vec v, vec w){
			if(!approx(v.x,w.x)){return false;}
			if(!approx(v.y,w.y)){return false;}
			if(!approx(v.z,w.z)){return false;}
			return true;
		}

		//override ToString
		public override string ToString(){return $"{x} {y} {z}";}
	}	

}
