namespace TicTacToe.Models
{
    public enum CellState
    {
        Available,
        TakenByPlayer1,
        TakenByPlayer2
    }

    public interface IBoardCell
    {
        CellState State { get; set; }
    }
}
