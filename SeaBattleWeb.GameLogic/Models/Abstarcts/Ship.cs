using System;

namespace SeaBattleWeb.GameLogic.Models.Abstracts
{
    public abstract class Ship
    {
        public int hitCount { get; private set; }

        public string Name { get; init; }
        public int Size { get; init; }

        public bool IsDestroyed => hitCount == Size;

        public void AddHit()
        {
            if (IsDestroyed)
                return;
            hitCount++;
        }
        protected Ship(int size, string name)
        {
            Size = size;
            Name = name;
        }
    }
}
