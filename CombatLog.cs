using System;
using System.Collections.Generic;
using Exiled.API.Features;

namespace CombatLog
{
    public class CombatLog : Plugin<Config>
    {
        public List<CombatLine> combatLines = new List<CombatLine>();
        public static CombatLog instance { get; private set; }
        private EventHandlers Eventhandler { get; set; }
        public override void OnEnabled()
        {
            instance = this;
            Eventhandler = new EventHandlers();
            RegisterEvents();
            base.OnEnabled();
            Log.Warn("¡Gracias por usar mi plugin! Cualquier bug o sugerencia notificar a: \"nombre_original#8857\"");
        }
        public override void OnDisabled()
        {
            UnregisterEvents();
            Eventhandler = null;
            instance = null;
            base.OnDisabled();
        }
        private void RegisterEvents()
        {
            Exiled.Events.Handlers.Player.Hurting += Eventhandler.OnHurting;
            Exiled.Events.Handlers.Player.Dying += Eventhandler.OnDying;
            Exiled.Events.Handlers.Server.RoundStarted += Eventhandler.OnRoundStart;
        }
        private void UnregisterEvents()
        {
            Exiled.Events.Handlers.Player.Hurting -= Eventhandler.OnHurting;
            Exiled.Events.Handlers.Player.Dying -= Eventhandler.OnDying;
            Exiled.Events.Handlers.Server.RoundStarted -= Eventhandler.OnRoundStart;
        }
        public override string Name => "CombatLog";
        public override string Author => "MarcosVLl2";
        public override Version Version { get; } = new Version(1, 0, 0);
        public override Version RequiredExiledVersion { get; } = new Version(5, 0, 0);
    }
}