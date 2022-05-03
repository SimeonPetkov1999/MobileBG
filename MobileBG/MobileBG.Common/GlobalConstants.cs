namespace MobileBG.Common;
public static class GlobalConstants
{
    public const string SystemName = "MobileBG";

    public const string AdministratorRoleName = "Administrator";

    public static class ErrorMessages
    {
        public const string RequiredErrorMessage = "{0} is required";

        public const string InvalidValueErrorMessage = "The {0} must be between {1} and {2}!";

        public const string ImageErrorMessage = "You need to upload atleast one picture!";
    }

    public const int CarsPerPage = 8;

    public const int BlogsPerPage = 5;
}
