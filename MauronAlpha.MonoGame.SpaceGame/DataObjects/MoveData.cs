using MauronAlpha.MonoGame.SpaceGame.Quantifiers;
using MauronAlpha.MonoGame.SpaceGame.Units;

namespace MauronAlpha.MonoGame.SpaceGame.DataObjects {
	using MauronAlpha.MonoGame.SpaceGame.Interfaces;
	public class MoveData:GameComponent {

		public GameLocation Start;
		public GameLocation End;
		public MovementType Type;
		public I_Moveable Actor;

		public MoveData(I_Moveable source, I_MoveActor target)
			: base() {

		}

	}

	public abstract class MovementType : GameComponent {
		public abstract GameName Name { get; }
	}

}

namespace MauronAlpha.MonoGame.SpaceGame.Interfaces {
	using MauronAlpha.MonoGame.SpaceGame.DataObjects;
	public interface I_Moveable : I_MoveActor { }
	public interface I_MoveTarget : I_MoveActor { }
	public interface I_MoveActor : I_Locatable { }
	public interface I_Locatable {
		GameLocation Location { get; }
	}
}

namespace MauronAlpha.MonoGame.SpaceGame.Actuals {
	using MauronAlpha.MonoGame.SpaceGame.Collections;
	using MauronAlpha.MonoGame.SpaceGame.DataObjects;
	
	public class DefaultMovementTypes : MovementTypes {

		public static MovementTypes Static(SpeciesDefinition definition) {
			return new MovementTypes() { new MOVE_static(), new MOVE_obstacle() };
		}
		public static MovementTypes Obstacle(SpeciesDefinition definition) {
			return new MovementTypes() { new MOVE_static(), new MOVE_obstacle() };
		}
		public static MovementTypes Demon(SpeciesDefinition definition) {
			return new MovementTypes() {
				new MOVE_standAround(),
			};
		}
		public static MovementTypes Humanoid(SpeciesDefinition definition) {
			return new MovementTypes() {
				new MOVE_standAround(),
			};
		}
		public static MovementTypes Creature(SpeciesDefinition definition) {
			return new MovementTypes() {
				new MOVE_standAround(),
			};
		}
		public static MovementTypes Robot(SpeciesDefinition definition) {
			return new MovementTypes() {
				new MOVE_standAround(),
			};
		}

	}

	public class ActionType_Idle : ActionType {
		public override GameName Name {
			get { return new GameName("Idle"); }
		}
	}
	public class ActionType_Mobility : ActionType {
		public override GameName Name {
			get { return new GameName("Mobility"); }
		}
	}
	public class ActionType_AttackMotion : ActionType {
		public override GameName Name {
			get { return new GameName("AttackMotion"); }
		}
	}
	public class ActionType_Attack : ActionType {

		public override GameName Name {
			get { return new GameName("Attack"); }
		}
	}
	public class ActionType_Boost : ActionType {
		public override GameName Name {
			get { return new GameName("Boost"); }
		}
	}
	public class ActionType_Transition : ActionType {
		public override GameName Name {
			get { return new GameName("Transition"); }
		}
	}

	public class MOVE_standAround : MovementType {
		public override GameName Name { get { return new GameName("standAround"); } }
	}

	public class MOVE_static : MovementType {

		public override GameName Name { get { return new GameName("static"); } }

	}
	public class MOVE_obstacle : MovementType {

		public override GameName Name { get { return new GameName("obstacle"); } }

	}
	public class MOVE_topple : MovementType {

		public override GameName Name { get { return new GameName("topple"); } }

	}
	public class MOVE_walk : MovementType {

		public override GameName Name { get { return new GameName("walk"); } }

	}
	public class MOVE_land : MovementType {
		public override GameName Name { get { return new GameName("land"); } }
	}
	public class MOVE_ascend : MovementType {
		public override GameName Name { get { return new GameName("ascend"); } }
	}
	public class MOVE_descend : MovementType {
		public override GameName Name { get { return new GameName("descend"); } }
	}
	public class MOVE_hover : MovementType {
		public override GameName Name { get { return new GameName("hover"); } }
	}
	public class MOVE_glide : MovementType {
		public override GameName Name { get { return new GameName("glide"); } }
	}
	public class MOVE_drive : MovementType {

		public override GameName Name { get { return new GameName("drive"); } }

	}
	public class MOVE_dive : MovementType {

		public override GameName Name { get { return new GameName("dive"); } }

	}
	public class MOVE_thrown : MovementType {
		public override GameName Name { get { return new GameName("thrown"); } }
	}
	public class MOVE_collide : MovementType {
		public override GameName Name {
			get { return new GameName("collide"); }
		}
	}
	public class MOVE_slide : MovementType {
		public override GameName Name { get { return new GameName("slide"); } }
	}
	public class MOVE_leap : MovementType {

		public override GameName Name { get { return new GameName("leap"); } }

	}
	public class MOVE_fly : MovementType {

		public override GameName Name { get { return new GameName("fly"); } }

	}
	public class MOVE_swim : MovementType {

		public override GameName Name { get { return new GameName("swim"); } }

	}
	public class MOVE_fall : MovementType {
		public override GameName Name { get { return new GameName("fall"); } }
	}
	public class MOVE_crash : MovementType {
		public override GameName Name { get { return new GameName("crash"); } }
	}
	public class MOVE_surface : MovementType {
		public override GameName Name { get { return new GameName("surface"); } }
	}
	public class MOVE_stand : MovementType {
		public override GameName Name { get { return new GameName("stand"); } }
	}
	public class MOVE_duck : MovementType {
		public override GameName Name { get { return new GameName("duck"); } }
	}
	public class MOVE_standUp : MovementType {
		public override GameName Name { get { return new GameName("standUp"); } }
	}
	public class MOVE_onGround : MovementType {
		public override GameName Name {
			get { return new GameName("onGround"); }
		}
	}
	public class MOVE_advance : MovementType {
		public override GameName Name {
			get { return new GameName("advance"); }
		}
	}
	public class MOVE_fallback : MovementType {
		public override GameName Name {
			get { return new GameName("fallback"); }
		}
	}
	public class MOVE_teleport : MovementType {
		public override GameName Name {
			get { return new GameName("teleport"); }
		}
	}
	public class MOVE_sneak : MovementType {
		public override GameName Name {
			get { return new GameName("sneak"); }
		}
	}
	public class MOVE_explode : MovementType {
		public override GameName Name {
			get { return new GameName("sneak"); }
		}
	}
	public class MOVE_implode : MovementType {
		public override GameName Name {
			get { return new GameName("sneak"); }
		}
	}
	public class MOVE_accelerate : MovementType {
		public override GameName Name {
			get { return new GameName("accelerate"); }
		}
	}
	public class MOVE_decelerate : MovementType {
		public override GameName Name {
			get { return new GameName("decelerate"); }
		}
	}
	public class MOVE_jog : MovementType {
		public override GameName Name {
			get { return new GameName("jog"); }
		}
	}
	public class MOVE_sideStep : MovementType {
		public override GameName Name {
			get { return new GameName("sideStep"); }
		}
	}
	public class MOVE_sit : MovementType {
		public override GameName Name {
			get { return new GameName("sit"); }
		} 
	}
	public class MOVE_propel : MovementType {
		public override GameName Name {
			get { return new GameName("propel"); }
		}
	}
	public class MOVE_enter : MovementType {
		public override GameName Name {
			get { return new GameName("load"); }
		}
	}
	public class MOVE_exit : MovementType {
		public override GameName Name {
			get { return new GameName("load"); }
		}
	}
	public class MOVE_lieDown : MovementType {
		public override GameName Name { get { return new GameName("lieDown"); } }
	}
	public class MOVE_float : MovementType {
		public override GameName Name { get { return new GameName("float"); } }
	}
	public class MOVE_evade : MovementType {
		public override GameName Name { get { return new GameName("evade"); } }
	}
	public class MOVE_crawl : MovementType {
		public override GameName Name { get { return new GameName("crawl"); } }
	}
	public class MOVE_transit : MovementType {
		public override GameName Name { get { return new GameName("transit"); } }
	}
	public class MOVE_flee : MovementType {
		public override GameName Name { get { return new GameName("flee"); } }
	}
	public class MOVE_orbit : MovementType {
		public override GameName Name { get { return new GameName("orbit"); } }
	}
	public class MOVE_turn : MovementType {
		public override GameName Name {
			get { return new GameName("turn"); }
		}
	}
	public class MOVE_spin : MovementType {
		public override GameName Name {
			get { return new GameName("spin"); }
		}
	}
	public class MOVE_transport : MovementType {
		public override GameName Name {
			get { return new GameName("transport"); }
		}
	}
	public class MOVE_load : MovementType {
		public override GameName Name {
			get { return new GameName("load"); }
		}
	}
	public class MOVE_unload : MovementType {
		public override GameName Name {
			get { return new GameName("unload"); }
		}
	}
	public class MOVE_fire : MovementType {
		public override GameName Name {
			get { return new GameName("fire"); }
		}
	}
	public class MOVE_impact : MovementType {
		public override GameName Name {
			get { return new GameName("impact"); }
		}
	}
	public class MOVE_penetrate : MovementType {
		public override GameName Name {
			get { return new GameName("penetrate"); }
		}
	}
	public class MOVE_throw : MovementType {
		public override GameName Name {
			get { return new GameName("throw"); }
		}
	}
	public class MOVE_lift : MovementType {
		public override GameName Name {
			get { return new GameName("lift"); }
		}
	}
	public class MOVE_drop : MovementType {
		public override GameName Name {
			get { return new GameName("drop"); }
		}
	}
	public class MOVE_slam : MovementType {
		public override GameName Name {
			get { return new GameName("slam"); }
		}
	}
	public class MOVE_grab : MovementType {
		public override GameName Name {
			get { return new GameName("grab"); }
		}
	}
	public class MOVE_setDown : MovementType {
		public override GameName Name {
			get { return new GameName("setDown"); }
		}
	}
	public class MOVE_mount : MovementType {
		public override GameName Name {
			get { return new GameName("mount"); }
		}
	}
	public class MOVE_unmount : MovementType {
		public override GameName Name {
			get { return new GameName("unmount"); }
		}
	}
	public class MOVE_recoil : MovementType {
		public override GameName Name {
			get { return new GameName("recoil"); }
		}
	}
	public class MOVE_push : MovementType {
		public override GameName Name {
			get { return new GameName("push"); }
		}
	}
	public class MOVE_pushed : MovementType {
		public override GameName Name {
			get { return new GameName("pushed"); }
		}
	}
	public class MOVE_grind : MovementType {
		public override GameName Name {
			get { return new GameName("grind"); }
		}
	}
	public class MOVE_scratch : MovementType {
		public override GameName Name {
			get { return new GameName("scratch"); }
		}
	}
	public class MOVE_struggle : MovementType {
		public override GameName Name {
			get { return new GameName("struggle"); }
		}
	}
	public class MOVE_swing : MovementType {
		public override GameName Name {
			get { return new GameName("swing"); }
		}
	}
	public class MOVE_smash : MovementType {
		public override GameName Name {
			get { return new GameName("smash"); }
		}
	}
	public class MOVE_strike : MovementType {
		public override GameName Name {
			get { return new GameName("strike"); }
		}
	}
	public class MOVE_stab : MovementType {
		public override GameName Name {
			get { return new GameName("stab"); }
		}
	}
	public class MOVE_kick : MovementType {
		public override GameName Name {
			get { return new GameName("kick"); }
		}
	}
	public class MOVE_bite : MovementType {
		public override GameName Name {
			get { return new GameName("bite"); }
		}
	}
	public class MOVE_sleep : MovementType {
		public override GameName Name {
			get { return new GameName("sleep"); }
		}
	}
	public class MOVE_freeze : MovementType {
		public override GameName Name {
			get { return new GameName("freeze"); }
		}
	}
	public class MOVE_brake : MovementType {
		public override GameName Name {
			get { return new GameName("brake"); }
		}
	}
	public class MOVE_produce : MovementType {
		public override GameName Name {
			get { return new GameName("produce"); }
		}
	}
	public class MOVE_spit : MovementType {
		public override GameName Name {
			get { return new GameName("produce"); }
		}
	}
	public class MOVE_sting : MovementType {
		public override GameName Name {
			get { return new GameName("sting"); }
		}
	}
	public class MOVE_regenerate : MovementType {
		public override GameName Name {
			get { return new GameName("regenerate"); }
		}
	}
	public class MOVE_reload : MovementType {
		public override GameName Name {
			get { return new GameName("reload"); }
		}
	}
	public class MOVE_charge : MovementType {
		public override GameName Name {
			get { return new GameName("charge"); }
		}
	}
	public class MOVE_ready: MovementType {
		public override GameName Name {
			get { return new GameName("charge"); }
		}
	}
	public class MOVE_catch : MovementType {
		public override GameName Name {
			get { return new GameName("catch"); }
		}
	}
	public class MOVE_aim : MovementType {
		public override GameName Name {
			get { return new GameName("aim"); }
		}
	}
	public class MOVE_interact : MovementType {
		public override GameName Name {
			get { return new GameName("interact"); }
		}
	}
	public class MOVE_dead : MovementType {
		public override GameName Name {
			get { return new GameName("dead"); }
		}
	}
	public class MOVE_vanish : MovementType {
		public override GameName Name {
			get { return new GameName("vanish"); }
		}
	}
	public class MOVE_reappear : MovementType {
		public override GameName Name {
			get { return new GameName("reappear"); }
		}
	}
	public class MOVE_liquify : MovementType {
		public override GameName Name {
			get { return new GameName("liquify"); }
		}
	}
	public class MOVE_melt : MovementType {
		public override GameName Name {
			get { return new GameName("liquify"); }
		}
	}
	public class MOVE_vaporize : MovementType {
		public override GameName Name {
			get { return new GameName("vaporize"); }
		}
	}
	public class MOVE_powerUp : MovementType {
		public override GameName Name {
			get { return new GameName("powerUp"); }
		}
	}
	public class MOVE_powerDown : MovementType {
		public override GameName Name {
			get { return new GameName("powerDown"); }
		}
	}
	public class MOVE_disCharge : MovementType {
		public override GameName Name {
			get { return new GameName("disCharge"); }
		}
	}
	public class MOVE_beam : MovementType {
		public override GameName Name {
			get { return new GameName("beam"); }
		}
	}
	public class MOVE_ray : MovementType {
		public override GameName Name {
			get { return new GameName("ray"); }
		}
	}
	public class MOVE_bullet : MovementType {
		public override GameName Name {
			get { return new GameName("bullet"); }
		}
	}
	public class MOVE_arrow : MovementType {
		public override GameName Name {
			get { return new GameName("arrow"); }
		}
	}

	public class MOVE_grenade : MovementType {
		public override GameName Name {
			get { return new GameName("grenade"); }
		}
	}
	public class MOVE_stream : MovementType {
		public override GameName Name {
			get { return new GameName("stream"); }
		}
	}
	public class MOVE_loaded : MovementType {
		public override GameName Name {
			get { return new GameName("loaded"); }
		}
	}
	public class MOVE_loading : MovementType {
		public override GameName Name {
			get { return new GameName("loading"); }
		}
	}
	public class MOVE_unloading : MovementType {
		public override GameName Name {
			get { return new GameName("unloading"); }
		}
	}
	public class MOVE_burn : MovementType {
		public override GameName Name {
			get { return new GameName("burn"); }
		}
	}
	public class MOVE_extinguish : MovementType {
		public override GameName Name {
			get { return new GameName("extinguish"); }
		}
	}
	public class MOVE_disintegrate : MovementType {
		public override GameName Name {
			get { return new GameName("disintegrate"); }
		}
	}
	public class MOVE_suck : MovementType {
		public override GameName Name {
			get { return new GameName("suck"); }
		}
	}
	public class MOVE_holdOn : MovementType {
		public override GameName Name {
			get { return new GameName("holdOn"); }
		}
	}
	public class MOVE_vomit : MovementType {
		public override GameName Name {
			get { return new GameName("vomit"); }
		}
	}
	public class MOVE_choke : MovementType {
		public override GameName Name {
			get { return new GameName("choke"); }
		}
	}
	public class MOVE_suffocate : MovementType {
		public override GameName Name {
			get { return new GameName("suffocate"); }
		}
	}
	public class MOVE_exhale : MovementType {
		public override GameName Name {
			get { return new GameName("exhale"); }
		}
	}
	public class MOVE_inhale : MovementType {
		public override GameName Name {
			get { return new GameName("exhale"); }
		}
	}

	public class MOVE_eat : MovementType {
		public override GameName Name {
			get { return new GameName("eat"); }
		}
	}
	public class MOVE_drink : MovementType {
		public override GameName Name {
			get { return new GameName("drink"); }
		}
	}
	public class MOVE_recover : MovementType {
		public override GameName Name {
			get { return new GameName("recover"); }
		}
	}
	public class MOVE_hunkerDown : MovementType {
		public override GameName Name {
			get { return new GameName("hunkerDown"); }
		}
	}
	public class MOVE_shout : MovementType {
		public override GameName Name {
			get { return new GameName("shout"); }
		}
	}
	public class MOVE_scream : MovementType {
		public override GameName Name {
			get { return new GameName("scream"); }
		}
	}
	public class MOVE_disable : MovementType {
		public override GameName Name {
			get { return new GameName("disable"); }
		}
	}
	public class MOVE_repair : MovementType {
		public override GameName Name {
			get { return new GameName("repair"); }
		}
	}
	public class MOVE_listen : MovementType {
		public override GameName Name {
			get { return new GameName("listen"); }
		}
	}
	public class MOVE_observe : MovementType {
		public override GameName Name {
			get { return new GameName("observe"); }
		}
	}
	public class MOVE_study : MovementType {
		public override GameName Name {
			get { return new GameName("study"); }
		}
	}
	public class MOVE_recharge : MovementType {
		public override GameName Name {
			get { return new GameName("recharge"); }
		}
	}
	public class MOVE_open : MovementType {
		public override GameName Name {
			get { return new GameName("open"); }
		}
	}
	public class MOVE_close : MovementType {
		public override GameName Name {
			get { return new GameName("close"); }
		}
	}
	public class MOVE_held : MovementType {
		public override GameName Name {
			get { return new GameName("held"); }
		}
	}
	public class MOVE_dissolve : MovementType {
		public override GameName Name {
			get { return new GameName("dissolve"); }
		}
	}
	public class MOVE_spawn : MovementType {
		public override GameName Name {
			get { return new GameName("spawn"); }
		}
	}
	public class MOVE_breed : MovementType {
		public override GameName Name {
			get { return new GameName("breed"); }
		}
	}
	public class MOVE_nap : MovementType {
		public override GameName Name {
			get { return new GameName("nap"); }
		}
	}
	public class MOVE_see : MovementType {
		public override GameName Name {
			get { return new GameName("see"); }
		}
	}
	public class MOVE_scan : MovementType {
		public override GameName Name {
			get { return new GameName("scan"); }
		}
	}
	public class MOVE_operate: MovementType {
		public override GameName Name {
			get { return new GameName("operate"); }
		}
	}
	public class MOVE_channel : MovementType {
		public override GameName Name {
			get { return new GameName("channel"); }
		}
	}
	public class MOVE_cast : MovementType {
		public override GameName Name {
			get { return new GameName("cast"); }
		}
	}
	public class MOVE_equip : MovementType {
		public override GameName Name {
			get { return new GameName("equip"); }
		}
	}
	public class MOVE_unEquip : MovementType {
		public override GameName Name {
			get { return new GameName("equip"); }
		}
	}
	public class MOVE_putAway : MovementType {
		public override GameName Name {
			get { return new GameName("putAway"); }
		}
	}
	public class MOVE_sheathe : MovementType {
		public override GameName Name {
			get { return new GameName("sheathe"); }
		}
	}
	public class MOVE_unSheathe : MovementType {
		public override GameName Name {
			get { return new GameName("unSheathe"); }
		}
	}
	public class MOVE_changeStance : MovementType {
		public override GameName Name {
			get { return new GameName("changeStance"); }
		}
	}
	public class MOVE_climb : MovementType {
		public override GameName Name {
			get { return new GameName("climb"); }
		}
	}
	public class MOVE_wallWalk : MovementType {
		public override GameName Name {
			get { return new GameName("wallWalk"); }
		}
	}
	public class MOVE_wallRun : MovementType {
		public override GameName Name {
			get { return new GameName("wallRun"); }
		}
	}
	public class MOVE_ceilingRun : MovementType {
		public override GameName Name {
			get { return new GameName("ceilingRun"); }
		}
	}
	public class MOVE_ceilingWalk : MovementType {
		public override GameName Name {
			get { return new GameName("ceilingWalk"); }
		}
	}
	public class MOVE_pushSelf : MovementType {
		public override GameName Name {
			get { return new GameName("pushSelf"); }
		}
	}
	public class MOVE_burstOut : MovementType {
		public override GameName Name {
			get { return new GameName("burstOut"); }
		}
	}
	public class MOVE_climbUp : MovementType {
		public override GameName Name {
			get { return new GameName("climbUp"); }
		} 
	}
	public class MOVE_climbDown : MovementType {
		public override GameName Name {
			get { return new GameName("climbDown"); }
		}
	}
	public class MOVE_jumpOff : MovementType {
		public override GameName Name {
			get { return new GameName("jumpOff"); }
		}
	}
	public class MOVE_jumpUp : MovementType {
		public override GameName Name {
			get { return new GameName("jumpUp"); }
		}
	}
	public class MOVE_jumpOn : MovementType {
		public override GameName Name {
			get { return new GameName("jumpOn"); }
		}
	}
	public class MOVE_enterWarp : MovementType {
		public override GameName Name {
			get { return new GameName("enterWarp"); }
		}
	}
	public class MOVE_exitWarp : MovementType {
		public override GameName Name {
			get { return new GameName("exitWarp"); }
		}
	}
	public class MOVE_takeOff : MovementType {
		public override GameName Name {
			get { return new GameName("takeOff"); }
		}
	}
	public class MOVE_timer : MovementType {
		public override GameName Name {
			get { return new GameName("timer"); }
		}
	}
	public class MOVE_arm : MovementType {
		public override GameName Name {
			get { return new GameName("arm"); }
		}
	}
	public class MOVE_disArm : MovementType {
		public override GameName Name {
			get { return new GameName("disArm"); }
		}
	}
	public class MOVE_lower : MovementType {
		public override GameName Name {
			get { return new GameName("lower"); }
		}
	}
	public class MOVE_raise : MovementType {
		public override GameName Name {
			get { return new GameName("raise"); }
		}
	}
	public class MOVE_threaten : MovementType {
		public override GameName Name {
			get { return new GameName("threaten"); }
		}
	}
	public class MOVE_distract : MovementType {
		public override GameName Name {
			get { return new GameName("distract"); }
		}
	}
	public class MOVE_scare : MovementType {
		public override GameName Name {
			get { return new GameName("scare"); }
		}
	}
	public class MOVE_lure : MovementType {
		public override GameName Name {
			get { return new GameName("lure"); }
		}
	}
	public class MOVE_hack : MovementType {
		public override GameName Name {
			get { return new GameName("hack"); }
		}
	}
	public class MOVE_manifest : MovementType {
		public override GameName Name {
			get { return new GameName("manifest"); }
		}
	}
	public class MOVE_puff : MovementType {
		public override GameName Name {
			get { return new GameName("puff"); }
		}
	}
	public class MOVE_transformOther : MovementType {
		public override GameName Name {
			get { return new GameName("transformOther"); }
		}
	}
	public class MOVE_transformSelf : MovementType {
		public override GameName Name {
			get { return new GameName("transformSelf"); }
		}
	}
	public class MOVE_parry : MovementType {
		public override GameName Name {
			get { return new GameName("parry"); }
		}
	}
	public class MOVE_counterAttack : MovementType {
		public override GameName Name {
			get { return new GameName("counterAttack"); }
		}
	}
	public class MOVE_deflect : MovementType {
		public override GameName Name {
			get { return new GameName("deflect"); }
		}
	}
	public class MOVE_block : MovementType {
		public override GameName Name {
			get { return new GameName("block"); }
		}
	}
	public class MOVE_holdBack : MovementType {
		public override GameName Name {
			get { return new GameName("holdBack"); }
		}
	}
	public class MOVE_ricochet : MovementType {
		public override GameName Name {
			get { return new GameName("ricochet"); }
		}
	}
	public class MOVE_bounceOff : MovementType {
		public override GameName Name {
			get { return new GameName("bounceOff"); }
		}
	}
	public class MOVE_carry : MovementType {
		public override GameName Name {
			get { return new GameName("carry"); }
		}
	}
	public class MOVE_carried : MovementType {
		public override GameName Name {
			get { return new GameName("carried"); }
		}
	}
	public class MOVE_charm : MovementType {
		public override GameName Name {
			get { return new GameName("charm"); }
		}
	}
	public class MOVE_learn : MovementType {
		public override GameName Name {
			get { return new GameName("learn"); }
		}
	}
	public class MOVE_teach : MovementType {
		public override GameName Name {
			get { return new GameName("teach"); }
		}
	}
	public class MOVE_argue : MovementType {
		public override GameName Name {
			get { return new GameName("argue"); }
		}
	}
	public class MOVE_socialize : MovementType {
		public override GameName Name {
			get { return new GameName("socialize"); }
		}
	}
	public class MOVE_surrender : MovementType {
		public override GameName Name {
			get { return new GameName("surrender"); }
		}
	}
	public class MOVE_panic : MovementType {
		public override GameName Name {
			get { return new GameName("panic"); }
		}
	}
	public class MOVE_surrendered : MovementType {
		public override GameName Name {
			get { return new GameName("surrendered"); }
		}
	}
	public class MOVE_inChains: MovementType {
		public override GameName Name {
			get { return new GameName("inChains"); }
		}
	}
	public class MOVE_inTrap : MovementType {
		public override GameName Name {
			get { return new GameName("inTrap"); }
		}
	}
	public class MOVE_inPrison : MovementType {
		public override GameName Name {
			get { return new GameName("inPrison"); }
		}
	}
	public class MOVE_inCouncil : MovementType {
		public override GameName Name {
			get { return new GameName("inCouncil"); }
		}
	}
	public class MOVE_working : MovementType {
		public override GameName Name {
			get { return new GameName("working"); }
		}
	}
	public class MOVE_building : MovementType {
		public override GameName Name {
			get { return new GameName("building"); }
		}
	}
	public class MOVE_mining : MovementType {
		public override GameName Name {
			get { return new GameName("mining"); }
		}
	}
	public class MOVE_upGrade : MovementType {
		public override GameName Name {
			get { return new GameName("upGrade"); }
		}
	}
	public class MOVE_downGrade : MovementType {
		public override GameName Name {
			get { return new GameName("upGrade"); }
		}
	}
	public class MOVE_manufacture : MovementType {
		public override GameName Name {
			get { return new GameName("manufacture"); }
		}
	}
	public class MOVE_assemble : MovementType {
		public override GameName Name {
			get { return new GameName("assemble"); }
		}
	}
	public class MOVE_disAssemble : MovementType {
		public override GameName Name {
			get { return new GameName("disAssemble"); }
		}
	}
	public class MOVE_organize : MovementType {
		public override GameName Name {
			get { return new GameName("organize"); }
		}
	}
	public class MOVE_sabotage : MovementType {
		public override GameName Name {
			get { return new GameName("sabotage"); }
		}
	}
	public class MOVE_spy : MovementType {
		public override GameName Name {
			get { return new GameName("spy"); }
		}
	}
	public class MOVE_investigate : MovementType {
		public override GameName Name {
			get { return new GameName("investigate"); }
		}
	}
	public class MOVE_peek : MovementType {
		public override GameName Name {
			get { return new GameName("peek"); }
		}
	}
	public class MOVE_attach : MovementType {
		public override GameName Name {
			get { return new GameName("attach"); }
		}
	}
	public class MOVE_safeCrack : MovementType {
		public override GameName Name {
			get { return new GameName("safeCrack"); }
		}
	}
	public class MOVE_pickPocket : MovementType {
		public override GameName Name {
			get { return new GameName("pickPocket"); }
		}
	}
	public class MOVE_frisk : MovementType {
		public override GameName Name {
			get { return new GameName("frisk"); }
		}
	}
	public class MOVE_interrogate : MovementType {
		public override GameName Name {
			get { return new GameName("interrogate"); }
		}
	}
	public class MOVE_dig : MovementType {
		public override GameName Name {
			get { return new GameName("dig"); }
		}
	}
	public class MOVE_coverUp : MovementType {
		public override GameName Name {
			get { return new GameName("coverUp"); }
		}
	}
	public class MOVE_slime : MovementType {
		public override GameName Name {
			get { return new GameName("slime"); }
		}
	}
	public class MOVE_commute : MovementType {
		public override GameName Name {
			get { return new GameName("commute"); }
		}
	}
	public class MOVE_trade : MovementType {
		public override GameName Name {
			get { return new GameName("trade"); }
		}
	}
	public class MOVE_aquire : MovementType {
		public override GameName Name {
			get { return new GameName("aquire"); }
		}
	}
	public class MOVE_standOn : MovementType {
		public override GameName Name {
			get { return new GameName("standOn"); }
		}
	}
	public class MOVE_sitOn : MovementType {
		public override GameName Name {
			get { return new GameName("sitOn"); }
		}
	}
	public class MOVE_leanOn : MovementType {
		public override GameName Name {
			get { return new GameName("sitOn"); }
		}
	}
	public class MOVE_steady : MovementType {
		public override GameName Name {
			get { return new GameName("steady"); }
		}
	}
	public class MOVE_recall : MovementType {
		public override GameName Name {
			get { return new GameName("recall"); }
		}
	}
	public class MOVE_sendOut : MovementType {
		public override GameName Name {
			get { return new GameName("recall"); }
		}
	}
	public class MOVE_scramble : MovementType {
		public override GameName Name {
			get { return new GameName("scramble"); }
		}
	}
	public class MOVE_rileUp : MovementType {
		public override GameName Name {
			get { return new GameName("rileUp"); }
		}
	}
	public class MOVE_calmDown : MovementType {
		public override GameName Name {
			get { return new GameName("calmDown"); }
		}
	}
	public class MOVE_convince : MovementType {
		public override GameName Name {
			get { return new GameName("convince"); }
		}
	}
	public class MOVE_disgust : MovementType {
		public override GameName Name {
			get { return new GameName("disgust"); }
		}
	}
	public class MOVE_terrify : MovementType {
		public override GameName Name {
			get { return new GameName("terrify"); }
		}
	}
	public class MOVE_charmed : MovementType {
		public override GameName Name {
			get { return new GameName("charmed"); }
		}
	}
	public class MOVE_controlled : MovementType {
		public override GameName Name {
			get { return new GameName("controlled"); }
		}
	}
	public class MOVE_tamed : MovementType {
		public override GameName Name {
			get { return new GameName("tamed"); }
		}
	}
	public class MOVE_fallAsleep : MovementType {
		public override GameName Name {
			get { return new GameName("fallAsleep"); }
		}
	}
	public class MOVE_wakeUp : MovementType {
		public override GameName Name {
			get { return new GameName("wakeUp"); }
		}
	}
	public class MOVE_engage : MovementType {
		public override GameName Name {
			get { return new GameName("engage"); }
		}
	}
	public class MOVE_disEngage : MovementType {
		public override GameName Name {
			get { return new GameName("disEngage"); }
		}
	}
	public class MOVE_visit : MovementType {
		public override GameName Name {
			get { return new GameName("visit"); }
		}
	}
	public class MOVE_diplomaticMission : MovementType {
		public override GameName Name {
			get { return new GameName("diplomaticMission"); }
		}
	}
	public class MOVE_sing : MovementType {
		public override GameName Name {
			get { return new GameName("sing"); }
		}
	}
	public class MOVE_signal : MovementType {
		public override GameName Name {
			get { return new GameName("signal"); }
		}
	}
	public class MOVE_chant : MovementType {
		public override GameName Name {
			get { return new GameName("chant"); }
		}
	}
	public class MOVE_formulate : MovementType {
		public override GameName Name {
			get { return new GameName("formulate"); }
		}
	}
	public class MOVE_remember : MovementType {
		public override GameName Name {
			get { return new GameName("remember"); }
		}
	}
	public class MOVE_think : MovementType {
		public override GameName Name {
			get { return new GameName("think"); }
		}
	}
	public class MOVE_ponder : MovementType {
		public override GameName Name {
			get { return new GameName("ponder"); }
		}
	}
	public class MOVE_disguise : MovementType {
		public override GameName Name {
			get { return new GameName("disguise"); }
		}
	}
	public class MOVE_clean : MovementType {
		public override GameName Name {
			get { return new GameName("clean"); }
		}
	}
	public class MOVE_bandage : MovementType {
		public override GameName Name {
			get { return new GameName("bandage"); }
		}
	}
	public class MOVE_performSurgery : MovementType {
		public override GameName Name {
			get { return new GameName("performSurgery"); }
		}
	}
	public class MOVE_performCPR : MovementType {
		public override GameName Name {
			get { return new GameName("performCPR"); }
		}
	}
	public class MOVE_mercyKill : MovementType {
		public override GameName Name {
			get { return new GameName("mercyKill"); }
		}
	}
	public class MOVE_finishOff : MovementType {
		public override GameName Name {
			get { return new GameName("finishOff"); }
		}
	}
	public class MOVE_knockOut : MovementType {
		public override GameName Name {
			get { return new GameName("knockOut"); }
		}
	}
	public class MOVE_brace : MovementType {
		public override GameName Name {
			get { return new GameName("brace"); }
		}
	}
	public class MOVE_treat : MovementType {
		public override GameName Name {
			get { return new GameName("treat"); }
		}
	}
	public class MOVE_feed : MovementType {
		public override GameName Name {
			get { return new GameName("feed"); }
		}
	}
	public class MOVE_holdPerson : MovementType {
		public override GameName Name {
			get { return new GameName("holdPerson"); }
		}
	}
	public class MOVE_holdObject: MovementType {
		public override GameName Name {
			get { return new GameName("holdObject"); }
		}
	}
	public class MOVE_supress : MovementType {
		public override GameName Name {
			get { return new GameName("supress"); }
		}
	}
	public class MOVE_worn : MovementType {
		public override GameName Name {
			get { return new GameName("worn"); }
		}
	}
	public class MOVE_dropped : MovementType {
		public override GameName Name {
			get { return new GameName("dropped"); }
		}
	}
	public class MOVE_smuggle : MovementType {
		public override GameName Name {
			get { return new GameName("smuggle"); }
		}
	}
	public class MOVE_smuggled : MovementType {
		public override GameName Name {
			get { return new GameName("smuggled"); }
		}
	}
	public class MOVE_steal : MovementType {
		public override GameName Name {
			get { return new GameName("steal"); }
		}
	}
	public class MOVE_rob : MovementType {
		public override GameName Name {
			get { return new GameName("rob"); }
		}
	}
	public class MOVE_assault: MovementType {
		public override GameName Name {
			get { return new GameName("assault"); }
		}
	}
	public class MOVE_board : MovementType {
		public override GameName Name {
			get { return new GameName("board"); }
		}
	}
	public class MOVE_broadSide : MovementType {
		public override GameName Name {
			get { return new GameName("broadSide"); }
		}
	}
	public class MOVE_intercept : MovementType {
		public override GameName Name {
			get { return new GameName("intercept"); }
		}
	}
	public class MOVE_shield : MovementType {
		public override GameName Name {
			get { return new GameName("shield"); }
		}
	}
	public class MOVE_reInforce : MovementType {
		public override GameName Name {
			get { return new GameName("shield"); }
		}
	}
	public class MOVE_lurch : MovementType {
		public override GameName Name {
			get { return new GameName("lurch"); }
		}
	}
	public class MOVE_breach : MovementType {
		public override GameName Name {
			get { return new GameName("breach"); }
		}
	}
	public class MOVE_face : MovementType {
		public override GameName Name {
			get { return new GameName("face"); }
		}
	}
	public class MOVE_avertFace : MovementType {
		public override GameName Name {
			get { return new GameName("avertFace"); }
		}
	}
	public class MOVE_resist : MovementType {
		public override GameName Name {
			get { return new GameName("resist"); }
		}
	}
	public class MOVE_useCover : MovementType {
		public override GameName Name {
			get { return new GameName("useCover"); }
		}
	}
	public class MOVE_lookAround : MovementType {
		public override GameName Name {
			get { return new GameName("lookAround"); }
		}
	}
	public class MOVE_recognize : MovementType {
		public override GameName Name {
			get { return new GameName("recongnize"); }
		}
	}
	public class MOVE_forget : MovementType {
		public override GameName Name {
			get { return new GameName("forget"); }
		}
	}
	public class MOVE_starve : MovementType {
		public override GameName Name {
			get { return new GameName("starve"); }
		}
	}
	public class MOVE_stall : MovementType {
		public override GameName Name {
			get { return new GameName("stall"); }
		}
	}
	public class MOVE_guard : MovementType {
		public override GameName Name {
			get { return new GameName("guard"); }
		}
	}
	public class MOVE_watchObject : MovementType {
		public override GameName Name {
			get { return new GameName("watchObject"); }
		}
	}
	public class MOVE_dormant : MovementType {
		public override GameName Name {
			get { return new GameName("dormant"); }
		}
	}
	public class MOVE_recoverGuard : MovementType {
		public override GameName Name {
			get { return new GameName("recoverGuard"); }
		}
	}
	public class MOVE_recoverMind : MovementType {
		public override GameName Name {
			get { return new GameName("recoverMind"); }
		}
	}
	public class MOVE_focus : MovementType {
		public override GameName Name {
			get { return new GameName("focus"); }
		}
	}
	public class MOVE_lie : MovementType {
		public override GameName Name {
			get { return new GameName("lie"); }
		}
	}
	public class MOVE_warn : MovementType {
		public override GameName Name {
			get { return new GameName("warn"); }
		}
	}
	public class MOVE_alert : MovementType {
		public override GameName Name {
			get { return new GameName("alert"); }
		}
	}
	public class MOVE_comms : MovementType {
		public override GameName Name {
			get { return new GameName("comms"); }
		}
	}
	public class MOVE_muffle : MovementType {
		public override GameName Name {
			get { return new GameName("muffle"); }
		}
	}
	public class MOVE_jam : MovementType {
		public override GameName Name {
			get { return new GameName("jam"); }
		}
	}
	public class MOVE_unJam : MovementType {
		public override GameName Name {
			get { return new GameName("unJam"); }
		}
	}
	public class MOVE_jammed : MovementType {
		public override GameName Name {
			get { return new GameName("jammed"); }
		}
	}
	public class MOVE_distracted : MovementType {
		public override GameName Name {
			get { return new GameName("distracted"); }
		}
	}
	public class MOVE_stun : MovementType {
		public override GameName Name {
			get { return new GameName("stun"); }
		}
	}
	public class MOVE_stunned : MovementType {
		public override GameName Name {
			get { return new GameName("stunned"); }
		}
	}
	public class MOVE_confuse : MovementType {
		public override GameName Name {
			get { return new GameName("confuse"); }
		}
	}
	public class MOVE_confused : MovementType {
		public override GameName Name {
			get { return new GameName("confused"); }
		}
	}
	public class MOVE_malfunction : MovementType {
		public override GameName Name {
			get { return new GameName("malfunction"); }
		}
	}
	public class MOVE_boost : MovementType {
		public override GameName Name {
			get { return new GameName("boost"); }
		}
	}
	public class MOVE_mumble : MovementType {
		public override GameName Name {
			get { return new GameName("mumble"); }
		}
	}
	public class MOVE_haggle : MovementType {
		public override GameName Name {
			get { return new GameName("haggle"); }
		}
	}
	public class MOVE_wait : MovementType {
		public override GameName Name {
			get { return new GameName("wait"); }
		}
	}
	public class MOVE_convert : MovementType {
		public override GameName Name {
			get { return new GameName("convert"); }
		}
	}
}

