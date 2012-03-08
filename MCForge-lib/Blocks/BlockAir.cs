using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCForge.Blocks
{
    /// <summary>
    /// The Air block (ID = 0)
    /// </summary>
    /// <remarks></remarks>
    public class BlockAir : Block
    {
        public override string name
        {
            get { return "air"; }
        }
        public override bool anyBuild
        {
            get
            {
                return true;
            }
        }
        public override bool placable
        {
            get
            {
                return true;
            }
        }
        public override bool lightPass
        {
            get
            {
                return true;
            }
        }
        public BlockAir(ushort id) : base(id)
        {
        }
    }
}
