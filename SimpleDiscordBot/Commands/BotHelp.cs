/// <file>
/// SimpleDiscordBot\Commands\BotHelp.cs
/// </file>
///
/// <copyright file="BotHelp.cs" company="">
/// Copyright (c) 2022 Christian Webber. All rights reserved.
/// </copyright>
///
/// <summary>
/// Implements the bottom help class.
/// </summary>
using System.Collections.Generic;
using System.Text;
using Velentr.Miscellaneous.CommandParsing;

namespace SimpleDiscordBot.Commands
{
    /// <summary>
    /// A bottom help.
    /// </summary>
    ///
    /// <seealso cref="DefaultHelpCommand"/>
    public class BotHelp : DefaultHelpCommand
    {
        /// <summary>
        /// True to tts.
        /// </summary>
        private bool _tts;

        /// <summary>
        /// Constructor.
        /// </summary>
        ///
        /// <param name="tts">  (Optional) True to tts. </param>
        public BotHelp(bool tts = false) : base()
        {
            _tts = tts;
            AddArgument("output_as_text_file", "Whether to print the whole message in a single message by attaching the results as a text file or not. Defaults to true if the commands response exceeds the max message length.", typeof(bool), true, false, true);
            AddArgument("tts", "Whether to use TTS to speak the message. Defaults to false.", typeof(bool), false, false, true);
        }

        /// <summary>
        /// Gets or sets the discord bottom.
        /// </summary>
        ///
        /// <value>
        /// The discord bottom.
        /// </value>
        public Bot DiscordBot { get; internal set; }

        /// <summary>
        /// Executes the 'pre' command.
        /// </summary>
        ///
        /// <param name="str">          The string. </param>
        /// <param name="parameters">   Options for controlling the operation. </param>
        /// <param name="args">         The arguments. </param>
        ///
        /// <returns>
        /// A StringBuilder.
        /// </returns>
        public override StringBuilder ExecutePreCommand(StringBuilder str, Dictionary<string, IParameter> parameters, Dictionary<string, object> args)
        {
            var discordMessageInfo = (DiscordMessageInfo)args["MessageInfo"];

            DiscordBot.Log.Info($"Executing command [{Name}] for user [{discordMessageInfo.Author.Username}] on server [{discordMessageInfo.Server.Name}]");

            return str;
        }

        /// <summary>
        /// Executes the 'post' command.
        /// </summary>
        ///
        /// <param name="str">          The string. </param>
        /// <param name="parameters">   Options for controlling the operation. </param>
        /// <param name="args">         The arguments. </param>
        public override void ExecutePostCommand(StringBuilder str, Dictionary<string, IParameter> parameters, Dictionary<string, object> args)
        {
            var discordMessageInfo = (DiscordMessageInfo)args["MessageInfo"];
            var messageInfo = new MessageInfo(CommandName, discordMessageInfo, parameters, _tts);

            MessageHelpers.SendMessage(str.ToString(), messageInfo, true, _tts);

            var commandToExecuteOn = (parameters["command"].RawValue).ToLowerInvariant();
            var failureCase = GetParameterValueIfExistsAsString("failure_case", parameters);
            if (string.IsNullOrWhiteSpace(commandToExecuteOn) && string.IsNullOrWhiteSpace(failureCase))
            {
                MessageHelpers.SendMessage($"To get more help about a specific method, use the following command: `{DiscordBot.CommandPrefix}_help <methodname>`", messageInfo, true, _tts);
            }
        }
    }
}