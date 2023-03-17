// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
int[] ifixedarray = new int[4];
Console.WriteLine(String.Format("The length of the ifixedarray = {0} ",ifixedarray.Length));

int[] ifixedassignedArray = new int[]{1,2,3,4};
Console.WriteLine(String.Format("The length of the ifixedassignedArray = {0} ",ifixedassignedArray.Length));

for(int idx = 0; idx < ifixedassignedArray.Length; idx++)
{
    Console.WriteLine(ifixedassignedArray[idx]);
}