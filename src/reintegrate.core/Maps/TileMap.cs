using System.Collections.Generic;

namespace reintegrate.Core.Maps
{
    public class TileMap
    {
        public TileMap()
        {
            Row = new List<string>();
        }

        public List<string> Row { get; set; }
    }
}
