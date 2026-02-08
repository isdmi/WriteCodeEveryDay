using ConsoleApp1;

AtCoder0208 test = new AtCoder0208();
test.QuestionB();

var S1 = Console.ReadLine();
var array1 = S1.Split(' ').Select(x => int.Parse(x)).ToList();

int result = Enumerable.Range(1, array1[0]).Where(x => SumRange(x) == array1[1]).Count();

Console.WriteLine($"{result}");

int SumRange(int x)
{
    int sum = 0;
    while (x > 0)
    {
        sum += x % 10;
        x /= 10;
    }
    return sum;
}