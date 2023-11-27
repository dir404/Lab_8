using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory
{

    public abstract class AbstractFactory
    {
        public abstract IProcessor CreateProcessor();
        public abstract IScreen CreateScreen();
        public abstract ICamera CreateCamera();
    }

    public class SmartphoneFactory : AbstractFactory
    {
        public override IProcessor CreateProcessor()
        {
            return new SmartphoneProcessor();
        }

        public override IScreen CreateScreen()
        {
            return new SmartphoneScreen();
        }

        public override ICamera CreateCamera()
        {
            return new SmartphoneCamera();
        }
    }

    public class LaptopFactory : AbstractFactory
    {
        public override IProcessor CreateProcessor()
        {
            return new LaptopProcessor();
        }

        public override IScreen CreateScreen()
        {
            return new LaptopScreen();
        }

        public override ICamera CreateCamera()
        {
            return new LaptopCamera();
        }
    }

    public interface IProcessor { }
    public interface IScreen { }
    public interface ICamera { }

    public class SmartphoneProcessor : IProcessor { }
    public class SmartphoneScreen : IScreen { }
    public class SmartphoneCamera : ICamera { }

    public class LaptopProcessor : IProcessor { }
    public class LaptopScreen : IScreen { }
    public class LaptopCamera : ICamera { }

    public class Client
    {
        private IProcessor _processor;
        private IScreen _screen;
        private ICamera _camera;

        public Client(AbstractFactory factory)
        {
            _processor = factory.CreateProcessor();
            _screen = factory.CreateScreen();
            _camera = factory.CreateCamera();
        }

        public void Run()
        { }
            class Program
            {
            static void Main(string[] args)
            {
                Console.WriteLine("Виберіть тип продукту для створення: 1 - Смартфон, 2 - Ноутбук");
                string choice = Console.ReadLine();

                AbstractFactory factory;
                if (choice == "1")
                {
                    factory = new SmartphoneFactory();
                }
                else if (choice == "2")
                {
                    factory = new LaptopFactory();
                }
                else
                {
                    Console.WriteLine("Невірний вибір. Будь ласка, виберіть 1 або 2.");
                    return;
                }

                Client client = new Client(factory);
                client.Run();

                Console.ReadLine();
            }
        } 

    }
}

