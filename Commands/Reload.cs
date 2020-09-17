﻿using System;
using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using Microsoft.SqlServer.Server;
using RemoteAdmin;

namespace Subclass.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    class Reload : ICommand
    {
        public string Command { get; } = "reloadsubclasses";

        public string[] Aliases { get; } = { "rsc" };

        public string Description { get; } = "Reloads all subclasses, takes effect the next round.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (sender is PlayerCommandSender player)
            {
                Player p = Player.Get(player.SenderId);
                if (!p.CheckPermission("sc.reload"))
                {
                    response = "You do not have the necessary permissions to run this command. Requires: sc.reload";
                    return false;
                }

                response = "Reloaded";
                Subclass.Instance.Classes.Clear();
                if(Subclass.Instance.ClassesAdditive != null) Subclass.Instance.ClassesAdditive.Clear();

                Subclass.Instance.Classes = Subclass.Instance.GetClasses();

                return true;
                
            }
            response = "";
            return false;
        }
    }
}
