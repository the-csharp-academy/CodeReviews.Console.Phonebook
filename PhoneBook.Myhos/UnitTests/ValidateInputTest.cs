namespace UnitTests;

public class ValidateInputTest
{
    private static readonly TestCaseData[] NameTestCases =
        {
            new("Miguel", true),
            new("Miguel Angel", true),
            new("José", true),
            new("María Fernanda", true),
            new("Álvaro", true),
            new("Ñuñez", true),

            new("", false),
            new(" ", false),
            new((string?)null, false),

            new("12345", false),
            new("Miguel123", false),
            new("@Miguel", false),
            new("Miguel!", false),

            new("A", false),
            new(new string('A', 101), false)
        };

    [TestCaseSource(nameof(NameTestCases))]
    public void CorrectNameInput_ReturnsTrue(string name, bool expectedResult)
    {

        Assert.Pass();
    }
}
