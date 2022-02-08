/// <file>
/// SimpleDiscordBot\Commands\AbstractBotCommand.cs
/// </file>
///
/// <copyright file="AbstractBotCommand.cs" company="">
/// Copyright (c) 2022 Christian Webber. All rights reserved.
/// </copyright>
///
/// <summary>
/// Implements the abstract bottom command class.
/// </summary>
using System;
using System.Collections.Generic;
using System.Text;
using Velentr.Miscellaneous.CommandParsing;

namespace SimpleDiscordBot.Commands
{
    /// <summary>
    /// An abstract bottom command.
    /// </summary>
    ///
    /// <seealso cref="Velentr.Miscellaneous.CommandParsing.AbstractCommand"/>
    /// <seealso cref="AbstractCommand"/>
    public abstract class AbstractBotCommand : AbstractCommand
    {
        /// <summary>
        /// True to tts.
        /// </summary>
        private bool _tts;

        /// <summary>
        /// Specialized constructor for use only by derived class.
        /// </summary>
        ///
        /// <param name="name">         The name. </param>
        /// <param name="description">  The description. </param>
        /// <param name="isHidden">     (Optional) True if is hidden, false if not. </param>
        /// <param name="numArguments"> (Optional) Number of arguments. </param>
        /// <param name="tts">          (Optional) The tts. </param>
        protected AbstractBotCommand(string name, string description, bool isHidden = false, int numArguments = 2, bool tts = false) : base(name, description, isHidden, numArguments, false)
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
        /// Executes the 'command' operation.
        /// </summary>
        ///
        /// <param name="parameters">   Options for controlling the operation. </param>
        /// <param name="args">         The arguments. </param>
        ///
        /// <returns>
        /// True if it succeeds, false if it fails.
        /// </returns>
        public override bool ExecuteCommand(Dictionary<string, IParameter> parameters, Dictionary<string, object> args)
        {
            var discordMessageInfo = (DiscordMessageInfo)args["MessageInfo"];
            var parameterMessages = new StringBuilder();
            foreach (var parameter in parameters)
            {
                parameterMessages.Append($"({parameter.Key},{parameter.Value.RawValue}), ");
            }
            parameterMessages.Length -= 2;
            var messageInfo = new MessageInfo(CommandName, discordMessageInfo, parameters, _tts);

            DiscordBot.Log.Info($"Executing command [{CommandName}] for user [{discordMessageInfo.Author.Username}] on server [{discordMessageInfo.Server.Name}] with parameters: {parameterMessages}");
            return ExecuteInternal(parameters, messageInfo);
        }

        /// <summary>
        /// Executes the 'internal' operation.
        /// </summary>
        ///
        /// <param name="parameters">   Options for controlling the operation. </param>
        /// <param name="messageInfo">  Information describing the message. </param>
        ///
        /// <returns>
        /// True if it succeeds, false if it fails.
        /// </returns>
        public abstract bool ExecuteInternal(Dictionary<string, IParameter> parameters, MessageInfo messageInfo);

        /// <summary>
        /// Sends a message.
        /// </summary>
        ///
        /// <param name="message">          The message. </param>
        /// <param name="messageInfo">      Information describing the message. </param>
        /// <param name="codeFormatted">    (Optional) True if code formatted. </param>
        /// <param name="tts">              (Optional) The tts. </param>
        public void SendMessage(string message, MessageInfo messageInfo, bool codeFormatted = false, bool? tts = null)
        {
            MessageHelpers.SendMessage(message, messageInfo, codeFormatted, tts);
        }

        /// <summary>
        /// Sends a message.
        /// </summary>
        ///
        /// <param name="message">          The message. </param>
        /// <param name="messageInfo">      Information describing the message. </param>
        /// <param name="codeFormatted">    (Optional) True if code formatted. </param>
        /// <param name="tts">              (Optional) The tts. </param>
        public void SendMessage(List<string> message, MessageInfo messageInfo, bool codeFormatted = false, bool? tts = null)
        {
            var str = string.Join(Environment.NewLine, message);
            MessageHelpers.SendMessage(str, messageInfo, codeFormatted, tts);
        }

        /// <summary>
        /// Sends a message.
        /// </summary>
        ///
        /// <param name="message">          The message. </param>
        /// <param name="messageInfo">      Information describing the message. </param>
        /// <param name="codeFormatted">    (Optional) True if code formatted. </param>
        /// <param name="tts">              (Optional) The tts. </param>
        public void SendMessage(StringBuilder message, MessageInfo messageInfo, bool codeFormatted = false, bool? tts = null)
        {
            MessageHelpers.SendMessage(message.ToString(), messageInfo, codeFormatted, tts);
        }
    }
}