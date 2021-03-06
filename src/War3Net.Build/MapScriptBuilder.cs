﻿// ------------------------------------------------------------------------------
// <copyright file="MapScriptBuilder.cs" company="Drake53">
// Licensed under the MIT license.
// See the LICENSE file in the project root for more information.
// </copyright>
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;

using War3Net.Build.Extensions;
using War3Net.Build.Info;
using War3Net.CodeAnalysis.Jass.Syntax;

using SyntaxFactory = War3Net.CodeAnalysis.Jass.JassSyntaxFactory;

namespace War3Net.Build
{
    public partial class MapScriptBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MapScriptBuilder"/> class.
        /// </summary>
        public MapScriptBuilder()
        {
            SetDefaultOptionsForMap(null);
        }

        public string? LobbyMusic { get; set; }

        public int MaxPlayerSlots { get; set; }

        public bool ForceGenerateGlobalUnitVariable { get; set; }

        public bool UseCSharpLua { get; set; }

        public bool UseLifeVariable { get; set; }

        public bool UseWeatherEffectVariable { get; set; }

        public virtual void SetDefaultOptionsForCSharpLua(string? lobbyMusic = null)
        {
            LobbyMusic = lobbyMusic;
            MaxPlayerSlots = 24;
            ForceGenerateGlobalUnitVariable = true;
            UseCSharpLua = true;
            UseLifeVariable = false;
            UseWeatherEffectVariable = false;
        }

        public virtual void SetDefaultOptionsForMap(Map? map)
        {
            LobbyMusic = null;
            MaxPlayerSlots = map is null || map.Info.FormatVersion >= MapInfoFormatVersion.Lua ? 24 : 12;
            ForceGenerateGlobalUnitVariable = false;
            UseCSharpLua = false;
            UseLifeVariable = true;
            UseWeatherEffectVariable = true;
        }

        public virtual JassCompilationUnitSyntax Build(Map map)
        {
            if (map is null)
            {
                throw new ArgumentNullException(nameof(map));
            }

            JassCommentDeclarationSyntax commentLine1 = new("===========================================================================");
            JassCommentDeclarationSyntax commentLine2 = new("***************************************************************************");
            JassCommentDeclarationSyntax commentLine3 = new("*");

            List<IDeclarationSyntax> declarations = new();
            var globalDeclarationList = new List<IDeclarationSyntax>();
            var generatedGlobals = new List<JassGlobalDeclarationSyntax>();

            void AppendBanner(string bannerText)
            {
                declarations.Add(commentLine2);
                declarations.Add(commentLine3);
                declarations.Add(new JassCommentDeclarationSyntax($"*  {bannerText}"));
                declarations.Add(commentLine3);
                declarations.Add(commentLine2);
                declarations.Add(JassEmptyDeclarationSyntax.Value);
            }

            void AppendBannerAndFunction(string bannerText, Func<Map, JassFunctionDeclarationSyntax> function, Func<Map, bool> condition)
            {
                if (condition(map))
                {
                    AppendBanner(bannerText);
                    declarations.Add(function(map));
                    declarations.Add(JassEmptyDeclarationSyntax.Value);
                }
            }

            void AppendBannerAndFunctions(string bannerText, Func<Map, IEnumerable<IDeclarationSyntax>> functions, Func<Map, bool> condition)
            {
                if (condition(map))
                {
                    AppendBanner(bannerText);
                    foreach (var function in functions(map))
                    {
                        declarations.Add(function);
                        declarations.Add(JassEmptyDeclarationSyntax.Value);
                    }
                }
            }

            void AppendFunction(Func<Map, JassFunctionDeclarationSyntax> function, Func<Map, bool> condition)
            {
                if (condition(map))
                {
                    declarations.Add(commentLine1);
                    declarations.Add(function(map));
                    declarations.Add(JassEmptyDeclarationSyntax.Value);
                }
            }

            void AppendFunctionForIndex(int index, Func<Map, int, JassFunctionDeclarationSyntax> function, Func<Map, int, bool> condition)
            {
                if (condition(map, index))
                {
                    declarations.Add(commentLine1);
                    declarations.Add(function(map, index));
                    declarations.Add(JassEmptyDeclarationSyntax.Value);
                }
            }

            declarations.AddRange(GetMapScriptHeader(map));
            declarations.Add(JassEmptyDeclarationSyntax.Value);

            AppendBanner("Global Variables");
            generatedGlobals.AddRange(Regions(map));
            generatedGlobals.AddRange(Cameras(map));
            generatedGlobals.AddRange(Sounds(map));
            generatedGlobals.AddRange(Units(map));
            generatedGlobals.AddRange(RandomUnitTables(map));

            if (generatedGlobals.Any())
            {
                globalDeclarationList.Add(new JassCommentDeclarationSyntax(" Generated"));
                globalDeclarationList.AddRange(generatedGlobals);
            }

            declarations.Add(new JassGlobalDeclarationListSyntax(globalDeclarationList.ToImmutableArray()));
            declarations.Add(JassEmptyDeclarationSyntax.Value);

            AppendBanner("Custom Script Code");
            AppendBannerAndFunction("Random Groups", InitRandomGroups, InitRandomGroupsCondition);
            AppendBannerAndFunctions("Map Item Tables", MapItemTables, MapItemTablesCondition);
            AppendBannerAndFunction("Items", CreateAllItems, CreateAllItemsCondition);
            AppendBannerAndFunctions("Unit Item Tables", UnitItemTables, UnitItemTablesCondition);
            AppendBannerAndFunctions("Destructable Item Tables", DestructableItemTables, DestructableItemTablesCondition);
            AppendBannerAndFunction("Sounds", InitSounds, InitSoundsCondition);
            AppendBannerAndFunction("Destructable Objects", CreateAllDestructables, CreateAllDestructablesCondition);

            if (CreateAllUnitsCondition(map))
            {
                AppendBanner("Unit Creation");

                foreach (var i in Enumerable.Range(0, MaxPlayerSlots))
                {
                    AppendFunctionForIndex(i, CreateBuildingsForPlayer, CreateBuildingsForPlayerCondition);
                    AppendFunctionForIndex(i, CreateUnitsForPlayer, CreateUnitsForPlayerCondition);
                }

                AppendFunction(CreateNeutralHostile, CreateNeutralHostileCondition);
                AppendFunction(CreateNeutralPassiveBuildings, CreateNeutralPassiveBuildingsCondition);
                AppendFunction(CreateNeutralPassive, CreateNeutralPassiveCondition);
                AppendFunction(CreatePlayerBuildings, CreatePlayerBuildingsCondition);
                AppendFunction(CreatePlayerUnits, CreatePlayerUnitsCondition);
                AppendFunction(CreateNeutralUnits, CreateNeutralUnitsCondition);
                AppendFunction(CreateAllUnits, CreateAllUnitsCondition);
            }

            AppendBannerAndFunction("Regions", CreateRegions, CreateRegionsCondition);
            AppendBannerAndFunction("Cameras", CreateCameras, CreateCamerasCondition);

            AppendBanner("Players");

            declarations.Add(InitCustomPlayerSlots(map));
            declarations.Add(JassEmptyDeclarationSyntax.Value);

            declarations.Add(InitCustomTeams(map));
            declarations.Add(JassEmptyDeclarationSyntax.Value);

            var ids = Enumerable.Range(0, MaxPlayerSlots).ToArray();
            if (map.Info.Players.Any(p => ids.Any(id => p.AllyLowPriorityFlags[id] || p.AllyHighPriorityFlags[id])))
            {
                declarations.Add(InitAllyPriorities(map));
                declarations.Add(JassEmptyDeclarationSyntax.Value);
            }

            AppendBannerAndFunction("Main Initialization", main, mainCondition);
            AppendBannerAndFunction("Map Configuration", config, configCondition);

            return SyntaxFactory.CompilationUnit(declarations);
        }

        protected virtual IEnumerable<JassCommentDeclarationSyntax> GetMapScriptHeader(Map map)
        {
            if (map is null)
            {
                throw new ArgumentNullException(nameof(map));
            }

            var mapInfo = map.Info;
            var mapTriggerStrings = map.TriggerStrings;

            yield return new JassCommentDeclarationSyntax($"===========================================================================");
            yield return new JassCommentDeclarationSyntax($" ");
            yield return new JassCommentDeclarationSyntax($" {mapInfo.MapName.Localize(mapTriggerStrings)}");
            yield return new JassCommentDeclarationSyntax($" ");
            yield return new JassCommentDeclarationSyntax($"   Warcraft III map script");
            yield return new JassCommentDeclarationSyntax($"   Generated by {Assembly.GetExecutingAssembly().GetName().Name}");
            yield return new JassCommentDeclarationSyntax($"   Date: {DateTime.Now:ddd MMM dd HH:mm:ss yyyy}");
            yield return new JassCommentDeclarationSyntax($"   Map Author: {mapInfo.MapAuthor.Localize(mapTriggerStrings)}");
            yield return new JassCommentDeclarationSyntax($" ");
            yield return new JassCommentDeclarationSyntax($"===========================================================================");
        }
    }
}