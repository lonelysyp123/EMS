using EMS.Storage.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Storage.DB.DBManage
{
    public interface IManage<TEntity> where TEntity : class
    {
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity">数据实体</param>
        /// <returns>是否添加成功</returns>
        bool Insert(TEntity entity);

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity">数据实体</param>
        /// <returns>是否更新成功</returns>
        bool Update(TEntity entity);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="entity">数据实体</param>
        /// <returns>是否删除成功</returns>
        bool Delete(TEntity entity);

        /// <summary>
        /// 删除所有数据
        /// </summary>
        /// <returns>是否删除成功</returns>
        bool DeleteAll();

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns>数据列表</returns>
        List<TEntity> Get();
    }
}
