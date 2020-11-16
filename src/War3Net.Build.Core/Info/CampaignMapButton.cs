﻿// ------------------------------------------------------------------------------
// <copyright file="CampaignMapButton.cs" company="Drake53">
// Licensed under the MIT license.
// See the LICENSE file in the project root for more information.
// </copyright>
// ------------------------------------------------------------------------------

using System.IO;

using War3Net.Common.Extensions;

namespace War3Net.Build.Info
{
    public sealed class CampaignMapButton
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CampaignMapButton"/> class.
        /// </summary>
        public CampaignMapButton()
        {
        }

        internal CampaignMapButton(BinaryReader reader, CampaignInfoFormatVersion formatVersion)
        {
            ReadFrom(reader, formatVersion);
        }

        public bool IsVisibleInitially { get; set; }

        public string Chapter { get; set; }

        public string Title { get; set; }

        public string MapFilePath { get; set; }

        internal void ReadFrom(BinaryReader reader, CampaignInfoFormatVersion formatVersion)
        {
            IsVisibleInitially = reader.ReadBool();
            Chapter = reader.ReadChars();
            Title = reader.ReadChars();
            MapFilePath = reader.ReadChars();
        }

        internal void WriteTo(BinaryWriter writer, CampaignInfoFormatVersion formatVersion)
        {
            writer.WriteBool(IsVisibleInitially);
            writer.WriteString(Chapter);
            writer.WriteString(Title);
            writer.WriteString(MapFilePath);
        }
    }
}