/// <file>
/// SimpleDiscordBot\Commands\AbstractSimpleTextCommand.cs
/// </file>
///
/// <copyright file="AbstractSimpleTextCommand.cs" company="">
/// Copyright (c) 2022 Christian Webber. All rights reserved.
/// </copyright>
///
/// <summary>
/// Implements the abstract simple text command class.
/// </summary>
using System.Collections.Generic;
using Velentr.Miscellaneous.CommandParsing;

namespace SimpleDiscordBot.Commands
{
    /// <summary>
    /// A simple text command.
    /// </summary>
    ///
    /// <seealso cref="SimpleDiscordBot.Commands.AbstractBotCommand"/>
    /// <seealso cref="AbstractBotCommand"/>
    public class SimpleTextCommand : AbstractBotCommand
    {
        /// <summary>
        /// The text to output.
        /// </summary>
        protected string _textToOutput;

        /// <summary>
        /// Constructor.
        /// </summary>
        ///
        /// <param name="name">         The name. </param>
        /// <param name="description">  The description. </param>
        /// <param name="textToOutput"> The text to output. </param>
        /// <param name="isHidden">     (Optional) True if is hidden, false if not. </param>
        /// <param name="numArguments"> (Optional) Number of arguments. </param>
        /// <param name="tts">          (Optional) True to tts. </param>
        public SimpleTextCommand(string name, string description, string textToOutput, bool isHidden = false, int numArguments = 2, bool tts = false) : base(name, description, isHidden, numArguments, tts)
        {
            _textToOutput = textToOutput;
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
        ///
        /// <seealso cref="SimpleDiscordBot.Commands.AbstractBotCommand.ExecuteInternal(Dictionary{string,IParameter},MessageInfo)"/>
        public override bool ExecuteInternal(Dictionary<string, IParameter> parameters, MessageInfo messageInfo)
        {
            SendMessage(_textToOutput, messageInfo, false, false);
            return true;
        }
    }
}