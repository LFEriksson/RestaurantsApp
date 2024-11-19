namespace Infrastructure.Authorization;

public static class PolicyNames
{
    public const string HasDateOfBirth = "HasDateOfBirth";
    public const string AtLeast18Years = "AtLeast18Years";
}

public static class AppClaimTypes
{
    public const string DateOfBirth = "DateOfBirth";
}


