﻿// ------------------------------------------------------------------------------
// <copyright file="BinaryReaderExtensionsTests.cs" company="Drake53">
// Licensed under the MIT license.
// See the LICENSE file in the project root for more information.
// </copyright>
// ------------------------------------------------------------------------------

#nullable enable

using System.Collections.Generic;
using System.IO;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using War3Net.Common.Extensions;

namespace War3Net.Common.Tests.Extensions
{
    [TestClass]
    public sealed class BinaryReaderExtensionsTests
    {
        [DataTestMethod]
        [DynamicData(nameof(GetTestReadStrings), DynamicDataSourceType.Method)]
        public void TestReadString(string? s)
        {
            using var memoryStream = new MemoryStream();
            using var binaryWriter = new BinaryWriter(memoryStream);

            binaryWriter.WriteString(s);

            var expectedString = s ?? string.Empty;
            if (expectedString.EndsWith(char.MinValue))
            {
                expectedString = expectedString.TrimEnd(char.MinValue);
            }

            memoryStream.Position = 0;
            using var binaryReader = new BinaryReader(memoryStream);
            Assert.AreEqual(expectedString, binaryReader.ReadChars());
        }

        private static IEnumerable<object?[]> GetTestReadStrings()
        {
            return TestData.GetTestStrings().Where(objects => objects.Length == 1);
        }
    }
}