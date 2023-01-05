using Server.Game.Entities;

namespace Server.Game.Chat
{
    public class BattleshipsGameChat
    {

        private readonly BattleshipsGame game;

        public BattleshipsGameChat(BattleshipsGame game) 
        {
            this.game = game;
        }

        private async Task Send(string message)
        {
            foreach (Player player in game.Players)
            {
                player.Chat.SendMessage(message);
            }
        }

        public async Task SendGameMessage(string message)
        {
            await Send("[GAME]" + message);
        }

        public async void SendJoinMessage(Player player)
        {
            string message = $"{player.Name} just joined. ({game.Players.Count}/{BattleshipsGame.s_gamePlayerSize})";
            await SendGameMessage(message);
        }

        public async void SendStartMessage()
        {
            await SendGameMessage("Game is starting now!");
        }

        public async void SendGameOverMessage(Player winner)
        {
            await SendGameMessage(winner.Name + " won the game!");
        }
    }
}
