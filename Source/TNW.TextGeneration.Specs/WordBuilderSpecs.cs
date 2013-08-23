using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TNW.TextGeneration.Specs
{
  [TestFixture]
  public class WordBuilderSpecs : AssertionHelper
  {
    private WordAnalyzer WordAnalyzer;
    private WordBuilder WordBuilder;
    private IEnumerable<string> Names;

    private void BuildWords(int minSubwordLength, int maxSubwordLength, params string[] words) {
      this.WordAnalyzer = new WordAnalyzer {
        MinSubwordLength = minSubwordLength,
        MaxSubwordLength = maxSubwordLength,
      };
      this.WordAnalyzer.Analyze(words);

      this.WordBuilder = new WordBuilder(this.WordAnalyzer);
      this.Names = this.WordBuilder.Build();
    }

    [Test]
    public void It_scrambles_mad_sad_bananas_and_damn_cocoa()
    {
      // NOTE: This training set is poor, not enough unique combinations of subwords.  Even when allowing for
      //       1 character subwords, we can only generate 47 new words, several of which have the characteristic
      //       awkward consonant clusters we see with 1 character subwords
      this.BuildWords(1, 4, "mad", "sad", "bananas", "and", "damn", "cocoa");
      Expect(this.WordBuilder.ChoiceArrayMemorySize, EqualTo(544));

      var words = this.Names.Take(50).ToArray();
      Expect(words.Length, EqualTo(47));
      Expect(words, EquivalentTo(new[] { "mada", "bana", "sada", "ban", "coa", "coco", "dana", "banana", "damnanas", "coan", "ananas", "amad",
                                         "mnandamn", "dam", "coc", "amn", "sananas", "mnana", "banan", "bandamn", "danan", "coanas", "adam", 
                                         "banananas", "band", "mand", "bananda", "ada", "manan", "anananana", "cocoananas", "bad", "damnana",
                                         "mndamn", "bananana", "banas", "samn", "bananan", "ana", "coadamn", "mnan", "madam", "cococo", "sam",
                                         "mas", "coad", "sas" }));
    }

    [Test]
    public void It_can_make_some_new_continents_and_oceans()
    {
      this.BuildWords(1, 4, "Africa", "America", "Asia", "Arctic", "Atlantic", "Australia", "Europe", "Indian", "Pacific");
      Expect(this.WordBuilder.ChoiceArrayMemorySize, EqualTo(2044));

      var names = this.Names.Take(400).Select(name => name.Capitalize()).ToArray();
      // NOTE: This training set is rich enough for 363 names
      Expect(names.Length, EqualTo(363));
      Expect(names, EquivalentTo(new[] { "Acifi", "Asiant", "Eurica", "Asicifi", "Atla", "Andian", "Australi", "Euro", "Arcandian", "Indiantic",
                                         "Acificifi", "Arcticat", "Antic", "Austral", "Atlantica", "Austra", "Alantictral", "Americ", "Ictic", 
                                         "Asific", "Atlantlant", "Atlandi", "Ifictlant", "Austrcti", "Asiamer", "Pacifica", "Afrctindi", 
                                         "Arctrali", "Pacifric", "Indiant", "Pacifi", "Pacifictic", "Atlanti", "Aust", "Atlant", "Aliaustr", 
                                         "Ameria", "Indiacifi", "Acifind", "Icifia", "Paciaustr", "Arctif", "Afrarcti", "Afrind", "Indiandia",
                                         "Europacif", "Asiandian", "Pactic", "Arctla", "Atlantral", "Austr", "Indi", "Atindian", "Atic", "Arctict",
                                         "Arctia", "Atlandia", "Arctifi", "Afrafric", "Eurctic", "Austlan", "Perica", "Africic", "Arctindia", 
                                         "Austrop", "Euralia", "Europerica", "Europaustr", "Iantrali", "Paci", "Arctica", "Asialia", "Acifica", 
                                         "Afri", "Australic", "Amer", "Pacafrica", "Africarc", "Atlafrica", "Acific", "Austlant", "Asica", 
                                         "Icififi", "Alia", "Acificific", "Amerand", "Auropacif", "Asiaust", "Eric", "Afrcifi", "Europeur", 
                                         "Erica", "Pacifr", "Anticaustr", "Icticific", "Americacif", "Atlatl", "Anticific", "Europaci", "Asiameri", 
                                         "Arctin", "Pacifrica", "Ericandia", "Arctian", "Amercti", "Afriali", "Asiarct", "Indiafr", "Austlanti",
                                         "Pacificatla", "Australan", "Ificaf", "India", "Atlifrica", "Afrific", "Euratla", "Amerific", "Pacifific", 
                                         "Aurope", "Asiastral", "Atral", "Arope", "Amerind", "Africandi", "Euri", "Indicific", "Asiandia",
                                         "Aricaustr", "Indicif", "Pacif", "Austrauro", "Indiasia", "Europ", "Asiacif", "Paciameri", "Ameralia", 
                                         "Indialia", "Africaustr", "Pacififrica", "Alanticifi", "Pacind", "Acificara", "Africindi", "Iarcti", 
                                         "Pacificifi", "Peur", "Americifi", "Asiafri", "Indica", "Alanti", "Ictlantic", "Iandian", "Africarcti",
                                         "Pacificific", "Alalia", "Arct", "Americaustr", "Austrct", "Antralameri", "Africame", "Africif",
                                         "Asiantic", "Atlacifi", "Peurope", "Iacticti", "Afica", "Indictic", "Andiacif", "Africific", "Aropeurop", 
                                         "Pacafr", "Icticifi", "Indiaustr", "Andiarcti", "Atlaust", "Indiatl", "Europa", "Afric", "Asiandi", 
                                         "Africtica", "Arica", "Indicantic", "Indifi", "Eurcticti", "Americti", "Europeuro", "Icific",
                                         "Arctlantic", "Asiacifi", "Ameustr", "Arcticti", "Iaustrali", "Atlandian", "Pafric", "Asiameric",
                                         "Iantic", "Austranti", "Pacicif", "Atland", "Atlian", "Ameur", "Ausiandian", "Alantind", "Americant", 
                                         "Ificific", "Arctralia", "Atliciastra", "Arctra", "Americif", "Africandian", "Acifici", "Arctici",
                                         "Indiameri", "Aliame", "Africi", "Eurcti", "Iatlantic", "Pacifra", "Indianti", "Paustral", "Afrctic", 
                                         "Atlantrct", "Arcticific", "Austraci", "Ericatlaf", "Perali", "Anticin", "Americarc", "Peurop", "Asiand",
                                         "Africam", "Erific", "Africali", "Indicti", "Pacifict", "Africti", "Africarct", "Ica", "Arcifiasi", 
                                         "Arcticif", "Pericantic", "Europac", "Pameur", "Acicatlan", "Arctictic", "Pauro", "Arctific", "Europanti", 
                                         "Andianti", "Amerian", "Arctlant", "Europeurop", "Iameric", "Indiandian", "Atlatlan", "Ameurop", "Antica", 
                                         "Pacificic", "Amerindia", "Indiarcti", "Atlan", "Austrope", "Antrcti", "Americatl", "Pacicifi", "Aficali",
                                         "Asicific", "Atlacia", "Amerctic", "Arcti", "Ausi", "Pasiarc", "Atlarctic", "Afrope", "Acalant", 
                                         "Eralifri", "Atlanticifi", "Amerindi", "Asiaustr", "Atlanticif", "Pacicind", "Ericaf", "Arctindian", 
                                         "Indindia", "Arcific", "Africarctic", "Indiam", "Pacindia", "Atlantici", "Aficacifi", "Australanti", 
                                         "Africifi", "Alanticif", "Africatl", "Austrctic", "Africafric", "Atlindian", "Amerope", "Americtlant", 
                                         "Pacificau", "Pacifind", "Aliarcti", "Atlafri", "Europeu", "Africara", "Atlantlan", "Atlantian", 
                                         "Europeurope", "Acifindia", "Erope", "Atlarc", "Pacificti", "Icicific", "Arctindi", "Africalia", 
                                         "Ameriantic", "Pantic", "Arctlanti", "Pacifricif", "Ameropa", "Ameri", "Americific", "Iameri", "Andindia",
                                         "Atrala", "Ericandian", "Ausicica", "Anticif", "Eust", "Aliatl", "Atlameric", "Antictic", "Africasia", 
                                         "Pacifif", "Atlaustral", "Aliarc", "Asianticific", "Andindi", "Ericanti", "Austifi", "Amerafri", 
                                         "Arcticaci", "Perctic", "Alianticti", "Asianti", "Arcifi", "Pacica", "Atlasia", "Pacificif", "Afrct", 
                                         "Indiantlan", "Europeric", "Indiaust", "Anticifi", "Asiacific", "Europer", "Atlamer", "Ameuro", "Iafrica", 
                                         "Australant", "Andiantrica", "Asiafrica", "Americaf" }));
    }

    [Test]
    public void It_can_make_some_new_states()
    {
      this.BuildWords(2, 4, "Alabama", "Alaska", "Arizona", "Arkansas", "California", "Carolina", "Colorado", "Connecticut", "Dakota", "Delaware", 
                       "Florida", "Georgia", "Hampshire", "Hawaii", "Idaho", "Illinois", "Indiana", "Iowa", "Jersey", "Kansas", "Kentucky", 
                       "Louisiana", "Maine", "Maryland", "Massachusetts", "Mexico", "Michigan", "Minnesota", "Mississippi", "Missouri", "Montana",
                       "Nebraska", "Nevada", "Ohio", "Oklahoma", "Oregon", "Pennsylvania", "Rhode", "Tennessee", "Texas", "Utah", "Vermont",
                       "Virginia", "Washington", "Wisconsin", "Wyoming", "York");
      Expect(this.WordBuilder.ChoiceArrayMemorySize, EqualTo(5852));

      var names = this.Names.Take(500).Select(name => name.Capitalize()).ToArray();
      Expect(names.Length, EqualTo(479));
      Expect(names, EquivalentTo(new[] { "Illinoi", "Arolora", "Georginia", "Massachuse", "Missipp", "Florid", "Missour", "Connectic", "Louisia",
                                         "Massach", "Tennesse", "Minnesot", "Arkans", "Lorida", "Mexiconnec", "Virgi", "Mexiconsi", "Massou", 
                                         "Hampshingt", "Orego", "Dakot", "Alifor", "Vermontan", "Michiga", "Vermo", "Minnesotana", "Washire", 
                                         "Carolin", "Rhod", "Pennes", "Marylaska", "Wisconsi", "Carolorad", "Montuck", "Dakotana", "Pennsyl", 
                                         "Floridaho", "Mexiconsin", "Michig", "Delask", "Kansa", "Oklah", "Nebraskansas", "Hampshir", "Alifornia",
                                         "Michigansa", "Carolino", "Minne", "Missis", "Hawai", "Connebraska", "Wisco", "Mexicutah", "Wyomin", 
                                         "Hampshin", "Consassac", "Louis", "Vermonnect", "Wisconsiansa", "Kansaskansa", "Kentuc", "Vermontana", 
                                         "Pennsylva", "Washing", "Nevad", "Kansaska", "Connect", "Alabampshi", "Kansylan", "Delawar", "Indiansas", 
                                         "Rhodelaw", "Nebr", "Vermon", "Montucky", "Califor", "Mass", "Delawa", "Colorad", "Missachu", "Hampshi", 
                                         "Georgiana", "Michi", "Pennsylvan", "Michingto", "Calif", "Maryl", "Minnesse", "Vermontuc", "Missou", 
                                         "Pennsinne", "Kentuck", "Orginia", "Orgia", "Alask", "Florizon", "Tennec", "Marylahoma", "Floridahoma", 
                                         "Virgingto", "Tennesot", "Oklaho", "Massachu", "Alaba", "Nesota", "Marylania", "Delawashin", "Nebrask", 
                                         "Color", "Delawarkansa", "Arolin", "Washinia", "Californi", "Tennsyl", "Misso", "Arylan", "Illi", "Nebra", 
                                         "Alabam", "Illindian", "Mississ", "Minnessee", "Montan", "Washinneso", "Utahomin", "Arizon", 
                                         "Hampshiregon", "Massa", "Marylansas", "Washin", "Caroli", "Virginneso", "Pennec", "Louisi", "Wyomi", 
                                         "Colorida", "Louisian", "Carolinoi", "Kansashington", "Tennesour", "Mississi", "Maryla", "Penns", 
                                         "Virgini", "Nessissi", "Nectic", "Wiscon", "Floregon", "Georgi", "Wiscolora", "Indian", "Nebrassac", 
                                         "Penn", "Idahoma", "Nesse", "Mary", "Florego", "Alif", "Connecticu", "Aryla", "Nevania", "Nesot", "Consi",
                                         "Georegon", "Colorego", "Pennsylv", "Californ", "Wyominoi", "Aryland", "Miss", "Washingt", "Virginiana", 
                                         "Vermonnec", "Alassac", "Wyomichig", "Tenn", "Mississipp", "Mainebraska", "Washingto", "Kansassachus", 
                                         "Delawariz", "Jerse", "Mainesota", "Califo", "Wisconsylv", "Lorad", "Illiniana", "Pennsylvani", 
                                         "Pennesota", "Hawaine", "Wisconne", "Consin", "Mingto", "Conn", "Kansach", "Verm", "Mainneso", "Nebrado", 
                                         "Nebrashingt", "Minneso", "Tenness", "Arkansassach", "Arylandian", "Oklahode", "Oklahomassac", "Consylv", 
                                         "Arol", "Arkansin", "Alassachu", "Delawaroli", "Kansindian", "Georado", "Mexichiga", "Pennsy", 
                                         "Utahominia", "Delawaii", "Colo", "Hampshirego", "Kansin", "Calina", "Californiana", "Hawaregon", 
                                         "Monta", "Orgini", "Texassac", "Nessouisia", "Alabampsh", "Mississip", "Wyomington", "Florizona", 
                                         "Wiscons", "Pennsington", "Utahom", "Carolinois", "Connebra", "Wyominneso", "Marylvania", "Inoi", 
                                         "Floridahode", "Connecti", "Illiforn", "Carol", "Vermonsin", "Arkansa", "Colora", "Texa", "Wisc",
                                         "Indianiana", "Dela", "Illino", "Consissip", "Alabamassac", "Oklahoming", "Massouisia", "Kansachuset",
                                         "Okla", "Hamp", "Arolorado", "Marylan", "Wyominne", "Illinoisian", "Mainecti", "Arkansashingto", 
                                         "Missiana", "Texaskansas", "Missotana", "Texashingto", "Delaska", "Hamps", "Inois", "Florad", "Arizo",
                                         "Idahomassa", "Washirginia", "Jersett", "Michiganiana", "Oklahominnes", "Floridah", "Massachus", 
                                         "Missippi", "Tennes", "Vermonne", "Consaska", "Michigansachu", "Illinoisiana", "Rhodelawa", "Missachuse",
                                         "Kansassachu", "Alabamps", "Louisippi", "Georidaho", "Necticu", "Carolinne", "Delawarego", "Nesouri", 
                                         "Idahomaine", "Washi", "Texashin", "Oklahomington", "Mainesse", "Florizo", "Montansas", "India", 
                                         "Ohiowaregon", "Oridahoma", "Kansassa", "Washinois", "Oklawaii", "Michigansas", "Pennsylvaniana", 
                                         "Nevadaho", "Idahomaryl", "Conne", "Mainiana", "Geor", "Georgini", "Necticut", "Arkaniana", 
                                         "Marylandia", "Minnes", "Missourizon", "Lorado", "Arka", "Louississipp", "Florado", "Iowaii", "Wyomiss",
                                         "Delahoma", "Deland", "Wyominnessee", "Virgin", "Coloui", "Massac", "Waii", "Virg", "Georgindi",
                                         "Missississip", "Arizonsin", "Kent", "Delabama", "Connecticutah", "Consissi", "Alaskansa", 
                                         "Wyomissourizona", "Hampshingto", "Oklahom", "Nesour", "Hampshirgi", "Illington", "Virgingt", 
                                         "Vermontansa", "Mexiconsas", "Idahomassac", "Kansassach", "Marylaba", "Yoming", "Florginia", "Mexi",
                                         "Hawashingt", "Aliforni", "Wisconnebra", "Kansachuse", "Michusetts", "Washiga", "Alashing", 
                                         "Dahomassachuse", "Illin", "Alashin", "Alaskans", "Missisconsingto", "Wyominnesot", "Kentu", 
                                         "Wyominnecticut", "Arolina", "Ohiowa", "Yorkan", "Hampshirgini", "Carolindian", "Delawash", "Carolorgi", 
                                         "Colornia", "Yomington", "Wiscolouis", "Kentan", "Mont", "Lorid", "Missourizona", "Oridaho", "Marylabama",
                                         "Massippi", "Kans", "Tennectic", "Rhodelaware", "Nevani", "Missississ", "Massachuset", "Connes", "Orni",
                                         "Alas", "Marylaho", "Yorkansas", "Minnecticut", "Coloridaho", "Tennecti", "Mexicolo", "Minnessett", 
                                         "Colouisi", "Delawaine", "Iowashirgi", "Ohiowashi", "Wiscontana", "Oklawar", "Connesota", "Penness", 
                                         "Ingto", "Hampsh", "Missee", "Mich", "Connessee", "Hawashin", "Louississ", "Texassa", "Mexic", 
                                         "Coloregon", "Washiregon", "Vermontansach", "Idahomin", "Jersetts", "Georad", "Illinnessee", "Loridaho",
                                         "Arkansylv", "Haware", "Ariz", "Missississi", "Orad", "Pennsaska", "Rhodela", "Missiansa", "Carolifo",
                                         "Virgiana", "Iniana", "Nevadakota", "Coloraska", "Nessour", "Floridakot", "Arylandiana", "Missi", 
                                         "Kansachus", "Georg", "Hampshing", "Aliforn", "Floridakota", "Georgington", "Mainevada", "Wyom", 
                                         "Tennessis", "Innesot", "Aliforad", "Nessach", "Delawashi", "Missisiana", "Oregonne", "Arolinneso", 
                                         "Alabamassa", "Aliforgia", "Innecti", "Texassachu", "Missachuset", "Kansachiga", "Missouiscon", 
                                         "Innessee", "Wyominnes", "Kansylva", "Rhodelabama", "Colorask", "Marylaware", "Tennect", "Caro" }));
    }

    [Test]
    public void It_can_make_some_new_employees()
    {
      this.BuildWords(1, 3, "Adam", "Alan", "Anthony", "Brendan", "Chris", "Doug", "Jace", "James", "Jason", "Jennifer", "Jessica", "John", "Kene",
                       "Kenny", "Keshav", "Mark", "Matthew", "Randall", "Ricardo", "Ryan", "Sharique", "Steve", "Tej", "Todd", "Tom");
      Expect(this.WordBuilder.ChoiceArrayMemorySize, EqualTo(2908));

      var names = this.Names.Take(1000).Select(name => name.Capitalize()).ToArray();
      Expect(names.Length, EqualTo(918));
      Expect(names, EquivalentTo(new[] { "Adama", "Shar", "Kenn", "Jalan", "Bren", "Kejes", "Brenny", "Jesha", "Menny", "Richr", "Jessic", 
                                         "Alannife", "Shacariq", "Ariquesha", "Kenndan", "Adandoug", "Jen", "Call", "Andala", "Stenny", "Jamar", 
                                         "Kerica", "Rand", "Stevejac", "Tomat", "Ardodd", "Chricatth", "Shave", "Matenica", "Mari", "Jassi", 
                                         "Anda", "Cark", "Jamatt", "Renni", "Jomark", "Dony", "Jenni", "Stesha", "Amessic", "Danthony", "Andal",
                                         "Keny", "Chri", "Jame", "Mar", "Mandall", "Randalav", "Matt", "Sharis", "Atthew", "Brenn", "Matthe", 
                                         "Jashar", "Kennthon", "Sonyant", "Jess", "Shav", "Teve", "Kenyanth", "Jes", "Chariqu", "Rdala", 
                                         "Brenda", "Jaso", "Jesssic", "Anthones", "Matth", "Adarantho", "Macard", "Aso", "Shari", "Jas", "Johon", 
                                         "Ster", "Adannife", "Alanyan", "Ant", "Adameve", "Jennif", "Jardoda", "Candall", "Shada", "Bryandan", 
                                         "Ricard", "Brend", "Kesha", "Anth", "Stev", "Jesiqu", "Ric", "Alandda", "Rica", "Ricar", "Kenif", 
                                         "Jesica", "Randa", "Adame", "Anthew", "Jacener", "Cardoug", "Meve", "Randanda", "Rdoue", "Chrique",
                                         "Randal", "Tomatt", "Rant", "Chrife", "Sisha", "Alllanda", "Adames", "Johohn", "Thev", "Jessi", "Amat", 
                                         "Amariq", "Kenntho", "Ssic", "Jardodoug", "Jama", "Ranif", "Thav", "Sten", "Ark", "Jarandall", "Antho", 
                                         "Jacenni", "Calan", "Meshav", "Sthav", "Kendan", "Kesh", "Chrk", "Jeshardo", "Rique", "Kenni", "Jacene",
                                         "Keshari", "Alanife", "Chricar", "Dandal", "Jasonnda", "Asondall", "Jew", "Tod", "Ryanni", "Thew", 
                                         "Brendar", "Rark", "Teshariq", "Jamat", "Andan", "Anto", "Son", "Rani", "Sife", "Johnn", "Jamessic", 
                                         "Alarife", "Keshannn", "Sohn", "Shonyan", "Ardo", "Jenntej", "Ala", "Brendal", "Brejark", "Jamew", 
                                         "Thryan", "Shariqu", "Chr", "Doueryan", "Jasonife", "Ricala", "Thonyan", "Adandan", "Jod", "Adard", 
                                         "Sharanda", "Mevew", "Arkes", "Randan", "Mason", "Shark", "Jacew", "Brendall", "Adan", "Matoddod", 
                                         "Ranni", "Jesh", "Mathew", "Bre", "Ttho", "Adamatth", "Marken", "Johew", "Shariq", "Jamesony", 
                                         "Brendave", "Anthonya", "Jacenda", "Thari", "Matod", "Rameste", "Dontodd", "Ran", "Jenendan", "Tthend", 
                                         "Rkenn", "Resh", "Riquenny", "Jamestom", "Ste", "Ariqu", "Rdou", "Chriq", "Jark", "Breshav", "Jaman", 
                                         "Antej", "Brene", "Kes", "Thon", "Jacendall", "Rdo", "Shames", "Stevenif", "Jennife", "Anthon", 
                                         "Alanyar", "Marica", "Reny", "Douenny", "Kesonyames", "All", "Kend", "Shandan", "Rannnifer", "Kenniqu", 
                                         "Ames", "Antodd", "Adoug", "Adamames", "Dan", "Kendo", "Sic", "Anthr", "Mard", "Sodd", "Mariqu", 
                                         "Jasoh", "Mes", "Tthew", "Mattho", "Kenyan", "Alar", "Ricariq", "Jenny", "Randandal", "Kennife", 
                                         "Maric", "Shastev", "Tew", "Jessices", "Ricarkesh", "Brendand", "Jessicant", "Kesic", "Adamark", 
                                         "Jacenny", "Anew", "Jessicar", "Sshn", "Toma", "Jacard", "Matttom", "Jameshris", "Jesssi", "Sica", 
                                         "Shaveve", "Jasonyan", "Adamess", "Brya", "Anifer", "Anthoni", "Carkesh", "Alla", "Avennife", "Malan",
                                         "Jacev", "Rdalant", "Rdoug", "Dandoug", "Stevesh", "Jendohri", "Jeson", "Stevend", "Jamariq", "Sondan",
                                         "Joh", "Randark", "Toha", "Todonnif", "Canda", "Ssicard", "Manne", "Dalan", "Kenda", "Jamesshar", 
                                         "Sonyadam", "Ranifer", "Shac", "Jessicard", "Bressica", "Jamenni", "Steverif", "Jasoni", "Chricardo", 
                                         "Mattej", "Jeshav", "Jarisha", "Jest", "Shardo", "Brdo", "Todall", "Aric", "Teni", "Mariques", 
                                         "Adanifer", "Kessi", "Jacennif", "Ryanda", "Mattthe", "Antom", "Adantho", "Ricadam", "Shason", 
                                         "Ryandall", "Anthasic", "Jenndan", "Sicardo", "Jacessic", "Tomes", "Shardalan", "Matthony", "Cardo", 
                                         "Kennnis", "Shon", "Brennif", "Rdal", "Jesi", "Dodason", "Shal", "Mat", "Ricariqu", "Keneve", "Arala", 
                                         "Dou", "Sicard", "Jodd", "Adamendal", "Sifer", "Adal", "Jandond", "Matthri", "Jamessica", "Alandames", 
                                         "Richris", "Mariq", "Javenny", "Riqu", "Brendandan", "Tevess", "Ricaris", "Rkennyan", "Jamessi", 
                                         "Rarique", "Tome", "Avenyalan", "Douessi", "Matom", "Manthew", "Sodark", "Stenda", "Matho", "Riss", 
                                         "Kentthe", "Jamalan", "Jonn", "Jasic", "Todou", "Dardo", "Kenend", "Mardo", "Siferiqu", "Mener", 
                                         "Tevesha", "Jasonya", "Marique", "Ddda", "Amany", "Jatt", "Sharkene", "Jasonndal", "Shaveri", "Jam",
                                         "Ason", "Jonnifer", "Andall", "Kennyark", "Ryanthe", "Sonif", "Matodd", "Jennnnif", "Dadall", "Tejen", 
                                         "Jenn", "Randdda", "Ace", "Ryand", "Bresi", "Rantho", "Thessic", "Amarken", "Keshariq", "Todant", 
                                         "Jacam", "Ryardoma", "Janya", "Chari", "Todan", "Brenya", "Ricarda", "Matthon", "Jasodd", "Jacal", 
                                         "Jend", "Ricatom", "Jasssica", "Riquesha", "Mary", "Andam", "Ddoug", "Jasha", "Kenistev", "Jachri", 
                                         "Chre", "Matej", "Jameshav", "Jarantho", "Jacar", "Johnif", "Arica", "Mateve", "Jac", "Jamesha", 
                                         "Alamark", "Stevess", "Jamesshav", "Andatth", "Jamestod", "Ryalan", "Jashav", "Jasomark", "Sony", 
                                         "Ariqug", "Jariqu", "Adamary", "Ryandd", "Tejan", "Jacesic", "Tthe", "Ricardou", "Jon", "Stevenni", 
                                         "Shatthe", "Jenndon", "Sshav", "Kessica", "Brdon", "Alanth", "Bran", "Thodd", "Kenyaris", "Ris", 
                                         "Sicarken", "Andames", "Stej", "Mattohn", "Brendam", "Toddatt", "Jennicar", "Alanthon", "Keshani", 
                                         "Alant", "Jessh", "Jall", "Allan", "Jasony", "Randamark", "Ricames", "Javess", "Keniferiq", "Damesh", 
                                         "Cadam", "Masha", "Thony", "Kessic", "Keve", "Brennny", "Sharicar", "Anthav", "Tonife", "Thricada",
                                         "Rend", "Mattthony", "Alllandal", "Rifer", "Tony", "Marifer", "Chrifer", "Matthar", "Rdatt", "Sonyarya",
                                         "Tene", "Alame", "Messi", "Kenifenda", "Toddo", "Anthryan", "Jardo", "Chriquesh", "Thonyand", 
                                         "Steventho", "Jasodam", "Antheshav", "Adamar", "Alandal", "Ricardoug", "Adantthe", "Markej", "Rist", 
                                         "Sha", "Alantho", "Stevenn", "Jennntho", "Toddohe", "Car", "Ateve", "Sharya", "Tennifer", "Ryame", 
                                         "Andou", "Ann", "Tten", "Randala", "Jamevend", "Jamesiq", "Chew", "Todal", "Mene", "Tejennif", "Todam", 
                                         "Thnt", "Ryantho", "Kendall", "Bric", "Randohe", "Janda", "Touene", "Richonda", "Rannny", "Aranthew", 
                                         "Jadam", "Brendanth", "Jassson", "Ken", "Rendand", "Messt", "Rkene", "Andaso", "Marand", "Chrantho", 
                                         "Ariq", "Mesha", "Rya", "Ryandandal", "Tonda", "Randomes", "Menife", "Camark", "Chrkesha", "Shan", 
                                         "Cason", "Jaceve", "Adamanda", "Johnny", "Keshard", "Ssicari", "Kenyamat", "Jacennifer", "Alark", 
                                         "Brifer", "Ryanisica", "Alama", "Kentej", "Jacennife", "Jesonyan", "Card", "Shariqug", "Kenenni", 
                                         "Matev", "Chryan", "Shadal", "Kentodd", "Mattodd", "Todalll", "The", "Teven", "Dall", "Thonyanda",
                                         "Dond", "Matthenny", "Douesha", "Adamardo", "Jeshenn", "Madam", "Ryalandan", "Ricark", "Shavesic", 
                                         "Chrishony", "Kess", "Keshadam", "Donife", "Chrdo", "Chrichris", "Ssicalan", "Allandoh", "Cevew", 
                                         "Kenife", "Shame", "Ratthew", "Randaso", "Anthn", "Toddoug", "Adamatt", "Riq", "Kenev", "Tthony", 
                                         "Keshoug", "Ryany", "Kenenda", "Breve", "Douej", "Ricarique", "Johr", "Atth", "Mathari", "Kennndam", 
                                         "Jendan", "Jesif", "Rendan", "Tev", "Toddd", "Jasonniqu", "Shanifer", "Jamark", "Johnton", "Marend", 
                                         "Jenndod", "Kennth", "Jasoug", "Toddomew", "Keshar", "Kestth", "Thonyariq", "Tenn", "Jasondan", 
                                         "Ryandoda", "Mennica", "Jardodd", "Meshave", "Marya", "Ricatth", "Rkennife", "Rannife", "Soh", 
                                         "Cejessi", "Cacard", "Mala", "Johony", "Shavesha", "Teveshari", "Ardoha", "Keja", "Cardondal", "Dondan",
                                         "Shonyame", "Brenife", "Jacenya", "Jasondam", "Keshanda", "Rkenni", "Ssicant", "Ryanthew", "Mennya", 
                                         "Kesheve", "Sshar", "Toddant", "Asshariq", "Rejas", "Mall", "Jameshon", "Kennifer", "Kennif", "Ryanth", 
                                         "Matthesh", "Maricard", "Adariqu", "Maryan", "Shatt", "Ryanica", "Todondal", "Ricanda", "Cames", 
                                         "Mateves", "Brennife", "Ryam", "Jamenny", "Kennny", "Canyacar", "Risonya", "Brenttod", "Kendachew",
                                         "Kew", "Jessique", "Jamen", "Jasonny", "Maris", "Keneven", "Jestejas", "Andallan", "Bricard", 
                                         "Johnyant", "Adand", "Kennyan", "Annife", "Stevesha", "Ttthe", "Rdandall", "Messica", "Arand", "Jenda", 
                                         "Adamessi", "Cew", "Jamendall", "Brejony", "Brendala", "Mara", "Adace", "Aniferen", "Jessichri", 
                                         "Riquenni", "Ari", "Arifer", "Kestodd", "Anyan", "Chriqu", "Jamarique", "Jasonnif", "Jeshariq", 
                                         "Jathew", "Jouesha", "Any", "Antha", "Jamenda", "Kennifes", "Kenyand", "Andandall", "Keryan", "Messic", 
                                         "Mardou", "Alanif", "Sharica", "Ryar", "Jandal", "Cariqu", "Jamene", "Kenenny", "Jamesica", "Kestev", 
                                         "Sstev", "Jant", "Ryanifer", "Mace", "Keste", "Meshony", "Allllan", "Matthari", "Jassic", "Tejes", 
                                         "Keshark", "Charife", "Manif", "Arifery", "Dari", "Rdan", "Andant", "Adamesha", "Bris", "Aranife",
                                         "Aris", "Allalan", "Rissoddoug", "Breny", "Kenifer", "Ryalanife", "Adall", "Jesshri", "Jomat", "Dadal", 
                                         "Jessicason", "Johe", "Jar", "Johndall", "Tomace", "Mandanni", "Brendames", "Sonyanth", "Cas", 
                                         "Atoddal", "Anthandan", "Carif", "Alanda", "Rendat", "Cesha", "Kev", "Arend", "Randane", "Ada", 
                                         "Dandall", "Shaveryan", "Jennd", "Johrica", "Jacen", "Rissica", "Tomatthe", "Can", "Ando", "Ryann", 
                                         "Casonnif", "Markenni", "Kenyandd", "Johni", "Jal", "Mameve", "Kenesha", "Thonyandal", "Renyad", 
                                         "Chard", "Kenesh", "Janifen", "Brendandal", "Sharic", "Jameshari", "Riqug", "Joharkene", "Jasonda", 
                                         "Matteve", "Janny", "Jeve", "Johnneve", "Tendan", "Rdari", "Randama", "Amark", "Richony", "Atthe", 
                                         "Jennttthe", "Jamenicar", "Jennyark", "Stendada", "Shris", "Jony", "Mariqug", "Ceve", "Stevenda",
                                         "Ryaniste", "Jacend", "Adamennif", "Jameson", "Jan", "Ddachri", "Ricardohn", "Ameshav", "Jasontho", 
                                         "Matthenda", "Douen", "Rdall", "Shohn", "Mariquenn", "Stejames", "Rane", "Mattoug", "Kennene", 
                                         "Tevenyan", "Jesharica", "Jennnda", "Adardo", "Anenni", "Shony", "Allandam", "Brenniq", "Ryason", 
                                         "Meshavenny", "Jennifess", "Maramesha" }));
    }
  }
}
