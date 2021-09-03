// Вставьте сюда финальное содержимое файла GhostsTask.cs

using System;
using System.Text;

namespace hashes
{
    public class GhostsTask :
        IFactory<Document>, IFactory<Vector>, IFactory<Segment>, IFactory<Cat>, IFactory<Robot>,
        IMagic
    {
        private static readonly Vector Vector = new Vector(0, 0);
        private readonly byte[] documentArray = { 8, 4, 4, 8 };
        private readonly Document document;
        private readonly Segment segment = new Segment(Vector, Vector);
        private readonly Cat cat = new Cat("Alex", "Siberian", DateTime.MaxValue);
        private readonly Robot robot = new Robot("Robot");

        public GhostsTask() => document = new Document("C#", Encoding.UTF8, documentArray);

        public void DoMagic()
        {
            Vector.Add(new Vector(1, 1));
            segment.End.Add(new Vector(2, 2));
            cat.Rename("Cat");
            documentArray[0] = 1;
            Robot.BatteryCapacity *= 100;
        }

        Vector IFactory<Vector>.Create() => Vector;
        Document IFactory<Document>.Create() => document;
        Segment IFactory<Segment>.Create() => segment;
        Cat IFactory<Cat>.Create() => cat;
        Robot IFactory<Robot>.Create() => robot;
    }
}