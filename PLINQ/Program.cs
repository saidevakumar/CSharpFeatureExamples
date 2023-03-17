// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
var source = Enumerable.Range(1, 10000);

// Opt in to PLINQ with AsParallel.
var evenNums = from num in source.AsParallel()
    where num % 2 == 0
    select num;

Console.WriteLine("{0} even numbers out of {1} total",evenNums.Count(), source.Count());

