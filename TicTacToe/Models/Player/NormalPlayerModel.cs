using System;
namespace TicTacToe.Models
{
    public class NormalPlayerModel : IPlayerModel
    {
        public int PlayerId { get; set; }

        public bool IsPlayerTurn { get; set; }

        public PlayerType PlayerType { get; set; } = PlayerType.Normal;

        public int PlayerAvatar { get; set; }

        public AIDifficulty Difficulty { get; set; }
    }
}
