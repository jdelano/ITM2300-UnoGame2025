using System;

namespace UnoGame;

public class Player
{
    private readonly int _number;
    private static int playerCount = 0;

    public Hand Hand { get; set; }

    public int CardsInHand
    {
        get
        {
            return Hand.Count;
        }
    }

    public Player()
    {
        Hand = [];  // new();
        // playerCount++;
        _number = ++playerCount;
    }

    public void ReceiveCard(Card card)
    {
        Hand.Add(card);
    }

    public void RemoveCard(Card card)
    {
        Hand.Remove(card);
    }

    public void Display()
    {
        Console.WriteLine($"Player {_number}'s Hand:\n");
        Hand.Display();
    }

}
