namespace TicTacToe.Models.Player
{
    public class AiPlayerModel : IPlayerModel
	{
        public int PlayerId { get; set; }

        public bool IsPlayerTurn { get; set; }

        public PlayerType PlayerType { get; set; } = PlayerType.Ai;

        public int PlayerAvatar { get; set; }

        public AiDifficulty Difficulty { get; set; }
	    
		public int TurnsTaken { get; set; }
    }
}
