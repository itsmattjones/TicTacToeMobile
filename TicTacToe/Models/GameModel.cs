using System.Collections.Generic;
using TicTacToe.Models.Player;

namespace TicTacToe.Models
{
    public class GameModel
    {
        public List<CellModel> Board { get; set; }
        public int BoardSize { get; set; } = 9;
        public IPlayerModel PlayerOne { get; set; }
        public IPlayerModel PlayerTwo { get; set; }
    }
}
