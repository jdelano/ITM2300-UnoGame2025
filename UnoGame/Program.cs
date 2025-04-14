using UnoGame;

Deck deck = new();
foreach (var card in deck)
{
    Console.WriteLine($"{card}");
}
Console.WriteLine(deck.Count);
