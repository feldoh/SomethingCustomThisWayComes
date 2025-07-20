using Verse;

namespace SomethingCustomThisWayComes
{
	public class Mod : Verse.Mod
	{
		public Mod(ModContentPack content) : base(content)
		{
			Log.Message("Something custom this way comes!");
		}
	}
}
