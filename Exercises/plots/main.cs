using static System.Console;
class main{
	public static void Main(string[] args){
	foreach(var arg in args){
	if(arg=="gamma"){
	for(double x=-5+1.0/128;x<=5;x+=1.0/64){
		WriteLine($"{x} {sfuns.gamma(x)}");
	}
	}
	}
	}
}
