using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Lab1WPF.Logic;
using SharpGL;
using SharpGL.SceneGraph;
using SharpGL.WPF;

namespace Lab1WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double angle = Math.PI / 2; //Угол поворота на 90 градусов
        double ang1 = Math.PI / 4;  //Угол поворота на 45 градусов
        double ang2 = Math.PI / 6;  //Угол поворота на 30 градусов
        private List<Tree> _trees;
        private OpenGL _gl = new OpenGL();
        static public double deg_2_rad = Math.PI / 180.0;
        static readonly object _locky = new object();
        public double[] arr = new double[3]{0.5,0.5,0.5};
                    

        public MainWindow()
        {
            InitializeComponent();
            CustomInitialize();
        }

        public void CustomInitialize()
        {
            _gl = OpenGlControl.OpenGL;
            _trees = new List<Tree>();
        }

        private void OpenGlControl_OnLoaded(object sender, RoutedEventArgs e)
        {
            var openGlControl = (OpenGLControl)sender;
            Draw.Initialize(openGlControl.OpenGL);
        }

        public void DrawHouse(double r,double g, double b)
        {
            
            //window
            _gl.Begin(OpenGL.GL_QUADS);          // Each set of 3 vertices form a triangle
            _gl.Color(0f, 1f, 1f);
            _gl.Vertex(0.5f, -0.2f);
            _gl.Vertex(0.6f, -0.2f);
            _gl.Vertex(0.6f, -0.3f);
            _gl.Vertex(0.5f, -0.3f);
            _gl.End();

            ;
            
            //Wall
            _gl.Begin(OpenGL.GL_QUADS);          // Each set of 3 vertices form a triangle
            _gl.Color(1f,0f,0f); // Blue
            _gl.Vertex(0.1f, -0.6f);
            _gl.Vertex(0.7f, -0.6f);
            _gl.Vertex(0.7f, -0.1f);
            _gl.Vertex(0.1f, -0.1f);
            _gl.End();

            _gl.End();

            //two wall
            _gl.Begin(OpenGL.GL_QUADS);
            _gl.Color(150f,	75f,0f); 
            _gl.Vertex(0.7f, -0.1f);
            _gl.Vertex(0.7f, -0.6f);
            _gl.Vertex(0.99f, -0.46f);
            _gl.Vertex(0.99f, 0.02f);
            _gl.End();

            //two roof
            _gl.Begin(OpenGL.GL_QUADS);
            _gl.Color(r, g, b); // Blue
            _gl.Vertex(0.3f, 0.2f);
            _gl.Vertex(0.65f, 0.33f);
            _gl.Vertex(0.99f, 0.02f);
            _gl.Vertex(0.7f, -0.1f);
           
            _gl.End();

            //Roof
            _gl.Begin(OpenGL.GL_TRIANGLES);
            _gl.Color(0.11f,	0.67f,0.33f);
            _gl.Vertex(0.3f, 0.2f);
            _gl.Vertex(0.7f, -0.1f);
            _gl.Vertex(0.1f, -0.1f);
            _gl.End();
           
            //door
            _gl.Begin(OpenGL.GL_QUADS);
            _gl.Color(1f,1f,1f);
            //_gl.Color(0.127f, 0.255f, 0.212f);
            _gl.Vertex(0.3f, -0.3f);
            _gl.Vertex(0.4f, -0.3f);
            _gl.Vertex(0.4f, -0.6f);
            _gl.Vertex(0.3f, -0.6f);
           // gl.Vertex(0.1f, -0.6f);
            //gl.Vertex(0.2f, -0.6f);
            //gl.Vertex(0.2f, -0.3f);
            //gl.Vertex(0.1f, -0.3f);
           
            _gl.End();

            string text = "House";
            _gl.DrawText(0,0,1,0,1,"Consolas",4,text);
            _gl.Flush();

        }

        private void OpenGLControl_OpenGLDraw(object sender, OpenGLEventArgs args)
        {
            
            _gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            var gl = args.OpenGL;
            
            lock (_trees)
            {
               // _gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
                foreach (var tree in _trees)
                {
                    foreach (var point in tree.Points)
                    {
                       
                        _gl.Color(tree.Color.R, tree.Color.G, tree.Color.B);
                        treeDraw((float)point.X, (float)point.Y,(float)point.X,(float)point.Y+(float)0.3, (float)120.0, 5);
                      
                    }
                    //
                    //Draw.DrawTree(_gl, tree);
                    
                    var textPosition = TreeHelper.TextPosition(tree, OpenGlControl.Width, OpenGlControl.Height);
                    DrawHouse(arr[0], arr[1], arr[2]);
                    Draw.DrawText(gl, tree.Color.ToString(), textPosition);
                    
                    
                }
                
            }
            
      
        }

        private void OpenGLControl_OpenGLInitialized(object sender, OpenGLEventArgs args)
        {
            //var gl = args.OpenGL;

            _gl.ClearColor(0.3f, 0.3f, 0.3f, 0.3f);
           
        }

        private void OpenGLControl_Resized(object sender, OpenGLEventArgs args)
        {
            
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int count = int.Parse(AddCount.Text);
                Thread thread = new Thread(() =>
                {
                    for (int i = 0; i < count; ++i)
                    {
                        var tree = new Tree();

                        lock (_trees)
                        {
                            _trees.Add(tree);
                        }
                        Thread.Sleep(500);
                    }
                });
                thread.IsBackground = true;
                thread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int count = int.Parse(RemoveCount.Text);
                if (count > _trees.Count)
                    throw new Exception("Ви не можете видалити більше трикутників, ніж намальовано.");
                int left = _trees.Count - count;
                Thread thread = new Thread(() =>
                {
                    while (_trees.Count > left)
                    {
                        lock (_trees)
                        {
                            _trees.Remove(_trees.Last());
                        }
                        Thread.Sleep(500);
                    }
                });
              
                thread.IsBackground = true;
                thread.Start();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        void treeDraw(float x1, float y1, float x2, float y2, float angle, int n )
        {

            _gl.Begin(OpenGL.GL_LINE_STRIP);
            _gl.Vertex(x1,y1);// root of tree 
            _gl.Vertex(x2,y2);//tree top 
            _gl.End();
            if(n<1)return ;        
            int nn = n-1;
            float x3 = ((x2 - x1)*(float)1.9 + x1 - x2);
            float y3 = ((y2 - y1) * (float)1.4 + y1 - y2);  
      treeDraw(x2,y2,(float)(x3 * Math.Cos(angle) + y3 * Math.Sin(angle) + x2),(float) (-x3 * Math.Sin(angle) + y3 * Math.Cos(angle)) + y2, angle, nn);
      treeDraw(x2, y2,(float) (x3 * Math.Cos(-angle) + y3 * Math.Sin(-angle) + x2),(float) (-x3 * Math.Sin(-angle) + y3 * Math.Cos(-angle) + y2),
                angle,nn);
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            arr[0] = 0.5;
            arr[1] = 0.5;
            arr[2] = 0.5;
            
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            arr[0] = 0.255;
            arr[1] = 0.255;
            arr[2] = 0.51;
            
        }

        private void RadioButton_Checked_2(object sender, RoutedEventArgs e)
        {
            arr[0] = 0;
            arr[1] = 0.204;
            arr[2] = 0;
            
        }

        private void RadioButton_Checked_3(object sender, RoutedEventArgs e)
        {
            arr[0] = 1;
            arr[1] = 0;
            arr[2] = 0;
           
        }




    }
}
