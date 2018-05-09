
namespace Justgrow
{
    public class Constants
    {
        // Resources constants
        public const string musicForest = "musics/forest";
        public const string textureForest = "textures/forest";
        public const string textureBranch = "textures/branch";
        public const string textureLeaf = "textures/leaf";
        public const string textureRoot = "textures/root";
        public const string textureGround = "textures/ground";
        public const string textureGrass = "textures/grass";
        public const string textureCell = "textures/cell";
        public const string textureWater = "textures/water";
        public const string textureOverview = "textures/overview";        
        public const string soundWind = "sounds/wind";
        public const string fontTitle = "fonts/title";
       
        // Graphics constants
        public const int windowHeight = 1080;
        public const float windowRatio = 1.6f;

        // Specific constants

        // Time in milliseconds for the wond to go from null to "tempest"
        public const float windLatency = 10000.0f;

        // The fade-in time of the main menu, in milliseconds
        public const int menuFadeIn = 15000;

        // The height of the ground
        public const float overviewRatio = 0.7f;
        
        public const int groundOffset = 50;

        public const float groundAnimationVelocity = 0.003f;
    }
}