using System;
using static System.Console;
using static System.Math;

public static class inout2{
	public static void Main(string[] args){
	char[] split_delimiters = {' ', '\t','n'};
		foreach(var arg in args){
		var numbers = arg.Split(split_delimiters);
			foreach(var number in numbers){
			double x = double.Parse(number);
			WriteLine($"{x} {Sin(x)} {Cos(x)}");
			}
		}
	}
}
