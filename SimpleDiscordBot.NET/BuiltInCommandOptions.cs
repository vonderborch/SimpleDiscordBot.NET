/// <file>
/// SimpleDiscordBot\BuiltInCommandOptions.cs
/// </file>
///
/// <copyright file="BuiltInCommandOptions.cs" company="">
/// Copyright (c) 2022 Christian Webber. All rights reserved.
/// </copyright>
///
/// <summary>
/// Implements the built in command options class.
/// </summary>
using System.Collections.Generic;

namespace SimpleDiscordBot
{
    /// <summary>
    /// A built in command options.
    /// </summary>
    public class BuiltInCommandOptions
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        ///
        /// <param name="addCommand">   True if add command, false if not. </param>
        /// <param name="isHidden">     True if this object is hidden, false if not. </param>
        /// <param name="defaultTts">   True if default tts, false if not. </param>
        /// <param name="aliases">      (Optional)
        ///                             The aliases. </param>
        public BuiltInCommandOptions(bool addCommand, bool isHidden, bool defaultTts, List<string> aliases = null)
        {
            AddCommand = addCommand;
            IsHidden = isHidden;
            DefaultTts = defaultTts;
            Aliases = aliases;
        }

        /// <summary>
        /// Gets a value indicating whether the add command.
        /// </summary>
        ///
        /// <value>
        /// True if add command, false if not.
        /// </value>
        public bool AddCommand { get; }

        /// <summary>
        /// Gets a value indicating whether this object is hidden.
        /// </summary>
        ///
        /// <value>
        /// True if this object is hidden, false if not.
        /// </value>
        public bool IsHidden { get; }

        /// <summary>
        /// Gets a value indicating whether the default tts.
        /// </summary>
        ///
        /// <value>
        /// True if default tts, false if not.
        /// </value>
        public bool DefaultTts { get; }

        /// <summary>
        /// Gets the aliases.
        /// </summary>
        ///
        /// <value>
        /// The aliases.
        /// </value>
        public List<string> Aliases { get; }
    }
}