﻿// ------------------------------------------------------------------------------
// <copyright file="BinaryReaderExtensions.cs" company="Drake53">
// Licensed under the MIT license.
// See the LICENSE file in the project root for more information.
// </copyright>
// ------------------------------------------------------------------------------

#pragma warning disable SA1600

using System.IO;

using War3Net.Build.Audio;
using War3Net.Build.Environment;
using War3Net.Build.Info;
using War3Net.Build.Object;
using War3Net.Build.Script;
using War3Net.Build.Widget;

namespace War3Net.Build.Extensions
{
    public static class BinaryReaderExtensions
    {
        public static MapSounds ReadMapSounds(this BinaryReader reader) => new MapSounds(reader);

        public static Sound ReadSound(this BinaryReader reader, MapSoundsFormatVersion formatVersion) => new Sound(reader, formatVersion);

        public static MapCameras ReadMapCameras(this BinaryReader reader) => new MapCameras(reader);

        public static Camera ReadCamera(this BinaryReader reader, MapCamerasFormatVersion formatVersion, bool useNewFormat) => new Camera(reader, formatVersion, useNewFormat);

        public static MapEnvironment ReadMapEnvironment(this BinaryReader reader) => new MapEnvironment(reader);

        public static MapTile ReadTerrainTile(this BinaryReader reader, MapEnvironmentFormatVersion formatVersion) => new MapTile(reader, formatVersion);

        public static MapPreviewIcons ReadMapPreviewIcons(this BinaryReader reader) => new MapPreviewIcons(reader);

        public static MapPreviewIcon ReadMapPreviewIcon(this BinaryReader reader, MapPreviewIconsFormatVersion formatVersion) => new MapPreviewIcon(reader, formatVersion);

        public static MapRegions ReadMapRegions(this BinaryReader reader) => new MapRegions(reader);

        public static Region ReadRegion(this BinaryReader reader, MapRegionsFormatVersion formatVersion) => new Region(reader, formatVersion);

        public static PathingMap ReadPathingMap(this BinaryReader reader) => new PathingMap(reader);

        public static ShadowMap ReadShadowMap(this BinaryReader reader) => new ShadowMap(reader);

        public static CampaignInfo ReadCampaignInfo(this BinaryReader reader) => new CampaignInfo(reader);

        public static CampaignMapButton ReadCampaignMapButton(this BinaryReader reader, CampaignInfoFormatVersion formatVersion) => new CampaignMapButton(reader, formatVersion);

        public static CampaignMap ReadCampaignMap(this BinaryReader reader, CampaignInfoFormatVersion formatVersion) => new CampaignMap(reader, formatVersion);

        public static MapInfo ReadMapInfo(this BinaryReader reader) => new MapInfo(reader);

        public static PlayerData ReadPlayerData(this BinaryReader reader, MapInfoFormatVersion formatVersion) => new PlayerData(reader, formatVersion);

        public static ForceData ReadForceData(this BinaryReader reader, MapInfoFormatVersion formatVersion) => new ForceData(reader, formatVersion);

        public static UpgradeData ReadUpgradeData(this BinaryReader reader, MapInfoFormatVersion formatVersion) => new UpgradeData(reader, formatVersion);

        public static TechData ReadTechData(this BinaryReader reader, MapInfoFormatVersion formatVersion) => new TechData(reader, formatVersion);

        public static RandomUnitTable ReadRandomUnitTable(this BinaryReader reader, MapInfoFormatVersion formatVersion) => new RandomUnitTable(reader, formatVersion);

        public static RandomItemTable ReadRandomItemTable(this BinaryReader reader, MapInfoFormatVersion formatVersion) => new RandomItemTable(reader, formatVersion);

        public static RandomUnitSet ReadRandomUnitSet(this BinaryReader reader, MapInfoFormatVersion formatVersion) => new RandomUnitSet(reader, formatVersion);

        public static RandomItemSet ReadRandomItemSet(this BinaryReader reader, MapInfoFormatVersion formatVersion) => new RandomItemSet(reader, formatVersion);

        public static ObjectData ReadObjectData(this BinaryReader reader, bool fromCampaign = false) => new ObjectData(reader, fromCampaign);

        public static AbilityObjectData ReadAbilityObjectData(this BinaryReader reader, bool fromCampaign = false) => fromCampaign ? new CampaignAbilityObjectData(reader) : new MapAbilityObjectData(reader);

        public static BuffObjectData ReadBuffObjectData(this BinaryReader reader, bool fromCampaign = false) => fromCampaign ? new CampaignBuffObjectData(reader) : new MapBuffObjectData(reader);

        public static DestructableObjectData ReadDestructableObjectData(this BinaryReader reader, bool fromCampaign = false) => fromCampaign ? new CampaignDestructableObjectData(reader) : new MapDestructableObjectData(reader);

        public static DoodadObjectData ReadDoodadObjectData(this BinaryReader reader, bool fromCampaign = false) => fromCampaign ? new CampaignDoodadObjectData(reader) : new MapDoodadObjectData(reader);

        public static ItemObjectData ReadItemObjectData(this BinaryReader reader, bool fromCampaign = false) => fromCampaign ? new CampaignItemObjectData(reader) : new MapItemObjectData(reader);

        public static UnitObjectData ReadUnitObjectData(this BinaryReader reader, bool fromCampaign = false) => fromCampaign ? new CampaignUnitObjectData(reader) : new MapUnitObjectData(reader);

        public static UpgradeObjectData ReadUpgradeObjectData(this BinaryReader reader, bool fromCampaign = false) => fromCampaign ? new CampaignUpgradeObjectData(reader) : new MapUpgradeObjectData(reader);

        public static LevelObjectModification ReadLevelObjectModification(this BinaryReader reader, ObjectDataFormatVersion formatVersion) => new LevelObjectModification(reader, formatVersion);

        public static LevelObjectDataModification ReadLevelObjectDataModification(this BinaryReader reader, ObjectDataFormatVersion formatVersion) => new LevelObjectDataModification(reader, formatVersion);

        public static SimpleObjectModification ReadSimpleObjectModification(this BinaryReader reader, ObjectDataFormatVersion formatVersion) => new SimpleObjectModification(reader, formatVersion);

        public static SimpleObjectDataModification ReadSimpleObjectDataModification(this BinaryReader reader, ObjectDataFormatVersion formatVersion) => new SimpleObjectDataModification(reader, formatVersion);

        public static VariationObjectModification ReadVariationObjectModification(this BinaryReader reader, ObjectDataFormatVersion formatVersion) => new VariationObjectModification(reader, formatVersion);

        public static VariationObjectDataModification ReadVariationObjectDataModification(this BinaryReader reader, ObjectDataFormatVersion formatVersion) => new VariationObjectDataModification(reader, formatVersion);

        public static MapCustomTextTriggers ReadMapCustomTextTriggers(this BinaryReader reader) => new MapCustomTextTriggers(reader);

        public static CustomTextTrigger ReadCustomTextTrigger(this BinaryReader reader, MapCustomTextTriggersFormatVersion formatVersion, bool useNewFormat) => new CustomTextTrigger(reader, formatVersion, useNewFormat);

        public static MapTriggers ReadMapTriggers(this BinaryReader reader, TriggerData triggerData) => new MapTriggers(reader, triggerData);

        public static DeletedTriggerItem ReadDeletedTriggerItem(this BinaryReader reader, TriggerData triggerData, MapTriggersFormatVersion formatVersion, bool useNewFormat) => new DeletedTriggerItem(reader, triggerData, formatVersion, useNewFormat);

        public static TriggerCategoryDefinition ReadTriggerCategoryDefinition(this BinaryReader reader, TriggerData triggerData, MapTriggersFormatVersion formatVersion, bool useNewFormat) => new TriggerCategoryDefinition(reader, triggerData, formatVersion, useNewFormat);

        public static TriggerDefinition ReadTriggerDefinition(this BinaryReader reader, TriggerData triggerData, MapTriggersFormatVersion formatVersion, bool useNewFormat) => new TriggerDefinition(reader, triggerData, formatVersion, useNewFormat);

        public static TriggerVariableDefinition ReadTriggerVariableDefinition(this BinaryReader reader, TriggerData triggerData, MapTriggersFormatVersion formatVersion, bool useNewFormat) => new TriggerVariableDefinition(reader, triggerData, formatVersion, useNewFormat);

        public static VariableDefinition ReadVariableDefinition(this BinaryReader reader, TriggerData triggerData, MapTriggersFormatVersion formatVersion, bool useNewFormat) => new VariableDefinition(reader, triggerData, formatVersion, useNewFormat);

        public static TriggerFunction ReadTriggerFunction(this BinaryReader reader, TriggerData triggerData, MapTriggersFormatVersion formatVersion, bool useNewFormat) => new TriggerFunction(reader, triggerData, formatVersion, useNewFormat);

        public static TriggerFunctionParameter ReadTriggerFunctionParameter(this BinaryReader reader, TriggerData triggerData, MapTriggersFormatVersion formatVersion, bool useNewFormat) => new TriggerFunctionParameter(reader, triggerData, formatVersion, useNewFormat);

        public static MapDoodads ReadMapDoodads(this BinaryReader reader) => new MapDoodads(reader);

        public static MapDoodadData ReadMapDoodadData(this BinaryReader reader, MapWidgetsVersion formatVersion, MapWidgetsSubVersion subVersion, bool useNewFormat) => new MapDoodadData(reader, formatVersion, subVersion, useNewFormat);

        public static MapSpecialDoodadData ReadMapSpecialDoodadData(this BinaryReader reader, MapWidgetsVersion formatVersion, MapWidgetsSubVersion subVersion, bool useNewFormat) => new MapSpecialDoodadData(reader, formatVersion, subVersion, useNewFormat);

        public static MapUnits ReadMapUnits(this BinaryReader reader) => new MapUnits(reader);

        public static MapUnitData ReadMapUnitData(this BinaryReader reader, MapWidgetsVersion formatVersion, MapWidgetsSubVersion subVersion, bool useNewFormat) => new MapUnitData(reader, formatVersion, subVersion, useNewFormat);

        public static DroppedItemSetData ReadDroppedItemSetData(this BinaryReader reader, MapWidgetsVersion formatVersion, MapWidgetsSubVersion subVersion, bool useNewFormat) => new DroppedItemSetData(reader, formatVersion, subVersion, useNewFormat);

        public static InventoryItemData ReadInventoryItemData(this BinaryReader reader, MapWidgetsVersion formatVersion, MapWidgetsSubVersion subVersion, bool useNewFormat) => new InventoryItemData(reader, formatVersion, subVersion, useNewFormat);

        public static ModifiedAbilityData ReadModifiedAbilityData(this BinaryReader reader, MapWidgetsVersion formatVersion, MapWidgetsSubVersion subVersion, bool useNewFormat) => new ModifiedAbilityData(reader, formatVersion, subVersion, useNewFormat);
    }
}