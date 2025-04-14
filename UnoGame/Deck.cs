using System;

namespace UnoGame;

public class Deck : List<Card>
{
    private static readonly Random _random = new();

    public Deck()
    {
        for (int color = 0; color < 4; color++)
        {
            for (int number = 0; number < 10; number++)
            {
                if (number > 0)
                {
                    Add(new($"{number}", (CardColor)color));
                }
                Add(new($"{number}", (CardColor)color));
            }

            for (int count = 0; count < 2; count++)
            {
                Add(new("S", (CardColor)color, CardType.Skip));
                Add(new("R", (CardColor)color, CardType.Reverse));
                Add(new("+2", (CardColor)color, CardType.DrawTwo));
            }
        }

        for (int count = 0; count < 4; count++)
        {
            Add(new("W", CardColor.Black, CardType.Wild));
            Add(new("+4", CardColor.Black, CardType.DrawFour));
        }
        Shuffle();
    }

    public Card DrawCard()
    {
        Card cardDrawn = this[^1];
        Remove(cardDrawn);
        return cardDrawn;
    }

    public void Shuffle()
    {
        for (int cardIndex = 0; cardIndex < Count; cardIndex++)
        {
            int randomIndex = _random.Next(0, Count);
            (this[cardIndex], this[randomIndex]) = (this[randomIndex], this[cardIndex]);
        }

    }
}
