using Assignment2.GameElements.NPC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.GameElements.Level
{
    class LevelThree : LevelBase
    {


        public LevelThree()
        {

        }

        public override void Initialize()
        {
            NPCJill jill = new NPCJill();
            jill.Initialize();
            m_npcs.Add(jill);
            base.Initialize();
        }


    }
}
