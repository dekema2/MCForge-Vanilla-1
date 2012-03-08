using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCForge.Blocks
{
    /// <summary>
    /// The Grass block (ID = 2)
    /// </summary>
    /// <remarks></remarks>
    public class BlockGrass : Block
    {
        public override string name
        {
            get { return "grass"; }
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
        public BlockGrass(ushort id) : base(id)
        {
        }
    }
}
