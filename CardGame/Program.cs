using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
           PokerDeck deck = new PokerDeck();

          //  BlackjackDeck deck = new BlackjackDeck();


            var hand = deck.DealCards();

            foreach (var card in hand)
            {
                Console.WriteLine($"{card.Suit.ToString()} {card.Value.ToString()}");
            }


            Console.ReadLine();
        }
    }

    public abstract class Deck
    {
        protected List<PlayingCard> fulldeck = new List<PlayingCard>();
        protected List<PlayingCard> drawPile = new List<PlayingCard>();
        protected List<PlayingCard> discardPile = new List<PlayingCard>();

        protected void CreateDeck()
        {

            fulldeck.Clear();

            for (int suit = 0; suit < 4; suit++)
            {
                for (int val = 0; val < 13; val++)
                {
                    fulldeck.Add(new PlayingCard { Suit = (CardSUit)suit, Value = (CardValue)val });
                }
            }

        }

        public virtual void ShuffleDeck()
        {

            //var rand = new Random();
            //Var ramdomList = imagesEasy.OrderBy(x => rand.Next()).ToList();
            var rnd = new Random();
            drawPile = fulldeck.OrderBy(x => rnd.Next()).ToList();


        }


        public abstract List<PlayingCard> DealCards();

        protected virtual PlayingCard DrawOnecard()
        {
            PlayingCard output = drawPile.Take(1).First();
            drawPile.Remove(output);

            return output;
        }

    }


    public class PokerDeck : Deck {
    
        public PokerDeck() {

            CreateDeck();
            ShuffleDeck();
        }
    public override List<PlayingCard> DealCards()
        {
            List<PlayingCard> output = new List<PlayingCard>();

            for (int i = 0; i < 5; i++)
            {
                output.Add(DrawOnecard());

            }

            return output;
        }

        public List<PlayingCard> RequestCards(List<PlayingCard> cardsToDiscard)
        {
            List<PlayingCard> output = new List<PlayingCard>();

            foreach (var card in cardsToDiscard)
            {
                output.Add(DrawOnecard());
                discardPile.Add(card);
            }
            return output;
        }
    }

    public class BlackjackDeck : Deck
    {


        public BlackjackDeck()
        {

            CreateDeck();
            ShuffleDeck();
        }
        public override List<PlayingCard> DealCards()
        {
            List<PlayingCard> output = new List<PlayingCard>();

            for (int i = 0; i < 2; i++)
            {
                output.Add(DrawOnecard());

            }

            return output;
        }

        public PlayingCard RequestCard()
        {
            return DrawOnecard();
        }
    }
}
