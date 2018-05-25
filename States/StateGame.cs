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

        float m_modelRotation = 0.0f;
        Vector3 m_modelPosition = new Vector3(0, 0, 0);

        Vector3 camPosition = new Vector3(0, 1, -1);
        Vector3 camLookAtVector = new Vector3(0, 1, 0);//Vector3.Zero;

        Camera m_camera = new Camera(new Vector3(0, 3.5f, -2));

        private Model m_classroom;

        private Texture2D m_blackboardTex;
        private SpriteFont m_font;

        GameManager m_gameManager;
        /// <summary>
        /// Creates an instance of StateGame
        /// </summary>
        /// <param name="spriteBatch">The sprite batch to allow 2D drawing</param>
        /// <param name="manager">The content manager to allow loading in values</param>
        public StateGame(SpriteBatch spriteBatch, ContentManager manager)
        {
            m_contentManager = new ContentManager(manager.ServiceProvider, "Content");
            m_spriteBatch = spriteBatch;
            m_gameManager = new GameManager();
        }
        /// <summary>
        /// Initializes the game state
        /// </summary>
        /// <param name="manager">The state manager so it can call another state.</param>
        public void Init(IStateManager manager)
        {
            m_stateManager = manager;
            m_projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), Properties.Settings.Default.SCREEN_RES_X / Properties.Settings.Default.SCREEN_RES_Y, 0.1f, 1000f);
            
        }
        /// <summary>
        /// Loads all the assets this state uses.
        /// </summary>
        public void Load()
        {
            
            m_classroom = m_contentManager.Load<Model>("Classroom/classroom");
            
            m_blackboardTex = m_contentManager.Load<Texture2D>("Classroom/blackboard");
            m_worldMatrix = Matrix.Identity * Matrix.CreateRotationX(MathHelper.ToRadians(270));
            m_viewMatrix = Matrix.CreateLookAt(Vector3.Zero, Vector3.Zero, Vector3.UnitZ);
            m_font = m_contentManager.Load<SpriteFont>("font");
            m_gameManager.Initialize();
            m_gameManager.OutOfLevels += onOutOfLevels;
            Counter.Instance.Reset();
        }
        /// <summary>
        /// Called when the game runs out of levels
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">Event Args</param>
        private void onOutOfLevels(object sender, EventArgs e)
        {
            m_stateManager.PopState();
            m_stateManager.PushState((int)StateManager.States.STATE_RESULTS);
        }
        /// <summary>
        /// Draws the game state. Draws the classroom and the writing.
        /// </summary>
        /// <param name="time">The gametime</param>
        public void Draw(GameTime time)
        {

            Matrix[] transforms = new Matrix[m_classroom.Bones.Count];
            m_classroom.CopyAbsoluteBoneTransformsTo(transforms);

            foreach(ModelMesh mesh in m_classroom.Meshes)
            {
                foreach(BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = m_worldMatrix;
                    effect.View = m_camera.View;
                    
                    effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, 1024 / 768, 0.1f, 1000f);
                }
                mesh.Draw();
            }
            m_spriteBatch.Begin();
            m_spriteBatch.DrawString(m_font, WrapText(m_gameManager.GetString(), m_font, Properties.Settings.Default.SCREEN_RES_X), new Vector2(0, 0), Color.Black);
            m_spriteBatch.End();
        }
        
        /// <summary>
        /// Updates the game state
        /// </summary>
        /// <param name="time">The game time</param>
        public void Update(GameTime time)
        {
            //m_camera.Move();
            m_camera.Update(time);

            m_gameManager.Update(time);
        }
        /// <summary>
        /// Unloads the data
        /// </summary>
        public void Unload()
        {
            m_contentManager.Unload();
        }
        /// <summary>
        /// Gets the ID of the game state.
        /// </summary>
        /// <returns>The ID of the game state</returns>
        public int GetID()
        {
            return (int)StateManager.States.STATE_GAME;
        }


        public void Dispose()
        {
            
        }
        /// <summary>
        /// Word Wrap. Make sure that the text wont go off the blackboard.
        /// </summary>
        /// <param name="text">The text to wrap</param>
        /// <param name="font">THe font</param>
        /// <param name="maxWidth">The maximum width</param>
        /// <returns></returns>
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
