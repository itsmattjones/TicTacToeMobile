namespace TicTacToe.Models
{
    public enum PlayerType 
    {
        Unknown,
        Normal,
        AI
    }

    public enum AIDifficulty
    {
        Easy,
        Medium,
        Hard
    }

    public interface IPlayerModel
    {
        int PlayerId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether it's this players turn.
        /// </summary>
        /// <value><c>true</c> if is player turn; otherwise, <c>false</c>.</value>
        bool IsPlayerTurn { get; set; }

        /// <summary>
        /// Gets or sets the type of the player.
        /// </summary>
        /// <value>The type of the player.</value>
        PlayerType PlayerType { get; set; }

        /// <summary>
        /// Gets or sets the player avatar. (Noughts/Crosses)
        /// </summary>
        /// <value>The player avatar.</value>
        int PlayerAvatar { get; set; }

        AIDifficulty Difficulty { get; set; }
    }
}
