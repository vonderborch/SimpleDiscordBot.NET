/// <file>
/// SimpleDiscordBot\CommandRegistrationItem.cs
/// </file>
///
/// <copyright file="CommandRegistrationItem.cs" company="">
/// Copyright (c) 2022 Christian Webber. All rights reserved.
/// </copyright>
///
/// <summary>
/// Implements the command registration item class.
/// </summary>
using SimpleDiscordBot.Commands;
using System;
using System.Collections.Generic;

namespace SimpleDiscordBot
{
    /// <summary>
    /// A command registration item.
    /// </summary>
    public struct CommandRegistrationItem
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        ///
        /// <param name="command">  The command. </param>
        /// <param name="aliases">  (Optional)
        ///                         The aliases. </param>
        public CommandRegistrationItem(Type command, List<string> aliases = null)
        {
            Command = (AbstractBotCommand)Activator.CreateInstance(command);
            Aliases = aliases ?? new List<string>();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        ///
        /// <param name="command">  The command. </param>
        /// <param name="aliases">  (Optional)
        ///                         The aliases. </param>
        public CommandRegistrationItem(AbstractBotCommand command, List<string> aliases = null)
        {
            Command = command;
            Aliases = aliases ?? new List<string>();
        }

        /// <summary>
        /// The command.
        /// </summary>
        public AbstractBotCommand Command;

        /// <summary>
        /// The aliases.
        /// </summary>
        public List<string> Aliases;
    }
}