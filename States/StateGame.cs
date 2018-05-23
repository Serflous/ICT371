using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment2.EngineComponents;
using Assignment2.GameElements;
using Assignment2.GameElements.Level;
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
        private SpriteBatch m_spriteBatch;
        private Matrix m_projectionMatrix;
        private Matrix m_viewMatrix;
        private Matrix m_worldMatrix;

        Vector3 camPosition = new Vector3(0, 1, -1);
        Vector3 camLookAtVector = new Vector3(0, 1, 0);//Vector3.Zero;

        Camera m_camera = new Camera(new Vector3(0, 1, -1));

        private Model m_classroom;

        private Texture2D m_blackboardTex;
        private SpriteFont m_font;

        GameManager m_gameManager;

        public StateGame(SpriteBatch spriteBatch, ContentManager manager)
        {
            m_contentManager = new ContentManager(manager.ServiceProvider, "Content");
            m_spriteBatch = spriteBatch;
            m_gameManager = new GameManager();
        }

        public void Init(IStateManager manager)
        {
            m_stateManager = manager;
            m_projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), Properties.Settings.Default.SCREEN_RES_X / Properties.Settings.Default.SCREEN_RES_Y, 0.1f, 1000f);
            
        }

        public void Load()
        {
            m_classroom = m_contentManager.Load<Model>("Classroom/classroom");
            m_blackboardTex = m_contentManager.Load<Texture2D>("Classroom/blackboard");
            m_worldMatrix = Matrix.Identity;//Matrix.CreateWorld(Vector3.Zero, Vector3.Zero, Vector3.Up);
            m_viewMatrix = Matrix.CreateLookAt(Vector3.Zero, Vector3.Zero, Vector3.UnitZ);
            m_font = m_contentManager.Load<SpriteFont>("font");
            m_gameManager.Initialize();
            m_gameManager.OutOfLevels += onOutOfLevels;
            Counter.Instance.Reset();
        }

        private void onOutOfLevels(object sender, EventArgs e)
        {
            m_stateManager.PopState();
            m_stateManager.PushState((int)StateManager.States.STATE_RESULTS);
        }

        public void Draw(GameTime time)
        {
            foreach(ModelMesh mesh in m_classroom.Meshes)
            {
                foreach(BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    //effect.Texture = m_blackboardTex;
                    //effect.TextureEnabled = true;
                    effect.PreferPerPixelLighting = true;
                    effect.World = m_worldMatrix;
                    effect.View = m_camera.GetViewMatrix();// Matrix.CreateLookAt(camPosition, camLookAtVector, Vector3.UnitY);
                    effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, 1024 / 768, 0.1f, 1000f);
                }
                mesh.Draw();
            }
            m_spriteBatch.Begin();
            m_spriteBatch.DrawString(m_font, WrapText(m_gameManager.GetString(), m_font, Properties.Settings.Default.SCREEN_RES_X), new Vector2(0, 0), Color.Black);
            m_spriteBatch.End();
        }
        
        public void Update(GameTime time)
        {
            if(Keyboard.GetState().IsKeyDown(Keys.LeftControl))
            {
                m_stateManager.PopState();
            }
            
            if(Keyboard.GetState().IsKeyDown(Keys.T))
            {
                m_worldMatrix *= Matrix.CreateTranslation(0, 0, -0.1f);
            }
            m_camera.Move();
            m_camera.Update(time);

            m_gameManager.Update(time);
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

        private string WrapText(string text, SpriteFont font, int maxWidth)
        {
            string[] words = text.Split(' ');
            float currentWidth = 0;
            string output = "";
            float spaceWidth = font.MeasureString(" ").X;
            foreach(string word in words)
            {
                Vector2 wordSize = font.MeasureString(word);
                if (currentWidth + wordSize.X < maxWidth)
                {
                    output += word + ' ';
                    currentWidth += wordSize.X + spaceWidth;
                }
                else
                {
                    output += '\n' + word + ' ';
                    currentWidth = wordSize.X + spaceWidth;
                }
            }
            return output;
        }

    }
}
