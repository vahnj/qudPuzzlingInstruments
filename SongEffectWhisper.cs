using System;
using System.Collections.Generic;
using XRL.World;
using XRL.Rules;
using XRL.World.Parts;
using XRL.UI;

namespace XRL.World.Effects
{
	[Serializable]
	public class acegiak_SongEffectWhisper : acegiak_SongEffect
	{
		public acegiak_SongEffectWhisper()
		{
			base.DisplayName = "&Cwhispersong";
		}

		public acegiak_SongEffectWhisper(int _Duration)
			: this()
		{
			Duration = _Duration;
		}

		public override string GetDetails()
		{
			return "inspiring to secrets";

		}

		public override bool CanApplyToStack()
		{
			return true;
		}

		public override bool Apply(GameObject Object)
		{
            int radius = 20;
            Cell currentCell = Object.CurrentCell;
				if (currentCell != null)
				{
					List<GameObject> list = currentCell.ParentZone.FastSquareSearch(currentCell.X, currentCell.Y, radius, "Combat", null);
					foreach (GameObject item in list)
					{
						if (Object.DistanceTo(item) <= radius && item.pBrain != null)
						{
                            if(item.pBrain.GetOpinion(Object) == Brain.CreatureOpinion.allied){
							
                                item.ApplyEffect(new acegiak_SongSecondaryEffectWhisper(10* Stat.Random(1, 10)));
                                if(item.IsPlayer()){
					                Popup.Show("You are inspired to secrets!");
                                }else{
                                    IPart.AddPlayerMessage(item.The+item.DisplayNameOnly+item.Is+" inspired to secrets!");
                                }
                            }
						}
					}
				}
			return true;
		}

		public override void Register(GameObject Object)
		{
			base.Register(Object);
		}

		public override void Unregister(GameObject Object)
		{
			base.Unregister(Object);
		}

		public override bool FireEvent(Event E)
		{
			return true;
		}
	}
}
