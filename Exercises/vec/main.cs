using System;

class first {
	static void Main(){
		var a = new vec.vec(1,2,3);
		var b = new vec.vec(4,5,6);
		var c = new vec.vec(1,2,3);
       		var dot = vec.vec.dot(a,b);
		var cross = vec.vec.cross(a,b);
		var norm_a = vec.vec.norm(a);
		var check_ac = vec.vec.approx(a,c);
		var check_ab = vec.vec.approx(a,b);
				
		Console.WriteLine($"{dot}");
		Console.WriteLine($"{norm_a}");
		string string_cross = cross.ToString();
		Console.WriteLine($"{string_cross}");
		Console.WriteLine($"{check_ac}");
		Console.WriteLine($"{check_ab}");
	}
}

