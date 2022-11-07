using Excample.ExceptionFilter;

WithoutExceptionFilter oldWay = new();
oldWay.DoSomething();
WithExceptionFilter betterWay = new();
betterWay.DoSomehting();

Console.ReadLine();