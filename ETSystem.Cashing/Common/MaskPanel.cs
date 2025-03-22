using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ETSystem.Cashing.Common
{
    public partial class MaskPanel : Control
    {
        private Container components = new Container();

        private bool _isTransparent = true;//是否透明

        [Category("透明"), Description("是否使用透明,默认为True")]

        public bool IsTransparent
        {
            get { return _isTransparent; }
            set { _isTransparent = value; }
        }

        private int _alpha = 125; //设置透明度

        [Category("透明"), Description("设置透明度")]
        public int Alpha
        {
            get { return _alpha; }
            set { _alpha = value; }
        }


        public MaskPanel(Control parent)
            : this(parent, 125)
        {

        }

        /// <summary>
        /// 初始化加载控件
        /// </summary>
        /// <param name="Alpha"透明度</param>
        public MaskPanel(Control parent, int alpha)
        {
            SetStyle(ControlStyles.Opaque, true); //设置背景透明

            base.CreateControl();

            _alpha = alpha;

            parent.Controls.Add(this);

            this.Parent = parent;

            this.Size = this.Parent.ClientSize;

            this.Left = 0;

            this.Top = 0;

            this.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;

            this.BringToFront();

            PictureBox pictureBox_Loading = new PictureBox
            {
                BackColor = Color.Transparent,

                Image = Properties.Resources.loading,

                Name = "pictureBox_Loading",

                SizeMode = PictureBoxSizeMode.AutoSize
            };

            Point Location = new Point(this.Location.X + (this.Width - pictureBox_Loading.Width) / 2, this.Location.Y + (this.Height - pictureBox_Loading.Height) / 2); //居中
            
            pictureBox_Loading.Location = Location;
           
            pictureBox_Loading.Anchor = AnchorStyles.None;

            this.Controls.Add(pictureBox_Loading);

            this.Visible = false;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;

                cp.ExStyle |= 0x00000020; // 开启 WS_EX_TRANSPARENT,使控件支持透明

                return cp;
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            Pen labelBorderPen;

            SolidBrush labelBackColorBrush;

            if (_isTransparent)
            {
                Color cl = Color.FromArgb(_alpha, this.BackColor);

                labelBorderPen = new Pen(cl, 0);

                labelBackColorBrush = new SolidBrush(cl);
            }
            else
            {
                labelBorderPen = new Pen(this.BackColor, 0);

                labelBackColorBrush = new SolidBrush(this.BackColor);
            }

            base.OnPaint(pe);

            pe.Graphics.DrawRectangle(labelBorderPen, 0, 0, this.Width, this.Height);

            pe.Graphics.FillRectangle(labelBackColorBrush, 0, 0, this.Width, this.Height);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!((components == null)))
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

    }
}
