using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameForm.framework
{
    public class ObjectID
    {
        public static readonly ObjectID Player = new ObjectID("Player");
        public static readonly ObjectID Player2 = new ObjectID("Player2");
        public static readonly ObjectID Block = new ObjectID("Block");
        public static readonly ObjectID Flag = new ObjectID("Flag");
        public static readonly ObjectID Trap = new ObjectID("Trap");
        public static readonly ObjectID Trap2 = new ObjectID("Trap2");
        public static readonly ObjectID Boulder = new ObjectID("Boulder");
        public static readonly ObjectID Projectile = new ObjectID("Projectile");
        public static readonly ObjectID JumpOrb = new ObjectID("JumpOrb");
        public static readonly ObjectID death_rasengan = new ObjectID("death_rasengan");
        public static readonly ObjectID Hole = new ObjectID("Hole");
        public static readonly ObjectID PowerOrb = new ObjectID("PowerOrb");

        private readonly string name;

        public string Name { get { return name; } }

        public ObjectID(string name)
        {
            this.name = name;
        }

    }
}
