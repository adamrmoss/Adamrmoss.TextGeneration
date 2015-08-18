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
        "Acheron", "Africa", "America", "Arborea", "Arcadia", "Arctica", "Asgard", "Asia", "Atlantica", "Atlantis",
        "Australia", "Avalonia", "Baltica", "Brittain", "Caledonia", "California", "Celestia", "Cherokee", "China",
        "Cimmeria", "Cincinnati", "Columbia", "Congo", "EarthSea", "Echo", "Ecotopia", "Elfheim", "Elysium",
        "Eriador", "Europa", "Europe", "Freedonia", "Gondwana", "Harbor", "Harbour", "Hinterland", "Hyrule",
        "India", "Inuit", "Islandia", "Jotunheim", "Kalaharia", "Kazakhstania", "Kenorland", "Kerguelia",
        "Kumaria", "Kush", "Laurasia", "Laurentia", "Lemuria", "Limbo", "Meropis", "Midgard", "Narnia", "Navajo",
        "Nena", "Nevada", "Niflheim", "Oceania", "Pacifica", "Pangaea", "Pannotia", "Persia", "Rodinia", "Siberia",
        "Siniscalchi", "Utah", "Vaalbara", "Yangon", "Zealandia"
      };
      this.BuildWords(2, 3, true, sampleCountryNames);
      this.Expect(this.wordBuilder.ChoiceArrayMemorySize, this.EqualTo(4988));

      var names = Enumerables.Infinite().Select(i => this.wordBuilder.BuildNextWord()).Except(sampleCountryNames).Distinct().Take(265).ToArray();
      this.Expect(names, this.EquivalentTo(new[] {
        "Arnia", "Eliador", "Earthse", "Kumarctica", "Kenor", "Kalaharctica", "Ropangondwana", "Zealaur", "Kalah",
        "Arctic", "Kenorlan", "Baltic", "Rodinuit", "Cimmeriador", "Limborea", "Haria", "Cale", "Kergueli",
        "Kerguelimb", "Siniscalifor", "Arthsea", "Congondia", "Arcadi", "Valonia", "Ea", "Celestralia", "Barasia",
        "Siberica", "Yangondia", "Merica", "Atlantic", "Cimmericale", "Europis", "Hinterlandia", "In",
        "Chinarnia", "Hyrul", "Nevaaledon", "Avalongond", "Kumari", "Niscalchina", "Atlangon", "Australifo",
        "Rodiniscalc", "Cele", "Cimmer", "Ameriador", "Jotunhei", "Avajo", "Harborea", "Africadinia", "Pacifor",
        "Acheropis", "Niflheimborea", "Atlango", "Eriadia", "Persinis", "Erongondia", "Hariador", "Meric",
        "Elfheimbour", "Cinnat", "Gondiador", "Asgarbo", "Europia", "Pacificadia", "Sinis", "Kalaharbou",
        "Cinnotunheim", "Kazakhst", "Persiberia", "Cheropis", "Rope", "Na", "Pacific", "Navada", "Celestiadonia",
        "Yango", "Brit", "Cimmeri", "Arniflhei", "Cinnavalonia", "Utaharbour", "Is", "Celemuriador", "Jo",
        "Zealandi", "Siniscalon", "Celesticale", "Ameriadorea", "Islannotia", "Sium", "Ameriado", "Laharbour",
        "Atlanticale", "Island", "Afrittai", "Brittai", "Niflhei", "Midgar", "Arbou", "Elysinis", "Kenorlahar",
        "Australedonia", "Pannotica", "Ealandia", "Lemuri", "Pe", "Calchero", "Ecotopiador", "Hyrulemuria",
        "Sibersinisca", "Kazakhsta", "Elfherokee", "Iscalemuria", "Avalon", "Lemur", "Ecotopi", "Celestica",
        "Persiador", "Pangard", "Cheron", "Freedonisc", "Australifor", "Jotunherope", "Arborland", "Americ",
        "Islandiador", "Jotunhe", "Meropi", "Paciforniador", "Pacifi", "Nevador", "Zealantic", "Ocea", "Vaalongo",
        "Chersia", "Merittain", "Eric", "Calestrasia", "Celestiador", "Limbiador", "Oceannorla", "Midgarnisca",
        "Kenotica", "Ropis", "Lemuropia", "Chinavada", "Cific", "Brittania", "Lemurenor", "Pangon", "Siniscalc",
        "Le", "Nevalonia", "Ropi", "Inuittania", "Persinisc", "Utaharborea", "Indiador", "Pacificalchi",
        "Kumarbou", "Columbi", "Hina", "Ealandinia", "Vajotunheim", "Yangae", "Meropisca", "Oceandia", "Zealoni",
        "Balticalchintis", "Cherodiniador", "Landi", "Fric", "Afriador", "Vaalbaralia", "Nevajo", "Congond",
        "Cheriador", "Aciflheim", "Eriado", "Islanticale", "Paciflhe", "Lemurentia", "Rokee", "Kumarbo", "Afric",
        "Cheroke", "Meria", "Atlandia", "Elfhei", "Asgaeania", "Columbiador", "Yangondwana", "Zealaharborea",
        "Eurodinia", "Ealantis", "Rodindia", "Inna", "Cinnatica", "Kerguelestia", "Islangon", "Narnisc",
        "Narniscalc", "Pacinnati", "Ameriad", "Elestica", "Ceandia", "Amerlandini", "Sinisca", "Calific",
        "Zealahar", "Ca", "Lantiador", "Inuittain", "Atlaharbo", "Acherope", "Liforniador", "Nenatica",
        "Achersinis", "Chiniadonia", "Gondwandia", "Yangond", "Kerguel", "Incifica", "Kumariadonia", "Landiador",
        "Ameropi", "Oceaniador", "Limbourenti", "Siniscalantis", "Atlangae", "Harbo", "Lemurasia", "Oceango",
        "Harbou", "Niscalchi", "Paciflheim", "Go", "Elysiu", "Navaj", "Liforni", "Cincinnotun", "Rodin",
        "Cifica", "Cincinnoti", "Arbour", "Cimmeriadonia", "Congondwanti", "Aurasiadonia", "Kenorea",
        "Rokerguel", "Califica", "Islangae", "Hinter", "Kumarborea", "Frica", "Persiniflhei", "Atlandinia",
        "Siniscadia", "Atlangaea", "Nenavada", "Hinterlan", "Amerittain", "Kenotia" 
      }));
    }

    [Test]
    public void It_can_make_some_new_states()
    {
      var sampleStates = new[] {
        "Alabama", "Alaska", "Arizona", "Arkansas", "California", "Carolina", "Colorado", "Connecticut", "Dakota",
        "Delaware", "Florida", "Georgia", "Hampshire", "Hawaii", "Idaho", "Illinois", "Indiana", "Iowa", "Jersey",
        "Kansas", "Kentucky", "Louisiana", "Maine", "Maryland", "Massachusetts", "Mexico", "Michigan",
        "Minnesota", "Mississippi", "Missouri", "Montana", "Nebraska", "Nevada", "Ohio", "Oklahoma", "Oregon",
        "Pennsylvania", "Rhode", "Tennessee", "Texas", "Utah", "Vermont", "Virginia", "Washington", "Wisconsin",
        "Wyoming", "York"
      };
      this.BuildWords(2, 3, true, sampleStates);
      this.Expect(this.wordBuilder.ChoiceArrayMemorySize, this.EqualTo(3452));

      var names = Enumerables.Infinite().Select(i => this.wordBuilder.BuildNextWord()).Except(sampleStates).Distinct().Take(106).ToArray();
      this.Expect(names, this.EquivalentTo(new[] {
        "Illindiana", "Pennsylvaniana", "Missour", "Arizon", "Pennesot", "Massachu", "Tennessett", "Ka", "Vermon",
        "Caroloregon", "Oklahomaine", "Massachigan", "Hampshir", "Indian", "Tennes", "Waii", "Tenne", "Montan",
        "Virgingtonsin", "Nebrask", "Nesotana", "Da", "Idahoma", "Iowashire", "Carolifornia", "Nevad", "Mexicutah",
        "Orego", "Arkan", "Mexic", "Iowashin", "Wa", "Orad", "Co", "Montucky", "Utahoma", "Tennessetts",
        "Marylandiana", "Alask", "Ohiowa", "Pennsaskansas", "Marylvania", "Innessetts", "Minnesot", "Louisian",
        "Louisiania", "Vermontan", "Wiscon", "Kentuck", "Lorad", "De", "Georgiana", "Virgini", "Vermontana",
        "Wisconne", "Louis", "Ware", "Alabampshir", "Califoridah", "Pennsylva", "Californi", "Maindiana",
        "Idahomain", "Delawaii", "Georni", "Michington", "Oklawar", "Georad", "Washiregon", "Missipp", "Virgiana",
        "Hampshiregon", "Virgin", "Pennsylvan", "Maryla", "Oklahomington", "Iowaregon", "Idahomainnesota",
        "Hampshing", "Marylva", "Alabamain", "Washire", "Georginia", "Aliforni", "Iowaii", "Pennsylv", "Pennsin",
        "Wisconsi", "Virginiana", "Louisco", "Iniana", "Yorkansas", "Michigansas", "Louisiansas", "Carolinois",
        "Dakotana", "Califorida", "Rhodelahode", "Virginois", "Caroli", "Waregon", "Illiforni", "Kentaniana",
        "Idahodelaware", "Idahode", "Kent"
      }));
    }

    [Test]
    public void It_can_make_some_new_employees()
    {
      var sampleNames = new[] {
        "Aaron", "Adam", "Alan", "Alex", "Andrew", "Anthony", "Austin", "Brendan", "Brent", "Bryan", "Charlie",
        "Chris", "Doug", "Emory", "Jace", "James", "Jason", "Jennifer", "Jessica", "Jim", "John", "Kene",
        "Kenny", "Keshav", "Kevin", "Kyle", "Lynette", "Mark", "Matthew", "Michael", "Mit", "Prabu", "Randall",
        "Ricardo", "Richard", "Robert", "Rodney", "Roya", "Ryan", "Sharique", "Shawn", "Skipp", "Steve",
        "Steven", "Tavares", "Tej", "Tim", "Todd", "Tom", "Trent", "Will", "Wright", "Yakov"
      };
      this.BuildWords(2, 3, true, sampleNames);
      this.Expect(this.wordBuilder.ChoiceArrayMemorySize, this.EqualTo(1936));

      var names = Enumerables.Infinite().Select(i => this.wordBuilder.BuildNextWord()).Except(sampleNames).Distinct().Take(55).ToArray();
      this.Expect(names, this.EquivalentTo(new[] { 
        "Jennif", "Emoryan", "Ricardoug", "Rique", "Bryandrew", "Trennifer", "Tavare", "Tavardo", "Michar",
        "Emor", "Tavarique", "Matthony", "Randal", "Lynett", "Richae", "Jessic", "Jame", "Micha", "Ya",
        "Austi", "Rabu", "Tavar", "Michae", "Lynetteven", "Matthe", "Shaw", "Rober", "Riqu", "Adameshav",
        "Sharicha", "Brendall", "Jessicardo", "Jameshav", "Chard", "Matthon", "Wrigh", "Trenthony", "Andalan",
        "Michard", "Sharight", "Jamessicard", "Rodne", "Lynetthew", "Keshariq", "Randre", "Anthon", "Charon",
        "Prab", "Rodnette", "Ryanthon", "Bren", "Andall", "Tavariqu", "Micharlie", "Lynetteve"
      }));
    }
  }
}
