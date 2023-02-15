using System;

class first {
	static void Main(){
		vec.vec a = new vec.vec(1,2,3);
		vec.vec b = new vec.vec(4,5,6);
		vec.vec c = new vec.vec(1,2,3);
		vec.vec plus_ab = a+b;
		vec.vec minus_ab = a-b;
		vec.vec threea = 3*a;
		vec.vec athree = a*3;
       		double dot_ab = vec.vec.dot(a,b);
		vec.vec cross_ab = vec.vec.cross(a,b);
		double norm_a = vec.vec.norm(a);
		bool check_ac = vec.vec.approx(a,c);
		bool check_ab = vec.vec.approx(a,b);
		
			

		//print of relevant vectors		
		a.print("this is a: ");
		b.print("this is b: ");
		c.print("this is c: ");

		plus_ab.print("a+b = ");
		minus_ab.print("a-b = ");
		threea.print("3*a = ");
		athree.print("a*3 = ");
		
		Console.WriteLine($"Dot product of a and b: {dot_ab}");
		Console.WriteLine($"Norm of a: {norm_a}");
		string string_cross_ab = cross_ab.ToString();
		Console.WriteLine($"Cross product of a and b: {string_cross_ab}");
		Console.WriteLine($"a is equal to c: {check_ac}");
		Console.WriteLine($"a is equal to b: {check_ab}");
	}
}

