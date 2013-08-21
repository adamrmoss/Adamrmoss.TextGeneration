using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TNW.TextGeneration.Specs
{
  [TestFixture]
  public class NamesBuilderSpecs : AssertionHelper
  {
    private WordAnalyzer WordAnalyzer;
    private NamesBuilder NamesBuilder;

    private void BuildNamesBuilder()
    {
      NamesBuilder = new NamesBuilder {
        Seed = 69,
        WordAnalyzer = WordAnalyzer
      };
    }

    [Test]
    public void It_scrambles_mad_sad_bananas_and_damn_cocoa()
    {
      WordAnalyzer = new WordAnalyzer {
        MinSubwordLength = 2,
        MaxSubwordLength = 3,
      };

      WordAnalyzer.Analyze("mad", "sad", "bananas", "and", "damn", "cocoa");

      BuildNamesBuilder();

      var names = NamesBuilder.Build().Take(20).ToArray();
      Expect(names, EquivalentTo(new[] { "mama", "coad", "dadadada", "damna", "coco", "andanadam", "daman", "andam", "bandas", "coc", "coan",
                                         "bandasan", "andanan", "madanasad", "dana", "coas", "dananas", "dam", "ban", "bandan" }));
    }

    [Test]
    public void It_can_make_some_new_continents_and_oceans()
    {
      WordAnalyzer = new WordAnalyzer {
        MinSubwordLength = 2,
        MaxSubwordLength = 4,
      };

      WordAnalyzer.Analyze("Africa", "America", "Asia", "Arctic", "Atlantic", "Australia", "Europe", "Indian", "Pacific");

      BuildNamesBuilder();

      var names = NamesBuilder.Build().Take(20).Select(name => name.Capitalize()).ToArray();
      Expect(names, EquivalentTo(new[] { "Afrop", "Indint", "Euricafri", "Ameurca", "Europerop", "Arctli", "Ausiandian", "Arcameuro", "Africtic", "Atlalantra", "Atla",
                                         "Pacicif", "Atlantra", "Atlaran", "Atlantlamer", "Pafiasicif", "Atliantic", "Eurcatr", "Intlica", "Pacict" }));
    }

    [Test]
    public void It_can_make_some_new_states()
    {
      WordAnalyzer = new WordAnalyzer {
        MinSubwordLength = 2,
        MaxSubwordLength = 4,
      };

      WordAnalyzer.Analyze("Alabama", "Alaska", "Arizona", "Arkansas", "California", "Carolina", "Colorado", "Connecticut", "Dakota", "Delaware", "Florida", "Georgia",
                           "Hampshire", "Hawaii", "Idaho", "Illinois", "Indiana", "Iowa", "Jersey", "Kansas", "Kentucky", "Louisiana", "Maine", "Maryland",
                           "Massachusetts", "Mexico", "Michigan", "Minnesota", "Mississippi", "Missouri", "Montana", "Nebraska", "Nevada", "Ohio", "Oklahoma", "Oregon",
                           "Pennsylvania", "Rhode", "Tennessee", "Texas", "Utah", "Vermont", "Virginia", "Washington", "Wisconsin", "Wyoming", "York");

      BuildNamesBuilder();

      var names = NamesBuilder.Build().Take(50).Select(name => name.Capitalize()).ToArray();
      Expect(names, EquivalentTo(new[] { "Aripshinsy", "Arick", "Illiansas", "Penect", "Peneorhodans", "Monebrigohi", "Kamppen", "Arkasettes", "Innev", "Mexiscorh", "Lotuck",
                                         "Utaloranoi", "Loui", "Kansakyorig", "Hasyllowis", "Ilicolaryl", "Rhisorad", "Wylifodia", "Texirgi", "Vershirn", "Rhodawyorylva",
                                         "Kentticusebam", "Hanect", "Rhomintu", "Montaska", "Teorisk", "Rhonnnoi", "Iowahom", "Okawasian", "Coregodela", "Flolask", "Kanseg",
                                         "Conn", "Rhona", "Indomin", "Mexianio", "Missyomal", "Wyomali", "Utadasylor", "Carhirerse", "Flonnss", "Wasonsirg", "Mississo",
                                         "Tenorgidakohio", "Okourniannare", "Alahodeb", "Mizonantan", "Jerinaba", "Caloregan", "Miomiansidi" }));
    }

    [Test]
    public void It_can_make_some_new_employees()
    {
      WordAnalyzer = new WordAnalyzer {
        MinSubwordLength = 2,
        MaxSubwordLength = 4,
      };

      WordAnalyzer.Analyze("Adam", "Alan", "Anthony", "Brendan", "Chris", "Doug", "Jace", "James", "Jason", "Jennifer", "Jessica", "John", "Kene",
                           "Kenny", "Keshav", "Mark", "Matthew", "Randall", "Ricardo", "Ryan", "Sharique", "Steve", "Tej", "Todd", "Tom");

      BuildNamesBuilder();

      var names = NamesBuilder.Build().Take(50).Select(name => name.Capitalize()).ToArray();
      Expect(names, EquivalentTo(new[] { "Alatth", "Shar", "Alanifes", "Jaso", "Anth", "Ricantod", "Breste", "Jess", "Marav", "Rannnif", "Jenya",
                                         "Stesonyan", "Kenicesha", "Kentom", "Shardohris", "Sthnyaca", "Johris", "Anthariq", "Randam", "Sharan", 
                                         "Randala", "Ranyark", "Ryace", "Jasoue", "Tesha", "Tejanda", "Chrannnif", "Stevessi", "Tevera", "Alacari", 
                                         "Jacannif", "Kenni", "Marife", "Johad", "Ryama", "Kendall", "Rand", "Manda", "Briquene", "Douenda", "Jenn", 
                                         "Chatth", "Alalan", "Kenyani", "Maricala", "Kest", "Janife", "Kentho", "Jachri", "Doujace" }));
    }
  }
}
