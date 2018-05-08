﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.States
{
    interface IState : IDisposable
    {

        void Load();
        void Update(GameTime time);
        void Draw(GameTime time);
        void Unload();

        int GetID();

        void Init(IStateManager manager);

    }
}
