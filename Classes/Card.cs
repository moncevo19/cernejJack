using System;
using System.Collections.Generic;
using System.Text;

namespace cernejJack.Classes
{
    class Card
    {
        public int value;
        public int color;

        public Card(int _value, int _color)
        {
            this.value = _value;
            this.color = _color;
        }
        public string returnColorString()
        {
            string colorTmp = "";
            colorTmp = (this.color == 0) ? "♣" : (this.color == 1) ? "♦" : (this.color == 2) ? "♥" : "♠"; //kouzlo :-)
            return colorTmp;

        }
        public string returnValueString()
        {
            string valueTmp = "";
            valueTmp = (this.value == 11) ? " J" : (this.value == 12) ? " Q" : (this.value == 13) ? " K" : (this.value == 1) ? " A" : " "+this.value; //kouzlo :-)
            valueTmp = (this.value == 10) ? "10" : valueTmp;
            return valueTmp;

        }
    }
}
