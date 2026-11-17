using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sprites
{
    internal class Pokemon
    {

            public string nombre { get; set; }
            public int nivel { get; set; }

            public int ps { get; set; }
            public int psa { get; set; }

            public int ataque { get; set; }

            public int defensa { get; set; }

            public int ae { get; set; }

            public int de { get; set; }

            public int velocidad { get; set; }

            public string estado { get; set; }

            public Sprite frente { get; set; }

            public Pokemon(string nombre, int nivel, int ps, int psa, int ataque, int defensa, int ae, int de, int velocidad, string estado, Sprite frente)
            {
                this.nombre = nombre;
                this.nivel = nivel;
                this.ps = ps;
                this.psa = psa;
                this.ataque = ataque;
                this.defensa = defensa;
                this.ae = ae;
                this.de = de;
                this.velocidad = velocidad;
                this.estado = estado;
                this.frente = frente;
            }



            public void recibirDaño(int daño, bool tipo)
            {
                //Tipo es un booleano, asumamos 0 para ataque comun 1 para ataque especial.
                int porcentajeDefensa = 0;
                if (tipo)
                {
                    porcentajeDefensa = (daño * (defensa / 2)) / 100;
                    psa = psa - (daño - porcentajeDefensa);
                }
                else
                {
                    porcentajeDefensa = (daño * (de / 2)) / 100;
                    psa = psa - (daño - porcentajeDefensa);
                }
            }

        }
    }

