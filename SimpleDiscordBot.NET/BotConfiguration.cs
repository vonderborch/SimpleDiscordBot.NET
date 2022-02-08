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
    /// <summary>
    /// A bottom configuration.
    /// </summary>
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

        /// <summary>
        /// Gets or sets the name of the bottom.
        /// </summary>
        ///
        /// <value>
        /// The name of the bottom.
        /// </value>
        public string BotName { get; set; }

        /// <summary>
        /// Gets or sets information describing the bottom.
        /// </summary>
        ///
        /// <value>
        /// Information describing the bottom.
        /// </value>
        public string BotDescription { get; set; }

        /// <summary>
        /// Gets or sets the bottom support link.
        /// </summary>
        ///
        /// <value>
        /// The bottom support link.
        /// </value>
        public string BotSupportLink { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the bottom user.
        /// </summary>
        ///
        /// <value>
        /// The identifier of the bottom user.
        /// </value>
        public ulong BotUserId { get; set; }

        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        ///
        /// <value>
        /// The token.
        /// </value>
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the command prefix.
        /// </summary>
        ///
        /// <value>
        /// The command prefix.
        /// </value>
        public string CommandPrefix { get; set; }

        /// <summary>
        /// Gets or sets the 'add bottom help' command.
        /// </summary>
        ///
        /// <value>
        /// The 'add bottom help' command.
        /// </value>
        public BuiltInCommandOptions AddBotHelpCommand { get; set; }

        /// <summary>
        /// Gets or sets the 'add bottom information' command.
        /// </summary>
        ///
        /// <value>
        /// The 'add bottom information' command.
        /// </value>
        public BuiltInCommandOptions AddBotInfoCommand { get; set; }

        /// <summary>
        /// Gets or sets the 'add bottom version' command.
        /// </summary>
        ///
        /// <value>
        /// The 'add bottom version' command.
        /// </value>
        public BuiltInCommandOptions AddBotVersionCommand { get; set; }

        /// <summary>
        /// Gets or sets the 'add bottom report issue' command.
        /// </summary>
        ///
        /// <value>
        /// The 'add bottom report issue' command.
        /// </value>
        public BuiltInCommandOptions AddBotReportIssueCommand { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the ignore own messages.
        /// </summary>
        ///
        /// <value>
        /// True if ignore own messages, false if not.
        /// </value>
        public bool IgnoreOwnMessages { get; set; }
    }
}