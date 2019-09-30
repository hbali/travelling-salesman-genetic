using GeneticAlgorithm;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
using System.Windows.Threading;

namespace TravellingSalesman
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Town> towns;

        DispatcherTimer timer;

        Salesman s;

        public MainWindow()
        {
            InitializeComponent();
            LoadTowns();
            s = new Salesman(towns);

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(10);

            Task t = new Task(() =>
            {
                s.Travel();
            });

            t.Start();

            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (s.Fittest() != null)
            {
                canvas.Children.Clear();
                DrawTravel(s.Fittest());
            }
        }

        private void DrawTravel(List<Point> best)
        {
            DrawPoint(best[0].X, best[0].Y, Colors.Red);

            for (int i = 0; i < best.Count - 1; i++)
            {
                Point p1 = best[i];
                Point p2 = best[i + 1];
                DrawPoint(best[i + 1].X, best[i + 1].Y, Colors.Red);
                DrawLine(p1.X, p1.Y, p2.X, p2.Y);
            }
        }

        private void LoadTowns()
        {
            towns = new List<Town>();
            foreach(string s in File.ReadAllLines("Towns.txt"))
            {
                towns.Add(new Town(float.Parse(s.Split(';')[0])*2, float.Parse(s.Split(';')[1])*2));
            }
        }


        private Ellipse DrawPoint(double x, double y, Color color, float dotSize = 10, bool fill = true)
        {
            Ellipse currentDot = new Ellipse();
            currentDot.Stroke = new SolidColorBrush(color);
            currentDot.StrokeThickness = 3;
            Canvas.SetZIndex(currentDot, 3);
            currentDot.Height = dotSize;
            currentDot.Width = dotSize;
            currentDot.Fill = fill ? new SolidColorBrush(Colors.Green) : null;
            currentDot.Margin = new Thickness(x - dotSize / 2, y - dotSize / 2, 0, 0); // Sets the position.
            canvas.Children.Add(currentDot);
            return currentDot;
        }

        private void DrawLine(double x1, double y1, double x2, double y2)
        {
            // Add a Line Element
            System.Windows.Shapes.Line myLine = new System.Windows.Shapes.Line();
            myLine.Stroke = System.Windows.Media.Brushes.LightSteelBlue;
            myLine.X1 = x1;
            myLine.X2 = x2;
            myLine.Y1 = y1;
            myLine.Y2 = y2;
            myLine.HorizontalAlignment = HorizontalAlignment.Left;
            myLine.VerticalAlignment = VerticalAlignment.Center;
            myLine.StrokeThickness = 2;
            canvas.Children.Add(myLine);
        }
    }
}
