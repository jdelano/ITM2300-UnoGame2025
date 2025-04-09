using System;

namespace UnoGame;
public enum CardColor
{
    Red, Yellow, Blue, Green, Black
}

public enum CardType
{
    Face, Reverse, Skip, DrawTwo, DrawFour, Wild
}

public class Card
{
    public string Number { get; private set; }

    public CardColor Color { get; private set; }

    public CardColor ChosenColor { get; set; }

    public CardType Type { get; private set; }

    public bool IsWildCard
    {
        get
        {
            return Type == CardType.DrawFour || Type == CardType.Wild;
        }
    }

    public CardColor EffectiveColor
    {
        get
        {
            if (!IsWildCard)
            {
                return Color;
            }
            else if (ChosenColor == CardColor.Black)
            {
                return Color;
            }
            else
            {
                return ChosenColor;
            }
        }
    }

    public Card(string number, CardColor color, CardType type)
    {
        Number = number;
        Color = color;
        ChosenColor = color;
        Type = type;
    }

    public Card(string number, CardColor color) : this(number, color, CardType.Face)
    {

    }

    public bool Matches(Card other)
    {
        if ((IsWildCard && EffectiveColor == CardColor.Black) ||
            (other.IsWildCard && other.EffectiveColor == CardColor.Black))
        {
            return true;
        }
        return EffectiveColor == other.EffectiveColor || Number == other.Number;
    }

    public override string ToString()
    {
        return $"{EffectiveColor}-{Number}";
    }

    public void UpdateChosenColor()
    {
        if (IsWildCard)
        {
            Console.Write("Please specify a new color: 0=Red, 1=Yellow, 2=Blue, 3=Green ");
            int response = int.Parse(Console.ReadLine());
            ChosenColor = (CardColor)response;
        }
    }
}
