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

        public LevelTwo()
        {

        }

        public override void Initialize()
        {
            NPCGreg greg = new NPCGreg();
            greg.Initialize();
            m_npcs.Add(greg);
            base.Initialize();
        }


    }
}
