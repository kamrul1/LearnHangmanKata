using Hangman.Library.Statuses;

namespace Hangman.Library
{

    public class Hangman
    {
        private readonly int NO_ALLOWED_GUESS = 4;
        private readonly string secret;
        private HashSet<char> inCorrectLettersAttempted = new();
        private HashSet<char> correctLettersAttempted = new();
        private HashSet<string> inCorrectWordsAttempted = new();

        public GameState GameState { get; private set; } = GameState.NotStarted;
        public int InCorrectGuessCounter { get; private set; } = 0;

        public Hangman(string secret)
        {
            this.secret = secret.ToUpper();
        }

        public GuessResult IsSecret(string checkWord)
        {
            GameState = GameState.InProgress;

            checkWord = checkWord.ToUpper();

            if (string.Equals(checkWord, secret)){
                GameState = GameState.Won;
                return GuessResult.Correct;
            }

            inCorrectWordsAttempted.Add(checkWord);
            InCorrectGuessCounter++;
            if(InCorrectGuessCounter== NO_ALLOWED_GUESS)
            {
                GameState = GameState.Lost;
                return GuessResult.Incorrect;
            }

            return GuessResult.Incorrect;
        }

        public GuessResult Guess(char c)
        {
            if (IsCharacterInValid(c))
            {
                UpdateGameState();
                return GuessResult.Invalid;
            }

            if (correctLettersAttempted.Contains(c) || inCorrectLettersAttempted.Contains(c))
            {
                UpdateGameState();
                return GuessResult.Duplicate;
            }

            if (!secret.Contains(char.ToUpper(c)))
            {
                inCorrectLettersAttempted.Add(c);
                InCorrectGuessCounter++;
                UpdateGameState();
                return GuessResult.Incorrect;
            }

            correctLettersAttempted.Add(c);
            UpdateGameState();
            return GuessResult.Correct; 
        }




        private void UpdateGameState()
        {
            var secretCharSet
                = secret
                    .ToCharArray()
                    .ToHashSet<char>();

            if (correctLettersAttempted.Equals(secretCharSet))
            {
                GameState = GameState.Won;
                return;
            }

            if (InCorrectGuessCounter == NO_ALLOWED_GUESS)
            {
                GameState = GameState.Lost;
                return;
            }

            GameState = GameState.InProgress;  
        }

        private bool IsCharacterInValid(char c)
        {
            if (c < 'A' || c > 'Z')
            {
                return true;
            }

            return false;
        }

    }

    

}