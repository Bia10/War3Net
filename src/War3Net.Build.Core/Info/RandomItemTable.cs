﻿// ------------------------------------------------------------------------------
// <copyright file="RandomItemTable.cs" company="Drake53">
// Licensed under the MIT license.
// See the LICENSE file in the project root for more information.
// </copyright>
// ------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using War3Net.Common.Extensions;

namespace War3Net.Build.Info
{
    public sealed class RandomItemTable : IEnumerable<RandomItemSet>
    {
        private readonly List<RandomItemSet> _sets;

        private int _tableNumber;
        private string _tableName;

        public RandomItemTable()
        {
            _sets = new List<RandomItemSet>();
        }

        public int Index => _tableNumber;

        public int ItemSetCount => _sets.Count;

        public IEnumerable<(int chance, string id)>[] ItemSets
        {
            get
            {
                var itemSetCount = ItemSetCount;
                var itemSets = new IEnumerable<(int chance, string id)>[itemSetCount];
                for (var i = 0; i < itemSetCount; i++)
                {
                    itemSets[i] = _sets[i].Select(itemSet => (itemSet.Item1, new string(itemSet.Item2)));
                }

                return itemSets;
            }
        }

        public static RandomItemTable Parse(Stream stream, bool leaveOpen = false)
        {
            var table = new RandomItemTable();
            using (var reader = new BinaryReader(stream, new UTF8Encoding(false, true), leaveOpen))
            {
                table._tableNumber = reader.ReadInt32();
                table._tableName = reader.ReadChars();

                var setCount = reader.ReadInt32();
                for (var i = 0; i < setCount; i++)
                {
                    var set = new RandomItemSet();
                    var setSize = reader.ReadInt32();
                    for (var j = 0; j < setSize; j++)
                    {
                        set.AddItem(reader.ReadInt32(), reader.ReadChars(4));
                    }

                    table._sets.Add(set);
                }
            }

            return table;
        }

        public void WriteTo(BinaryWriter writer)
        {
            writer.Write(_tableNumber);
            writer.WriteString(_tableName);

            writer.Write(_sets.Count);
            foreach (var set in _sets)
            {
                writer.Write(set.Size);
                foreach (var (chance, id) in set)
                {
                    writer.Write(chance);
                    writer.Write(id);
                }
            }
        }

        public RandomItemSet GetSet(int setIndex)
        {
            return _sets[setIndex];
        }

        public IEnumerator<RandomItemSet> GetEnumerator()
        {
            return ((IEnumerable<RandomItemSet>)_sets).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<RandomItemSet>)_sets).GetEnumerator();
        }
    }
}