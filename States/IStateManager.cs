using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.States
{
    interface IStateManager : IDisposable
    {


        void Init(SpriteBatch spriteBatch, ContentManager cm);
        void PushState(IState state);
        void PushState(int state);
        void PopState();
        void Update(GameTime gameTime);
        void Draw(GameTime gameTime);



    }
}
