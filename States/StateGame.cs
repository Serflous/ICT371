using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Assignment2.States
{
    class StateGame : IState
    {


        private IStateManager m_stateManager;
        private ContentManager m_contentManager;
        private Matrix m_projectionMatrix;
        private Matrix m_viewMatrix;
        private Matrix m_worldMatrix;

        Vector3 camPosition = new Vector3(0, 1, 0);
        Vector3 camLookAtVector = Vector3.Zero;

        private Model m_classroom;

        public StateGame(ContentManager manager)
        {
            m_contentManager = new ContentManager(manager.ServiceProvider, "Content");
        }

        public void Init(IStateManager manager)
        {
            m_stateManager = manager;
            m_projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), Properties.Settings.Default.SCREEN_RES_X / Properties.Settings.Default.SCREEN_RES_Y, 0.1f, 1000f);

        }

        public void Load()
        {
            m_classroom = m_contentManager.Load<Model>("Classroom/classroom");
            m_worldMatrix = Matrix.CreateWorld(Vector3.Zero, Vector3.Zero, Vector3.Up);
            m_viewMatrix = Matrix.CreateLookAt(Vector3.Zero, Vector3.Zero, Vector3.UnitZ);
        }

        public void Draw(GameTime time)
        {
            foreach(ModelMesh mesh in m_classroom.Meshes)
            {
                foreach(BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    //effect.TextureEnabled = true;
                    effect.PreferPerPixelLighting = true;
                    effect.World = Matrix.Identity;
                    
                    effect.View = Matrix.CreateLookAt(camPosition, camLookAtVector, Vector3.UnitZ);
                    effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, 1024 / 768, 0.1f, 1000f);
                }
                mesh.Draw();
            }
        }
        
        public void Update(GameTime time)
        {
            if(Keyboard.GetState().IsKeyDown(Keys.LeftControl))
            {
                m_stateManager.PopState();
            }
            if(Keyboard.GetState().IsKeyDown(Keys.W))
            {
                camPosition.Z += 0.1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                camPosition.Z -= 0.1f;
            }
        }

        public void Unload()
        {
            
        }

        public int GetID()
        {
            return (int)StateManager.States.STATE_GAME;
        }


        public void Dispose()
        {
            
        }

    }
}
