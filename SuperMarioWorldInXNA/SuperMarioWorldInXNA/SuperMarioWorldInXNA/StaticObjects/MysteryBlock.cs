using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace SuperMarioWorldInXNA.StaticObjects
{
    class MysteryBlock : StaticObject
    {
        enum Contain
        {
            Mushroom,
            Coin,
            Flower,
            Empty
        };

        Contain Contains;
        int ItemsLeft;

        public override Texture2D texture
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Creates an empty MysteryBlock
        /// </summary>
        public MysteryBlock() : base()
        {
            Contains = Contain.Empty;
            ItemsLeft = 0;
        }

        /// <summary>
        /// Puts the specific item in the MysteryBlock. 
        /// s determines the item can be "Coin", "Flower", or "Mushroom" otherwise the block will stay empty.
        /// </summary>
        /// <param name="s"></param>
        public MysteryBlock(string s) : base()
        {
            ItemsLeft = 1;
            if (s.Equals(Contain.Coin.ToString()))
                Contains = Contain.Coin;
            else if (s.Equals(Contain.Flower.ToString()))
                Contains = Contain.Flower;
            else if (s.Equals(Contain.Mushroom.ToString()))
                Contains = Contain.Mushroom;
            else
            {
                Contains = Contain.Empty;
                ItemsLeft = 0;
            }
        }

        /// <summary>
        /// Creates a MysteryBlock with a specific item.
        /// s determines the item can be "Coin", "Flower", or "Mushroom" otherwise the block will stay empty.
        /// i determines the amount of that item inside.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="i"></param>
        public MysteryBlock(string s, int i) : base()
        {
            ItemsLeft = i;
            if (s.Equals(Contain.Coin.ToString()))
                Contains = Contain.Coin;
            else if (s.Equals(Contain.Flower.ToString()))
                Contains = Contain.Flower;
            else if (s.Equals(Contain.Mushroom.ToString()))
                Contains = Contain.Mushroom;
            else
            {
                Contains = Contain.Empty;
                ItemsLeft = 0;
            }
        }


        public void OnHit()
        {
            SpitItem();
            if (ItemsLeft <= 0)
            {
                //change sprite
            }
        }

        private void SpitItem()
        {
            //Spit out the item inside (create new item) and lower the ItemsLeft by one.
        }

    }
}