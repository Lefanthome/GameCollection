using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyCsvParser.Mapping;

namespace GameCollection.ElasticSearch.Tool.TinyCsv
{
    public class CsvGameMapping : CsvMapping<Game>
    {
        public CsvGameMapping()
        : base()
        {
            MapProperty(0, x => x.Name);
            MapProperty(1, x => x.Developper);
            MapProperty(2, x => x.Console);
            MapProperty(3, x => x.Genre);
        }
    }
}
