using System.Drawing;

namespace FlappyBird
{
    class Pipe
    {
        float x;
        float y;
        float windowsSize;

        Image spriteTop;
        Image spriteBottom;

        int spriteSizeX;
        int spriteSizeY;

        bool passed;

        public float X { get => x; }
        public float Y { get => y; }
        public float WindowSize { get => windowsSize; }
        public Image SpriteTop { get => spriteTop; }
        public Image SpriteBottom { get => spriteBottom; }
        public int SpriteSizeX { get => spriteSizeX; }
        public int SpriteSizeY { get => spriteSizeY; }
        public bool Passed { get => passed; set { passed = value;} }

        public Pipe(float x, float y)
        {
            spriteBottom = new Bitmap("image//pipe.png");
            spriteTop = new Bitmap("image//pipe.png");
            spriteTop.RotateFlip(RotateFlipType.Rotate180FlipX);

            this.x = x;
            this.y = y;

            spriteSizeX = 45;
            spriteSizeY = 350;
            windowsSize = 150;
            passed = false;

        }
        public void Move()
        {
            x -= 2;
        }
    }
}
