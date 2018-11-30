namespace Game1.Core
{
    public class Roll
    {
        private static Roll _dice = null;
        public static Roll Dice
        {
            get
            {
                if (_dice == null) _dice = new Roll();

                return _dice;
            }
        }

        public int D100()
        {
            return RogueSharp.DiceNotation.Dice.Roll("1d100");
        }
    }
}
