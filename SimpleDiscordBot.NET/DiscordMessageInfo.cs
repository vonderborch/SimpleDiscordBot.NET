/// <file>
/// SimpleDiscordBot\DiscordMessageInfo.cs
/// </file>
///
/// <copyright file="DiscordMessageInfo.cs" company="">
/// Copyright (c) 2022 Christian Webber. All rights reserved.
/// </copyright>
///
/// <summary>
/// Implements the discord message information class.
/// </summary>
using Discord;
using Discord.WebSocket;

namespace SimpleDiscordBot.Commands
{
    /// <summary>
    /// Information about the discord message.
    /// </summary>
    public class DiscordMessageInfo
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        ///
        /// <param name="messageArgs">  The message arguments. </param>
        public DiscordMessageInfo(SocketMessage messageArgs)
        {
            Text = messageArgs.Content;
            SocketMessage = messageArgs;
            Server = ((SocketGuildChannel)messageArgs.Channel).Guild;
            Channel = (SocketGuildChannel)messageArgs.Channel;
            Author = Server.GetUser(messageArgs.Author.Id);
            AuthorChannelPermissions = Author.GetPermissions(Channel);
            AuthorServerPermissions = Author.GuildPermissions;
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        ///
        /// <value>
        /// The text.
        /// </value>
        public string Text { get; internal set; }

        /// <summary>
        /// Message describing the socket.
        /// </summary>
        public SocketMessage SocketMessage;

        /// <summary>
        /// The server.
        /// </summary>
        public SocketGuild Server;

        /// <summary>
        /// The channel.
        /// </summary>
        public SocketGuildChannel Channel;

        /// <summary>
        /// The author.
        /// </summary>
        public SocketGuildUser Author;

        /// <summary>
        /// The author channel permissions.
        /// </summary>
        public ChannelPermissions AuthorChannelPermissions;

        /// <summary>
        /// The author server permissions.
        /// </summary>
        public GuildPermissions AuthorServerPermissions;
    }
}