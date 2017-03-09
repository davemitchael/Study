using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using SharpGL;

namespace Lab1WPF.Logic
{
    public class Draw
    {
        private static Color _backgroundColor = Colors.White;
        private static Color _textColor = Colors.DarkBlue;
        private static string _fontFamily = "MS Reference Sans Serif";
        private static int _fontSize = 12;
        

        public double angle = Math.PI / 2; //Угол поворота на 90 градусов
        public static double ang1 = Math.PI / 4;  //Угол поворота на 45 градусов
        public static double ang2 = Math.PI / 6;  //Угол поворота на 30 градусов
        
         

        public static void DrawTree(SharpGL.OpenGL gl, Tree tree)
        {
            
            gl.Begin(OpenGL.GL_LINE_STRIP);
            gl.Color(tree.Color.R, tree.Color.G, tree.Color.B);
            foreach (var point in tree.Points)
                TreeDraw((float)point.X, (float)point.Y, (float)point.X, (float)point.Y + (float)0.3, (float)120.0, 5,gl);
                //gl.Vertex(point.X, point.Y);

            //gl.End();
            
        }

         public static void DrawText(OpenGL gl, string text, Point position) 
        {
            gl.DrawText(Convert.ToInt32(position.X), Convert.ToInt32(position.Y),
               _textColor.ScR, _textColor.ScG, _textColor.ScB,
               _fontFamily, _fontSize, text);
            gl.Flush();
        }

         static void TreeDraw(float x1, float y1, float x2, float y2, float angle, int n, OpenGL _gl)
         {

             _gl.Begin(OpenGL.GL_LINE_STRIP);
             _gl.Vertex(x1, y1);// root of tree 
             _gl.Vertex(x2, y2);//tree top 
             _gl.End();
             _gl.Flush();
             if (n < 1) return;
             int nn = n - 1;
             float x3 = ((x2 - x1) * (float)1.9 + x1 - x2);
             float y3 = ((y2 - y1) * (float)1.4 + y1 - y2);
             TreeDraw(x2, y2, (float)(x3 * Math.Cos(angle) + y3 * Math.Sin(angle) + x2), (float)(-x3 * Math.Sin(angle) + y3 * Math.Cos(angle)) + y2, angle, nn,_gl);
             TreeDraw(x2, y2, (float)(x3 * Math.Cos(-angle) + y3 * Math.Sin(-angle) + x2), (float)(-x3 * Math.Sin(-angle) + y3 * Math.Cos(-angle) + y2),
                       angle, nn,_gl);
         }

         internal static void Initialize(OpenGL gl)
         {
             gl.ClearColor(0.3f, 0.3f, 0.3f, 0.3f);
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.Flush();
         }
    }
}
