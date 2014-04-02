using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;//调试信息

using Gaea.Controls;
using Gaea.Core;
using Gaea;
using Gaea.Carto;  //里面有关与 controls 的信息
using Gaea.Geometry;
using Gaea.Renderable;  //包含了surfacelayer有关信息
using SlimDX.Direct3D9;

namespace GaeaPlayVideo
{
    public class Buffers
    {
        Texture tex;
        Queue<Texture> VideoQueue = new Queue<Texture>();

        public void write2Buffers(Bitmap pic)
        {
            lock (this)
            {
                tex = ImageHelper.LoadTexture(pic);
                this.VideoQueue.Enqueue(tex);
            }
        }
        public Texture readFromBuffers()
        {
            lock (this)
            {
                return this.VideoQueue.Dequeue();
            }
        }
    }
}
