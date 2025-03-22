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
        /// 总克重
        /// </summary>
        public decimal TotalGrammage { get; set; }

        /// <summary>
        /// 总面积
        /// </summary>
        public decimal TotalArea { get; set; }

        /// <summary>
        /// 充绒系数
        /// </summary>
        public decimal FillingFactor { get; set; }

        /// <summary>
        /// 裁片集合
        /// </summary>
        public List<Pieces> PiecesList { get; set; }
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
}
