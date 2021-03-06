﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Braces.Core.Models;
using System.Threading.Tasks;
using IniParser;
using IniParser.Parser;
using IniParser.Model;
using IniParser.Model.Configuration;

// TODO: Pass this to the ConfigurationSystem.
// TODO: Consider changing this public configuration file to JSON for more organization. Use the ini for the system config only.
namespace Braces.Core
{
    public class UserConfig
    {
        public UserConfig()
        {
        }

        public static readonly string INI_PATH = Path.Combine( FileStorage.GetHOMEPATH(), "_braces" );

        public IniData Config { get; private set; }

        #region PUBLIC METHODS

        /// <summary>
        /// 
        /// Init the UserConfiguration. It parses the user configuration file, or creates a new default one, and stores the data in the userConfig.Config instance.
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task InitAsync()
        {
            if (!File.Exists(INI_PATH))
                await this.InitIniFile();
            else
                await this.ParseIniFileAsync();
        }

        #endregion

        #region PRIVATE METHOD

        private async Task InitIniFile()
        {
            IniData defaultIni = GetDefaultWindowsConfiguration();

            FileIniDataParser parser = new FileIniDataParser( GetIniParserConfiguration() );
            parser.WriteFile( INI_PATH, defaultIni );

            this.Config = defaultIni;
        }

        private async Task ParseIniFileAsync()
        {
            try
            {
                byte[] userConfigBuffer = await FileStorage.ReadFileAsync(INI_PATH);
                // http://www.fileformat.info/info/unicode/char/feff/index.htm
                // Replace the the bytes Unicode Character 'ZERO WIDTH NO-BREAK SPACE' with 'SPACE's.
                userConfigBuffer[0] = 32;
                userConfigBuffer[1] = 32;
                userConfigBuffer[2] = 32;

                IniData userIni = GetIniParserConfiguration().Parse( Encoding.UTF8.GetString( userConfigBuffer ) );
                this.Config = userIni;
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error has occured: {e}");
                // Handle exception.
                throw new ApplicationException($"An error has occured: {e}");
            }
        }

        // Configuration Docs:
        // https://github.com/rickyah/ini-parser/wiki/Configuring-parser-behavior
        private static IniDataParser GetIniParserConfiguration()
        {
            return new IniDataParser(new IniParserConfiguration()
            {
                AllowDuplicateKeys = false,
                AllowDuplicateSections = false,
                AllowKeysWithoutSection = false,
                SectionStartChar = '[',
                SectionEndChar = ']'
            });
        }

        private static IniData GetDefaultWindowsConfiguration()
        {
           IniData defaultIni = new IniData();

            // TODO: Pass "env" to the system config. Add paths also.
            defaultIni.Sections.AddSection("env");
            defaultIni["env"].AddKey("os", "windows");
            defaultIni["env"].AddKey("open_path", FileStorage.GetUserProfilePath());
            

            defaultIni.Sections.AddSection("key_bindings");
            defaultIni["key_bindings"].AddKey("default_modifier", "Control");
            defaultIni["key_bindings"].AddKey("save", "S");
            // defaultIni["key_bindings"].AddKey("save_modifiers", "Control");
            // defaultIni["key_bindings"]["save_as_key"] = defaultUserConfig.SaveAsKey;
            defaultIni["key_bindings"].AddKey("save_as", "S");
            defaultIni["key_bindings"].AddKey("save_as_modifier", "Control+Shift");
            defaultIni["key_bindings"].AddKey("open", "O");

            return defaultIni;
        }

        #endregion
    }
}
