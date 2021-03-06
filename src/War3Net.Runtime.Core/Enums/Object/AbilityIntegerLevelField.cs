﻿// ------------------------------------------------------------------------------
// <copyright file="AbilityIntegerLevelField.cs" company="Drake53">
// Licensed under the MIT license.
// See the LICENSE file in the project root for more information.
// </copyright>
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

using War3Net.Runtime.Core;

namespace War3Net.Runtime.Enums.Object
{
    public sealed class AbilityIntegerLevelField : Handle
    {
        private static readonly Dictionary<int, AbilityIntegerLevelField> _fields = GetTypes().ToDictionary(t => (int)t, t => new AbilityIntegerLevelField(t));

        private readonly Type _type;

        private AbilityIntegerLevelField(Type type)
        {
            _type = type;
        }

        public enum Type
        {
            MANA_COST = 1634558835,
            NUMBER_OF_WAVES = 1214413361,
            NUMBER_OF_SHARDS = 1214413363,
            NUMBER_OF_UNITS_TELEPORTED = 1215132721,
            SUMMONED_UNIT_COUNT_HWE2 = 1215784242,
            NUMBER_OF_IMAGES = 1332570417,
            NUMBER_OF_CORPSES_RAISED_UAN1 = 1432448561,
            MORPHING_FLAGS = 1164797234,
            STRENGTH_BONUS_NRG5 = 1316120373,
            DEFENSE_BONUS_NRG6 = 1316120374,
            NUMBER_OF_TARGETS_HIT = 1331915826,
            DETECTION_TYPE_OFS1 = 1332114225,
            NUMBER_OF_SUMMONED_UNITS_OSF2 = 1332962866,
            NUMBER_OF_SUMMONED_UNITS_EFN1 = 1164340785,
            NUMBER_OF_CORPSES_RAISED_HRE1 = 1215456561,
            STACK_FLAGS = 1214472500,
            MINIMUM_NUMBER_OF_UNITS = 1315205170,
            MAXIMUM_NUMBER_OF_UNITS_NDP3 = 1315205171,
            NUMBER_OF_UNITS_CREATED_NRC2 = 1316119346,
            SHIELD_LIFE = 1097691955,
            MANA_LOSS_AMS4 = 1097691956,
            GOLD_PER_INTERVAL_BGM1 = 1114074417,
            MAX_NUMBER_OF_MINERS = 1114074419,
            CARGO_CAPACITY = 1130459697,
            MAXIMUM_CREEP_LEVEL_DEV3 = 1147500083,
            MAX_CREEP_LEVEL_DEV1 = 1147500081,
            GOLD_PER_INTERVAL_EGM1 = 1164406065,
            DEFENSE_REDUCTION = 1180788017,
            DETECTION_TYPE_FLA1 = 1181507889,
            FLARE_COUNT = 1181507891,
            MAX_GOLD = 1198285873,
            MINING_CAPACITY = 1198285875,
            MAXIMUM_NUMBER_OF_CORPSES_GYD1 = 1199137841,
            DAMAGE_TO_TREE = 1214345777,
            LUMBER_CAPACITY = 1214345778,
            GOLD_CAPACITY = 1214345779,
            DEFENSE_INCREASE_INF2 = 1231971890,
            INTERACTION_TYPE = 1315271986,
            GOLD_COST_NDT1 = 1315206193,
            LUMBER_COST_NDT2 = 1315206194,
            DETECTION_TYPE_NDT3 = 1315206195,
            STACKING_TYPE_POI4 = 1349478708,
            STACKING_TYPE_POA5 = 1349476661,
            MAXIMUM_CREEP_LEVEL_PLY1 = 1349286193,
            MAXIMUM_CREEP_LEVEL_POS1 = 1349481265,
            MOVEMENT_UPDATE_FREQUENCY_PRG1 = 1349674801,
            ATTACK_UPDATE_FREQUENCY_PRG2 = 1349674802,
            MANA_LOSS_PRG6 = 1349674806,
            UNITS_SUMMONED_TYPE_ONE = 1382115633,
            UNITS_SUMMONED_TYPE_TWO = 1382115634,
            MAX_UNITS_SUMMONED = 1432576565,
            ALLOW_WHEN_FULL_REJ3 = 1382378035,
            MAXIMUM_UNITS_CHARGED_TO_CASTER = 1383096885,
            MAXIMUM_UNITS_AFFECTED = 1383096886,
            DEFENSE_INCREASE_ROA2 = 1383031090,
            MAX_UNITS_ROA7 = 1383031095,
            ROOTED_WEAPONS = 1383034673,
            UPROOTED_WEAPONS = 1383034674,
            UPROOTED_DEFENSE_TYPE = 1383034676,
            ACCUMULATION_STEP = 1398893618,
            NUMBER_OF_OWLS = 1165192756,
            STACKING_TYPE_SPO4 = 1399877428,
            NUMBER_OF_UNITS = 1399809073,
            SPIDER_CAPACITY = 1399873841,
            INTERVALS_BEFORE_CHANGING_TREES = 1466458418,
            AGILITY_BONUS = 1231120233,
            INTELLIGENCE_BONUS = 1231646324,
            STRENGTH_BONUS_ISTR = 1232303218,
            ATTACK_BONUS = 1231123572,
            DEFENSE_BONUS_IDEF = 1231316326,
            SUMMON_1_AMOUNT = 1232301617,
            SUMMON_2_AMOUNT = 1232301618,
            EXPERIENCE_GAINED = 1232629863,
            HIT_POINTS_GAINED_IHPG = 1231581287,
            MANA_POINTS_GAINED_IMPG = 1231908967,
            HIT_POINTS_GAINED_IHP2 = 1231581234,
            MANA_POINTS_GAINED_IMP2 = 1231908914,
            DAMAGE_BONUS_DICE = 1231317347,
            ARMOR_PENALTY_IARP = 1231123056,
            ENABLED_ATTACK_INDEX_IOB5 = 1232036405,
            LEVELS_GAINED = 1231840630,
            MAX_LIFE_GAINED = 1231841638,
            MAX_MANA_GAINED = 1231905134,
            GOLD_GIVEN = 1231515500,
            LUMBER_GIVEN = 1231844717,
            DETECTION_TYPE_IFA1 = 1231446321,
            MAXIMUM_CREEP_LEVEL_ICRE = 1231254117,
            MOVEMENT_SPEED_BONUS = 1231910498,
            HIT_POINTS_REGENERATED_PER_SECOND = 1231581298,
            SIGHT_RANGE_BONUS = 1232300386,
            DAMAGE_PER_DURATION = 1231251044,
            MANA_USED_PER_SECOND = 1231251053,
            EXTRA_MANA_REQUIRED = 1231251064,
            DETECTION_RADIUS_IDET = 1231316340,
            MANA_LOSS_PER_UNIT_IDIM = 1231317357,
            DAMAGE_TO_SUMMONED_UNITS_IDID = 1231317348,
            MAXIMUM_NUMBER_OF_UNITS_IREC = 1232233827,
            DELAY_AFTER_DEATH_SECONDS = 1232233316,
            RESTORED_LIFE = 1769104178,
            RESTORED_MANA__1_FOR_CURRENT = 1769104179,
            HIT_POINTS_RESTORED = 1231581299,
            MANA_POINTS_RESTORED = 1231908979,
            MAXIMUM_NUMBER_OF_UNITS_ITPM = 1232367725,
            NUMBER_OF_CORPSES_RAISED_CAD1 = 1130456113,
            TERRAIN_DEFORMATION_DURATION_MS = 1467118387,
            MAXIMUM_UNITS = 1432646449,
            DETECTION_TYPE_DET1 = 1147499569,
            GOLD_COST_PER_STRUCTURE = 1316188209,
            LUMBER_COST_PER_USE = 1316188210,
            DETECTION_TYPE_NSP3 = 1316188211,
            NUMBER_OF_SWARM_UNITS = 1433170737,
            MAX_SWARM_UNITS_PER_TARGET = 1433170739,
            NUMBER_OF_SUMMONED_UNITS_NBA2 = 1315070258,
            MAXIMUM_CREEP_LEVEL_NCH1 = 1315137585,
            ATTACKS_PREVENTED = 1316186417,
            MAXIMUM_NUMBER_OF_TARGETS_EFK3 = 1164340019,
            NUMBER_OF_SUMMONED_UNITS_ESV1 = 1165194801,
            MAXIMUM_NUMBER_OF_CORPSES_EXH1 = 1702389809,
            ITEM_CAPACITY = 1768846897,
            MAXIMUM_NUMBER_OF_TARGETS_SPL2 = 1936747570,
            ALLOW_WHEN_FULL_IRL3 = 1769106483,
            MAXIMUM_DISPELLED_UNITS = 1768186675,
            NUMBER_OF_LURES = 1768779569,
            NEW_TIME_OF_DAY_HOUR = 1768125489,
            NEW_TIME_OF_DAY_MINUTE = 1768125490,
            NUMBER_OF_UNITS_CREATED_MEC1 = 1835361073,
            MINIMUM_SPELLS = 1936745011,
            MAXIMUM_SPELLS = 1936745012,
            DISABLED_ATTACK_INDEX = 1735549235,
            ENABLED_ATTACK_INDEX_GRA4 = 1735549236,
            MAXIMUM_ATTACKS = 1735549237,
            BUILDING_TYPES_ALLOWED_NPR1 = 1315992113,
            BUILDING_TYPES_ALLOWED_NSA1 = 1316184369,
            ATTACK_MODIFICATION = 1231118641,
            SUMMONED_UNIT_COUNT_NPA5 = 1315987765,
            UPGRADE_LEVELS = 1231514673,
            NUMBER_OF_SUMMONED_UNITS_NDO2 = 1315204914,
            BEASTS_PER_SECOND = 1316189233,
            TARGET_TYPE = 1315138610,
            OPTIONS = 1315138611,
            ARMOR_PENALTY_NAB3 = 1315004979,
            WAVE_COUNT_NHS6 = 1315468086,
            MAX_CREEP_LEVEL_NTM3 = 1316252979,
            MISSILE_COUNT = 1315140403,
            SPLIT_ATTACK_COUNT = 1315728691,
            GENERATION_COUNT = 1315728694,
            ROCK_RING_COUNT = 1316381489,
            WAVE_COUNT_NVC2 = 1316381490,
            PREFER_HOSTILES_TAU1 = 1415673137,
            PREFER_FRIENDLIES_TAU2 = 1415673138,
            MAX_UNITS_TAU3 = 1415673139,
            NUMBER_OF_PULSES = 1415673140,
            SUMMONED_UNIT_TYPE_HWE1 = 1215784241,
            SUMMONED_UNIT_UIN4 = 1432972852,
            SUMMONED_UNIT_OSF1 = 1332962865,
            SUMMONED_UNIT_TYPE_EFNU = 1164340853,
            SUMMONED_UNIT_TYPE_NBAU = 1315070325,
            SUMMONED_UNIT_TYPE_NTOU = 1316253557,
            SUMMONED_UNIT_TYPE_ESVU = 1165194869,
            SUMMONED_UNIT_TYPES = 1315268145,
            SUMMONED_UNIT_TYPE_NDOU = 1315204981,
            ALTERNATE_FORM_UNIT_EMEU = 1164797301,
            PLAGUE_WARD_UNIT_TYPE = 1097886837,
            ALLOWED_UNIT_TYPE_BTL1 = 1114926129,
            NEW_UNIT_TYPE = 1130914097,
            RESULTING_UNIT_TYPE_ENT1 = 1701737521,
            CORPSE_UNIT_TYPE = 1199137909,
            ALLOWED_UNIT_TYPE_LOA1 = 1282367793,
            UNIT_TYPE_FOR_LIMIT_CHECK = 1382115701,
            WARD_UNIT_TYPE_STAU = 1400136053,
            EFFECT_ABILITY = 1232036469,
            CONVERSION_UNIT = 1315201842,
            UNIT_TO_PRESERVE = 1316187185,
            UNIT_TYPE_ALLOWED = 1130916913,
            SWARM_UNIT_TYPE = 1433170805,
            RESULTING_UNIT_TYPE_COAU = 1668243829,
            UNIT_TYPE_EXHU = 1702389877,
            WARD_UNIT_TYPE_HWDU = 1752654965,
            LURE_UNIT_TYPE = 1768779637,
            UNIT_TYPE_IPMU = 1768975733,
            FACTORY_UNIT_ID = 1316190581,
            SPAWN_UNIT_ID_NFYU = 1315338613,
            DESTRUCTIBLE_ID = 1316381557,
            UPGRADE_TYPE = 1231514741,
        }

        public static implicit operator Type(AbilityIntegerLevelField abilityIntegerLevelField) => abilityIntegerLevelField._type;

        public static explicit operator int(AbilityIntegerLevelField abilityIntegerLevelField) => (int)abilityIntegerLevelField._type;

        public static AbilityIntegerLevelField GetAbilityIntegerLevelField(int i)
        {
            if (!_fields.TryGetValue(i, out var abilityIntegerLevelField))
            {
                abilityIntegerLevelField = new AbilityIntegerLevelField((Type)i);
                _fields.Add(i, abilityIntegerLevelField);
            }

            return abilityIntegerLevelField;
        }

        private static IEnumerable<Type> GetTypes()
        {
            foreach (Type type in Enum.GetValues(typeof(Type)))
            {
                yield return type;
            }
        }
    }
}