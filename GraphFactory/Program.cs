using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphFactory
{

    public abstract class Graph
    {
        public abstract void Draw();
    }

    public class LineGraph : Graph
    {
        public override void Draw()
        {
            Console.WriteLine("Drawing a line graph.");
        }
    }

    public class BarGraph : Graph
    {
        public override void Draw()
        {
            Console.WriteLine("Drawing a bar graph.");
        }
    }

    public class PieChart : Graph
    {
        public override void Draw()
        {
            Console.WriteLine("Drawing a pie chart.");
        }
    }

    public abstract class GraphFactory
    {
        public abstract Graph CreateGraph();
    }

    public class LineGraphFactory : GraphFactory
    {
        public override Graph CreateGraph()
        {
            return new LineGraph();
        }
    }

    public class BarGraphFactory : GraphFactory
    {
        public override Graph CreateGraph()
        {
            return new BarGraph();
        }
    }

    public class PieChartFactory : GraphFactory
    {
        public override Graph CreateGraph()
        {
            return new PieChart();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, GraphFactory> factories = new Dictionary<string, GraphFactory>
        {
            { "line", new LineGraphFactory() },
            { "bar", new BarGraphFactory() },
            { "pie", new PieChartFactory() }
        };

            while (true)
            {
                Console.WriteLine("Enter graph type (line, bar, pie, or exit):");
                string type = Console.ReadLine();

                if (type == "exit")
                {
                    break;
                }

                if (factories.ContainsKey(type))
                {
                    Graph graph = factories[type].CreateGraph();
                    graph.Draw();
                }
                else
                {
                    Console.WriteLine("Unknown graph type.");
                }
            }
        }
    }

}
