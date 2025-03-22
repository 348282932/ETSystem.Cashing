using ETSystem.Cashing.Common;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace ETSystem.Cashing
{
    public partial class BaseForm : Form
    {
        public BaseForm()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterParent;
        }

        /// <summary>
        /// 多线程异步后台处理某些耗时的数据，不会卡死界面
        /// </summary>
        /// <param name="workFunc">Func委托，包装耗时处理（不含UI界面处理），示例：(o)=>{ 具体耗时逻辑; return 处理的结果数据 }</param>
        /// <param name="funcArg">Func委托参数，用于跨线程传递给耗时处理逻辑所需要的对象，示例：String对象、JObject对象或DataTable等任何一个值</param>
        /// <param name="workCompleted">Action委托，包装耗时处理完成后，下步操作（一般是更新界面的数据或UI控件），示列：(r)=>{ datagirdview1.DataSource=r; }</param>
        protected void DoWorkAsync(Func<object, dynamic> workFunc, object funcArg = null, Action<dynamic> workCompleted = null)
        {
            var bgWorkder = new BackgroundWorker();

            Control loadingPan = null;

            bgWorkder.WorkerReportsProgress = true;

            bgWorkder.ProgressChanged += (s, arg) =>
            {
                if (arg.ProgressPercentage > 1) return;

                #region Panel模式

                var result = this.Controls.Find("loadingPan", true);

                if (result == null || result.Length <= 0)
                {
                    loadingPan = new MaskPanel(this)
                    {
                        Name = "loadingPan"
                    };
                }
                else
                {
                    loadingPan = result[0];
                }

                loadingPan.BringToFront();

                loadingPan.Visible = true;

                #endregion
            };

            bgWorkder.RunWorkerCompleted += (s, arg) =>
            {

                #region Panel模式

                if (loadingPan != null)
                {
                    loadingPan.Visible = false;
                }

                #endregion

                bgWorkder.Dispose();

                workCompleted?.Invoke(arg.Result);
            };

            bgWorkder.DoWork += (s, arg) =>
            {
                bgWorkder.ReportProgress(1);

                var result = workFunc(arg.Argument);

                arg.Result = result;

                bgWorkder.ReportProgress(100);
            };

            bgWorkder.RunWorkerAsync(funcArg);
        }
    }
}
