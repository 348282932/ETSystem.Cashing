using ETSystem.Cashing.Model;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ETSystem.Cashing
{
    public partial class CashingFileConvertForm : BaseForm
    {
        /// <summary>
        /// 用户缓存文件地址
        /// </summary>
        public static readonly string cachePath = $"{Application.StartupPath}\\UserCache.data";

        public static string historyPath = "";

        public static ETFile uploadFile = new ETFile();

        public CashingFileConvertForm()
        {
            StartPosition = FormStartPosition.CenterScreen;

            this.MaximizeBox = false; // 隐藏最大化按钮

            InitializeComponent();
        }

        /// <summary>
        /// 选择文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_OpenFile_Click(object sender, EventArgs e)
        {
            // 定义一个文件打开控件

            OpenFileDialog ofd = new OpenFileDialog();

            // 设置打开对话框的初始目录，默认目录为exe运行文件所在的路径

            if (string.IsNullOrEmpty(historyPath))
            {
                ofd.InitialDirectory = historyPath;
            }

            // 设置打开对话框的标题

            ofd.Title = "请选择要打开的文件";

            // 设置打开对话框可以多选

            ofd.Multiselect = false;

            // 设置对话框打开的文件类型

            ofd.Filter = "Excel文件|*.xlsx";

            // 设置文件对话框当前选定的筛选器的索引

            ofd.FilterIndex = 2;

            // 设置对话框是否记忆之前打开的目录

            ofd.RestoreDirectory = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                // 获取用户选择的文件完整路径

                lbl_FliePath.Text = ofd.FileName;

                // 获取对话框中所选文件的文件名和扩展名，文件名不包括路径

                tbx_FileName.Text = ofd.SafeFileName;

                // 输出路径默认等于输入路径

                if (string.IsNullOrEmpty(tbx_OutputPath.Text))
                {
                    tbx_OutputPath.Text = $"{Path.GetDirectoryName(ofd.FileName)}";
                }

                uploadFile = ReadExcelFile();

                lbl_GrammageStatus.Text = "未设置";

                historyPath = $"{Path.GetDirectoryName(ofd.FileName)}";
            } 
        }

        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ConvertFile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lbl_FliePath.Text))
            {
                MessageBox.Show("请选择要转换的文件后再操作！");

                return;
            }

            if (string.IsNullOrEmpty(tbx_OutputPath.Text))
            {
                MessageBox.Show("请设置转换后文件的保存路径后再操作！");

                return;
            }

            uploadFile.ReservedBit = cbx_ReservedBit.SelectedIndex + 1;

            this.DoWorkAsync((o) => //耗时逻辑处理(此处不能操作UI控件，因为是在异步中)
            {
                return ExportExcelFile(uploadFile);

            }, null, (success) => //显示结果（此处用于对上面结果的处理，比如显示到界面上）
            {
                if (success)
                {
                    lbl_FliePath.Text = string.Empty;

                    tbx_FileName.Text = string.Empty;

                    MessageBox.Show($"转换成功！");
                }
                else
                {
                    MessageBox.Show("转换失败！");
                }
            });
        }

        /// <summary>
        /// 关闭程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EasyFileConvertForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //询问是否关闭窗体

            DialogResult result = MessageBox.Show("真的要退出程序吗？", "退出程序", MessageBoxButtons.OKCancel);

            //关闭窗体判断

            if (result == DialogResult.OK)
            {
                var authorizeCache = JsonConvert.SerializeObject(new
                {
                    ReservedBit = cbx_ReservedBit.SelectedIndex,
                    Style = cbx_Style.SelectedIndex,
                    OutputPath = tbx_OutputPath.Text,
                    HistoryPath = historyPath
                });

                File.WriteAllText(cachePath, authorizeCache);

                this.Hide();

                Environment.Exit(Environment.ExitCode);

                this.Dispose();

                this.Close();
            }
            else
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 选择导出路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_OpenPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();

            dialog.Description = "请选择转换后文件的保存路径";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                tbx_OutputPath.Text = dialog.SelectedPath;
            }
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EasyFileConvertForm_Load(object sender, EventArgs e)
        {
            // 读取缓存初始化参数

            if (File.Exists(cachePath))
            {
                var jTokenObj = (JToken)JsonConvert.DeserializeObject(File.ReadAllText(cachePath));

                cbx_ReservedBit.SelectedIndex = Convert.ToInt32(jTokenObj["ReservedBit"]);

                cbx_Style.SelectedIndex = Convert.ToInt32(jTokenObj["Style"]);

                tbx_OutputPath.Text = jTokenObj["OutputPath"].ToString();

                historyPath = jTokenObj["HistoryPath"].ToString(); 
            }
            else 
            {
                cbx_ReservedBit.SelectedIndex = 1;

                cbx_Style.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 读取文件
        /// </summary>
        /// <returns></returns>
        private ETFile ReadExcelFile() 
        {
            var file = new ETFile();

            file.PiecesList = new List<Pieces>();

            file.WeightList = new List<Weight>();

            file.PiecesTypeList = new List<PiecesType>();

            file.PictureInfoList = new List<PictureInfo>();

            var order = 0;
            
            try
            {
                using (var fileStream = new FileStream(lbl_FliePath.Text, FileMode.Open, FileAccess.Read))
                {
                    IWorkbook workbook = new XSSFWorkbook(fileStream);

                    ISheet sheet = workbook.GetSheetAt(0); // 获取第一个工作表

                    var number = 0;

                    var sizeDic = new Dictionary<int, string>();

                    var isReadSize = false;

                    var endCellNum = 0;

                    var piecesName = "";

                    var piecesTypeName = "";

                    var piecesTypeNumber = 0;

                    var isEndRead = false;

                    // 遍历行和单元格

                    for (int rowIndex = 1; rowIndex <= sheet.LastRowNum; rowIndex++)
                    {
                        if (isEndRead) break;

                        IRow row = sheet.GetRow(rowIndex);

                        if (row != null)
                        {
                            for (int colIndex = 0; colIndex < row.LastCellNum; colIndex++)
                            {
                                ICell cell = row.GetCell(colIndex);

                                if (string.IsNullOrEmpty(cell?.ToString()))
                                {
                                    // 如果第一列没数据，则跳出这一行

                                    if (colIndex == 0 && number > 0) break;

                                    continue;
                                }

                                var value = cell.ToString();

                                if (value == "单件总表" && colIndex == 0)
                                {
                                    isEndRead = true;

                                    break;
                                }

                                // 读取到合计列时重置参数并跳过此行

                                if (value == "合计" && colIndex == 0)
                                {
                                    file.PiecesTypeList.Add(new PiecesType { Name = piecesTypeName, Number = piecesTypeNumber - 2, Order = ++order });

                                    number = 0;

                                    sizeDic.Clear();

                                    isReadSize = false;

                                    endCellNum = 0;

                                    piecesName = "";

                                    piecesTypeName = "";

                                    piecesTypeNumber = 0;

                                    break;
                                }

                                // 当定位到结束列后按条件跳出列循环

                                if (endCellNum != 0 && colIndex >= endCellNum)
                                {
                                    piecesName = "";

                                    break;
                                }

                                // 读取款号

                                if (string.IsNullOrEmpty(file.Name) && value.StartsWith("款号:"))
                                {
                                    file.Name = value.Substring(3);

                                    break;
                                }

                                // 读取裁片数量

                                if (number == 0)
                                {
                                    number = Convert.ToInt32(Regex.Match(value, @".+?\*(\d+)").Result("$1"));

                                    piecesTypeName = Regex.Match(value, @"(.+?)\*\d+").Result("$1").ToString();

                                    break;
                                }

                                // 读取型号

                                if (!isReadSize)
                                {
                                    if (value == "充绒系数")
                                    {
                                        isReadSize = true;

                                        endCellNum = colIndex;

                                        break;
                                    }

                                    if (colIndex != 0)
                                    {
                                        sizeDic.Add(colIndex, value);
                                    }

                                    continue;
                                }

                                // 读取裁片名称

                                if (string.IsNullOrEmpty(piecesName) && colIndex == 0)
                                {
                                    piecesName = value;

                                    continue;
                                }

                                // 实例化裁片

                                var pieces = new Pieces();

                                pieces.Name = piecesName;

                                pieces.Size = sizeDic[colIndex];

                                pieces.Number = number;

                                pieces.Area = Convert.ToDecimal(value);

                                file.PiecesList.Add(pieces);
                            }

                            if (!string.IsNullOrEmpty(piecesTypeName)) piecesTypeNumber++;
                        }
                    }

                    file.PictureInfoList.AddRange(GetPicturesFromSheet((XSSFSheet)sheet));
                }

                var weightList = file.PiecesList.GroupBy(p => p.Size).Select(s => new Weight { ETSize = s.Key, TotalGrammage = 0 }).ToList();

                file.WeightList.AddRange(weightList);

                return file;
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message);

                lbl_FliePath.Text = "";

                tbx_FileName.Text = "";

                return file;
            }
            catch (Exception) 
            {
                MessageBox.Show("导入的excel格式不正确！");

                lbl_FliePath.Text = "";

                tbx_FileName.Text = "";

                return file;
            }
        }

        /// <summary>
        /// 导出文件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private bool ExportExcelFile(ETFile file) 
        {
            if (file.PiecesList == null || !file.PiecesList.Any()) 
            {
                return false;
            }

            const int piecesStart = 4;

            var sizeArray = file.PiecesList.GroupBy(g => g.Size).Select(s => s.Key).ToArray();

            var pictureArray = file.PictureInfoList.OrderBy(o => o.Row1).ToArray();

            var numberArray = file.PiecesTypeList.OrderBy(g => g.Order).Select(s => s.Number).ToArray();

            // 获取文件路径

            string filePath = Path.Combine(Application.StartupPath, "Template", "模板一.xlsx");

            // 加载模板文件

            using (var templateStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                // 创建工作簿

                IWorkbook workbook = new XSSFWorkbook(templateStream);

                // 计算公式结果

                IFormulaEvaluator evaluator = workbook.GetCreationHelper().CreateFormulaEvaluator();

                var style = GetDefaultStyle(workbook);

                var noLockStyle = GetDefaultStyle(workbook);

                noLockStyle.IsLocked = false;

                // 设置背景颜色为绿色

                noLockStyle.FillForegroundColor = IndexedColors.LightGreen.Index;

                noLockStyle.FillPattern = FillPattern.SolidForeground;

                for (int i = 0; i < sizeArray.Length; i++)
                {
                    if (i == 0)
                    {
                        int sheetIndex = workbook.GetSheetIndex("Sheet1"); // 获取工作表索引

                        workbook.SetSheetName(sheetIndex, sizeArray[i]);

                        continue;
                    }

                    // 复制工作表

                    ISheet newSheet = workbook.CloneSheet(workbook.GetSheetIndex(sizeArray[0]));

                    workbook.SetSheetName(workbook.GetSheetIndex(newSheet), sizeArray[i]); // 重命名新工作表
                }

                for (int i = 0; i < sizeArray.Length; i++)
                {
                    var data = file.PiecesList.Where(w => w.Size == sizeArray[i]).ToList();

                    int index = piecesStart;

                    ISheet sheet = workbook.GetSheet(sizeArray[i]); // 获取工作表

                    IRow firstRow = sheet.GetRow(1) ?? sheet.CreateRow(1);

                    firstRow.GetCell(0).SetCellValue($"款号:{file.Name}");

                    firstRow.GetCell(1).SetCellValue($"{sizeArray[i]}# 单件克重：");

                    firstRow.GetCell(2).CellStyle = noLockStyle;

                    var totalGrammage = file.WeightList.FirstOrDefault(f => f.ETSize == sizeArray[i])?.TotalGrammage;

                    if (totalGrammage != null) 
                    {
                        firstRow.GetCell(2).SetCellValue(totalGrammage.ToString());
                    }

                    firstRow.GetCell(5).SetCellValue($"{sizeArray[i]}# 充绒系数：");

                    firstRow.GetCell(6).SetCellFormula($"C2/SUM(F{piecesStart + 1}:F{piecesStart + data.Count})");

                    firstRow.GetCell(9).CellStyle = noLockStyle;

                    foreach (var item in data)
                    {
                        IRow row = sheet.GetRow(index) ?? sheet.CreateRow(index);

                        row.HeightInPoints = 25;

                        row.CreateCell(0).SetCellValue(item.Name);

                        row.CreateCell(1).SetCellValue((double)item.Area);

                        row.CreateCell(2).SetCellValue("×");

                        row.CreateCell(3).SetCellValue(item.Number);

                        row.CreateCell(4).SetCellValue("﹦");

                        row.CreateCell(5).SetCellFormula($"B{index + 1}*D{index + 1}");

                        row.CreateCell(6).SetCellValue(1);

                        row.CreateCell(7).SetCellFormula($"F{index + 1}*G{index + 1}");

                        row.CreateCell(8).SetCellFormula($"$C$2/SUM($H${piecesStart + 1}:$H${piecesStart + data.Count})*F{index + 1}*G{index + 1}");

                        row.CreateCell(9).SetCellFormula($"I{index + 1}/D{index + 1}");

                        for (int j = 0; j <= 9; j++)
                        {
                            if (j == 3) 
                            {
                               var intStyle = GetDefaultStyle(workbook);

                                var numFormat = workbook.CreateDataFormat();

                                intStyle.DataFormat = numFormat.GetFormat("0");

                                row.GetCell(j).CellStyle = intStyle;

                                continue;
                            }

                            if (j == 6)
                            {
                                row.GetCell(j).CellStyle = noLockStyle;

                                continue;
                            }

                            row.GetCell(j).CellStyle = style;
                        }

                        index++;
                    }

                    IRow emptyRow = sheet.GetRow(index) ?? sheet.CreateRow(index);

                    emptyRow.HeightInPoints = 25;

                    for (int j = 0; j <= 9; j++)
                    {
                        var cell = emptyRow.GetCell(j) ?? emptyRow.CreateCell(j);

                        cell.CellStyle = style;
                    }

                    IRow lastRow = sheet.GetRow(index + 1) ?? sheet.CreateRow(index + 1);

                    lastRow.HeightInPoints = 25;

                    for (int j = 0; j <= 9; j++)
                    {
                        var cell = lastRow.GetCell(j) ?? lastRow.CreateCell(j);

                        if (j == 4) cell.SetCellValue("总面积：");

                        if (j == 5) cell.SetCellFormula($"SUM(F{piecesStart + 1}:F{piecesStart + data.Count})");

                        if (j == 6) cell.SetCellValue("平方厘米");

                        cell.CellStyle = style;
                    }

                    for (int j = piecesStart; j < piecesStart + data.Count; j++) 
                    {
                        evaluator.EvaluateFormulaCell(sheet.GetRow(j).GetCell(5));
                        evaluator.EvaluateFormulaCell(sheet.GetRow(j).GetCell(7));
                        evaluator.EvaluateFormulaCell(sheet.GetRow(j).GetCell(8));
                        evaluator.EvaluateFormulaCell(sheet.GetRow(j).GetCell(9));
                    }

                    evaluator.EvaluateFormulaCell(sheet.GetRow(piecesStart + data.Count + 1).GetCell(5));

                    evaluator.EvaluateFormulaCell(sheet.GetRow(1).GetCell(6));

                    // 将图片插入到指定区域

                    int endRow = 0;

                    for (int j = 0; j < pictureArray.Length; j++)
                    {
                        int pictureIndex = workbook.AddPicture(pictureArray[j].Data, PictureType.PNG); // 添加图片到工作簿

                        if (j == 0) 
                        {
                            endRow = 4 + numberArray[j];

                            InsertPictureToSheet((XSSFWorkbook)workbook, sheet, pictureIndex, 4, 11, endRow, 11 + pictureArray[j].Col2 - pictureArray[j].Col1);

                            continue;
                        }

                        InsertPictureToSheet((XSSFWorkbook)workbook, sheet, pictureIndex, endRow, 11, endRow + numberArray[j], 11 + pictureArray[j].Col2 - pictureArray[j].Col1);

                        endRow = endRow + numberArray[j];
                    }

                    // 保护工作表

                    sheet.ProtectSheet("ETSystemReadOnly");
                }

                // 保存为新文件

                var outputPath = $"{tbx_OutputPath.Text}\\{Path.GetFileNameWithoutExtension(tbx_FileName.Text)}_输出{Path.GetExtension(tbx_FileName.Text)}";

                using (var outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    workbook.Write(outputStream);
                }

                // 获取文件路径

                string cahcePath = Path.Combine(Application.StartupPath, "Export");

                if (!Directory.Exists(cahcePath))
                {
                    Directory.CreateDirectory(cahcePath);
                }

                cahcePath += $"\\{Path.GetFileNameWithoutExtension(tbx_FileName.Text)}_{DateTime.Now:yyyyMMddhhmmss}{Path.GetExtension(tbx_FileName.Text)}";

                // 备份输出
                using (var outputStream = new FileStream(cahcePath, FileMode.Create, FileAccess.Write))
                {
                    workbook.Write(outputStream);
                }
            }

            return true;
        }

        /// <summary>
        /// 获取默认样式
        /// </summary>
        /// <param name="workbook"></param>
        /// <returns></returns>
        private ICellStyle GetDefaultStyle(IWorkbook workbook)
        {
            // 创建字体

            IFont font = workbook.CreateFont();

            font.FontName = "宋体"; // 设置字体为宋体

            font.FontHeightInPoints = 12; // 设置字体大小

            // 创建单元格样式

            ICellStyle style = workbook.CreateCellStyle();

            style.SetFont(font); // 应用字体

            style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center; // 水平居中

            style.VerticalAlignment = VerticalAlignment.Center; // 垂直居中

            // 设置边框

            style.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;

            style.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;

            style.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;

            style.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;

            // 创建数字格式

            IDataFormat dataFormat = workbook.CreateDataFormat();

            short format = 0;

            switch (uploadFile.ReservedBit)
            {
                case 1:
                    format = dataFormat.GetFormat("0.0"); // 保留一位小数
                    break;
                case 2:
                    format = dataFormat.GetFormat("0.00"); // 保留两位小数
                    break;
                case 3:
                    format = dataFormat.GetFormat("0.000"); // 保留三位小数
                    break;
                case 4:
                    format = dataFormat.GetFormat("0.0000"); // 保留四位小数
                    break;
                default:
                    break;
            }

            style.DataFormat = format;

            style.FillForegroundColor = IndexedColors.White.Index; // 设置前景色为白色

            style.FillPattern = FillPattern.SolidForeground; // 设置填充模式为纯色

            style.IsLocked = true;

            return style;
        }

        /// <summary>
        /// 设置克重
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_SetGrammage_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(uploadFile.Name)) 
            {
                MessageBox.Show("请先选取文件！");

                return;
            }

            var setGrammageForm = new SetGrammageForm(uploadFile);

            setGrammageForm.OKClick += (weights) =>
            {
                lbl_GrammageStatus.Text = "已设置";

                uploadFile.WeightList = weights;
            };

            setGrammageForm.ShowDialog(this);
        }

        /// <summary>
        /// 从工作表中提取所有图片信息
        /// </summary>
        private List<PictureInfo> GetPicturesFromSheet(XSSFSheet sheet)
        {
            List<PictureInfo> pictures = new List<PictureInfo>();

            // 获取工作表中的绘图对象
            var drawing = sheet.CreateDrawingPatriarch() as XSSFDrawing;
            if (drawing == null)
                return pictures;

            // 遍历所有形状
            foreach (var shape in drawing.GetShapes())
            {
                if (shape is XSSFPicture picture)
                {
                    // 获取图片数据
                    var pictureData = picture.PictureData;
                    byte[] data = pictureData.Data;
                    string format = pictureData.SuggestFileExtension();

                    // 获取锚点信息
                    var anchor = picture.GetPreferredSize() as XSSFClientAnchor;
                    int row1 = anchor.Row1;
                    int col1 = anchor.Col1;
                    int row2 = anchor.Row2;
                    int col2 = anchor.Col2;

                    // 添加到列表
                    pictures.Add(new PictureInfo
                    {
                        Data = data,
                        Format = format,
                        Row1 = row1,
                        Col1 = col1,
                        Row2 = row2,
                        Col2 = col2
                    });
                }
            }

            return pictures;
        }


        /// <summary>
        /// 将图片插入到工作表的指定区域
        /// </summary>
        /// <param name="workbook">工作簿</param>
        /// <param name="sheet">工作表</param>
        /// <param name="pictureIndex">图片索引</param>
        /// <param name="row1">起始行</param>
        /// <param name="col1">起始列</param>
        /// <param name="row2">结束行</param>
        /// <param name="col2">结束列</param>
        private void InsertPictureToSheet(XSSFWorkbook workbook, ISheet sheet, int pictureIndex, int row1, int col1, int row2, int col2)
        {
            // 创建绘图对象
            XSSFDrawing drawing = (XSSFDrawing)sheet.CreateDrawingPatriarch();

            // 创建锚点，设置图片的位置和大小
            XSSFClientAnchor anchor = new XSSFClientAnchor(
                0, 0, 0, 0, // dx1, dy1, dx2, dy2
                col1, row1, col2, row2 // 起始列, 起始行, 结束列, 结束行
            );

            // 插入图片
            drawing.CreatePicture(anchor, pictureIndex);
        }
    }
}
