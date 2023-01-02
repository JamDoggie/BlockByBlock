using System;
using System.Collections;
using net.minecraft.client.entity;

namespace net.minecraft.src
{

    public class EntityList
	{
		private static System.Collections.IDictionary stringToClassMapping = new Hashtable();
		private static System.Collections.IDictionary classToStringMapping = new Hashtable();
		private static System.Collections.IDictionary IDtoClassMapping = new Hashtable();
		private static System.Collections.IDictionary classToIDMapping = new Hashtable();
		private static System.Collections.IDictionary stringToIDMapping = new Hashtable();
		public static Hashtable entityEggs = new Hashtable();

		private static void addMapping(Type entType, string entName, int id)
		{
			stringToClassMapping[entName] = entType;
			classToStringMapping[entType] = entName;
			IDtoClassMapping[id] = entType;
			classToIDMapping[entType] = id;
			stringToIDMapping[entName] = id;
		}

		private static void addMapping(Type entType, string entName, int id, int eggColor1, int eggColor2)
		{
			addMapping(entType, entName, id);
			entityEggs[id] = new EntityEggInfo(id, eggColor1, eggColor2);
		}

		public static Entity? createEntityByName(string entName, World world)
		{
			Entity? entity = null;

			try
			{
				Type? type = stringToClassMapping[entName] as Type;
				if (type != null)
				{
					entity = Activator.CreateInstance(type, world) as Entity; // Creates an instance of the entity, passing in the world as a parameter to the constructor.
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
				Console.Write(e.StackTrace);
			}

			return entity;
		}

		public static Entity? createEntityFromNBT(NBTTagCompound nBTTagCompound, World world)
		{
			Entity? entity = null;

			try
			{
				Type? type = stringToClassMapping[nBTTagCompound.getString("id")] as Type;
				if (type != null)
				{
					entity = Activator.CreateInstance(type, world) as Entity; // Creates an instance of the entity, passing in the world as a parameter to the constructor.
				}
			}
			catch (Exception exception4)
			{
				Console.WriteLine(exception4.ToString());
				Console.Write(exception4.StackTrace);
			}

			if (entity != null)
			{
				entity.readFromNBT(nBTTagCompound);
			}
			else
			{
				Console.WriteLine("Skipping Entity with id " + nBTTagCompound.getString("id"));
			}

			return entity;
		}

		public static Entity? createEntityByID(int id, World world)
		{
			Entity? entity = null;

			try
			{
				Type? type = IDtoClassMapping[id] as Type;
				if (type != null)
				{
					entity = Activator.CreateInstance(type, world) as Entity; // Creates an instance of the entity, passing in the world as a parameter to the constructor.
				}
			}
			catch (Exception exception4)
			{
				Console.WriteLine(exception4.ToString());
				Console.Write(exception4.StackTrace);
			}

			if (entity == null)
			{
				Console.WriteLine("Skipping Entity with id " + id);
			}

			return entity;
		}

		public static int getEntityID(Entity entity0)
		{
			return ((int?)classToIDMapping[entity0.GetType()]).Value;
		}

		public static string getEntityString(Entity entity0)
		{
			return (string)classToStringMapping[entity0.GetType()];
		}

		public static string getStringFromID(int i0)
		{
			Type class1 = (Type)IDtoClassMapping[i0];
			return class1 != null ? (string)classToStringMapping[class1] : null;
		}

		static EntityList()
		{
			addMapping(typeof(EntityItem), "Item", 1);
			addMapping(typeof(EntityXPOrb), "XPOrb", 2);
			addMapping(typeof(EntityPainting), "Painting", 9);
			addMapping(typeof(EntityArrow), "Arrow", 10);
			addMapping(typeof(EntitySnowball), "Snowball", 11);
			addMapping(typeof(EntityFireball), "Fireball", 12);
			addMapping(typeof(EntitySmallFireball), "SmallFireball", 13);
			addMapping(typeof(EntityEnderPearl), "ThrownEnderpearl", 14);
			addMapping(typeof(EntityEnderEye), "EyeOfEnderSignal", 15);
			addMapping(typeof(EntityPotion), "ThrownPotion", 16);
			addMapping(typeof(EntityExpBottle), "ThrownExpBottle", 17);
			addMapping(typeof(EntityTNTPrimed), "PrimedTnt", 20);
			addMapping(typeof(EntityFallingSand), "FallingSand", 21);
			addMapping(typeof(EntityMinecart), "Minecart", 40);
			addMapping(typeof(EntityBoat), "Boat", 41);
			addMapping(typeof(EntityLiving), "Mob", 48);
			addMapping(typeof(EntityMob), "Monster", 49);
			addMapping(typeof(EntityCreeper), "Creeper", 50, 894731, 0);
			addMapping(typeof(EntitySkeleton), "Skeleton", 51, 12698049, 4802889);
			addMapping(typeof(EntitySpider), "Spider", 52, 3419431, 11013646);
			addMapping(typeof(EntityGiantZombie), "Giant", 53);
			addMapping(typeof(EntityZombie), "Zombie", 54, 44975, 7969893);
			addMapping(typeof(EntitySlime), "Slime", 55, 5349438, 8306542);
			addMapping(typeof(EntityGhast), "Ghast", 56, 16382457, 12369084);
			addMapping(typeof(EntityPigZombie), "PigZombie", 57, 15373203, 5009705);
			addMapping(typeof(EntityEnderman), "Enderman", 58, 1447446, 0);
			addMapping(typeof(EntityCaveSpider), "CaveSpider", 59, 803406, 11013646);
			addMapping(typeof(EntitySilverfish), "Silverfish", 60, 7237230, 3158064);
			addMapping(typeof(EntityBlaze), "Blaze", 61, 16167425, 16775294);
			addMapping(typeof(EntityMagmaCube), "LavaSlime", 62, 3407872, 16579584);
			addMapping(typeof(EntityDragon), "EnderDragon", 63);
			addMapping(typeof(EntityPig), "Pig", 90, 15771042, 14377823);
			addMapping(typeof(EntitySheep), "Sheep", 91, 15198183, 16758197);
			addMapping(typeof(EntityCow), "Cow", 92, 4470310, 10592673);
			addMapping(typeof(EntityChicken), "Chicken", 93, 10592673, 16711680);
			addMapping(typeof(EntitySquid), "Squid", 94, 2243405, 7375001);
			addMapping(typeof(EntityWolf), "Wolf", 95, 14144467, 13545366);
			addMapping(typeof(EntityMooshroom), "MushroomCow", 96, 10489616, 12040119);
			addMapping(typeof(EntitySnowman), "SnowMan", 97);
			addMapping(typeof(EntityOcelot), "Ozelot", 98, 15720061, 5653556);
			addMapping(typeof(EntityIronGolem), "VillagerGolem", 99);
			addMapping(typeof(EntityVillager), "Villager", 120, 5651507, 12422002);
			addMapping(typeof(EntityEnderCrystal), "EnderCrystal", 200);
		}
	}

}