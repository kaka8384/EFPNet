using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using EFPNet.Infrastructure.Tools;

namespace EFPNet.Infrastructure.Data
{
    public interface IUnitOfWorkContext : IUnitOfWork, IDisposable
    {
        /// <summary>
        ///   注册一个新的聚合根到仓储上下文中
        /// </summary>
        /// <typeparam name="TAggregateRoot"> 要注册的类型 </typeparam>
        /// <typeparam name="TKey">实体主键类型</typeparam>
        /// <param name="entity"> 要注册的聚合根 </param>
        void RegisterNew<TAggregateRoot, TKey>(TAggregateRoot entity) where TAggregateRoot : AggregateRoot<TKey>;

        /// <summary>
        ///   批量注册多个新的聚合根到仓储上下文中
        /// </summary>
        /// <typeparam name="TAggregateRoot"> 要注册的类型 </typeparam>
        /// <typeparam name="TKey">实体主键类型</typeparam>
        /// <param name="entities"> 要注册的聚合根集合 </param>
        void RegisterNew<TAggregateRoot, TKey>(IEnumerable<TAggregateRoot> entities) where TAggregateRoot : AggregateRoot<TKey>;

        /// <summary>
        ///   注册一个更改的聚合根到仓储上下文中
        /// </summary>
        /// <typeparam name="TAggregateRoot"> 要注册的类型 </typeparam>
        /// <typeparam name="TKey">实体主键类型</typeparam>
        /// <param name="entity"> 要注册的聚合根 </param>
        void RegisterModified<TAggregateRoot, TKey>(TAggregateRoot entity) where TAggregateRoot : AggregateRoot<TKey>;

        /// <summary>
        /// 使用指定的属性表达式指定注册更改的聚合根到仓储上下文中
        /// </summary>
        /// <typeparam name="TAggregateRoot">要注册的类型</typeparam>
        /// <typeparam name="TKey">主键类型</typeparam>
        /// <param name="propertyExpression">属性表达式，包含要更新的实体属性</param>
        /// <param name="entity">附带新值的实体信息，必须包含主键</param>
        void RegisterModified<TAggregateRoot, TKey>(Expression<Func<TAggregateRoot, object>> propertyExpression, TAggregateRoot entity) where TAggregateRoot : AggregateRoot<TKey>;

        /// <summary>
        ///   注册一个删除的聚合根到仓储上下文中
        /// </summary>
        /// <typeparam name="TAggregateRoot"> 要注册的类型 </typeparam>
        /// <typeparam name="TKey">实体主键类型</typeparam>
        /// <param name="entity"> 要注册的聚合根 </param>
        void RegisterDeleted<TAggregateRoot, TKey>(TAggregateRoot entity) where TAggregateRoot : AggregateRoot<TKey>;

        /// <summary>
        ///   批量注册多个删除的聚合根到仓储上下文中
        /// </summary>
        /// <typeparam name="TAggregateRoot"> 要注册的类型 </typeparam>
        /// <typeparam name="TKey">实体主键类型</typeparam>
        /// <param name="entities"> 要注册的聚合根集合 </param>
        void RegisterDeleted<TAggregateRoot, TKey>(IEnumerable<TAggregateRoot> entities) where TAggregateRoot : AggregateRoot<TKey>;
    }
}
