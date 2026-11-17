using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace sprites
{
    internal class Sprite
    {
        public Texture2D textura;
        public Vector2 posicion;

        public Sprite(Texture2D textura, Vector2 posicion)
        {
            this.textura = textura;
            this.posicion = posicion;
        }
    }
}

