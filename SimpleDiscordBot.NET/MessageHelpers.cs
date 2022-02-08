/// <file>
/// SimpleDiscordBot\MessageHelpers.cs
/// </file>
///
/// <copyright file="MessageHelpers.cs" company="">
/// Copyright (c) 2022 Christian Webber. All rights reserved.
/// </copyright>
///
/// <summary>
/// Implements the message helpers class.
/// </summary>
using SimpleDiscordBot.Commands;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using Velentr.Miscellaneous.StringHelpers;

namespace SimpleDiscordBot
{
    /// <summary>
    /// A message helpers.
    /// </summary>
    public static class MessageHelpers
    {
        /// <summary>
        /// The maximum length of the message.
        /// </summary>
        public static int MaxMessageLength = 2000;

        /// <summary>
        /// The maximum length of the code formatted message.
        /// </summary>
        public static int MaxCodeFormattedMessageLength = MaxMessageLength - 8;

        /// <summary>
        /// Sends a message.
        /// </summary>
        ///
        /// <param name="message">          The message. </param>
        /// <param name="messageInfo">      Information describing the message. </param>
        /// <param name="codeFormatted">    (Optional) True if code formatted. </param>
        /// <param name="tts">              (Optional) True to tts. </param>
        public static void SendMessage(string message, MessageInfo messageInfo, bool codeFormatted = false, bool? tts = null)
        {
            var actualTts = tts ?? messageInfo.DefaultTts;

            var maxLength = codeFormatted ? MaxCodeFormattedMessageLength : MaxMessageLength;
            if (message.Length <= maxLength)
            {
                if (codeFormatted)
                {
                    message = GetCodeFormattedMessage(message);
                }

                InternalSendMessage(message, messageInfo, actualTts);
            }
            else if (messageInfo.PrintAsTextFile)
            {
                InternalSendMessageAsFile(message, messageInfo, actualTts);
            }
            else
            {
                var messages = SplitMessageByMaxSize(message, maxLength);
                for (var i = 0; i < messages.Count; i++)
                {
                    var m = messages[i];
                    if (codeFormatted)
                    {
                        m = GetCodeFormattedMessage(m);
                    }

                    InternalSendMessage(m, messageInfo, actualTts);
                    Thread.Sleep(250);
                }
            }
        }

        /// <summary>
        /// Splits message by maximum size.
        /// </summary>
        ///
        /// <param name="message">  The message. </param>
        /// <param name="maxSize">  (Optional) The maximum size of the. </param>
        ///
        /// <returns>
        /// A List&lt;string&gt;
        /// </returns>
        public static List<string> SplitMessageByMaxSize(string message, int maxSize = 2000)
        {
            // if we're under the max message size, return the message as-is
            if (message.Length < maxSize)
            {
                return new List<string> { message };
            }

            // otherwise, lets split it. Let's first try to split by new lines...
            var lines = StringSplitters.SplitStringByNewLines(message);
            var output = new List<string>();
            var currentMessage = new StringBuilder();
            for (var i = 0; i < lines.Count; i++)
            {
                // try to add it to the current message...
                if (currentMessage.Length + lines[i].Length < maxSize)
                {
                    currentMessage.AppendLine(lines[i]);
                }
                else
                {
                    // if it is too big for the current message (and the current message has something on it), clear the current message and try to add it to a new message
                    if (currentMessage.Length > 0)
                    {
                        output.Add(currentMessage.ToString());
                        currentMessage.Clear();
                    }

                    if (lines[i].Length < maxSize)
                    {
                        currentMessage.AppendLine(lines[i]);
                    }
                    else
                    {
                        // if it is too big for any single message, split it into multiple...
                        var chunks = StringSplitters.SplitStringByChunkSize(lines[i], maxSize);
                        for (var j = 0; j < chunks.Count; j++)
                        {
                            output.Add(chunks[j]);
                        }
                    }
                }
            }

            if (currentMessage.Length > 0)
            {
                output.Add(currentMessage.ToString());
            }

            return output;
        }

        /// <summary>
        /// Gets code formatted message.
        /// </summary>
        ///
        /// <param name="message">  The message. </param>
        ///
        /// <returns>
        /// The code formatted message.
        /// </returns>
        private static string GetCodeFormattedMessage(string message)
        {
            var actualMessage = new StringBuilder();
            actualMessage.AppendLine("```");
            actualMessage.AppendLine(message);
            actualMessage.AppendLine("```");

            return actualMessage.ToString();
        }

        /// <summary>
        /// Internal send message.
        /// </summary>
        ///
        /// <param name="message">      The message. </param>
        /// <param name="messageInfo">  Information describing the message. </param>
        /// <param name="tts">          True to tts. </param>
        private static void InternalSendMessage(string message, MessageInfo messageInfo, bool tts)
        {
            messageInfo.DiscordMessageInfo.SocketMessage.Channel.SendMessageAsync(message, tts);
        }

        /// <summary>
        /// Internal send message as file.
        /// </summary>
        ///
        /// <param name="message">      The message. </param>
        /// <param name="messageInfo">  Information describing the message. </param>
        /// <param name="tts">          True to tts. </param>
        private static void InternalSendMessageAsFile(string message, MessageInfo messageInfo, bool tts)
        {
            using var stream = new MemoryStream(Encoding.ASCII.GetBytes(message));
            messageInfo.DiscordMessageInfo.SocketMessage.Channel.SendFileAsync(stream, $"{messageInfo.CommandName}.txt", $"[{messageInfo.CommandName}] Results:");
        }
    }
}