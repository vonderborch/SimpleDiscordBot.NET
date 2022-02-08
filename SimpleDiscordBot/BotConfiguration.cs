/// <file>
/// SimpleDiscordBot\BotConfiguration.cs
/// </file>
///
/// <copyright file="Bot.cs" company="">
/// Copyright (c) 2022 Christian Webber. All rights reserved.
/// </copyright>
///
/// <summary>
/// Implements the bot configuration class.
/// </summary>
using System.Collections.Generic;

namespace SimpleDiscordBot
{
    public struct BotConfiguration
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        ///
        /// <param name="botName">                  Name of the bottom. </param>
        /// <param name="botDescription">           Information describing the bottom. </param>
        /// <param name="botSupportLink">           The bottom support link. </param>
        /// <param name="botUserId">                Identifier for the bottom user. </param>
        /// <param name="token">                    The token. </param>
        /// <param name="commandPrefix">            (Optional) The command prefix. </param>
        /// <param name="addBotHelpCommand">        (Optional) The add bottom help command. </param>
        /// <param name="addBotInfoCommand">        (Optional) The add bottom information command. </param>
        /// <param name="addBotVersionCommand">     (Optional) The add bottom version command. </param>
        /// <param name="addBotReportIssueCommand"> (Optional) The add bottom report issue command. </param>
        /// <param name="ignoreOwnMessages">        (Optional) True to ignore own messages. </param>
        public BotConfiguration(string botName, string botDescription, string botSupportLink, ulong botUserId, string token, string commandPrefix = "!", BuiltInCommandOptions addBotHelpCommand = null, BuiltInCommandOptions addBotInfoCommand = null, BuiltInCommandOptions addBotVersionCommand = null, BuiltInCommandOptions addBotReportIssueCommand = null, bool ignoreOwnMessages = true)
        {
            BotName = botName;
            BotDescription = botDescription;
            BotSupportLink = botSupportLink;
            BotUserId = botUserId;
            Token = token;
            CommandPrefix = commandPrefix;
            IgnoreOwnMessages = ignoreOwnMessages;

            AddBotHelpCommand = addBotHelpCommand ?? new BuiltInCommandOptions(true, false, false, new List<string>() { CommandPrefix });

            AddBotInfoCommand = addBotInfoCommand ?? new BuiltInCommandOptions(true, true, false);
            AddBotVersionCommand = addBotVersionCommand ?? new BuiltInCommandOptions(true, true, false);
            AddBotReportIssueCommand = addBotReportIssueCommand ?? new BuiltInCommandOptions(true, true, false);
        }

        public string BotName { get; set; }

        public string BotDescription { get; set; }

        public string BotSupportLink { get; set; }

        public ulong BotUserId { get; set; }

        public string Token { get; set; }

        public string CommandPrefix { get; set; }

        public BuiltInCommandOptions AddBotHelpCommand { get; set; }

        public BuiltInCommandOptions AddBotInfoCommand { get; set; }

        public BuiltInCommandOptions AddBotVersionCommand { get; set; }

        public BuiltInCommandOptions AddBotReportIssueCommand { get; set; }

        public bool IgnoreOwnMessages { get; set; }
    }
}