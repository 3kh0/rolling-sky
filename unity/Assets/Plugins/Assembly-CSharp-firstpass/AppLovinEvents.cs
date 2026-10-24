public class AppLovinEvents
{
	public class Types
	{
		public const string UserLoggedIn = "login";

		public const string UserCreatedAccount = "registration";

		public const string UserCompletedTutorial = "tutorial";

		public const string UserCompletedLevel = "level";

		public const string UserCompletedAchievement = "achievement";

		public const string UserSpentVirtualCurrency = "vcpurchase";

		public const string UserCompletedInAppPurchase = "iap";

		public const string UserSentInvitation = "invite";

		public const string UserSharedLink = "share";
	}

	public class Parameters
	{
		public const string UserAccountIdentifier = "username";

		public const string SearchQuery = "query";

		public const string CompletedLevelIdentifier = "level_id";

		public const string CompletedAchievementIdentifier = "achievement_id";

		public const string VirtualCurrencyAmount = "vcamount";

		public const string VirtualCurrencyName = "vcname";

		public const string InAppPurchaseTransactionIdentifier = "store_id";

		public const string InAppPurchaseProductIdentifier = "product_id";

		public const string GooglePlayInAppPurchaseData = "receipt_data";

		public const string GooglePlayInAppPurchaseDataSignature = "receipt_data_signature";

		public const string RevenueAmount = "amount";

		public const string RevenueCurrency = "currency";
	}
}
