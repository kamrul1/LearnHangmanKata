using Hangman.Library.Statuses;
using Xunit;

namespace Hangman.Library.Tests
{
    public class HangmanTests
    {
        private readonly Hangman hangman = new Hangman("MySecret");

        [Fact]
        public void ShouldAcceptAndStoreSecretInCAPS()
        {

            var result = hangman.IsSecret("MYSECRET");

            Assert.Equal(GuessResult.Correct, result);
        }

        [Fact]
        public void ShouldReturn3IncorrectGuess()
        {
            _ = hangman.IsSecret("A");
            _ = hangman.IsSecret("B");
            _ = hangman.IsSecret("C");

            

            Assert.Equal(3, hangman.InCorrectGuessCounter);

        }

        [Fact]
        public void ShouldReturnInValidGameResult()
        {
            var result = hangman.Guess('}');

            Assert.Equal(GuessResult.Invalid, result);
        }

        [Fact]
        public void ShouldReturnInCorrectGameResult()
        {
            var result = hangman.Guess('P');
            Assert.Equal(GuessResult.Incorrect,result);
        }

        [Fact]
        public void ShouldReturnDuplicateGameResultGivenCorrectChar()
        {
            _ = hangman.Guess('M');
            var result = hangman.Guess('M');

            Assert.Equal(GuessResult.Duplicate, result);
        }

        [Fact]
        public void ShouldReturnDuplicateGameResultGivenInCorrectChar()
        {
            _ = hangman.Guess('Z');
            var result = hangman.Guess('Z');

            Assert.Equal(GuessResult.Duplicate, result);
        }
    }
}