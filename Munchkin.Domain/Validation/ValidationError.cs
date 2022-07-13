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

        public static ValidationResult UnexpectedTurn
            => ValidationResult.Fail("The other player must take the turn now.");

        public static ValidationResult UnexpectedCommand
            => ValidationResult.Fail("Now is not the time for that action.");

        public static ValidationResult ItemIsEquipped
            => ValidationResult.Fail("This type of item is already on the character.");

        public static ValidationResult TooHighLevel
            => ValidationResult.Fail("The character has too high a level for this action.");

        public static ValidationResult EnhancementOnEmptyCombatField
            => ValidationResult.Fail("There is not a monster on the combat field to apply enhancement.");

        public static ValidationResult EquipmentDuringCombat
            => ValidationResult.Fail("The character cannot be equipped on the combat field.");

        public static ValidationResult GoUpLevelDuringCombat
            => ValidationResult.Fail("The character's level cannot be raised on the combat field.");
    }
}
