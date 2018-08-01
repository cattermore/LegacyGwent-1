using System;
using System.Linq;
using System.Threading.Tasks;
using Alsein.Utilities;
using Alsein.Utilities.LifetimeAnnotations;

namespace Cynthia.Card.Client
{
    [Transient]
    public class GwentClientGameService
    {
        private LocalPlayer _player;
        public async Task Play(LocalPlayer player)
        {
            _player = player;
            var op1 = await _player.ReceiveFromDownstreamAsync();
            var gameInformation = op1.Arguments.ToArray()[0].ToType<GameInfomation>();
            Console.WriteLine($"~匹配成功~");
            Console.WriteLine($"您的对手是:{gameInformation.OpponentName},他的卡组有{gameInformation.OpponentCardCount}张牌的说~!\n\n");
            Console.WriteLine($"抽到了手牌呢,您的手牌是");
            var hand = GwentMap.DeckChange(gameInformation.YourHandCard);
            hand.Select(x => $"{GwentMap.FlavorMap[x.Flavor]}{x.Strength}  ").ForAll(Console.Write);
            Console.WriteLine("\n\n看起来不错呢~不过因为游戏还没有完成,胜负只能交给伟大的RNG啦!\n因为需要一点悬念~所以请按下任意键知晓比赛结果~");
            var op2 = await _player.ReceiveFromDownstreamAsync();
            var end = op2.Arguments.ToArray()[0].ToType<bool>();
            if (end)
            {
                Console.WriteLine($"胜利了呢,RNG万岁!");
                return;
            }
            Console.WriteLine($"失败了呢...看来运气有点不好呐");
        }
    }
}