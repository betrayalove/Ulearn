public static double Calculate(string userInput)
{
    return 0.0;
}

public static double Calculate(string userInput)
{
    var inputs = userInput.Split(' ');
    var sum = double.Parse(inputs[0]);
    var bankPercent = double.Parse(inputs[1]);
    var months = int.Parse(inputs[2]);
    var maxProcent = 100;
    var monthsInYear = 12;
    var percentFrac = 1 + bankPercent / maxProcent / monthsInYear;
    return sum * Math.Pow(percentFrac, months);
}