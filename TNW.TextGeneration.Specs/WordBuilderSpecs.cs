using System.Linq;
using NUnit.Framework;

namespace TNW.TextGeneration.Specs
{
  [TestFixture]
  public class WordBuilderSpecs : AssertionHelper
  {
    private WordAnalyzer wordAnalyzer;
    private WordBuilder wordBuilder;

    private const int standardSeed = 69;

    private void BuildWords(int minSubwordLength, int maxSubwordLength, bool capitalize, params string[] words)
    {
      this.wordAnalyzer = new WordAnalyzer {
        MinSubwordLength = minSubwordLength,
        MaxSubwordLength = maxSubwordLength,
      };
      this.wordAnalyzer.Analyze(words);

      this.wordBuilder = new WordBuilder(this.wordAnalyzer, standardSeed).Capitalize(capitalize);
    }

    [Test]
    public void It_can_make_some_new_continents()
    {
      var sampleCountryNames = new[] {
        "Africa", "America", "Asia", "Australia", "Europe", "Gondwana", "Laurasia", "Pangaea", "Pannotia", "Rodinia", "Columbia", "Kenorland", "Nena",
        "Ur", "Vaalbara", "Arctica", "Arctica", "Atlantica", "Pacifica", "Atlantica", "Avalonia", "Baltica", "Cimmeria", "Congo", "Kalaharia",
        "Kazakhstania", "Laurentia", "China", "Siberia", "India", "Kerguelia", "Zealandia", "Oceania", "Atlantis", "Kumaria", "Lemuria", "Meropis", "Mu",
        "Eriador", "EarthSea", "Hyrule", "Narnia", "Freedonia", "Islandia", "Niflheim", "Yggdrasil", "Midgard", "Asgard", "Jotunheim", "Elfheim", "Hel",
        "Elysium", "Arborea", "Limbo", "Acheron", "Arcadia", "Ecotopia", "Celestia", "Europa", "Teu", "Harbor", "Harbour", "Tif", "Tir", "Echo",
        "Hinterland", "Brittain", "Caledonia", "California", "Utah", "Persia", "Egypt", "Kush", "Nevada", "Navajo", "Cherokee", "Inuit" };
      this.BuildWords(2, 3, true, sampleCountryNames);
      this.Expect(this.wordBuilder.ChoiceArrayMemorySize, this.EqualTo(5012));

      var names = Enumerables.Infinite().Select(i => this.wordBuilder.BuildNextWord()).Except(sampleCountryNames).Distinct().Take(265).ToArray();
      this.Expect(names, this.EquivalentTo(new[] { "Europisland", "Eu", "Midgarctic", "Asgar", "Narniflhei", "Aureedoni", "Cimmerica", "Erica", "Ledonia",
                                                   "Kumariadonia", "Cheropis", "Arctic", "Oceaniador", "Celestiador", "Ariador", "Atlandia", "Gondiadonia",
                                                   "Siumbia", "Egyp", "Harbou", "Narniador", "Harbo", "Cherica", "Hariador", "Zealandi", "Atlantic",
                                                   "Ariadonia", "Siberica", "Limbiadonia", "Kerguelimbo", "Kumari", "Nenarnia", "Arthsea", "Lemurica",
                                                   "Hyrulemuria", "Zealaharbou", "Lemurentia", "Midgar", "Merittain", "Atlauropis", "Zealantica",
                                                   "Acherope", "Pannoti", "Eriado", "Persi", "Kenorlandia", "Hyrul", "Elfheron", "Elfhei", "Brittai",
                                                   "Haria", "Liforni", "Kalaharctica", "Le", "Limbouropia", "Pac", "Afriadia", "Kalaur", "Chinarni",
                                                   "Europis", "Atlandi", "Jotunhei", "Earthsea", "Harbore", "Laurasil", "Chinui", "Landiadonia",
                                                   "Ocearthsea", "Kenorlantica", "Colu", "Pannotica", "Arboreania", "Meriadonia", "Pangae", "Arthseania",
                                                   "Ear", "Rokenor", "Europislandia", "Baltic", "Pan", "Kazakh", "Zea", "Pacimmerica", "Interlandia",
                                                   "Navaj", "Laurasiberia", "Colum", "Oceart", "Caledoni", "Amersiberia", "Arborlantica", "Rodini",
                                                   "Elysiu", "Hin", "Nevaalbarnia", "Arcador", "Kazakhsea", "Cadini", "Aureedonia", "Lemuriadonia",
                                                   "Harborea", "Lemuri", "Ameriador", "Congae", "Aciflheim", "Europislantis", "Cheropi", "Pannorland",
                                                   "Islandinia", "Kumariador", "Congondwana", "Yggdralia", "Kerguelysium", "Laurenticadinia", "Limbia",
                                                   "Asgarthsea", "Avalonifl", "Pacific", "Acificadia", "Asiador", "Islantis", "Kazak", "Persiumari",
                                                   "Cheron", "Califorland", "Kenorea", "Atlandini", "Harboreedon", "Interlantis", "Ledoniadonia",
                                                   "Islaure", "Gondiadoni", "Brittani", "Celedonia", "Laurentica", "Lanteropi", "Yggdrasium", "Arbour",
                                                   "Lanteria", "Laure", "Arthse", "Avaltic", "Lanterland", "Atlandinia", "Hyruledonia", "Hintislantis",
                                                   "Persium", "Siberiado", "Arcalifornia", "Atlanterland", "Ceani", "Narni", "Hinte", "Arc", "Avajotunhei",
                                                   "Arcadorea", "Yggdra", "Ecotopi", "Ygg", "Australon", "Fric", "Merica", "Afric", "Earthse",
                                                   "Rodiniador", "Acheropis", "Islantic", "Lantica", "Atland", "Nevador", "Cheropersia", "Avaj",
                                                   "Zealaharbor", "Islandi", "Acherguelia", "Hariadoni", "Hinterlandia", "Hina", "Ceandini", "Inuittania",
                                                   "Narniflheim", "Nevadia", "Frica", "Ledoniador", "Afrittain", "Zealaur", "Americalimbo", "Arbor",
                                                   "Nenarni", "Ealaharbo", "Cimmerittain", "Pacificale", "Celestiadonia", "Kumarctica", "Indinia",
                                                   "Kenorlania", "Acherokee", "Narniflheron", "Asiadoni", "Meriador", "Asgarctica", "Landiador",
                                                   "Barasiberia", "Avajotu", "Caledon", "Cherokeedonia", "Cimmeropis", "Asgarca", "Cim", "Zealangae",
                                                   "Ocea", "Austr", "Navaalbara", "Midgaea", "Niflheron", "Narniadonia", "Aurentica", "Ecotia", "Asil",
                                                   "Harbourope", "Interland", "Aurasil", "Gondwania", "Interlaur", "Kergueli", "Americ", "Freedoni", "Ely",
                                                   "Brittania", "Nevajotun", "Af", "Congaea", "Roke", "Arcticalimbo", "Americaledoni", "Califor",
                                                   "Elfherokee", "Meric", "Bri", "Interlandinia", "Laureedon", "Ka", "Lemur", "Eliadonia", "Harica", "Cel",
                                                   "Avajotun", "Arniflheim", "Hinterlaur", "Ceanticalifor", "Indiador", "Hariado", "Pann", "Arborealand",
                                                   "Vaa", "Australifo", "Niflhei", "Ealandinia", "Ocearthse", "Merokee", "Limbourope", "Yg", "Gondwan",
                                                   "Elia", "Elestia" }));
    }

    [Test]
    public void It_can_make_some_new_states()
    {
      var sampleStates = new[] { "Alabama", "Alaska", "Arizona", "Arkansas", "California", "Carolina", "Colorado", "Connecticut", "Dakota", "Delaware",
                                 "Florida", "Georgia", "Hampshire", "Hawaii", "Idaho", "Illinois", "Indiana", "Iowa", "Jersey", "Kansas", "Kentucky", 
                                 "Louisiana", "Maine", "Maryland", "Massachusetts", "Mexico", "Michigan", "Minnesota", "Mississippi", "Missouri", "Montana",
                                 "Nebraska", "Nevada", "Ohio", "Oklahoma", "Oregon", "Pennsylvania", "Rhode", "Tennessee", "Texas", "Utah", "Vermont",
                                 "Virginia", "Washington", "Wisconsin", "Wyoming", "York"};
      this.BuildWords(2, 3, true, sampleStates);
      this.Expect(this.wordBuilder.ChoiceArrayMemorySize, this.EqualTo(3452));

      var names = Enumerables.Infinite().Select(i => this.wordBuilder.BuildNextWord()).Except(sampleStates).Distinct().Take(106).ToArray();
      this.Expect(names, this.EquivalentTo(new[] { "Arylandia", "Rhod", "Nebrask", "Indiania", "Missipp", "Alask", "Oklaska", "Carolorado", "Hawashing",
                                                   "Oklaho", "Yominnessee", "Minnesouri", "Minnesot", "Consippi", "Caro", "Georegon", "Nevad",
                                                   "Idahomarylan", "Georego", "Jerse", "Vermontana", "Kentuck", "Michigania", "Minnessee", "Illinoi",
                                                   "Carolin", "Pennesota", "Florizona", "Idahoma", "Hawai", "Montucky", "Dakot", "Pennsylvan",
                                                   "Georegontan", "Hampshigan", "Mexiconsin", "Mainesot", "Nesot", "Missota", "Michiga", "Orado",
                                                   "Mainiana", "Pennsy", "Florizon", "Calif", "Californiana", "Califo", "Minne", "Delawaine", "Delask",
                                                   "Connesse", "Orego", "Missour", "Louis", "Utahomin", "Tennevad", "Washingtonne", "Kansaskansa",
                                                   "Vermontansas", "Illinoississian", "Virgin", "Alabampshire", "Arkansach", "Kansa", "Wyominne",
                                                   "Vermon", "Alifornia", "Colorad", "Califorado", "Georginia", "Massis", "Dahodelaware", "Dakotana",
                                                   "Washirginia", "Warego", "Mexichigan", "Virgini", "Michington", "Conn", "Massee", "Necticut",
                                                   "Wyominnesota", "Caroloridah", "Georginnesot", "Florid", "Oklahomin", "Hampshir", "Missachus",
                                                   "Aliforni", "Orgia", "Oklah", "Michingtonne", "Aliforniana", "Alabam", "Louiscolorida", "Ohiowashing",
                                                   "Tennsy", "Arylandian", "Califoridaho", "Minnect", "Rhodelaska", "Hampshiregon", "Massa", "Hawashire",
                                                   "Nebras", "Missis" }));
    }

    [Test]
    public void It_can_make_some_new_employees()
    {
      var sampleNames = new[] { "Aaron", "Adam", "Alan", "Alex", "Austin", "Anthony", "Brendan", "Bryan", "Charlie", "Chris", "Doug", "Jace", "James",
                                "Jason", "Jennifer", "Jessica", "Jim", "John", "Kene", "Kenny", "Keshav", "Mark", "Matthew", "Michael", "Mit", "Prabu",
                                "Randall", "Ricardo", "Robert", "Roya", "Ryan", "Sharique", "Shawn", "Skipp", "Steve", "Tavares", "Tej", "Todd", "Tom",
                                "Wright", "Yakov"};
      this.BuildWords(2, 3, true, sampleNames);
      this.Expect(this.wordBuilder.ChoiceArrayMemorySize, this.EqualTo(1488));

      var names = Enumerables.Infinite().Select(i => this.wordBuilder.BuildNextWord()).Except(sampleNames).Distinct().Take(55).ToArray();
      this.Expect(names, this.EquivalentTo(new[] { "Austi", "Adames", "Keshaw", "Rabu", "Shariqu", "Jes", "Keshawn", "Jame", "Jessic", "Brendall",
                                                   "Stev", "Rober", "Michae", "Wrigh", "Skip", "Shaw", "Stin", "Tavare", "Shavarlie", "Charli",
                                                   "Michris", "Austev", "Chriqu", "Yako", "Anthon", "Micharli", "Matthe", "Ryandall", "Jameshael",
                                                   "Randal", "Sharlie", "Royakov", "Brennifer", "Jessicard", "Chael", "Matthon", "Tavaron", "Shav",
                                                   "Kennifer", "Bryakov", "Ricard", "Kesharli", "Chricard", "Bryako", "Bryanthony", "Prab", "Sharight",
                                                   "Ryanthony", "Shavare", "Jessicares", "Tavarli", "Charique", "Jennife", "Matthony", "Mic" }));
    }
  }
}
