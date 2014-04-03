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
using Gaea.Display;//里面包含画线的信息
using Gaea.Renderable;  //包含了surfacelayer有关信息
using SlimDX.Direct3D9;

namespace GaeaPlayVideo
{
    public partial class Form1 : Form
    {
        WorldControl _WorldControl;//地理球
        SystemSettings _Settings;//系统设置
        Queue<Texture> videoQueue = new Queue<Texture>();//贴图纹理队列
        Buffers buffer = new Buffers();//建立缓冲区
        SurfaceImageLayer _newLayer;//地理球贴图图层
        Geocoorderates orderate1 = new Geocoorderates(37.00, 37.05, 111.95, 112.00); //坐标结构体
        
        //绘制线的点数组
        private PointF[] points;
        //绘制screenicon的点坐标
        private PointF point;

        public Form1()
        {
            InitializeComponent();//程序框架初始化
            InitWorldControl();//初始化地理球
            DataController dc = new DataController();//数据监听接收通信模块，单独线程
            dc.teleBuffers = buffer;//通信模块控制缓冲区
            dc.mainWindow = this;//以Form1为主窗口
            dc.start();//开始监听通信

            //测试利用坐标点数组画线,调用Drawline()函数
            points = new PointF[5];
            points[0] = new PointF(38.15F,112.20F);
            points[1] = new PointF(37.50F,112.20F);
            points[2] = new PointF(37.50F,112.80F);
            points[3] = new PointF(38.15F, 112.80F);
            points[4] = new PointF(38.15F, 112.20F);
            System.Drawing.Color col = System.Drawing.Color.Blue;
            DrawLines(points, col);
            this._WorldControl.GotoLatLonAltitude(37.50, 112.30, 300000);//定位视角感测点

            //测试绘制点screenicon，调用DrawPointLabel()函数
            point = new PointF(38, 112);
            string iconPath = Gaea.WorldManager.SystemSettings.DataPath
             + @"\icon\TYplace.png";
            DrawPointLabel(point, iconPath);
            this._WorldControl.GotoLatLonAltitude(38, 112, 300000);

            //PlayVideo();//播放视频
        }
        void InitWorldControl()//初始化地理球
        {
            try
            {
                _WorldControl = new WorldControl();
                this.Controls.Add(_WorldControl);
                _WorldControl.Dock = DockStyle.Fill;
                _WorldControl.Cursor = Cursors.Default;
                _WorldControl.Location = new System.Drawing.Point(0, 0);
                _WorldControl.Name = "_WorldControl";
                _WorldControl.TabIndex = 0;
                _WorldControl.Text = "WorldControl1";

                _WorldControl.InitializeEngineSystem();
                _WorldControl.InitializeScene();
                _WorldControl.OpenWorld(WorldNames.Earth);
                _WorldControl.StartRenderingLoop();

                _Settings = _WorldControl.Settings;
                this.Text = "智慧城市公共平台(原型系统)";

                _WorldControl.ScreenIconClick += new ScreenIconClickHandler(_WorldControl_ScreenIconClick);

            }
            catch (Exception e)
            {
                StackTrace st = new StackTrace(e, true);
                Console.WriteLine("错误描述：" + e.Message + " 错误跟踪：" + st.ToString());
            }
        }

        void _WorldControl_ScreenIconClick(string customParam, string id, MouseEventArgs e)
        {
            MessageBox.Show("相关属性");
        }

        void PlayVideo()//播放视频
        {
            _newLayer = new SurfaceImageLayer(
                "image",
                _WorldControl.CurrentWorld,//
                (float)100,//图层离地高度
                Gaea.WorldManager.SystemSettings.DataPath 
                + @"\base\basemap.jpg", //把一个透明的jpg当做底图
                orderate1.NWx, orderate1.NWy, orderate1.SEx, orderate1.SEy,
                0.01f * (100.0f - 0),//透明度
                _WorldControl.CurrentWorld.TerrainAccessor);
            _newLayer.Initialize(DrawArgs.Instance);//图层初始化
            _WorldControl.CurrentWorld.Add(_newLayer);//图层贴到球上

            Timer imageTimer = new Timer();//建立定时器
            imageTimer.Tick += new EventHandler(imageTimer_tick);//定时器触发事件
            imageTimer.Interval = 30;//定时器间隔
            imageTimer.Start();//开始计时
        }
        void imageTimer_tick(object sender, EventArgs e)//定时器触发事件
        {
            videoQueue.Enqueue(buffer.readFromBuffers());   //入队的操作
            _newLayer.Texture = videoQueue.Dequeue();        //从队列中取出 一个Texture
        }

        private void DrawLineButton_Click(object sender, EventArgs e)//手工点击画线
        {
            //其中主要调用的接口为Gaea.Controls.ControlsAddPolylineElenebtCommandTool(IMapControl mapControl)
            ControlsAddPolylineElementCommandTool toolPolyine = new ControlsAddPolylineElementCommandTool(_WorldControl.MapControlClass);
            toolPolyine.Completed += new Gaea.Sys.delegate_None_object(UpdateToc);
            _WorldControl.CurrentTool = toolPolyine;
        }
        void UpdateToc(object sender)
        {
            _WorldControl.CurrentTool = null;
        }

        /// <summary>
        /// 绘制线段，输入坐标格式为地理坐标
        /// </summary>
        /// <param name="pts"></param>
        private void DrawLines(PointF[] pts, System.Drawing.Color color)//利用坐标点画线接口函数
        {
            Polyline Lines = new Polyline();
            for (int i = 0; i < pts.Length; i++)
            {
                Lines.AddPoint(new Gaea.Geometry.Point(pts[i].Y,
                   pts[i].X));
            }
            SimpleLineSymbol2D LinesSymbol = new SimpleLineSymbol2D();
            LinesSymbol.Color = new ARGBColor(color.ToArgb());
            LinesSymbol.Width = 2.0f;
            Element element = new Element();
            element.Geometry = Lines;
            element.Symbol = LinesSymbol;
            this._WorldControl.AddElement(element, 0);
            _WorldControl.Refresh(gaeaViewDrawPhase.gaeaViewGraphics, null, element.Geometry.Envelope);
        }
        /// <summary>
        /// 绘制screenicon
        /// </summary>
        /// <param name="pt"></param>
        /// <param name="picturepath"></param>
        private void DrawPointLabel(PointF pt, string picturepath)//绘制screenicon接口函数
        {
            ScreenIcon peg1 =
                DrawArgs.World.ScreenIconManager.CreateIcon("picture", picturepath, pt.X, pt.Y);
            peg1.TipMessage = "picture";
            peg1.MaximumDisplayDistance = 3000000;
            peg1.MinimumDisplayDistance = 1;
            peg1.Width = 20;
            peg1.Height = 20;
            peg1.IsSelectable = false;
            peg1.TipFont = new System.Drawing.Font("黑体", 20, System.Drawing.FontStyle.Bold);
            peg1.TipFontColor = System.Drawing.Color.Red.ToArgb();
            peg1.TipBoundColor = System.Drawing.Color.Black.ToArgb();
            peg1.TipBoundWidth = 2;
            peg1.Visible = true;
            peg1.ShowTipAlways = true;
        }

        //地理坐标结构 体
    ///有构造函数
        internal struct Geocoorderates
        {
            public double NWx;
            public double NWy;
            public double SEx;
            public double SEy;
            public Geocoorderates(double x, double y, double w, double z)
            {
                NWx = x;
                NWy = y;
                SEx = w;
                SEy = z;
            }
        }


    }

}
