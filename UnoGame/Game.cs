using System;
using System.Security;

namespace UnoGame;

public enum Direction
{
    Left, Right
}

public class Game
{
    private bool isRoundOver = false;
    private bool isGameOver = false;
    private int currentPlayerIndex = 0;
    private Direction gameDirection;
    private int numberOfPlayers;

    private Player CurrentPlayer
    {
        get
        {
            return Players[currentPlayerIndex];
        }
    }

    public List<Player> Players { get; set; }
    public DiscardPile DiscardPile { get; set; }
    public Deck Deck { get; set; }

    public Game()
    {
        Deck = [];
        DiscardPile = [];
        Players = [];
    }

    public void Run()
    {
        InitializeGame();
        do
        {
            InitializeRound();
            do
            {
                ProcessInput();
                RenderOutput();
            } while (!isRoundOver);
            Console.WriteLine($"Congratulations! Player {currentPlayerIndex + 1} Wins!");
            Console.WriteLine("Press Q to quit or any other key to try another round...");
            var keyInfo = Console.ReadKey();
            if (keyInfo.Key == ConsoleKey.Q)
            {
                isGameOver = true;
            }
        } while (!isGameOver);
    }

    private void RenderOutput()
    {
        Console.Clear();
        DiscardPile.Display();
        CurrentPlayer.Display();
    }

    private void ProcessInput()
    {
        bool validCard = false;
        Card card;
        do
        {
            Console.Write("Which card would you like to play (enter D to draw a card)? ");
            string response = Console.ReadLine();
            if (response.ToUpper() == "D")
            {
                card = Deck.DrawCard();
                CurrentPlayer.ReceiveCard(card);
                Console.WriteLine($"\n\nYou drew the {card} card.");
            }
            else
            {
                if (!int.TryParse(response, out int index) || index < 0 || index >= CurrentPlayer.CardsInHand)
                {
                    Console.WriteLine("Invalid selection. Please try again.");
                    continue;
                }
                else
                {
                    card = CurrentPlayer.Hand[index];
                    Console.WriteLine($"\n\nYou selected the {card} card.");
                }
            }
            validCard = PlayCard(CurrentPlayer, card);
        } while (!validCard);
    }

    private bool PlayCard(Player currentPlayer, Card card)
    {
        if (DiscardPile.TopCard.Matches(card))
        {
            currentPlayer.RemoveCard(card);
            DiscardPile.Add(card);
            if (currentPlayer.CardsInHand == 0)
            {
                isRoundOver = true;
            }
            else
            {
                Action<Card> action = GetActionForCard(card);
                action(card);
            }
            return true;
        }
        else
        {
            Console.WriteLine("\nThe card selected/drawn does not match the top card on the discard pile. It remains in your hand. Please try again.");
            return false;
        }

    }

    private Action<Card> GetActionForCard(Card card)
    {
        // Face, Reverse, Skip, DrawTwo, DrawFour, Wild
        switch (card.Type)
        {
            case CardType.Reverse:
                return c =>
                {
                    ReverseDirection();
                    SetNextPlayer();
                };
            case CardType.Skip:
                return c => SetNextPlayer(2);
            case CardType.DrawTwo:
                return c =>
                {
                    SetNextPlayer();
                    for (int i = 0; i < 2; i++)
                    {
                        CurrentPlayer.ReceiveCard(Deck.DrawCard());
                    }
                    SetNextPlayer();
                };
            case CardType.DrawFour:
                return c =>
                {
                    c.UpdateChosenColor();
                    SetNextPlayer();
                    for (int i = 0; i < 4; i++)
                    {
                        CurrentPlayer.ReceiveCard(Deck.DrawCard());
                    }
                    SetNextPlayer();
                };
            case CardType.Wild:
                return c =>
                {
                    c.UpdateChosenColor();
                    SetNextPlayer();
                };
            default:
                return c => SetNextPlayer();

        }
    }

    private void ReverseDirection()
    {
        gameDirection = gameDirection == Direction.Right ? Direction.Left : Direction.Right;
    }

    private void SetNextPlayer(int skipPlayers = 1)
    {
        int direction = gameDirection == Direction.Right ? -1 : 1;
        currentPlayerIndex = (currentPlayerIndex + Players.Count + direction * skipPlayers) % Players.Count;
    }

    private void InitializeRound()
    {
        currentPlayerIndex = 0;
        isRoundOver = false;
        Deck.Shuffle();
        DealCards();
        DealCardToDiscardPile();
        gameDirection = Direction.Left;
        RenderOutput();
    }

    private void DealCardToDiscardPile()
    {
        DiscardPile.Add(Deck.DrawCard());
    }

    private void DealCards()
    {
        for (int cardIndex = 0; cardIndex < 7; cardIndex++)
        {
            foreach (var player in Players)
            {
                player.ReceiveCard(Deck.DrawCard());
            }
        }
    }

    private void InitializeGame()
    {
        Console.Write("Enter the number of players: ");
        numberOfPlayers = int.Parse(Console.ReadLine());
        for (int playerNumber = 0; playerNumber < numberOfPlayers; playerNumber++)
        {
            Players.Add(new());
        }
        isGameOver = false;
    }
}
