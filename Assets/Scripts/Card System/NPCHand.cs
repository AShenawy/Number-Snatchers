// This is a representation of the comnputer opponent's behaviour
public class NPCHand : Hand
{
    bool playerGuessCorrect;

    public void EvaluatePlayerMove(int expected, int playerGuess)
    {
        if (expected == playerGuess)
            playerGuessCorrect = true;
        else
            playerGuessCorrect = false;
    }

    public void ChallengePlayer()
    {
        //TODO challenging functionality
        if (playerGuessCorrect)
            print("Player guesssed correctly. NPC will pass the challenge.");
        else
            print("Player guess is incorrect. NPC will challenge");
    }
}
