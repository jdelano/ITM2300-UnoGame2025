using System;

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
            Console.WriteLine($"Congratulations! Player {currentPlayerIndex} Wins!");
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
        throw new NotImplementedException();
    }

    private void ProcessInput()
    {
        throw new NotImplementedException();
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
