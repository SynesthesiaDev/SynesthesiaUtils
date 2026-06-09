// Copyright (c) 2026 SynesthesiaDev <synesthesiadev@proton.me>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using Synesthesia.Utils.Extensions;

namespace Synesthesia.Utils.Tests;

[TestFixture]
public class ExtensionTests
{
    [Test]
    public void String_HexToRgb_WithHashPrefix_ReturnsRgbValues()
    {
        var rgb = "#336699".HexToRgb();

        Assert.Multiple(() =>
        {
            Assert.That(rgb.Item1, Is.EqualTo(0x33));
            Assert.That(rgb.Item2, Is.EqualTo(0x66));
            Assert.That(rgb.Item3, Is.EqualTo(0x99));
        });
    }

    [Test]
    public void String_HexToRgb_WithoutHashPrefix_ReturnsRgbValues()
    {
        var rgb = "FF8040".HexToRgb();

        Assert.Multiple(() =>
        {
            Assert.That(rgb.Item1, Is.EqualTo(255));
            Assert.That(rgb.Item2, Is.EqualTo(128));
            Assert.That(rgb.Item3, Is.EqualTo(64));
        });
    }

    [Test]
    public void String_HexToRgba_WithHashPrefix_ReturnsRgbaValues()
    {
        var rgba = "#336699CC".HexToRgba();

        Assert.Multiple(() =>
        {
            Assert.That(rgba.Item1, Is.EqualTo(0x33));
            Assert.That(rgba.Item2, Is.EqualTo(0x66));
            Assert.That(rgba.Item3, Is.EqualTo(0x99));
            Assert.That(rgba.Item4, Is.EqualTo(0xCC));
        });
    }

    [Test]
    public void String_HexToRgb_WithInvalidHex_ThrowsFormatException()
    {
        Assert.Throws<FormatException>(() => "#GG6699".HexToRgb());
    }

    [Test]
    public void String_HexToRgb_WithTooShortString_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => "#123".HexToRgb());
    }

    [Test]
    public void String_RemovePrefix_WhenPrefixExists_RemovesPrefix()
    {
        var result = "SynesthesiaUtils".RemovePrefix("Synesthesia");

        Assert.That(result, Is.EqualTo("Utils"));
    }

    [Test]
    public void String_RemovePrefix_WhenPrefixDoesNotExist_ReturnsOriginalString()
    {
        var result = "SynesthesiaUtils".RemovePrefix("Other");

        Assert.That(result, Is.EqualTo("SynesthesiaUtils"));
    }

    [Test]
    public void String_RemoveSuffix_WhenSuffixExists_RemovesSuffix()
    {
        var result = "SynesthesiaUtils".RemoveSuffix("Utils");

        Assert.That(result, Is.EqualTo("Synesthesia"));
    }

    [Test]
    public void String_RemoveSuffix_WhenSuffixDoesNotExist_ReturnsOriginalString()
    {
        var result = "SynesthesiaUtils".RemoveSuffix("Other");

        Assert.That(result, Is.EqualTo("SynesthesiaUtils"));
    }

    [Test]
    public void String_CutIfTooLong_WhenStringIsLongerThanLimit_AddsDotsByDefault()
    {
        var result = "SynesthesiaUtils".CutIfTooLong(10);

        Assert.That(result, Is.EqualTo("Synesthesi..."));
    }

    [Test]
    public void String_CutIfTooLong_WhenDotsDisabled_DoesNotAddDots()
    {
        var result = "SynesthesiaUtils".CutIfTooLong(10, false);

        Assert.That(result, Is.EqualTo("Synesthesi"));
    }

    [Test]
    public void String_CutIfTooLong_WhenStringIsShorterThanLimit_ReturnsOriginalString()
    {
        var result = "Utils".CutIfTooLong(10);

        Assert.That(result, Is.EqualTo("Utils"));
    }

    [Test]
    public void Enumerable_LastOrNull_WhenSequenceHasValues_ReturnsLastValue()
    {
        var values = new[] { 1, 2, 3 };

        var result = values.LastOrNull();

        Assert.That(result, Is.EqualTo(3));
    }

    [Test]
    public void Enumerable_LastOrNull_WhenSequenceIsEmpty_ReturnsDefault()
    {
        var values = Array.Empty<int>();

        var result = values.LastOrNull();

        Assert.That(result, Is.EqualTo(0));
    }

    [Test]
    public void Enumerable_Reversed_ReturnsValuesInReverseOrder()
    {
        var values = new[] { 1, 2, 3 };

        var result = values.Reversed();

        Assert.That(result, Is.EqualTo(new[] { 3, 2, 1 }));
    }

    [Test]
    public void List_CartesianProduct_ReturnsEveryCombination()
    {
        var sequences = new[]
        {
            new[] { "A", "B" },
            new[] { "1", "2" }
        };

        var result = sequences
            .CartesianProduct()
            .Select(items => string.Join("", items))
            .ToArray();

        Assert.That(result, Is.EqualTo(new[] { "A1", "A2", "B1", "B2" }));
    }

    [Test]
    public void List_CartesianProduct_WithThreeSequences_ReturnsEveryCombination()
    {
        var sequences = new[]
        {
            new[] { "A", "B" },
            new[] { "1", "2" },
            new[] { "X", "Y" }
        };

        var result = sequences
            .CartesianProduct()
            .Select(items => string.Join("", items))
            .ToArray();

        Assert.That(result, Is.EqualTo(new[]
        {
            "A1X",
            "A1Y",
            "A2X",
            "A2Y",
            "B1X",
            "B1Y",
            "B2X",
            "B2Y"
        }));
    }

    [Test]
    public void Dictionary_Filter_ReturnsOnlyMatchingEntries()
    {
        var dictionary = new Dictionary<string, int>
        {
            ["one"] = 1,
            ["two"] = 2,
            ["three"] = 3,
            ["four"] = 4
        };

        var result = dictionary.Filter(pair => pair.Value % 2 == 0);

        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result["two"], Is.EqualTo(2));
            Assert.That(result["four"], Is.EqualTo(4));
            Assert.That(result.ContainsKey("one"), Is.False);
            Assert.That(result.ContainsKey("three"), Is.False);
        });
    }

    [Test]
    public void Dictionary_GetValueOrThrow_WhenKeyExists_ReturnsValue()
    {
        var dictionary = new Dictionary<string, int>
        {
            ["value"] = 123
        };

        var result = dictionary.GetValueOrThrow("value");

        Assert.That(result, Is.EqualTo(123));
    }

    [Test]
    public void Dictionary_GetValueOrThrow_WhenKeyDoesNotExist_ThrowsKeyNotFoundException()
    {
        var dictionary = new Dictionary<string, int>();

        Assert.Throws<KeyNotFoundException>(() => dictionary.GetValueOrThrow("missing"));
    }

}
