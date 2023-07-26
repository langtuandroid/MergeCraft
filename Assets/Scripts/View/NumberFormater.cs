using UnityEngine;

public static class NumberFormater
{
    public static string FormatNumber(double number)
    {
        if (number == 0)
            return "0";

        number = Mathf.Round((float)number);
        int i = 0;

        while (i + 1 < NumberNames.Length && number >= 1000)
        {
            number /= 1000;
            i++;
        }

        return number.ToString("#.##") + NumberNames[i];
    }

    private static readonly string[] NumberNames = new string[]
    {
        "",
        "K",
        "M",
        "B",
        "T",
        "G",
        "V"
    };
}
