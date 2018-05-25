using Assignment2.GameElements.NPC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.GameElements.Level
{
    class LevelOne : LevelBase
    {


        /// <summary>
        /// Creates an instance of LevelOne
        /// </summary>
        public LevelOne()
        {
        }
        /// <summary>
        /// Initializes Level one with the NPC tim.
        /// </summary>
        public override void Initialize()
        {
            NPCTim m_npc = new NPCTim();
            m_npc.Initialize();
            m_npcs.Add(m_npc);

            base.Initialize();
        }


    }
}
