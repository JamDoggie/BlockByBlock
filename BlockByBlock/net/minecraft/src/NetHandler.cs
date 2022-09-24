namespace net.minecraft.src
{
	public abstract class NetHandler
	{
		public abstract bool ServerHandler {get;}

		public virtual void func_48487_a(Packet51MapChunk packet51MapChunk1)
		{
		}

		public virtual void registerPacket(Packet packet1)
		{
		}

		public virtual void handleErrorMessage(string string1, object[] object2)
		{
		}

		public virtual void handleKickDisconnect(Packet255KickDisconnect packet255KickDisconnect1)
		{
			this.registerPacket(packet255KickDisconnect1);
		}

		public virtual void handleLogin(Packet1Login packet1Login1)
		{
			this.registerPacket(packet1Login1);
		}

		public virtual void handleFlying(Packet10Flying packet10Flying1)
		{
			this.registerPacket(packet10Flying1);
		}

		public virtual void handleMultiBlockChange(Packet52MultiBlockChange packet52MultiBlockChange1)
		{
			this.registerPacket(packet52MultiBlockChange1);
		}

		public virtual void handleBlockDig(Packet14BlockDig packet14BlockDig1)
		{
			this.registerPacket(packet14BlockDig1);
		}

		public virtual void handleBlockChange(Packet53BlockChange packet53BlockChange1)
		{
			this.registerPacket(packet53BlockChange1);
		}

		public virtual void handlePreChunk(Packet50PreChunk packet50PreChunk1)
		{
			this.registerPacket(packet50PreChunk1);
		}

		public virtual void handleNamedEntitySpawn(Packet20NamedEntitySpawn packet20NamedEntitySpawn1)
		{
			this.registerPacket(packet20NamedEntitySpawn1);
		}

		public virtual void handleEntity(Packet30Entity packet30Entity1)
		{
			this.registerPacket(packet30Entity1);
		}

		public virtual void handleEntityTeleport(Packet34EntityTeleport packet34EntityTeleport1)
		{
			this.registerPacket(packet34EntityTeleport1);
		}

		public virtual void handlePlace(Packet15Place packet15Place1)
		{
			this.registerPacket(packet15Place1);
		}

		public virtual void handleBlockItemSwitch(Packet16BlockItemSwitch packet16BlockItemSwitch1)
		{
			this.registerPacket(packet16BlockItemSwitch1);
		}

		public virtual void handleDestroyEntity(Packet29DestroyEntity packet29DestroyEntity1)
		{
			this.registerPacket(packet29DestroyEntity1);
		}

		public virtual void handlePickupSpawn(Packet21PickupSpawn packet21PickupSpawn1)
		{
			this.registerPacket(packet21PickupSpawn1);
		}

		public virtual void handleCollect(Packet22Collect packet22Collect1)
		{
			this.registerPacket(packet22Collect1);
		}

		public virtual void handleChat(Packet3Chat packet3Chat1)
		{
			this.registerPacket(packet3Chat1);
		}

		public virtual void handleVehicleSpawn(Packet23VehicleSpawn packet23VehicleSpawn1)
		{
			this.registerPacket(packet23VehicleSpawn1);
		}

		public virtual void handleAnimation(Packet18Animation packet18Animation1)
		{
			this.registerPacket(packet18Animation1);
		}

		public virtual void handleEntityAction(Packet19EntityAction packet19EntityAction1)
		{
			this.registerPacket(packet19EntityAction1);
		}

		public virtual void handleHandshake(Packet2Handshake packet2Handshake1)
		{
			this.registerPacket(packet2Handshake1);
		}

		public virtual void handleMobSpawn(Packet24MobSpawn packet24MobSpawn1)
		{
			this.registerPacket(packet24MobSpawn1);
		}

		public virtual void handleUpdateTime(Packet4UpdateTime packet4UpdateTime1)
		{
			this.registerPacket(packet4UpdateTime1);
		}

		public virtual void handleSpawnPosition(Packet6SpawnPosition packet6SpawnPosition1)
		{
			this.registerPacket(packet6SpawnPosition1);
		}

		public virtual void handleEntityVelocity(Packet28EntityVelocity packet28EntityVelocity1)
		{
			this.registerPacket(packet28EntityVelocity1);
		}

		public virtual void handleEntityMetadata(Packet40EntityMetadata packet40EntityMetadata1)
		{
			this.registerPacket(packet40EntityMetadata1);
		}

		public virtual void handleAttachEntity(Packet39AttachEntity packet39AttachEntity1)
		{
			this.registerPacket(packet39AttachEntity1);
		}

		public virtual void handleUseEntity(Packet7UseEntity packet7UseEntity1)
		{
			this.registerPacket(packet7UseEntity1);
		}

		public virtual void handleEntityStatus(Packet38EntityStatus packet38EntityStatus1)
		{
			this.registerPacket(packet38EntityStatus1);
		}

		public virtual void handleUpdateHealth(Packet8UpdateHealth packet8UpdateHealth1)
		{
			this.registerPacket(packet8UpdateHealth1);
		}

		public virtual void handleRespawn(Packet9Respawn packet9Respawn1)
		{
			this.registerPacket(packet9Respawn1);
		}

		public virtual void handleExplosion(Packet60Explosion packet60Explosion1)
		{
			this.registerPacket(packet60Explosion1);
		}

		public virtual void handleOpenWindow(Packet100OpenWindow packet100OpenWindow1)
		{
			this.registerPacket(packet100OpenWindow1);
		}

		public virtual void handleCloseWindow(Packet101CloseWindow packet101CloseWindow1)
		{
			this.registerPacket(packet101CloseWindow1);
		}

		public virtual void handleWindowClick(Packet102WindowClick packet102WindowClick1)
		{
			this.registerPacket(packet102WindowClick1);
		}

		public virtual void handleSetSlot(Packet103SetSlot packet103SetSlot1)
		{
			this.registerPacket(packet103SetSlot1);
		}

		public virtual void handleWindowItems(Packet104WindowItems packet104WindowItems1)
		{
			this.registerPacket(packet104WindowItems1);
		}

		public virtual void handleUpdateSign(Packet130UpdateSign packet130UpdateSign1)
		{
			this.registerPacket(packet130UpdateSign1);
		}

		public virtual void handleUpdateProgressbar(Packet105UpdateProgressbar packet105UpdateProgressbar1)
		{
			this.registerPacket(packet105UpdateProgressbar1);
		}

		public virtual void handlePlayerInventory(Packet5PlayerInventory packet5PlayerInventory1)
		{
			this.registerPacket(packet5PlayerInventory1);
		}

		public virtual void handleTransaction(Packet106Transaction packet106Transaction1)
		{
			this.registerPacket(packet106Transaction1);
		}

		public virtual void handleEntityPainting(Packet25EntityPainting packet25EntityPainting1)
		{
			this.registerPacket(packet25EntityPainting1);
		}

		public virtual void handlePlayNoteBlock(Packet54PlayNoteBlock packet54PlayNoteBlock1)
		{
			this.registerPacket(packet54PlayNoteBlock1);
		}

		public virtual void handleStatistic(Packet200Statistic packet200Statistic1)
		{
			this.registerPacket(packet200Statistic1);
		}

		public virtual void handleSleep(Packet17Sleep packet17Sleep1)
		{
			this.registerPacket(packet17Sleep1);
		}

		public virtual void handleBed(Packet70Bed packet70Bed1)
		{
			this.registerPacket(packet70Bed1);
		}

		public virtual void handleWeather(Packet71Weather packet71Weather1)
		{
			this.registerPacket(packet71Weather1);
		}

		public virtual void handleMapData(Packet131MapData packet131MapData1)
		{
			this.registerPacket(packet131MapData1);
		}

		public virtual void handleDoorChange(Packet61DoorChange packet61DoorChange1)
		{
			this.registerPacket(packet61DoorChange1);
		}

		public virtual void handleServerPing(Packet254ServerPing packet254ServerPing1)
		{
			this.registerPacket(packet254ServerPing1);
		}

		public virtual void handleEntityEffect(Packet41EntityEffect packet41EntityEffect1)
		{
			this.registerPacket(packet41EntityEffect1);
		}

		public virtual void handleRemoveEntityEffect(Packet42RemoveEntityEffect packet42RemoveEntityEffect1)
		{
			this.registerPacket(packet42RemoveEntityEffect1);
		}

		public virtual void handlePlayerInfo(Packet201PlayerInfo packet201PlayerInfo1)
		{
			this.registerPacket(packet201PlayerInfo1);
		}

		public virtual void handleKeepAlive(Packet0KeepAlive packet0KeepAlive1)
		{
			this.registerPacket(packet0KeepAlive1);
		}

		public virtual void handleExperience(Packet43Experience packet43Experience1)
		{
			this.registerPacket(packet43Experience1);
		}

		public virtual void handleCreativeSetSlot(Packet107CreativeSetSlot packet107CreativeSetSlot1)
		{
			this.registerPacket(packet107CreativeSetSlot1);
		}

		public virtual void handleEntityExpOrb(Packet26EntityExpOrb packet26EntityExpOrb1)
		{
			this.registerPacket(packet26EntityExpOrb1);
		}

		public virtual void handleEnchantItem(Packet108EnchantItem packet108EnchantItem1)
		{
		}

		public virtual void handleCustomPayload(Packet250CustomPayload packet250CustomPayload1)
		{
		}

		public virtual void handleEntityHeadRotation(Packet35EntityHeadRotation packet35EntityHeadRotation1)
		{
			this.registerPacket(packet35EntityHeadRotation1);
		}

		public virtual void handleTileEntityData(Packet132TileEntityData packet132TileEntityData1)
		{
			this.registerPacket(packet132TileEntityData1);
		}

		public virtual void func_50100_a(Packet202PlayerAbilities packet202PlayerAbilities1)
		{
			this.registerPacket(packet202PlayerAbilities1);
		}
	}

}