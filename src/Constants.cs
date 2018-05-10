
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

        // Time in milliseconds for the wind to go from null to "tempest"
        public const float windLatency = 10000.0f;

        // The fade-in time of the main menu, in milliseconds
        public const int menuFadeIn = 15000;

        // The height of the ground
        public const float overviewRatio = 0.7f;
        
        // The amount (in pixel) of grass / air that is visible in underground mode
        public const int groundOffset = 50;
        
        // The duration of the animation when the grounch height changes
        public const float groundAnimationDuration = 3f;
        
        // The width (in pixel) of a cell used by the tree elements
        public const int cellWidth = 70;
    }
}