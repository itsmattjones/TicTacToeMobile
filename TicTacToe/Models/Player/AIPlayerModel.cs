namespace TicTacToe.Models
{
    public class AIPlayerModel : IPlayerModel
	{
        public int PlayerId { get; set; }

        public bool IsPlayerTurn { get; set; }

        public PlayerType PlayerType { get; set; } = PlayerType.AI;

        public int PlayerAvatar { get; set; }

        public AIDifficulty Difficulty { get; set; }
    }
}
