using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EFPNet.Infrastructure.Data;
using EFPNet.Infrastructure.Tools;
using Autofac;

namespace EFPNet.Domain.Data
{
    public abstract class EFRepositoryBase<TAggregateRoot, TKey> : IRepository<TAggregateRoot, TKey> where TAggregateRoot : AggregateRoot<TKey>
    {
        protected EFRepositoryBase()
        {
            UnitOfWork = DomainContainer.GetContainer().Resolve<IUnitOfWork>();
        }

        #region 属性

        /// <summary>
        ///     获取 仓储上下文的实例
        /// </summary>
        public IUnitOfWork UnitOfWork { get; set; }

        /// <summary>
        ///     获取 EntityFramework的数据仓储上下文
        /// </summary>
        protected UnitOfWorkContextBase EFContext
        {
            get
            {
                if (UnitOfWork is UnitOfWorkContextBase)
                {
                    return UnitOfWork as UnitOfWorkContextBase;
                }
                throw new DataAccessException(string.Format("数据仓储上下文对象类型不正确，应为UnitOfWorkContextBase，实际为 {0}", UnitOfWork.GetType().Name));
            }
        }

        /// <summary>
        /// 获取当前实体的查询数据集(通过读上下文进行读取，只读专用，返回的实体数据不会被上下文跟踪)
        /// </summary>
        public virtual IQueryable<TAggregateRoot> ReadEntities
        {
            get { return EFContext.Set<TAggregateRoot, TKey>().AsNoTracking(); }
        }

        /// <summary>
        /// 获取当前实体的查询数据集(通过写上下文进行读取，修改专用，返回的实体数据会被上下文进行跟踪)
        /// </summary>
        public virtual IQueryable<TAggregateRoot> WriteEntities 
        {
            get
            { 
                return EFContext.Set<TAggregateRoot, TKey>();
            }
        }

        #endregion

        #region 公共方法

        /// <summary>
        ///     插入实体记录
        /// </summary>
        /// <param name="entity"> 实体对象 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual int Insert(TAggregateRoot entity, bool isSave = true)
        {
            PublicHelper.CheckArgument(entity, "entity");
            EFContext.RegisterNew<TAggregateRoot, TKey>(entity);
            return isSave ? EFContext.Commit() : 0;
        }

        /// <summary>
        ///     批量插入实体记录集合
        /// </summary>
        /// <param name="entities"> 实体记录集合 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual int Insert(IEnumerable<TAggregateRoot> entities, bool isSave = true)
        {
            PublicHelper.CheckArgument(entities, "entities");
            EFContext.RegisterNew<TAggregateRoot, TKey>(entities);
            return isSave ? EFContext.Commit() : 0;
        }

        /// <summary>
        ///     删除指定编号的记录
        /// </summary>
        /// <param name="id"> 实体记录编号 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual int Delete(TKey id, bool isSave = true)
        {
            PublicHelper.CheckArgument(id, "id");
            TAggregateRoot entity = EFContext.Set<TAggregateRoot, TKey>().Find(id);
            return entity != null ? Delete(entity, isSave) : 0;
        }

        /// <summary>
        ///     删除实体记录
        /// </summary>
        /// <param name="entity"> 实体对象 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual int Delete(TAggregateRoot entity, bool isSave = true)
        {
            PublicHelper.CheckArgument(entity, "entity");
            EFContext.RegisterDeleted<TAggregateRoot, TKey>(entity);
            return isSave ? EFContext.Commit() : 0;
        }

        /// <summary>
        ///     删除实体记录集合
        /// </summary>
        /// <param name="entities"> 实体记录集合 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual int Delete(IEnumerable<TAggregateRoot> entities, bool isSave = true)
        {
            PublicHelper.CheckArgument(entities, "entities");
            EFContext.RegisterDeleted<TAggregateRoot, TKey>(entities);
            return isSave ? EFContext.Commit() : 0;
        }

        /// <summary>
        ///     删除所有符合特定表达式的数据
        /// </summary>
        /// <param name="predicate"> 查询条件谓语表达式 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual int Delete(Expression<Func<TAggregateRoot, bool>> predicate, bool isSave = true)
        {
            PublicHelper.CheckArgument(predicate, "predicate");
            List<TAggregateRoot> entities = EFContext.Set<TAggregateRoot, TKey>().Where(predicate).ToList();
            return entities.Count > 0 ? Delete(entities, isSave) : 0;
        }

        /// <summary>
        ///     更新实体记录
        /// </summary>
        /// <param name="entity"> 实体对象 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual int Update(TAggregateRoot entity, bool isSave = true)
        {
            PublicHelper.CheckArgument(entity, "entity");
            EFContext.RegisterModified<TAggregateRoot, TKey>(entity);
            return isSave ? EFContext.Commit() : 0;
        }

        /// <summary>
        /// 使用附带新值的实体信息更新指定实体属性的值
        /// </summary>
        /// <param name="propertyExpression">属性表达式</param>
        /// <param name="isSave">是否执行保存</param>
        /// <param name="entity">附带新值的实体信息，必须包含主键</param>
        /// <returns>操作影响的行数</returns>
        public int Update(Expression<Func<TAggregateRoot, object>> propertyExpression, TAggregateRoot entity, bool isSave = true)
        {
            throw new NotSupportedException("上下文公用，不支持按需更新功能。");
            //PublicHelper.CheckArgument(propertyExpression, "propertyExpression");
            //PublicHelper.CheckArgument(entity, "entity");
            //EFContext.RegisterModified<TAggregateRoot, TKey>(propertyExpression, entity);
            //if (isSave)
            //{
            //    var dbSet = EFContext.Set<TAggregateRoot, TKey>();
            //    dbSet.Local.Clear();
            //    var entry = EFContext.DbContext.Entry(entity);
            //    return EFContext.Commit(false);
            //}
            //return 0;
        }

        /// <summary>
        ///     查找指定主键的实体记录
        /// </summary>
        /// <param name="key"> 指定主键 </param>
        /// <returns> 符合编号的记录，不存在返回null </returns>
        public virtual TAggregateRoot GetByKey(TKey key)
        {
            PublicHelper.CheckArgument(key, "key");
            return EFContext.Set<TAggregateRoot, TKey>().Find(key);
        }

        /// <summary>
        /// 查找是否存在满足条件的数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>存在true,不存在false</returns>
        public bool IsExist(Expression<Func<TAggregateRoot, bool>> predicate)
        {
            return ReadEntities.Any(predicate);
        }

        #endregion
    }
}
