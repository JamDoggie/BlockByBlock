using System.Collections;

namespace net.minecraft.src
{

	public class Session
	{
		public static System.Collections.IList registeredBlocksList = new ArrayList();
		public string username;
		public string sessionId;
		public string mpPassParameter;

		public Session(string username, string sessionId)
		{
			this.username = username;
			this.sessionId = sessionId;
		}

		static Session()
		{
			registeredBlocksList.Add(Block.stone);
			registeredBlocksList.Add(Block.cobblestone);
			registeredBlocksList.Add(Block.brick);
			registeredBlocksList.Add(Block.dirt);
			registeredBlocksList.Add(Block.planks);
			registeredBlocksList.Add(Block.wood);
			registeredBlocksList.Add(Block.leaves);
			registeredBlocksList.Add(Block.torchWood);
			registeredBlocksList.Add(Block.stairSingle);
			registeredBlocksList.Add(Block.glass);
			registeredBlocksList.Add(Block.cobblestoneMossy);
			registeredBlocksList.Add(Block.sapling);
			registeredBlocksList.Add(Block.plantYellow);
			registeredBlocksList.Add(Block.plantRed);
			registeredBlocksList.Add(Block.mushroomBrown);
			registeredBlocksList.Add(Block.mushroomRed);
			registeredBlocksList.Add(Block.sand);
			registeredBlocksList.Add(Block.gravel);
			registeredBlocksList.Add(Block.sponge);
			registeredBlocksList.Add(Block.cloth);
			registeredBlocksList.Add(Block.oreCoal);
			registeredBlocksList.Add(Block.oreIron);
			registeredBlocksList.Add(Block.oreGold);
			registeredBlocksList.Add(Block.blockSteel);
			registeredBlocksList.Add(Block.blockGold);
			registeredBlocksList.Add(Block.bookShelf);
			registeredBlocksList.Add(Block.tnt);
			registeredBlocksList.Add(Block.obsidian);
		}
	}

}