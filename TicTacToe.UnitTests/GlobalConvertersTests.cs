using System;
using Xunit;
using TicTacToe.Converters;
using System.Globalization;

namespace TicTacToe.UnitTests
{
    public class GlobalConvertersTests
    {
        [Fact]
        public void AvatarIdToImageConverter()
        {
            var vConverter = new AvatarIdToImageConverter();

            Assert.Equal("avatar_blank.png", vConverter.Convert(5, typeof(string), null, CultureInfo.CurrentCulture));
            Assert.Equal("avatar_p1_0.png", vConverter.Convert(1, typeof(string), null, CultureInfo.CurrentCulture));
            Assert.Equal("avatar_p2_x.png", vConverter.Convert(2, typeof(string), null, CultureInfo.CurrentCulture));
            Assert.Equal("avatar_p2_hotdog.png", vConverter.Convert(3, typeof(string), null, CultureInfo.CurrentCulture));
            Assert.Equal("avatar_p1_burger.png", vConverter.Convert(4, typeof(string), null, CultureInfo.CurrentCulture));

            Assert.Throws<NotImplementedException>(() => {
                vConverter.ConvertBack("avatar_blank.png", typeof(int), null, CultureInfo.CurrentCulture);
            });
        }
    }
}
