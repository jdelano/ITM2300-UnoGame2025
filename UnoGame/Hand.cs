using System;

namespace UnoGame;

public class Hand : List<Card>
{
    public void Display()
    {
        for (int cardIndex = 0; cardIndex < Count; cardIndex++)
        {
            Console.WriteLine($"{cardIndex}: {this[cardIndex]}");
        }
        Console.WriteLine("D: Draw a card from the deck.");
    }

}
