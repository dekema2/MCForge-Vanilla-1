using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCForge.Blocks
{
    /// <summary>
    /// The Rock block (ID = 1)
    /// </summary>
    /// <remarks></remarks>
    public class BlockRock : Block
    {
        public override string name
        {
            get { return "rock"; }
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
        public BlockRock(ushort id) : base(id)
        {
        }
    }
}
