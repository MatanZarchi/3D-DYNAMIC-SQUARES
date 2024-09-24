using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace open_TK2
{
    internal class game
    {
        GameWindow window;
        double theta = 0.0;
        public game(GameWindow window)
        {
            this.window = window;
            this.Start();
        }

        void Start()
        {
            window.Load += loaded;
            window.Resize += resize;
            window.RenderFrame += renderF; // create the window frame
            window.Run(1.0 / 60.0);
        }

        void resize(object o, EventArgs E)
        {
            GL.Viewport(0, 0, window.Width, window.Height); // place the view in the window
            GL.MatrixMode(MatrixMode.Projection);// range of axis x and y היטלים
            GL.LoadIdentity();// load the matrix         
            Matrix4 matrix4 = Matrix4.Perspective(45.0f, window.Width / window.Height, 1.0f, 100.0f);// eye pespective
            GL.LoadMatrix(ref matrix4);
            GL.MatrixMode(MatrixMode.Modelview);// update the mat
        }
        void renderF(object o, EventArgs e)
        {
            GL.LoadIdentity();
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);//clear the buffers

            // Left object
            GL.PushMatrix();// save the mat state
            GL.Translate(-20.0f, 0.0f, -50.0f);//Changes the position of the object by adding values ​​to the axes(x,y,z).
            GL.Rotate(theta, 1.0, 0.0, 0.0);
            GL.Rotate(theta, 1.0, 1.0, 1.0);
            GL.Scale(0.5f, 0.5f, 0.5f);//chang the object size. scale(2,2,2) make it bigger twice       
            this.draw_cube();
            GL.PopMatrix(); //retrive the mat

            //Right object
            GL.PushMatrix();
            GL.Translate(15.0f, 0.0f, -50.0f);
            GL.Rotate(theta, 1.0, 1.0, 0.0);
            GL.Rotate(theta, 0.0, 1.0, 1.0);
            GL.Scale(1.5f, 0.5f, 1.3f);
            this.draw_cube();
            GL.PopMatrix();

            window.SwapBuffers();

            theta += 1.0;
            if (theta > 360)
                theta -= 360;
        }
        void loaded(object o, EventArgs e)
        {
            GL.ClearColor(0.0f, 0.0f, 0.0f, 0.0f); //window color
            GL.Enable(EnableCap.DepthTest);
        }

        void draw_cube()
        {
            GL.Begin(BeginMode.Quads);

            //front
            GL.Color3(1.0, 1.0, 0.0);
            GL.Vertex3(-10.0, 10.0, 10.0);
            GL.Vertex3(-10.0, 10.0, -10.0);
            GL.Vertex3(-10.0, -10.0, -10.0);
            GL.Vertex3(-10.0, -10.0, 10.0);

            //back
            GL.Color3(1.0, 0.0, 1.0);
            GL.Vertex3(10.0, 10.0, 10.0);
            GL.Vertex3(10.0, 10.0, -10.0);
            GL.Vertex3(10.0, -10.0, -10.0);
            GL.Vertex3(10.0, -10.0, 10.0);

            //top
            GL.Color3(0.0, 1.0, 1.0);
            GL.Vertex3(10.0, -10.0, 10.0);
            GL.Vertex3(10.0, -10.0, -10.0);
            GL.Vertex3(-10.0, -10.0, -10.0);
            GL.Vertex3(-10.0, -10.0, 10.0);

            //bottom
            GL.Color3(1.0, 0.0, 0.0);
            GL.Vertex3(10.0, 10.0, 10.0);
            GL.Vertex3(10.0, 10.0, -10.0);
            GL.Vertex3(-10.0, 10.0, -10.0);
            GL.Vertex3(-10.0, 10.0, 10.0);

            //right
            GL.Color3(0.0, 1.0, 0.0);
            GL.Vertex3(10.0, 10.0, -10.0);
            GL.Vertex3(10.0, -10.0, -10.0);
            GL.Vertex3(-10.0, -10.0, -10.0);
            GL.Vertex3(-10.0, 10.0, -10.0);

            //left
            GL.Color3(0.0, 0.0, 1.0);
            GL.Vertex3(10.0, 10.0, 10.0);
            GL.Vertex3(10.0, -10.0, 10.0);
            GL.Vertex3(-10.0, -10.0, 10.0);
            GL.Vertex3(-10.0, 10.0, 10.0);

            GL.End();
        }
    }

    /*
     GL.Enable(EnableCap.DepthTest):
     בדיקת עומק מבטיחה שכאשר אובייקטים חופפים במרחב תלת-ממדי, 
    רק את המשטחים הקרובים יותר (הנראים) מציירים. 
    זה נעשה על ידי השוואת ערכי העומק של פיקסלים.
     
    GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
     
    ColorBufferBit: מנקה את הבאפינג של הצבע, כך שהמסך יתמלא בצבע ברירת המחדל - לרוב שחור

    DepthBufferBit: מנקה את באף העומק, כך שכל המידע הקודם על עומק האובייקטים נמחק, ומאפשר רינדור מחדש של הסצנה.
     */
}


