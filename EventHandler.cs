using Exiled.API.Features;
using Exiled.Events.EventArgs;

namespace CombatLog
{
    internal sealed class EventHandlers
    {
        
        private static readonly Config config = CombatLog.instance.Config;
        public void OnHurting(HurtingEventArgs e)
        {
            CombatLog.instance.combatLines.Add(new CombatLine(e.Amount, e.Attacker.Nickname, e.Target.Nickname, CombatLine.DamageReceivedType.Taken));
            CombatLog.instance.combatLines.Add(new CombatLine(e.Amount, e.Target.Nickname, e.Attacker.Nickname, CombatLine.DamageReceivedType.Dealt));
        }
        public void OnDying(DyingEventArgs e)
        {
            CombatLog.instance.combatLines.Add(new CombatLine(0, e.Killer.Nickname, e.Target.Nickname, CombatLine.DamageReceivedType.Taken, true));
            CombatLog.instance.combatLines.Add(new CombatLine(0, e.Target.Nickname, e.Killer.Nickname, CombatLine.DamageReceivedType.Dealt, true));
        }
        public void OnRoundStart()
        {
            CombatLog.instance.combatLines.Clear();
        }
    }
    public class CombatLine
    {
        private float damage;
        private string attacker;
        private string victim;
        private DamageReceivedType dmgreceivedtype;
        private bool died;
        private System.DateTime timeOfAction;
        public CombatLine(float Dmg, string Attacker, string Victim, DamageReceivedType DamageReceivedType, bool Died = false)
        {
            damage = Dmg;
            attacker = Attacker;
            victim = Victim;
            dmgreceivedtype = DamageReceivedType;
            died = Died;
            timeOfAction = System.DateTime.Now;
        }
        public enum DamageReceivedType
        {
            Taken,
            Dealt
        }
        public bool IsRelatedToPlayer(Player player)
        {
            return (player.Nickname == attacker || player.Nickname == victim);
        }
        public string GetLine
        {
            get
            {
                if (dmgreceivedtype == DamageReceivedType.Taken)
                {
                    if (died)
                    {
                        return $"Attacker: {attacker} | Victim: {victim} | DMG: {damage} | Weapon: (N/A) | {timeOfAction} | Dead";
                    }
                    return $"Attacker: {attacker} | Victim: {victim} | DMG: {damage} | Weapon: (N/A) | {timeOfAction}";
                }
                if (died)
                {
                    return $"Attacker: {attacker} | Victim: {victim} | DMG: {damage} | Weapon: (N/A) | {timeOfAction} | Killed";
                }
                return $"Attacker: {attacker} | Victim: {victim} | DMG: {damage} | Weapon: (N/A) | {timeOfAction}";
            }
        }
    }
}