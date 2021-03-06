﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq.Expressions;
using EFPNet.Infrastructure.Data.Extensions;
using EFPNet.Infrastructure.Tools;

namespace EFPNet.Infrastructure.Data
{
    /// <summary>
    ///     单元操作实现基类
    /// </summary>
    public abstract class UnitOfWorkContextBase : IUnitOfWorkContext
    {
        /// <summary>
        /// 获取 当前使用的数据访问上下文对象
        /// </summary>
        protected virtual DbContext Context
        {
            get;set; 
        }

        /// <summary>
        ///     获取 当前单元操作是否已被提交
        /// </summary>
        public bool IsCommitted { get; private set; }

        public DbContext DbContext { get { return Context; } }

        /// <summary>
        ///     提交当前单元操作的结果
        /// </summary>
        /// <param name="validateOnSaveEnabled">保存时是否自动验证跟踪实体</param>
        /// <returns></returns>
        public int Commit(bool validateOnSaveEnabled = true)
        {
            if (IsCommitted)
            {
                return 0;
            }
            try
            {
                int result = Context.SaveChanges(validateOnSaveEnabled);
                IsCommitted = true;
                return result;
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException != null && e.InnerException.InnerException is SqlException)
                {
                    var sqlEx = e.InnerException.InnerException as SqlException;
                    string msg = DataHelper.GetSqlExceptionMessage(sqlEx.Number);
                    throw PublicHelper.ThrowDataAccessException("提交数据更新时发生异常：" + msg, sqlEx);
                }
                throw;
            }
        }

        /// <summary>
        ///     把当前单元操作回滚成未提交状态
        /// </summary>
        public void Rollback()
        {
            IsCommitted = false;
        }

        public void Dispose()
        {
            if (!IsCommitted)
            {
                Commit();
            }
            Context.Dispose();
        }

        /// <summary>
        ///   为指定的类型返回 System.Data.Entity.DbSet，这将允许对上下文中的给定实体执行 CRUD 操作。
        /// </summary>
        /// <typeparam name="TAggregateRoot"> 应为其返回一个集的实体类型。 </typeparam>
        /// <typeparam name="TKey">实体主键类型</typeparam>
        /// <returns> 给定实体类型的 System.Data.Entity.DbSet 实例。 </returns>
        public DbSet<TAggregateRoot> Set<TAggregateRoot, TKey>() where TAggregateRoot : AggregateRoot<TKey>
        {
            return Context.Set<TAggregateRoot>();
        }

        /// <summary>
        ///     注册一个新的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TAggregateRoot"> 要注册的类型 </typeparam>
        /// <typeparam name="TKey">实体主键类型</typeparam>
        /// <param name="entity"> 要注册的对象 </param>
        public void RegisterNew<TAggregateRoot, TKey>(TAggregateRoot entity) where TAggregateRoot : AggregateRoot<TKey>
        {
            var state = Context.Entry(entity).State;
            if (state == EntityState.Detached)
            {
                Context.Entry(entity).State = EntityState.Added;
            }
            IsCommitted = false;
        }

        /// <summary>
        ///     批量注册多个新的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TAggregateRoot"> 要注册的类型 </typeparam>
        /// <typeparam name="TKey">实体主键类型</typeparam>
        /// <param name="entities"> 要注册的对象集合 </param>
        public void RegisterNew<TAggregateRoot, TKey>(IEnumerable<TAggregateRoot> entities) where TAggregateRoot : AggregateRoot<TKey>
        {
            try
            {
                Context.Configuration.AutoDetectChangesEnabled = false;
                foreach (TAggregateRoot entity in entities)
                {
                    RegisterNew<TAggregateRoot, TKey>(entity);
                }
            }
            finally
            {
                Context.Configuration.AutoDetectChangesEnabled = true;
            }
        }

        /// <summary>
        ///     注册一个更改的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TAggregateRoot"> 要注册的类型 </typeparam>
        /// <typeparam name="TKey">实体主键类型</typeparam>
        /// <param name="entity"> 要注册的对象 </param>
        public void RegisterModified<TAggregateRoot, TKey>(TAggregateRoot entity) where TAggregateRoot : AggregateRoot<TKey>
        {
            Context.Update<TAggregateRoot, TKey>(entity);
            IsCommitted = false;
        }

        /// <summary>
        /// 使用指定的属性表达式指定注册更改的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TAggregateRoot">要注册的类型</typeparam>
        /// <typeparam name="TKey">主键类型</typeparam>
        /// <param name="propertyExpression">属性表达式，包含要更新的实体属性</param>
        /// <param name="entity">附带新值的实体信息，必须包含主键</param>
        public void RegisterModified<TAggregateRoot, TKey>(Expression<Func<TAggregateRoot, object>> propertyExpression, TAggregateRoot entity) where TAggregateRoot : AggregateRoot<TKey>
        {
            Context.Update<TAggregateRoot, TKey>(propertyExpression, entity);
            IsCommitted = false;
        }

        /// <summary>
        ///   注册一个删除的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TAggregateRoot"> 要注册的类型 </typeparam>
        /// <typeparam name="TKey">实体主键类型</typeparam>
        /// <param name="entity"> 要注册的对象 </param>
        public void RegisterDeleted<TAggregateRoot, TKey>(TAggregateRoot entity) where TAggregateRoot : AggregateRoot<TKey>
        {
            Context.Entry(entity).State = EntityState.Deleted;
            IsCommitted = false;
        }

        /// <summary>
        ///   批量注册多个删除的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TAggregateRoot"> 要注册的类型 </typeparam>
        /// <typeparam name="TKey">实体主键类型</typeparam>
        /// <param name="entities"> 要注册的对象集合 </param>
        public void RegisterDeleted<TAggregateRoot, TKey>(IEnumerable<TAggregateRoot> entities) where TAggregateRoot : AggregateRoot<TKey>
        {
            try
            {
                Context.Configuration.AutoDetectChangesEnabled = false;
                foreach (TAggregateRoot entity in entities)
                {
                    RegisterDeleted<TAggregateRoot, TKey>(entity);
                }
            }
            finally
            {
                Context.Configuration.AutoDetectChangesEnabled = true;
            }
        }
    }
}
