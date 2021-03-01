using System.Drawing;
using System.Media;

namespace FlappyBird
{
    class Bird
    {
        float x;
        float y;

        Image sprite;
        SoundPlayer jumpSound;
        int spriteSize;

        float fallingSpeed;

        public float X { get => x; }
        public float Y { get => y; }
        public Image Sprite { get => sprite; }
        public int SpriteSize { get => spriteSize; }
        public SoundPlayer JumpSound { get => jumpSound; }
        public float FallingSpeed { get => fallingSpeed; }

        public Bird (float x, float y)
        {
            sprite = new Bitmap("image//bird.png");
            jumpSound = new SoundPlayer("audio//fly.wav");
            this.x = x;
            this.y = y;
            spriteSize = 30;
            fallingSpeed = 0.1f;
        }

        public void fall(float gravity)
        {
            this.fallingSpeed += gravity;
            this.y += fallingSpeed;
        }

        public void jump()
        {
            this.fallingSpeed = -7f;
        }
    }
}
