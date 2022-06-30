namespace Munchkin.Domain.Validation
{
    public static class ValidationError
    {
        public static ValidationResult NoGame
            => ValidationResult.Fail("There is no game with specified ID.");

        public static ValidationResult NoCharacter
            => ValidationResult.Fail("There is no character with specified ID in the game.");

        public static ValidationResult NoPlayer
            => ValidationResult.Fail("There is no player with specified ID in the game.");

        public static ValidationResult UnregisteredPlayer
            => ValidationResult.Fail("The player with specified nickname or email is not registered.");

        public static ValidationResult RegisteredPlayer
            => ValidationResult.Fail("The player with specified nickname or email is already registered.");

        public static ValidationResult InvalidPlayerId
            => ValidationResult.Fail("There is no registered player with specified ID.");

        public static ValidationResult PlayerInLobby
            => ValidationResult.Fail("The player with specified ID is already in the lobby.");

        public static ValidationResult CharacterOnCombatField
            => ValidationResult.Fail("The character with specified ID is already on the combat field.");

        public static ValidationResult NoCharacterNeedsHelp
            => ValidationResult.Fail("There must be a character on the combat field who needs help.");

        public static ValidationResult AnotherCharacterCameToHelp
            => ValidationResult.Fail("Only one character can come to help.");

        public static ValidationResult NoOffer
            => ValidationResult.Fail("There is no offer with specified ID in the game");

        public static ValidationResult NoCurse
            => ValidationResult.Fail("There is no curse on the combat field.");

        public static ValidationResult UncursedCharacted
            => ValidationResult.Fail("The curse cannot be applied because it was not imposed on the character.");

        public static ValidationResult NoCard
            => ValidationResult.Fail("There is no card with specified ID in the player's hands.");

        public static ValidationResult CardDuplication
            => ValidationResult.Fail("The card must be only in one of collections.");

        public static ValidationResult InvalidNumberOfTreasures
            => ValidationResult.Fail("The number of treasures must be greater than 0 and less than victory treasures.");

        public static ValidationResult NoSaleableCards
            => ValidationResult.Fail("There are no saleable cards in the specified collection of card IDs.");
    }
}
