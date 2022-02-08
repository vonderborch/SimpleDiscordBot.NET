/// <file>
/// SimpleDiscordBot\Bot.cs
/// </file>
///
/// <copyright file="Bot.cs" company="">
/// Copyright (c) 2022 Christian Webber. All rights reserved.
/// </copyright>
///
/// <summary>
/// Implements the bot class.
/// </summary>
using Discord;
using Discord.WebSocket;
using SimpleDiscordBot.Commands;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Velentr.Logging.FileLogging;
using Velentr.Logging.Loggers;
using Velentr.Miscellaneous.CommandParsing;

namespace SimpleDiscordBot
{
    /// <summary>
    /// A bot.
    /// </summary>
    ///
    /// <seealso cref="IDisposable"/>
    public class Bot : IDisposable
    {
        /// <summary>
        /// (Immutable) the parser.
        /// </summary>
        private readonly CommandParser _parser;

        /// <summary>
        /// (Immutable) the token.
        /// </summary>
        private readonly string _token;

        /// <summary>
        /// The client.
        /// </summary>
        private DiscordSocketClient _client;

        /// <summary>
        /// (Immutable) identifier for the bottom.
        /// </summary>
        private readonly ulong _botId;

        /// <summary>
        /// (Immutable) true to ignore own messages.
        /// </summary>
        private readonly bool _ignoreOwnMessages;

        /// <summary>
        /// Constructor.
        /// </summary>
        ///
        /// <param name="configuration">    The configuration. </param>
        public Bot(BotConfiguration configuration)
        {
            BotName = configuration.BotName;
            BotDescription = configuration.BotDescription;
            BotSupportLink = configuration.BotSupportLink;
            _botId = configuration.BotUserId;
            _token = configuration.Token;
            CommandPrefix = configuration.CommandPrefix;
            _ignoreOwnMessages = configuration.IgnoreOwnMessages;
            Log = new AllLogger(BotName, new ConsoleLogger(BotName), new FileLogger(BotName, new FileLoggerSettings($"{BotName}.txt", RollingType.FileSize, maxFileSizeBytes: 256000000, appendIfFileExists: true)));

            // register the parser and any built-in commands specified...
            _parser = new CommandParser(configuration.CommandPrefix, false, false, false, null);

            if (configuration.AddBotHelpCommand.AddCommand)
            {
                RegisterHelpCommand(new BotHelp(configuration.AddBotHelpCommand.DefaultTts), configuration.AddBotHelpCommand.Aliases);
            }

            var botInfoText = new StringBuilder();
            botInfoText.AppendLine($"Bot Name: {BotName}");
            botInfoText.AppendLine($"Bot Version: {BotVersion}");
            botInfoText.AppendLine($"Bot Support Link: {BotSupportLink}");
            botInfoText.AppendLine("");
            botInfoText.AppendLine($"Bot Description: {BotDescription}");
            botInfoText.AppendLine("");
            botInfoText.AppendLine($"_Built using SimpleDiscordBot (v{BotFrameworkVersion}) and Discord.Net (v{BotDiscordNetVersion})_");

            var simpleBuiltIns = new List<(string, string, string, BuiltInCommandOptions)>
            {
                ("bot_info", "Displays some basic info about the Bot", botInfoText.ToString(), configuration.AddBotInfoCommand),
                ("bot_version", "Displays the current Bot Version", $"Bot Version: {BotVersion}", configuration.AddBotVersionCommand),
                ("report_issue", "Provides a link to report an issue", $"Please report an issue using the following link: {BotSupportLink}", configuration.AddBotReportIssueCommand)
            };

            for (var i = 0; i < simpleBuiltIns.Count; i++)
            {
                RegisterCommand(new SimpleTextCommand(simpleBuiltIns[i].Item1, simpleBuiltIns[i].Item2, simpleBuiltIns[i].Item3, simpleBuiltIns[i].Item4.IsHidden, 2, simpleBuiltIns[i].Item4.DefaultTts), simpleBuiltIns[i].Item4.Aliases);
            }
        }

        /// <summary>
        /// Gets the name of the bottom.
        /// </summary>
        ///
        /// <value>
        /// The name of the bottom.
        /// </value>
        public string BotName { get; }

        /// <summary>
        /// Gets information describing the bottom.
        /// </summary>
        ///
        /// <value>
        /// Information describing the bottom.
        /// </value>
        public string BotDescription { get; }

        /// <summary>
        /// Gets the bottom support link.
        /// </summary>
        ///
        /// <value>
        /// The bottom support link.
        /// </value>
        public string BotSupportLink { get; }

        /// <summary>
        /// Gets the bot version.
        /// </summary>
        ///
        /// <value>
        /// The bottom version.
        /// </value>
        public string BotVersion => Assembly.GetExecutingAssembly().GetName().Version.ToString();

        /// <summary>
        /// Gets the bot framework version.
        /// </summary>
        ///
        /// <value>
        /// The bottom version.
        /// </value>
        public string BotFrameworkVersion => Assembly.GetAssembly(typeof(Bot)).GetName().Version.ToString();

        /// <summary>
        /// Gets the bot framework version.
        /// </summary>
        ///
        /// <value>
        /// The bottom version.
        /// </value>
        public string BotDiscordNetVersion => Assembly.GetAssembly(typeof(SocketMessage)).GetName().Version.ToString();

        /// <summary>
        /// Gets the command prefix.
        /// </summary>
        ///
        /// <value>
        /// The command prefix.
        /// </value>
        public string CommandPrefix { get; }

        /// <summary>
        /// Executes the 'bottom' operation.
        /// </summary>
        ///
        /// <returns>
        /// A Task.
        /// </returns>
        public async Task RunBot()
        {
            _client = new DiscordSocketClient();

            _client.Log += DiscordLogger;
            _client.MessageReceived += ClientOnMessageReceived;

            Log.Info($"BotName: {BotName}, v{BotVersion} (SimpleDiscordBot v{BotFrameworkVersion}, Discord.Net v{BotDiscordNetVersion})");
            await _client.LoginAsync(TokenType.Bot, _token);
            await _client.StartAsync();

            await Task.Delay(-1);
        }

        /// <summary>
        /// Client on message received.
        /// </summary>
        ///
        /// <param name="arg">  The argument. </param>
        ///
        /// <returns>
        /// A Task.
        /// </returns>
        private Task ClientOnMessageReceived(SocketMessage arg)
        {
            // ignore my own messages if configured to do so :)
            if (_ignoreOwnMessages && _botId == arg.Author.Id)
            {
                return Task.CompletedTask;
            }

            // build the arguments object
            var message = new DiscordMessageInfo(arg);

            // parse the message
            Log.Info($"Parsing message: {message.Text}");
            var result = _parser.ParseCommand(message.Text);
            if (result != null && result.Command != null)
            {
                var args = new Dictionary<string, object>()
                {
                    {"MessageInfo", message},
                };
                var executionResult = result.Command.ExecuteCommand(result.Parameters, args);

                if (executionResult)
                {
                    Log.Info($"Succeeded in executing command [{result.Command.CommandName}]!");
                }
                else
                {
                    var helpCommand = _parser.ParseCommand($"{CommandPrefix}help {result.Command.CommandName}");
                    helpCommand.Command.ExecuteCommand(helpCommand.Parameters, args);
                    Log.Error($"Failed executing command [{result.Command.CommandName}]!");
                }
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Discord logger.
        /// </summary>
        ///
        /// <param name="message">  The message. </param>
        ///
        /// <returns>
        /// A Task.
        /// </returns>
        private Task DiscordLogger(LogMessage message)
        {
            Console.WriteLine(message.ToString());
            return Task.CompletedTask;
        }

        /// <summary>
        /// Gets the log.
        /// </summary>
        ///
        /// <value>
        /// The log.
        /// </value>
        public Logger Log { get; }

        /// <summary>
        /// Registers the command described by commands.
        /// </summary>
        ///
        /// <param name="command">  The command. </param>
        /// <param name="aliases">  (Optional) The aliases. </param>
        public void RegisterCommand(Type command, List<string> aliases = null)
        {
            var instance = (AbstractBotCommand)Activator.CreateInstance(command);
            RegisterCommand(instance, aliases);
        }

        /// <summary>
        /// Registers the help command.
        /// </summary>
        ///
        /// <param name="command">  The command. </param>
        /// <param name="aliases">  (Optional) The aliases. </param>
        public void RegisterHelpCommand(BotHelp command, List<string> aliases = null)
        {
            command.DiscordBot = this;
            _parser.RegisterHelpCommand(command, aliases);
        }

        /// <summary>
        /// Registers the command described by commands.
        /// </summary>
        ///
        /// <param name="command">  The command. </param>
        /// <param name="aliases">  (Optional) The aliases. </param>
        public void RegisterCommand(AbstractBotCommand command, List<string> aliases = null)
        {
            command.DiscordBot = this;
            _parser.RegisterCommand(command, aliases);
        }

        /// <summary>
        /// Registers the command described by commands.
        /// </summary>
        ///
        /// <param name="command">  The command. </param>
        public void RegisterCommand(CommandRegistrationItem command)
        {
            RegisterCommand(command.Command, command.Aliases);
        }

        /// <summary>
        /// Registers the command described by commands.
        /// </summary>
        ///
        /// <param name="commands"> The commands. </param>
        public void RegisterCommand(List<CommandRegistrationItem> commands)
        {
            for (var i = 0; i < commands.Count; i++)
            {
                RegisterCommand(commands[i]);
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged
        /// resources.
        /// </summary>
        ///
        /// <seealso cref="IDisposable.Dispose()"/>
        public void Dispose()
        {
            if (_client != null)
            {
                uint totalSleepTime = 0;
                while (_client.LoginState == LoginState.LoggingIn)
                {
                    Thread.Sleep(250);

                    totalSleepTime += 250;
                    if (totalSleepTime > 300000)
                    {
                        break;
                    }
                }

                if (_client.LoginState == LoginState.LoggedIn)
                {
                    _client.LogoutAsync().Wait(300000);
                }

                _client.Dispose();
            }
        }
    }
}