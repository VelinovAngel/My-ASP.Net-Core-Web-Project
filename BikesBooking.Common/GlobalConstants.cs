namespace BikesBooking.Common
{
    public static class GlobalConstants
    {
        public const string SystemName = "BikesBooking";

        public const string AdministratorRoleName = "Administrator";

        // DbContext validation
        public const int MinYearMotorcycle = 1998;

        public const int MaxYearMotorcycle = 2021;

        public const int CityNameMaxLength = 25;

        public const int AddressMaxLength = 50;

        public const int ContactUsernameLength = 15;

        public const int CountryNameLength = 25;

        public const int ManufacturerNameLength = 20;

        public const int ModelNameLength = 20;

        public const int DealerNameLength = 30;

        public const int DealerAddressLength = 30;

        // Form model validation
        public const int ContactFormUsernameMin = 3;

        public const int ContactFormUsernameMax = 15;

        public const int CubicCentimetreMin = 50;

        public const int CubicCentimetreMax = 1250;

        public const int MotorcyclePriceMax = 1000;

        public const int MotorcyclePriceMin = 1;

        public const int MotorcycleDescription = 600;

        public const int CityNameMinLength = 3;

        public const int CountryNameMinLength = 3;

        // Dealer
        public const int DealerNameMinLength = 3;

        public const int DealerNameMaxLength = 30;

        public const int DealerAddressMaxLength = 30;

        public const int DealerAddressMinLength = 5;

        public const int DescriptionMaxLength = 50;

        public const int DescriptionMinLength = 5;

        // Paging
        public const int MaxPageElements = 4;

        // Google and Fb Login
        public static class FacebookLogin
        {
            public const string AppId = "AuthenticationFacebook:Facebook:AppId";

            public const string AppSecret = "AuthenticationFacebook:Facebook:AppSecret";
        }

        public static class GoogleLogin
        {
            public const string AppId = "AuthenticationGoogle:Google:AppId";

            public const string AppSecret = "AuthenticationGoogle:Google:AppSecret";
        }
    }
}
