using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Numerics;

namespace EnginelessPhysics.src.engine.Components
{
    public class Animator
    {
        private PhysicalEntity targetEntity;
        private Vector2 spriteSize;

        public bool playing;
        public bool flipped;

        public Animator(PhysicalEntity ent, Vector2 scale)
        {
            targetEntity = ent;
            ent.sprite.RenderTransformOrigin = new Point(0.5, 0.5); // flip around center

            spriteSize = scale;
        }

        public async void Play(BitmapImage? anim, int frameCount, int durationMS, bool loop)
        {
            playing = true;

            for (int i = 0; i < frameCount; i++)
            {
                if (!playing)
                    break;

                //get the next frame from the animation sheet
                targetEntity.sprite.Fill = new ImageBrush(new CroppedBitmap(anim, 
                    new Int32Rect((int)spriteSize.X * i, 0, (int)spriteSize.X, (int)spriteSize.Y)));

                targetEntity.sprite.RenderTransform = new ScaleTransform(flipped ? -1 : 1, 1);

                await Task.Delay(durationMS / frameCount);
            }

            if (loop)
                Play(anim, frameCount, durationMS, loop);
        }
    }
}
