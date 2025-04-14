using System;

namespace UnoGame;

public class DiscardPile : List<Card>
{
    public Card TopCard
    {
        get
        {
            return this[^1];
        }
    }

    public void Display()
    {
        Console.WriteLine($"The top card on the discard pile is: {TopCard}");
    }

}
