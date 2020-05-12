namespace TicTacToe.Models
{
    public enum PlayerType 
    {
        Normal,
        Ai
    }

    public interface IPlayer
    {
        int PlayerId { get; set; }

        PlayerType PlayerType { get; set; }

        string PlayerName { get; set; }

        int PlayerAvatar { get; set; }

        bool IsPlayerTurn { get; set; }
        
        int TurnsTaken { get; set; }
    }
}
