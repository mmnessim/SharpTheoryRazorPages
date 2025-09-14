using System.Text.Json.Serialization;

namespace SharpTheory.Models
{
    public class TheoryRoot
    {
        [JsonPropertyName("description")]
        public required TheoryDescription Description { get; set; }

        [JsonPropertyName("integers")]
        public required List<TheoryInteger> Integers { get; set; }

        [JsonPropertyName("keys")]
        public required List<TheoryKey> Keys { get; set; }

        [JsonPropertyName("scales")]
        public required List<TheoryScale> Scales { get; set; }

        [JsonPropertyName("intervals")]
        public required List<TheoryInterval> Intervals { get; set; }

        [JsonPropertyName("nondiatonic_scales")]
        public required List<NondiatonicScale> NondiatonicScales { get; set; }
    }

    public class TheoryDescription
    {
        [JsonPropertyName("title")]
        public required string Title { get; set; }

        [JsonPropertyName("version")]
        public required string Version { get; set; }

        [JsonPropertyName("author")]
        public required string Author { get; set; }

        [JsonPropertyName("description")]
        public required string Description { get; set; }
    }

    public class TheoryInteger
    {
        [JsonPropertyName("integer")]
        public required int Integer { get; set; }

        [JsonPropertyName("names")]
        public required List<string> Names { get; set; }
    }

    public class TheoryKey
    {
        [JsonPropertyName("name")]
        public required string Name { get; set; }

        [JsonPropertyName("mode")]
        public required string Mode { get; set; }

        [JsonPropertyName("integer")]
        public required int Integer { get; set; }

        [JsonPropertyName("sharps")]
        public required List<string> Sharps { get; set; }

        [JsonPropertyName("flats")]
        public required List<string> Flats { get; set; }

        [JsonPropertyName("num_sharps")]
        public required int NumSharps { get; set; }

        [JsonPropertyName("num_flats")]
        public required int NumFlats { get; set; }

        [JsonPropertyName("relative")]
        public required string Relative { get; set; }

        [JsonPropertyName("enharmonic")]
        public List<string>? Enharmonic { get; set; }
    }

    public class TheoryScale
    {
        [JsonPropertyName("tonic")]
        public required string Tonic { get; set; }

        [JsonPropertyName("integer")]
        public required int Integer { get; set; }

        [JsonPropertyName("major")]
        public TheoryScaleType? Major { get; set; }

        [JsonPropertyName("natural_minor")]
        public TheoryScaleType? NaturalMinor { get; set; }

        [JsonPropertyName("harmonic_minor")]
        public TheoryScaleType? HarmonicMinor { get; set; }
    }

    public class TheoryScaleType
    {
        [JsonPropertyName("notes")]
        public required List<string> Notes { get; set; }

        [JsonPropertyName("integers")]
        public required List<int> Integers { get; set; }
    }

    public class TheoryInterval
    {
        [JsonPropertyName("name")]
        public required string Name { get; set; }

        [JsonPropertyName("abbreviation")]
        public required string Abbreviation { get; set; }

        [JsonPropertyName("semitones")]
        public required int Semitones { get; set; }
    }

    public class NondiatonicScale
    {
        [JsonPropertyName("name")]
        public required string Name { get; set; }

        [JsonPropertyName("integers")]
        public required List<int> Integers { get; set; }
    }

}
