using System;

class first {

	static void Main()
	{
		double square = Math.Sqrt(2);
		Console.WriteLine("sqrt(2) = " + square);
		
		double power = Math.Pow(2, 0.2);
		Console.WriteLine("2 to the power of 1/5 = " + power);
		
		double e_pi = Math.Pow(Math.E, Math.PI);
		Console.WriteLine("e to the power of pi = " + e_pi);

		double pi_e = Math.Pow(Math.PI, Math.E);
		Console.WriteLine("pi to the power of e = " + pi_e);

                double gam_one = sfuns.prime.gamma(1.0);
                Console.WriteLine("gamma(1.0) = " + gam_one);

		double gam_two = sfuns.prime.gamma(2.0);
		Console.WriteLine("gamma(2.0) = " + gam_two);

		double gam_three = sfuns.prime.gamma(3.0);
		Console.WriteLine("gamma(3.0) = " + gam_three);

		double gam_ten = sfuns.prime.gamma(10.0);
		Console.WriteLine("gamma(10.0) = " + gam_ten);

	}
}
