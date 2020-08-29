using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoFinal
{
    public partial class Form1 : Form
    {
        Bitmap bmpOriginal;
        Bitmap bmpScene;
        Bitmap bmpSimulation;
        Graphics render;
        Graph sceneGraph;
        int iIdGenerator;

        enum StopEvent
        {
            PreyOnObjective, NoPreysLeft
        }

        public Form1()
        {
            InitializeComponent();
            PictureBoxImage.BackgroundImageLayout = ImageLayout.Zoom;
        }

        /// <summary>Loads and builds the scene from an image.</summary>
        private void ButtonLoad_Click(object sender, EventArgs e)
        {
            // Load image
            OpenFileDialogImage.ShowDialog();
            Image image = Image.FromFile(OpenFileDialogImage.FileName);
            PictureBoxImage.BackgroundImage = null;
            PictureBoxImage.Image = image;

            // Build sceneGraph
            bmpOriginal = new Bitmap(OpenFileDialogImage.FileName);
            bmpScene = new Bitmap(bmpOriginal);

            List<Circle> circleList = new List<Circle>();

            // Find circles in image
            for (int i = 0; i < bmpOriginal.Height; i++)
            {
                for (int j = 0; j < bmpOriginal.Width; j++)
                {
                    Color col = bmpOriginal.GetPixel(j, i);
                    if (col.R == 0 && col.G == 0 && col.B == 0)
                    {
                        if (!IsInCircle(j, i, circleList))
                            circleList.Add(FindCenter(j, i, bmpOriginal));
                    }
                }
            }

            // Create Graph
            sceneGraph = new Graph(circleList, bmpOriginal);

            // Draw Graph
            Graphics gra = Graphics.FromImage(bmpScene);
            sceneGraph.Draw(gra);
            PictureBoxImage.Image = bmpScene;

            iIdGenerator = 0;

            bmpSimulation = new Bitmap(bmpScene.Width, bmpScene.Height);
            PictureBoxImage.BackgroundImage = bmpScene;
            PictureBoxImage.Image = bmpSimulation;
            render = Graphics.FromImage(bmpSimulation);
            render.Clear(Color.Transparent);

            LabelPreyCount.Text = "Presas: " + sceneGraph.Preys.Count;
            LabelPredatorCount.Text = "Depredadores: " + sceneGraph.Predators.Count;
            LabelStopEvent.Text = "";

            ButtonAddPrey.Enabled = true;
            ButtonAddPredator.Enabled = true;
            ButtonAddObjective.Enabled = true;

            MessageBox.Show("Para agragar objetos al escenario puede hacer click en el botón correspondiente, y después en algún vértice libre del escenario.", "Aviso");
        }

        /// <summary>Determines wether point (x, y) is in a <c>Circle</c> from list.</summary>
        bool IsInCircle(int x, int y, List<Circle> list)
        {
            if (list.Count == 0)
                return false;

            float x0;
            float y0;

            float xk = x;
            float yk = y;

            float radio;
            foreach (Circle c in list)
            {
                x0 = c.X;
                y0 = c.Y;
                radio = c.Radius + 5;
                float sol = (xk - x0) * (xk - x0) + (yk - y0) * (yk - y0) - radio * radio;
                if (sol < 0)
                    return true;
            }
            return false;
        }

        /// <summary>Finds center of <c>Circle</c> with top right point (x, y). Returns the <c>Circle</c> object.</summary>
        Circle FindCenter(int x, int y, Bitmap bmp)
        {
            int j = y;
            while (true)
            {
                Color col = bmp.GetPixel(x, j);
                if (col.R != 0 || col.G != 0 || col.B != 0)
                {
                    break;
                }
                j++;
            }
            j--; //Pixel no en circulo

            int i = x;
            while (true)
            {
                Color col = bmp.GetPixel(i, y);
                if (col.R != 0 || col.G != 0 || col.B != 0)
                {
                    break;
                }
                i++;
            }
            i--; //Pixel no en circulo

            float xCenter = ((i - x) / 2) + x;
            float yCenter = ((j - y) / 2) + y;
            float radius = (j - y) / 2;

            Circle circle = new Circle(xCenter, yCenter, radius);

            return circle;
        }

        Point PictureBox2bmpCoordinates(int x, int y, Bitmap bmp)
        {
            float w_p, h_p;
            float w_i, h_i;
            float x_p, y_p;
            float x_i, y_i;
            float k, k_2;
            float d_x, d_y;
            x_p = x;
            y_p = y;
            w_p = PictureBoxImage.Width;
            h_p = PictureBoxImage.Height;
            w_i = bmp.Width;
            h_i = bmp.Height;
            k = w_p / w_i;
            k_2 = h_p / h_i;
            if (k_2 < k)
                k = k_2;
            d_x = (w_p - k * w_i) / 2;
            d_y = (h_p - k * h_i) / 2;

            x_i = (x_p - d_x) / k;
            y_i = (y_p - d_y) / k;

            return new Point((int)Math.Round(x_i), (int)Math.Round(y_i));
        }

        private void ButtonAddPrey_Click(object sender, EventArgs e)
        {
            ButtonAddPrey.Enabled = false;

            ButtonAddPredator.Enabled = true;
            if (sceneGraph.Objective == null)
                ButtonAddObjective.Enabled = true;
        }

        private void ButtonAddPredator_Click(object sender, EventArgs e)
        {
            ButtonAddPredator.Enabled = false;

            ButtonAddPrey.Enabled = true;
            if (sceneGraph.Objective == null)
                ButtonAddObjective.Enabled = true;
        }

        private void ButtonAddObjective_Click(object sender, EventArgs e)
        {
            ButtonAddObjective.Enabled = false;

            ButtonAddPrey.Enabled = true;
            ButtonAddPredator.Enabled = true;
        }

        private void PictureBoxImage_MouseClick(object sender, MouseEventArgs e)
        {
            // Check if a vertex was clicked
            Point clickPoint = PictureBox2bmpCoordinates(e.X, e.Y, bmpScene);
            Vertex clickVertex = sceneGraph.BelongsToVertex(clickPoint.X, clickPoint.Y);
            if (clickVertex == null)
                return;

            // Check if there is already someting in clicked vertex
            if (clickVertex == sceneGraph.Objective)
                return;
            foreach (Prey p in sceneGraph.Preys)
                if (clickVertex == sceneGraph.BelongsToVertex(p.Position.X, p.Position.Y))
                    return;
            foreach (Predator p in sceneGraph.Predators)
                if (clickVertex == sceneGraph.BelongsToVertex(p.Position.X, p.Position.Y))
                    return;

            if (!ButtonAddPrey.Enabled)
            {
                Prey newPrey = new Prey(iIdGenerator++, sceneGraph, clickVertex);
                newPrey.Draw(render);
                sceneGraph.Preys.Add(newPrey);
                if (sceneGraph.Objective != null)
                    ButtonSimulate.Enabled = true;
                LabelPreyCount.Text = "Presas: " + sceneGraph.Preys.Count;
                LabelPreySpeed.Text = "Velocidad: " + newPrey.Speed;
            }
            else if (!ButtonAddPredator.Enabled)
            {
                Predator newPredator = new Predator(iIdGenerator++, sceneGraph, clickVertex);
                newPredator.Draw(render);
                sceneGraph.Predators.Add(newPredator);
                LabelPredatorCount.Text = "Depredadores: " + sceneGraph.Predators.Count;
                LabelPredatorRange.Text = "Rango: " + newPredator.VisionRange + " metros";
                LabelPredatorSpeed.Text = "Velocidad: " + newPredator.Speed;
            }
            else if (!ButtonAddObjective.Enabled)
            {
                if (sceneGraph.Objective == null)
                {
                    sceneGraph.Objective = clickVertex;
                    sceneGraph.DrawObjective(render);
                    if (sceneGraph.Preys.Count > 0)
                        ButtonSimulate.Enabled = true;
                }
            }
            PictureBoxImage.Refresh();
        }

        private void ButtonSimulate_Click(object sender, EventArgs e)
        {
            ButtonAddPrey.Enabled = false;
            ButtonAddPredator.Enabled = false;
            ButtonAddObjective.Enabled = false;
            ButtonSimulate.Enabled = false;

            foreach (Prey p in sceneGraph.Preys)
                p.SetInitialPath();

            Prey preyInObjective = null;
            StopEvent stop = StopEvent.NoPreysLeft;
            int iCurrentPreys = sceneGraph.Preys.Count;

            while (sceneGraph.Objective != null && sceneGraph.Preys.Count > 0)
            {
                render.Clear(Color.Transparent);
                foreach (Prey prey in sceneGraph.Preys)
                {
                    prey.Update();
                    prey.Draw(render);

                    if (sceneGraph.Objective == null && preyInObjective == null)
                    {
                        if (prey.Predator != null)
                            prey.Predator.Prey = null;
                        preyInObjective = prey;
                        stop = StopEvent.PreyOnObjective;
                    }
                }
                foreach (Predator predator in sceneGraph.Predators)
                {
                    predator.Update();
                    predator.Draw(render);
                }

                if (iCurrentPreys != sceneGraph.Preys.Count)
                {
                    LabelPreyCount.Text = "Presas: " + sceneGraph.Preys.Count;
                    iCurrentPreys = sceneGraph.Preys.Count;
                }

                sceneGraph.DrawObjective(render);
                PictureBoxImage.Refresh();
                Application.DoEvents();
            }
            
            if (preyInObjective != null)
                sceneGraph.Preys.Remove(preyInObjective);

            if (stop == StopEvent.NoPreysLeft)
                LabelStopEvent.Text = "No quedan mas presas";
            else
                LabelStopEvent.Text = "Presa en el objetivo";

            LabelPreyCount.Text = "Presas: " + sceneGraph.Preys.Count;

            ButtonAddPrey.Enabled = true;
            ButtonAddPredator.Enabled = true;
            ButtonAddObjective.Enabled = true;
        }
    }
}
