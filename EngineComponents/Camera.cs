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

        private Matrix m_viewMatrix;

        public Camera(Vector3 position)
        {
            m_position = position;
            m_lookAt = m_position + new Vector3(m_position.X, m_position.Y, m_position.Z + 1);//Vector3.Transform(m_position, Matrix.CreateTranslation(m_position.X, m_position.Y, m_position.Z + 10));
            m_up = Vector3.UnitY;
        }
        
        public void Update(GameTime time)
        {
            
            //m_lookAt = m_position + new Vector3(m_position.X, m_position.Y, m_position.Z + 1); //Vector3.Transform(m_position, Matrix.CreateTranslation(m_position.X, m_position.Y, m_position.Z + 10));
            m_viewMatrix = Matrix.CreateLookAt(m_position, m_lookAt, m_up);
        }

        public void Move()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                m_position.Z += 0.1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                m_position.Z -= 0.1f;
            }
        }

        public Matrix GetViewMatrix()
        {
            return m_viewMatrix;
        }


    }
}
