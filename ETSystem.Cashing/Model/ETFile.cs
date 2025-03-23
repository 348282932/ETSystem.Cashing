using NPOI.SS.Formula.Functions;
using System.Collections.Generic;

namespace ETSystem.Cashing.Model
{
    /// <summary>
    /// 充绒文件
    /// </summary>
    public class ETFile
    {
        /// <summary>
        /// 款号名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 保留精度
        /// </summary>
        public int ReservedBit { get; set; }

        /// <summary>
        /// 型号重量集合
        /// </summary>
        public List<Weight> WeightList { get; set; }

        /// <summary>
        /// 裁片集合
        /// </summary>
        public List<PiecesType> PiecesTypeList { get; set; }

        /// <summary>
        /// 裁片集合
        /// </summary>
        public List<Pieces> PiecesList { get; set; }

        /// <summary>
        /// 图片集合
        /// </summary>
        public List<PictureInfo> PictureInfoList { get; set; }
    }

    public class PiecesType 
    {
        public string Name { get; set; }

        public int Number { get; set; }

        public int Order { get; set; }

    }

    /// <summary>
    /// 裁片
    /// </summary>
    public class Pieces 
    {
        /// <summary>
        /// 裁片名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 面积
        /// </summary>
        public decimal Area { get; set; }

        /// <summary>
        /// 尺寸
        /// </summary>
        public string Size { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// 总面积
        /// </summary>
        public decimal SumArea { get { return Area * Number; } }
    }

    /// <summary>
    /// 重量
    /// </summary>
    public class Weight 
    {
        /// <summary>
        /// 型号
        /// </summary>
        public string ETSize { get; set; }

        /// <summary>
        /// 重量
        /// </summary>
        public decimal TotalGrammage { get; set; }
    }

    /// <summary>
    /// 图片信息类
    /// </summary>
    public class PictureInfo
    {
        public byte[] Data { get; set; } // 图片数据
        public string Format { get; set; } // 图片格式（如 png, jpeg）
        public int Row1 { get; set; } // 起始行
        public int Col1 { get; set; } // 起始列
        public int Row2 { get; set; } // 结束行
        public int Col2 { get; set; } // 结束列
    }
}
