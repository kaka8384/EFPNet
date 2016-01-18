using System;
using System.ComponentModel.DataAnnotations;

namespace EFPNet.Infrastructure.Tools
{
    /// <summary>
    ///     可持久到数据库的领域模型的基类。
    /// </summary>
    [Serializable]
    public abstract class EntityBase<TKey>
    {
        #region 构造函数

        /// <summary>
        ///  数据实体基类
        /// </summary>
        protected EntityBase()
        {
            IsDeleted = false;
            LastUpdateDate = DateTime.Now;
        }

        #endregion

        #region 属性

        [Key]
        public TKey Id { get; set; }

        /// <summary>
        ///  获取或设置是否禁用，逻辑上的删除，非物理删除
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        ///  添加时间
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime AddDate { get; set; }

        /// <summary>
        /// 最后一次修改时间
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime LastUpdateDate { get; set; }

        /// <summary>
        /// 最后一次操作用户
        /// </summary>
        public string LastOperateUser { get; set; }
        #endregion
    }
}
