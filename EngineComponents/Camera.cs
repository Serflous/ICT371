using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.EngineComponents
{
    class Camera
    {


        private Vector3 m_position;
        private Vector3 m_lookAt;
        private Vector3 m_up;
        Vector3 m_cameraRotation;
        float m_cameraSpeed;
        Vector3 m_mouseRotation;
        MouseState m_currentMouse;
        MouseState m_previousMouse;
        GamePadState m_previousGamePadState;

        /// <summary>
        /// Creates a new camera object at the specified position
        /// </summary>
        /// <param name="position">The initial position of the camera</param>
        public Camera(Vector3 position)
        {
            m_position = position;
            m_lookAt = m_position + new Vector3(m_position.X, m_position.Y, m_position.Z + 1);
            m_up = Vector3.UnitY;
            m_cameraSpeed = 5f;
            m_previousMouse = Mouse.GetState();
        }

        /// <summary>
        /// The projection of the camera
        /// </summary>
        public Matrix Projection
        {
            get;
            set;
        }

        /// <summary>
        /// The view of the camera
        /// </summary>
        public Matrix View
        {
            get
            {
                return Matrix.CreateLookAt(m_position, m_lookAt, m_up);
            }
        }

        /// <summary>
        /// The position of the camera
        /// </summary>
        public Vector3 Position
        {
            get
            {
                return m_position;
            }

            set
            {
                m_position = value;
                UpdateLookAt();
            }
        }

        /// <summary>
        /// The rotation of the camera
        /// </summary>
        public Vector3 Rotation
        {
            get
            {
                return m_cameraRotation;
            }

            set
            {
                m_cameraRotation = value;
                UpdateLookAt();
            }
        }

        /// <summary>
        /// Move the camera and rotate it using the values given
        /// </summary>
        /// <param name="pos">The position of the camera</param>
        /// <param name="rot">The rotation of the camera</param>
        private void MoveToPosition(Vector3 pos, Vector3 rot)
        {
            Rotation = rot;
            Position = pos;
        }

        /// <summary>
        /// Update the lookat position of the camera
        /// </summary>
        private void UpdateLookAt()
        {
            Matrix rotationMatrix = Matrix.CreateRotationX(m_cameraRotation.X) * Matrix.CreateRotationY(m_cameraRotation.Y);
            Vector3 lookAtOffset = Vector3.Transform(Vector3.UnitZ, rotationMatrix);
            m_lookAt = m_position + lookAtOffset;
        }

        /// <summary>
        /// Calculates the movement vector
        /// </summary>
        /// <param name="amount">The movement vector</param>
        /// <returns></returns>
        private Vector3 NextMove(Vector3 amount)
        {
            Matrix rotate = Matrix.CreateRotationY(m_cameraRotation.Y);
            Vector3 movement = new Vector3(amount.X, amount.Y, amount.Z);
            movement = Vector3.Transform(movement, rotate);
            return m_position + movement;
        }

        /// <summary>
        /// Moves to a certain position without specifiying rotation
        /// </summary>
        /// <param name="scale">The position to mvoe to</param>
        private void Move(Vector3 scale)
        {
            MoveToPosition(NextMove(scale), Rotation);
        }

        
        /// <summary>
        /// Updates the camera calculating the new rotation by user input
        /// </summary>
        /// <param name="time">The current game time.</param>
        public void Update(GameTime time)
        {

            float dt = (float)time.ElapsedGameTime.TotalSeconds;
            m_currentMouse = Mouse.GetState();
            KeyboardState ks = Keyboard.GetState();

            /*Vector3 moveVector = Vector3.Zero;

            if (ks.IsKeyDown(Keys.W))
            {
                moveVector.Z = 0.1f;
            }

            if(ks.IsKeyDown(Keys.S))
            {
                moveVector.Z = -0.1f;
            }

            if(ks.IsKeyDown(Keys.A))
            {
                moveVector.X = 0.1f;
            }

            if(ks.IsKeyDown(Keys.D))
            {
                moveVector.X = -0.1f;
            }

            if(moveVector != Vector3.Zero)
            {
                moveVector.Normalize();
                moveVector *= dt * m_cameraSpeed;

                Move(moveVector);
            }*/

            float tempMouseX;
            float tempMouseY;

            if (m_previousMouse != m_currentMouse || GamePad.GetState(PlayerIndex.One) != m_previousGamePadState)
            {
                tempMouseX = m_currentMouse.X - (Properties.Settings.Default.SCREEN_RES_X / 2);
                tempMouseY = m_currentMouse.Y - (Properties.Settings.Default.SCREEN_RES_Y / 2);
                float tempControllerX = GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X;
                float tempControllerY = GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Y;

                if (!(tempMouseX == 0 && tempMouseY == 0))
                {
                    m_mouseRotation.X -= 0.1f * tempMouseX * dt;
                    m_mouseRotation.Y -= 0.1f * tempMouseY * dt;
                }
                if (!(tempControllerX == 0 && tempControllerY == 0))
                {
                    m_mouseRotation.X -= 0.8f * tempControllerX * dt;
                    m_mouseRotation.Y -= 0.8f * -tempControllerY * dt;
                }

                if (m_mouseRotation.Y < MathHelper.ToRadians(-80.0f))
                {
                    m_mouseRotation.Y = m_mouseRotation.Y - (m_mouseRotation.Y - MathHelper.ToRadians(-80.0f));
                }

                if(m_mouseRotation.Y > MathHelper.ToRadians(80.0f))
                {
                    m_mouseRotation.Y = m_mouseRotation.Y - (m_mouseRotation.Y - MathHelper.ToRadians(80.0f));
                }

                Rotation = new Vector3(-MathHelper.Clamp(m_mouseRotation.Y, MathHelper.ToRadians(-80.0f), MathHelper.ToRadians(80.0f)),
                    MathHelper.WrapAngle(m_mouseRotation.X), 0);

                tempMouseX = 0;
                tempMouseY = 0;
            }

            Mouse.SetPosition(Properties.Settings.Default.SCREEN_RES_X / 2, Properties.Settings.Default.SCREEN_RES_Y / 2);

            m_previousMouse = m_currentMouse;
            m_previousGamePadState = GamePad.GetState(PlayerIndex.One);
        }

    }
}
