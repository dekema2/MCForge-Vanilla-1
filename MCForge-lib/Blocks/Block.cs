/*
	Copyright 2012 MCForge Team
	
	Dual-licensed under the	Educational Community License, Version 2.0 and
	the GNU General Public License, Version 3 (the "Licenses"), you may
	not use this file except in compliance with the Licenses. You may
	obtain a copy of the Licenses at
	
	http://www.opensource.org/licenses/ecl2.php
	http://www.gnu.org/licenses/gpl-3.0.html
	
	Unless required by applicable law or agreed to in writing,
	software distributed under the Licenses are distributed on an "AS IS"
	BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express
	or implied. See the Licenses for the specific language governing
	permissions and limitations under the Licenses.
*/
using MCForge.Blocks;

namespace MCForge
{
    public abstract class Block
    {
        public static Block[] Blocks = new Block[65536];

        /// <summary>
        /// Block ID
        /// </summary>
        public readonly ushort id;
        /// <summary>
        /// Block name
        /// </summary>
        /// <remarks></remarks>
        public abstract string name { get; }
        /// <summary>
        /// The type of door of the block (if it has a type)
        /// </summary>
        /// <remarks></remarks>
        public virtual DoorTypes doorType { get { return DoorTypes.None; } }
        /// <summary>
        /// Walkthroughable block
        /// </summary>
        /// <remarks></remarks>
        public virtual bool walkthrough { get { return false; } }
        /// <summary>
        /// Anyone can build block
        /// </summary>
        /// <remarks></remarks>
        public virtual bool anyBuild { get { return false; } }
        /// <summary>
        /// Placable block
        /// </summary>
        /// <remarks></remarks>
        public virtual bool placable { get { return false; } }
        /// <summary>
        /// OP block
        /// </summary>
        /// <remarks></remarks>
        public virtual bool op { get { return false; } }
        /// <summary>
        /// Block causes death
        /// </summary>
        /// <remarks></remarks>
        public virtual bool death { get { return false; } }
        /// <summary>
        /// People can build 'in' this block
        /// </summary>
        /// <remarks></remarks>
        public virtual bool buildIn { get { return false; } }
        public virtual bool lavaKill { get { return false; } }
        public virtual bool waterKill { get { return false; } }
        /// <summary>
        /// Does light pass through this block
        /// </summary>
        /// <remarks></remarks>
        public virtual bool lightPass { get { return false; } }
        public virtual bool needRestart { get { return false; } }
        /// <summary>
        /// Is this block a portal
        /// </summary>
        /// <remarks></remarks>
        public virtual bool portal { get { return false; } }
        public virtual bool mb { get { return false; } }
        public virtual bool physics { get { return false; } }
        /// <summary>
        /// What the block looks like
        /// </summary>
        /// <remarks></remarks>
        public abstract byte appearance { get; }
        /// <summary>
        /// ID for the block to be saved as
        /// </summary>
        /// <remarks></remarks>
        public virtual ushort saveConvert { get { return id; } }

        //To be added later when LevelPermision (or equivalent) has been added
        //public LevelPermission lowestRank;
        //public List<LevelPermission> disallow = new List<LevelPermission>();
        //public List<LevelPermission> allow = new List<LevelPermission>();

        /// <summary>
        /// Called on creation of this block
        /// If false is returned, the block is not created
        /// </summary>
        /// <remarks></remarks>
        public virtual bool OnBlockCreate { get { return true; } }
        /// <summary>
        /// Called on destroy of this block
        /// If false is returned, the block is not destoryed
        /// </summary>
        /// <remarks></remarks>
        public virtual bool OnBlockDestroy { get { return true; } }

        /// <summary>
        /// Types of door
        /// </summary>
        /// <remarks></remarks>
        public enum DoorTypes { None, Door, tDoor, oDoor };

        public Block(ushort id)
        {
            Blocks[id] = this;
            this.id = id;
        }

        public static const Block Air = new BlockAir(0);
        public static const Block Rock = new BlockRock(1);
        public static const Block Grass = new BlockGrass(2);

    }
}