namespace LocalTokenizer.Constants;

public static class ProcessBehaviorsConstants
{
    public const string IndulgentBehavior = "indulgent";
    public const string IndulgentBehaviorReduced = "ind";
    public const string StrictBehavior = "strict";
    public const string StrictBehaviorReduced = "str";

    public static readonly string[] ValidValues = new string[] {
        IndulgentBehavior,
        IndulgentBehaviorReduced,
        StrictBehavior,
        StrictBehaviorReduced
    };

    public static bool IsStrictBehavior(string selectedValue)
    {
        return selectedValue == StrictBehavior || selectedValue == StrictBehaviorReduced;
    }
}
