namespace CombatLog
{
    using System;

    using CommandSystem;
    using System.Collections.Generic;

    using Exiled.API.Features;
    using Exiled.API.Features.Items;

    /// <summary>
    /// CombatLog display command
    /// </summary>
    [CommandHandler(typeof(ClientCommandHandler))]
    public class CombatLogCommand : ICommand
    {
        public string Command { get; } = "combatlog";

        public string[] Aliases { get; } = new[] { "cl", "combat" };

        public string Description { get; } = "Displays the combat log";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            response = "";
            Player player = Player.Get(((CommandSender)sender).SenderId);
            List<CombatLine> playerlines = CombatLog.instance.combatLines.FindAll(line => line.IsRelatedToPlayer(player));
            playerlines.Reverse();
            foreach (CombatLine line in playerlines)
            {
                response += line.GetLine + "\n";
            }
            playerlines.Reverse();
            return true;
        }
    }
}
