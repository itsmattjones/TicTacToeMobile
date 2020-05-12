namespace TicTacToe.Models
{
    public class Player : IPlayer
    {
        public int PlayerId { get; set; }

        public PlayerType PlayerType { get; set; } = PlayerType.Normal;

        public string PlayerName { get; set; } = "User";

        public int PlayerAvatar { get; set; }

        public bool IsPlayerTurn { get; set; }

        public int TurnsTaken { get; set; }
    }
}
