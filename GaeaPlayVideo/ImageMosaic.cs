using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GaeaPlayVideo
{
    class ImageMosaic
    {
        public Bitmap BitmapMerge(Bitmap src1, Bitmap src2, int dstWidth, int dstHeight, int src1_LeftTop_x, int src1_LeftTop_y, int src2_LeftTop_x, int src2_LeftTop_y)//简单的bitmap图像合并
        {/*
            Bitmap bit = new Bitmap(360,400);
            Bitmap bit1 = new Bitmap("tt.jpg");
            Bitmap bit2 = new Bitmap("tt2.jpg");
            Graphics graph = Graphics.FromImage(bit);          
            graph.DrawImage(bit,bit1.Width,bit1.Height+bit2.Height);
            graph.DrawImage(bit1, 0, 0);
            graph.DrawImage(bit2, 0, bit1.Height);
            bit.Save("a.jpg");
          */
            Bitmap dst = new Bitmap(dstWidth, dstHeight);//新建Bitmap
            Graphics graph = Graphics.FromImage(dst);//获取绘图句柄
            //graph.DrawImage(dst, dstWidth,dstHeight);//
            graph.DrawImage(src1, src1_LeftTop_x, src1_LeftTop_y);//绘制src1
            graph.DrawImage(src2, src2_LeftTop_x, src2_LeftTop_y);//绘制src2
            dst.MakeTransparent();//透明化
            //dst.Save("a.jpg"); 
            return dst;//
     
        }

        public Bitmap BitmapMosaic(Bitmap src1, Bitmap src2)//bitmap图像拼接算法
        {
            Bitmap dst=src1;

            return dst;
        }

    }
}
