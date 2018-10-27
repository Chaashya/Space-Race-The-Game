using System.Drawing;
using System.ComponentModel;
using Object_Classes;


namespace Game_Logic_Class
{
    public static class SpaceRaceGame
    {
        // Minimum and maximum number of players.
        public const int MIN_PLAYERS = 2;
        public const int MAX_PLAYERS = 6;

        private static int numberOfPlayers = 2;  //default value for test purposes only 
        public static int NumberOfPlayers
        {
            get
            {
                return numberOfPlayers;
            }
            set
            {
                numberOfPlayers = value;
            }
        }

        public static string[] names = { "One", "Two", "Three", "Four", "Five", "Six" };  // default values

        // Only used in Part B - GUI Implementation, the colours of each player's token
        private static Brush[] playerTokenColours = new Brush[MAX_PLAYERS] { Brushes.Yellow, Brushes.Red,
                                                                       Brushes.Orange, Brushes.White,
                                                                      Brushes.Green, Brushes.DarkViolet};
        /// <summary>
        /// A BindingList is like an array which grows as elements are added to it.
        /// </summary>
        private static BindingList<Player> players = new BindingList<Player>();
        public static BindingList<Player> Players
        {
            get
            {
                return players;
            }
        }

        // The pair of die
        private static Die die1 = new Die(), die2 = new Die();


        /// <summary>
        /// Set up the conditions for this game as well as
        ///   creating the required number of players, adding each player 
        ///   to the Binding List and initialize the player's instance variables
        ///   except for playerTokenColour and playerTokenImage in Console implementation.
        ///   
        ///     
        /// Pre:  none
        /// Post:  required number of players have been initialsed for start of a game.
        /// </summary>
        public static void SetUpPlayers()
        {
            // for number of players
            //      create a new player object
            //      initialize player's instance variables for start of a game
            //      add player to the binding list
            Players.Clear();
            for (int current_player = 0; current_player < NumberOfPlayers; current_player++)
            {
                Player CurrentPlayer = new Player(names[current_player]);
                CurrentPlayer.Position = Board.START_SQUARE_NUMBER;
                CurrentPlayer.Location = Board.Squares[Board.START_SQUARE_NUMBER];
                CurrentPlayer.RocketFuel = Player.INITIAL_FUEL_AMOUNT;
                CurrentPlayer.HasPower = true;

                CurrentPlayer.PlayerTokenColour = playerTokenColours[current_player];
                players.Add(CurrentPlayer);
            }
        }

        /// <summary>
        ///  Plays one round of a game
        /// </summary>
        public static void PlayOneRound()
        {
            if (SingleStep)
            {
                Players[PlayerNum].Play(die1, die2);
                if (PlayerNum == (NumberOfPlayers - 1))
                {
                    CheckIfGameFinished();
                    PlayerNum = 0;

                }

                else
                {
                    PlayerNum++;
                }
            }
            else
            {
                foreach (Player player in Players)
                {
                    player.Play(die1, die2);
                }
                CheckIfGameFinished();
            }
        }

        private static bool gameFinished;
        public static bool GameFinished
        {
            get
            {
                return gameFinished;
            }

            set
            {
                gameFinished = value;
            }

        }//end SnakesAndLadders
        private static bool singleStep;
        public static bool SingleStep
        {
            get
            {
                return singleStep;
            }
            
            set
            {
                singleStep = value;
            }
        }
        private static int playerNum = 0;
        public static int PlayerNum
        {
            get
            {
                return playerNum;
            }

            set
            {
                playerNum = value;
            }
        }

        public static string final_output { get; private set; }

        public static string DisplayGameResults()
        {
            bool playerFinished = false;
            string final_output = " ";
            string finish_players = "";

            foreach (Player player in Players)
            {
                if (player.AtFinish)
                {
                    finish_players += string.Format("\n\t\t{0}", player.Name);
                    playerFinished = true;
                }
            }

            if (playerFinished) 
            {
                final_output += ("\n\n\tThe following player(s) finished the game.\n");
                final_output += (finish_players + "\n\n");
            }
            else 
            {
                final_output += ("\n\n\tNo players finished the game\n");
            }

            return final_output;
        }


        /// <summary>
        /// At the end of a round, checks that any player is at the finish or if all players cannot move (out of fuel)
        /// Sets the property 'GameFinished' to true if the game is finished.
        /// </summary>
        private static void CheckIfGameFinished()
        {
            //Should only be called at the end of the last player's turn in single step mode.
            bool allPlayersNoPower = true;


            foreach (Player player in Players)
            {
                //Check if at least one player has power
                if (player.HasPower)
                {
                    allPlayersNoPower = false;
                }
                //Check that it is the end of the game on the current round
                if (player.AtFinish || allPlayersNoPower)
                {
                    GameFinished = true;
                }
            }
        }




    }//end SnakesAndLadders
}
