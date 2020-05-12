namespace TicTacToe.Models
{
    public class AIPlayer : IPlayer
	{
        public int PlayerId { get; set; }

        public PlayerType PlayerType { get; set; } = PlayerType.Ai;

        public string PlayerName { get; set; } = "Computer";

        public int PlayerAvatar { get; set; }

        public bool IsPlayerTurn { get; set; }

		public int TurnsTaken { get; set; }
    }
}
