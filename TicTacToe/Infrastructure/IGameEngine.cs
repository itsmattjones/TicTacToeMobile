using System.Collections.Generic;
using TicTacToe.Models;

namespace TicTacToe.Infrastructure
{
    public struct GameResults
    {
        public bool GameHasWinner;
        public IPlayer Winner;
    }

    public struct EngineTickResult
    {
        public bool SuccessfulTick;
        public bool GameFinished;
        public GameResults Results;
    }

    public enum GameType
    {
        Singleplayer = 0,
        Multiplayer = 1
    }

    public interface IGameEngine
    {
        GameType GameType { get; }
        List<IBoardCell> Board { get; }
        List<IPlayer> Players { get; }

        EngineTickResult TickPlayerTurn(int chosenCell);
        EngineTickResult TickAIPlayerTurn();
    }
}
