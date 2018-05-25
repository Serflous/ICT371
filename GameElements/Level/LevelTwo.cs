using Assignment2.GameElements.NPC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.GameElements.Level
{
    class LevelTwo : LevelBase
    {
        /// <summary>
        /// Creates an instance of level two
        /// </summary>
        public LevelTwo()
        {

        }
        /// <summary>
        /// Initializes level two with the NPC greg.
        /// </summary>
        public override void Initialize()
        {
            NPCGreg greg = new NPCGreg();
            greg.Initialize();
            m_npcs.Add(greg);
            base.Initialize();
        }


    }
}
