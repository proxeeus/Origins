using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	[FlipableAttribute( 0x13be, 0x13c3 )]
	public class ChainLegs : BaseArmor
	{
        public override int BasePhysicalResistance { get { return 4; } }
        public override int BaseFireResistance { get { return 4; } }
        public override int BaseColdResistance { get { return 4; } }
        public override int BasePoisonResistance { get { return 1; } }
        public override int BaseEnergyResistance { get { return 2; } }

        public override int InitMinHits { get { return 45; } }
        public override int InitMaxHits { get { return 60; } }

        public override int AosStrReq { get { return 60; } }
        public override int OldStrReq { get { return 20; } }

       // public override int OldDexBonus { get { return 0; } }

        public override int ArmorBase { get { return 23; } }

        public override ArmorMaterialType MaterialType { get { return ArmorMaterialType.Chainmail; } }

		[Constructable]
		public ChainLegs() : base( 0x13BE )
		{
			Weight = 7.0;
		}

		public ChainLegs( Serial serial ) : base( serial )
		{
		}

        public override void OnSingleClick(Mobile from)
        {

            string durabilitylevel = GetDurabilityLevel();
            string protectionlevel = GetProtectionLevel();

            if (this.Name != null)
            {
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", this.Name));
            }
            else
            {
                if (this.Quality == ArmorQuality.Exceptional)
                {
                    if (this.Crafter != null)
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", String.Format("exceptional chainmail leggings (crafted by {0})", this.Crafter.Name)));
                    else
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "exceptional chainmail leggings"));
                }
                else if (IsInIDList(from) == false && (this.ProtectionLevel != ArmorProtectionLevel.Regular || this.Durability != ArmorDurabilityLevel.Regular))
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "magic chainmail leggings"));
                }
                else if (IsInIDList(from) == true)
                {
                    if (this.Durability > ArmorDurabilityLevel.Regular && this.ProtectionLevel == ArmorProtectionLevel.Regular)
                    {
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", durabilitylevel + " chainmail leggings"));
                    }
                    else if (this.ProtectionLevel > ArmorProtectionLevel.Regular && this.Durability == ArmorDurabilityLevel.Regular)
                    {
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "chainmail leggings " + protectionlevel));
                    }
                    else if (this.ProtectionLevel > ArmorProtectionLevel.Regular && this.Durability > ArmorDurabilityLevel.Regular)
                    {
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", durabilitylevel + " chainmail leggings " + protectionlevel));
                    }
                    else
                    {
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "chainmail leggings"));
                    }
                }
                else
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "chainmail leggings"));
                }
            }
        }
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}