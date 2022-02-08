/// <file>
/// SimpleDiscordBot\MessageInfo.cs
/// </file>
///
/// <copyright file="MessageInfo.cs" company="">
/// Copyright (c) 2022 Christian Webber. All rights reserved.
/// </copyright>
///
/// <summary>
/// Implements the message information class.
/// </summary>
using System.Collections.Generic;
using Velentr.Miscellaneous.CommandParsing;

namespace SimpleDiscordBot.Commands
{
    /// <summary>
    /// Information about the message.
    /// </summary>
    public class MessageInfo
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        ///
        /// <param name="commandName">  Name of the command. </param>
        /// <param name="messageInfo">  Information describing the message. </param>
        /// <param name="parameters">   Options for controlling the operation. </param>
        /// <param name="defaultTts">   True to default tts. </param>
        public MessageInfo(string commandName, DiscordMessageInfo messageInfo, Dictionary<string, IParameter> parameters, bool defaultTts)
        {
            CommandName = commandName;
            DiscordMessageInfo = messageInfo;
            CommandParameters = parameters;
            DefaultTts = defaultTts;
            PrintAsTextFile = parameters["output_as_text_file"].Value<bool>();
        }

        /// <summary>
        /// Name of the command.
        /// </summary>
        public string CommandName;

        /// <summary>
        /// Information describing the discord message.
        /// </summary>
        public DiscordMessageInfo DiscordMessageInfo;

        /// <summary>
        /// Options for controlling the command.
        /// </summary>
        public Dictionary<string, IParameter> CommandParameters;

        /// <summary>
        /// True to default tts.
        /// </summary>
        public bool DefaultTts;

        /// <summary>
        /// True to print as text file.
        /// </summary>
        public bool PrintAsTextFile;
    }
}