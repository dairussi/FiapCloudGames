namespace FiapCloudGames.Domain.Games.Enum;

public enum GameGenreEnum
{
    // First-Person e Third-Person Shooters (incluindo Battle Royale)
    FPS, // First-Person Shooter
    TPS, // Third-Person Shooter
    BattleRoyale,

    // RPGs (incluindo os massivos online)
    RPG,
    ActionRPG,
    MMORPG, // Massively Multiplayer Online RPG

    // Estratégia e Táticas
    Strategy,
    MOBA, // Multiplayer Online Battle Arena (ex: League of Legends)
    RTS, // Real-Time Strategy

    // Outros Gêneros com Alta Popularidade
    Sports,
    Survival,
    Horror,
    Racing
}