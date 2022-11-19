string prizeString;
decimal prizeDecimal;
decimal expectedValue;
const decimal oddOfJackpot = 292201338;
const string rawDataFilePath = @"../output.txt";
string resultImage;
const string pageFilePathFrom = @"../template.html";
const string pageFilePathTo = @"../index.html";
string UTCtime = DateTime.UtcNow.ToString("o");


GetPrizeFromFile(rawDataFilePath);
// Console.WriteLine(prizeString);
ParsePrizeToDecimal(prizeString);
// Console.WriteLine(prizeDecimal);
GetExpectedValue(oddOfJackpot, prizeDecimal);
// Console.WriteLine(expectedValue);
GetResultImage(expectedValue);
// Console.WriteLine(resultImage);
// Console.WriteLine(UpdatePage(pageFilePath, prizeString, resultImage, expectedValue));
UpdatePage(pageFilePathFrom, pageFilePathTo, prizeString, resultImage, expectedValue, UTCtime);



string GetPrizeFromFile(string path)
{
    prizeString = File.ReadAllText(path);
    return prizeString;
}

decimal ParsePrizeToDecimal(string prize)
{

    var temp = prize.Replace("$", "").Split(" ");
    var timesValue = 0;

    if (temp[1].Contains("Million"))
    {
        timesValue = 1000000;
    }
    else if (temp[1].Contains("Billion"))
    {
        timesValue = 1000000000;
    }

    prizeDecimal = Convert.ToDecimal(temp[0]) * timesValue;
    return prizeDecimal;
}

decimal GetExpectedValue(decimal odd, decimal prize)
{
    expectedValue = 1 / odd * prize;
    expectedValue = Math.Round(expectedValue, 2);
    return expectedValue;
}

string GetResultImage(decimal value)
{
    if (value >= 1)
    {
        resultImage = File.ReadAllText("ThumbsUpFillImage.txt");
    }
    else
    {
        resultImage = File.ReadAllText("ThumbsDownFillImage.txt");
    }

    return resultImage;
}

bool UpdatePage(string pathFrom, string pathTo, string prizeString, string resultImage, decimal expectedValue, string utc)
{
    var oldPage = File.ReadAllText(pathFrom);
    var newPage = oldPage.Replace("$prize", prizeString)
                            .Replace("$result_image", resultImage)
                            .Replace("$expected_value", expectedValue.ToString())
                            .Replace("$last_update", utc);
    try
    {
        File.WriteAllText(pathTo, newPage);
        return true;
    }
    catch (System.Exception)
    {
        Console.WriteLine("Fail!");
    }
    return false;
}
