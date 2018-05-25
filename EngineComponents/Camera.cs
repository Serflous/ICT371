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

        public Camera(Vector3 position)
        {
            m_position = position;
            m_lookAt = m_position + new Vector3(m_position.X, m_position.Y, m_position.Z + 1);
            m_up = Vector3.UnitY;
            m_cameraSpeed = 5f;
            m_previousMouse = Mouse.GetState();
        }

        public Matrix Projection
        {
            get;
            set;
        }

        public Matrix View
        {
            get
            {
                return Matrix.CreateLookAt(m_position, m_lookAt, m_up);
            }
        }

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

        private void MoveToPosition(Vector3 pos, Vector3 rot)
        {
            Rotation = rot;
            Position = pos;
        }

        private void UpdateLookAt()
        {
            Matrix rotationMatrix = Matrix.CreateRotationX(m_cameraRotation.X) * Matrix.CreateRotationY(m_cameraRotation.Y);
            Vector3 lookAtOffset = Vector3.Transform(Vector3.UnitZ, rotationMatrix);
            m_lookAt = m_position + lookAtOffset;
        }

        private Vector3 NextMove(Vector3 amount)
        {
            Matrix rotate = Matrix.CreateRotationY(m_cameraRotation.Y);
            Vector3 movement = new Vector3(amount.X, amount.Y, amount.Z);
            movement = Vector3.Transform(movement, rotate);
            return m_position + movement;
        }

        private void Move(Vector3 scale)
        {
            MoveToPosition(NextMove(scale), Rotation);
        }

        
        public void Update(GameTime time)
        {

            float dt = (float)time.ElapsedGameTime.TotalSeconds;
            m_currentMouse = Mouse.GetState();
            KeyboardState ks = Keyboard.GetState();

            Vector3 moveVector = Vector3.Zero;

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
            }

            float tempX;
            float tempY;

            if (m_previousMouse != m_currentMouse)
            {
                tempX = m_currentMouse.X - (Properties.Settings.Default.SCREEN_RES_X / 2);
                tempY = m_currentMouse.Y - (Properties.Settings.Default.SCREEN_RES_Y / 2);

                m_mouseRotation.X -= 0.1f * tempX * dt;
                m_mouseRotation.Y -= 0.1f * tempY * dt;

                if(m_mouseRotation.Y < MathHelper.ToRadians(-80.0f))
                {
                    m_mouseRotation.Y = m_mouseRotation.Y - (m_mouseRotation.Y - MathHelper.ToRadians(-80.0f));
                }

                if(m_mouseRotation.Y > MathHelper.ToRadians(80.0f))
                {
                    m_mouseRotation.Y = m_mouseRotation.Y - (m_mouseRotation.Y - MathHelper.ToRadians(80.0f));
                }

                Rotation = new Vector3(-MathHelper.Clamp(m_mouseRotation.Y, MathHelper.ToRadians(-80.0f), MathHelper.ToRadians(80.0f)),
                    MathHelper.WrapAngle(m_mouseRotation.X), 0);

                tempX = 0;
                tempY = 0;
            }

            Mouse.SetPosition(Properties.Settings.Default.SCREEN_RES_X / 2, Properties.Settings.Default.SCREEN_RES_Y / 2);

            m_previousMouse = m_currentMouse;
        }

    }
}
