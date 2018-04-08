using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCollection.Contrat.Dto
{
    public class GameDto : IEntity
    {
        public int Identifier { get; set; }
        public string Name { get; set; }
        public string Developper { get; set; }
        public string Console { get; set; }
        public string Genre { get; set; }
    }
}
