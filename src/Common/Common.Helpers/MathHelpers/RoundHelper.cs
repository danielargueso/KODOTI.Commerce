namespace Common.Helpers.MathHelpers
{
    public static class RoundHelper
	{
		public static decimal Round(this decimal number, int decimals) => Math.Round(number, decimals);
	}
}

