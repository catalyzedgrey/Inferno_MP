using GameForm.framework;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace GameForm.objects
{
    public abstract class GameObject : Sounds
    {
        #region fields
        protected float x, y, dx = 0, dy = 0;
        
        protected ObjectID id;
        // enum ObjectId ID{player, block, flag, enemy };
        protected bool isDead = false;

        protected bool falling = true;
        protected bool jumping = false;

        protected int facing = 0;

        public static bool debugDrawings;
        #endregion

        #region properties
        public float X
        {
            get { return this.x; }
            set { this.x = value; }
        }

        public float Y
        {
            get { return this.y; }
            set { this.y = value; }
        }

        public float DX
        {
            get { return this.dx; }
            set { this.dx = value; }
        }
        public float DY
        {
            get { return this.dy; }
            set { this.dy = value; }
        }

        public bool Falling
        {
            get { return this.falling; }
            set { this.falling = value; }
        }
        public int Facing
        {
            get { return this.facing; }
        }
        public bool Dead
        {
            get { return this.isDead; }
            set
            {
                DSoundPlay();
                this.isDead = value;
            }
        }

        public bool Jumping
        {
            get { return this.jumping; }
            set
            {
                JSoundPlay();
                this.jumping = value;
            }
        }
        #endregion

        static GameObject(){
            debugDrawings = false;
            }

         public GameObject(float x, float y, ObjectID id)
        {
            this.x = x;
            this.y = y;
            this.id = id;
        }

        public abstract void tick(List<GameObject> tempObject);
        public abstract void render(Graphics g);
        public abstract Rectangle getBounds();
        public ObjectID getId()
        {
            return id;
        }

    }
}
